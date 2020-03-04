using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.Model;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Chat.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Mensagem : ContentPage
	{

		public Mensagem (Chat.Model.Chat chat)
		{
			InitializeComponent ();
            BindingContext = new ViewModel.MensagemViewModel(chat, SLMensagemContainer);
		}
	}
}