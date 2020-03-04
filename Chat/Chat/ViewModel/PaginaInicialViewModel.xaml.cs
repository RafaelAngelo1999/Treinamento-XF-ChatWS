using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.ComponentModel;
using Chat.Model;
using Chat.Service;
using Newtonsoft.Json;
using Chat.Util;

using Xamarin.Forms.Xaml;

namespace Chat.ViewModel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaInicialViewModel : INotifyPropertyChanged
    {
        private string _mensagem;
        public string _nome;
        public string _senha;
        public string Nome
        {
            get { return _nome; }
            set { _nome = value; PropertyChanged(this, new PropertyChangedEventArgs("Nome")); }
        }
        public string Senha
        {
            get { return _senha; }
            set { _senha = value; PropertyChanged(this, new PropertyChangedEventArgs("Senha")); }
        }
        public string Mensagem
        {
            get { return _mensagem; }
            set { _mensagem = value; PropertyChanged(this, new PropertyChangedEventArgs("Mensagem")); }
        }
        public Command AcessarCommand { get; set; }

        public PaginaInicialViewModel()
        {
            AcessarCommand = new Command(Acessar);
        }
        private void Acessar()
        {
            var user = new Usuario();
            user.nome = Nome;
            user.password = Senha;

            var usuarioLogado = ServiceWS.GetUsuario(user);
            if (usuarioLogado == null)
            {
                Mensagem = "Falha na autenticação";
            }
            else
            {
                UsuarioUtil.SetUsuarioLogado(usuarioLogado);
                //App.Current.Properties["LOGIN"] = JsonConvert.SerializeObject(usuarioLogado);
                App.Current.MainPage = new NavigationPage(new View.Chats()) { BackgroundColor = Color.White, BarTextColor = Color.White,BarBackgroundColor=Color.FromHex("#B22222") };
            }
            
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}