using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.Model;
using Chat.Service;
using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Chat.ViewModel
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CadastrarChatViewModel : INotifyPropertyChanged
	{
        public string mensagem {
            get { return _mensagem; }
            set { _mensagem = value; OnPropertyChanged("mensagem"); } }
        private string _mensagem { get; set; }
        public string nome { get; set; }
        public CadastrarChatViewModel ()
		{
			InitializeComponent ();
            CadastrarCommand = new Command(Cadastrar);
		}
        private void Cadastrar()
        {
            var chat = new Chat.Model.Chat() { nome = nome };
            bool ok = ServiceWS.InsertChat(chat);
            if (ok == true)
            {
                ((NavigationPage)App.Current.MainPage).Navigation.PopAsync();

                var Nav = (NavigationPage)App.Current.MainPage;
                var Chats = (View.Chats)Nav.RootPage;
                var ViewModel = (ChatsViewModel)Chats.BindingContext;
                if (ViewModel.AtualizarCommand.CanExecute(null))
                {
                    ViewModel.AtualizarCommand.Execute(null);
                }
            }
            else
            {
                mensagem = "Ocorreu um erro no Cadastro";
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
            }
        }
        public Command CadastrarCommand { get; set; }
	}
}