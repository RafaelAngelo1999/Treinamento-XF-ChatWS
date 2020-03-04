using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Http;
using Chat.Model;
using Newtonsoft.Json;

namespace Chat.Service
{
    public class ServiceWS
    {
        public static string EnderecoBase = "http://ws.spacedu.com.br/xf2018/rest/api";
        public static Usuario GetUsuario(Usuario usuario)
        {
            var URL = EnderecoBase + "/usuario";

            //query string
            //StringContent param = new StringContent(string.Format("?nome={0}&password{1}", usuario.nome, usuario.password));
            FormUrlEncodedContent param = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string,string>("nome",usuario.nome),
                new KeyValuePair<string,string>("password",usuario.password),
            });

            HttpClient requisicao = new HttpClient();
            HttpResponseMessage resposta = requisicao.PostAsync(URL, param).GetAwaiter().GetResult();

            if (resposta.StatusCode == HttpStatusCode.OK)
            {
                var conteudo = resposta.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                //TODO - Deserializar, retornar no metodo e Salvar como Login.
                return JsonConvert.DeserializeObject<Usuario>(conteudo);
            }
            else
            {
                return null;
            }
        }
        public static List<Chat.Model.Chat> GetChats()
        {
            var URL = EnderecoBase + "/chats";
            HttpClient requisicao = new HttpClient();
            HttpResponseMessage resposta = requisicao.GetAsync(URL).GetAwaiter().GetResult();

            if (resposta.StatusCode == HttpStatusCode.OK)
            {
                string conteudo = resposta.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                if (conteudo.Length > 2)
                {
                    List<Chat.Model.Chat> Lista = JsonConvert.DeserializeObject<List<Chat.Model.Chat>>(conteudo);
                    return Lista;
                }
                else
                {
                    return null;
                }

            }
            else
            {
                return null;
            }
        }
        public static bool InsertChat(Chat.Model.Chat chat)
        {
            var URL = EnderecoBase + "/chat";

            FormUrlEncodedContent param = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string,string>("nome",chat.nome),
            });

            HttpClient requisicao = new HttpClient();
            HttpResponseMessage resposta = requisicao.PostAsync(URL, param).GetAwaiter().GetResult();

            if (resposta.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            return false;
        }
        public static bool RenomearChat(Chat.Model.Chat chat)
        {
            var URL = EnderecoBase + "/chat/" + chat.id;

            FormUrlEncodedContent param = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string,string>("nome",chat.nome),
            });

            HttpClient requisicao = new HttpClient();
            HttpResponseMessage resposta = requisicao.PutAsync(URL, param).GetAwaiter().GetResult();

            if (resposta.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            return false;
        }
        public static bool DeleteChats(Chat.Model.Chat chat)
        {
            var URL = EnderecoBase + "/chats/delete/" + chat.id;
            HttpClient requisicao = new HttpClient();
            HttpResponseMessage resposta = requisicao.DeleteAsync(URL).GetAwaiter().GetResult();

            if (resposta.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            return false;
        }
        public static List<Mensagem> GetMensagemChats(Chat.Model.Chat chat)
        {
            var URL = EnderecoBase + "/chat/" + chat.id + "/msg";
            HttpClient requisicao = new HttpClient();
            HttpResponseMessage resposta = requisicao.GetAsync(URL).GetAwaiter().GetResult();

            if (resposta.StatusCode == HttpStatusCode.OK)
            {
                string conteudo = resposta.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                if (conteudo.Length > 2)
                {
                    List<Mensagem> Lista = JsonConvert.DeserializeObject<List<Mensagem>>(conteudo);
                    return Lista;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        public static bool InsertMensagem(Mensagem mensagem)
        {
            var URL = EnderecoBase + "/chat/" + mensagem.id_chat + "/msg";

            FormUrlEncodedContent param = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string,string>("mensagem", mensagem.mensagem),
                new KeyValuePair<string,string>("id_usuario", mensagem.id_usuario.ToString())
            });

            HttpClient requisicao = new HttpClient();
            HttpResponseMessage resposta = requisicao.PostAsync(URL, param).GetAwaiter().GetResult();

            if (resposta.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            return false;
        }
        public static bool DeleteMensagem(Mensagem mensagem)
        {
            var URL = EnderecoBase + "/chat/" + mensagem.id_chat + "/delete/" + mensagem.id;
            HttpClient requisicao = new HttpClient();
            HttpResponseMessage resposta = requisicao.DeleteAsync(URL).GetAwaiter().GetResult();

            if (resposta.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            return false;
        }
    }
}
