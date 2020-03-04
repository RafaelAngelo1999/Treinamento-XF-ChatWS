using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Chat.Model;
using Chat.Service;
using Newtonsoft.Json;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Chat.ViewModel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatsViewModel : INotifyPropertyChanged
    {

        private Chat.Model.Chat _selectedItemChat;
        public Chat.Model.Chat SelectedItemChat
        {
            get { return _selectedItemChat; }
            set
            {
                _selectedItemChat = value; OnPropertyChanged("SelectedItemChat");
                GoPaginaMensagem(value);
            }
        }
        private void GoPaginaMensagem(Chat.Model.Chat chat)
        {
            if (chat != null)
            {
                _selectedItemChat = null;
                ((NavigationPage)App.Current.MainPage).Navigation.PushAsync(new View.Mensagem(chat));
                
            }

        }
        public Command AdicionarCommand { get; set; }
        public Command OrdernarCommand { get; set; }
        public Command AtualizarCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
            }
        }
        private List<Chat.Model.Chat> _chats;
        public List<Chat.Model.Chat> Chats
        {
            get { return _chats; }
            set { _chats = value; OnPropertyChanged("Chats"); }
        }
        public ChatsViewModel()
        {
            InitializeComponent();
            Chats = ServiceWS.GetChats();
            AdicionarCommand = new Command(Adicionar);
            OrdernarCommand = new Command(Ordernar);
            AtualizarCommand = new Command(Atualizar);
        }
        private void Adicionar()
        {
            ((NavigationPage)App.Current.MainPage).Navigation.PushAsync(new View.CadastrarChat());
        }
        private void Ordernar()
        {
            Chats = Chats.OrderBy(a => a.nome).ToList();
        }
        private void Atualizar()
        {
            Chats = ServiceWS.GetChats();
        }


    }
}