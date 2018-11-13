using CommonLib;
using CommonLib.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Notifier
{
    public class Notifier
    {
        private WebApiClient webApiClient = null;

        public Notifier(WebApiClient webApiClient)
        {
            this.webApiClient = webApiClient;
        }

        public void Start()
        {
            while (true)
            {
                var t = this.webApiClient.GetData("http://bk.xplatform.net/api/message");

                if (!string.IsNullOrEmpty(t.Result))
                {
                    string json = t.Result;

                    IEnumerable<Message> messages = JsonConvert.DeserializeObject<IEnumerable<Message>>(json);

                    foreach (var msg in messages)
                    {
                        SendMessage(msg.messageBody);

                        SendVK(msg.messageBody);

                    }

                    foreach (var msg in messages)
                    {
                        this.webApiClient.DeleteAsync(string.Format("http://bk.xplatform.net/api/message/{0}", msg.id));
                    }
                }

                Task.Delay(5 * 1000).Wait();
            }
        }

        private void SendMessage(string body)
        {
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.yandex.ru";
            client.EnableSsl = true;
            client.Port = 587;
            //client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("livebets@xplatform.net", "DTgy8e13B");

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("livebets@xplatform.net");
            mailMessage.To.Add(new MailAddress("info@xplatform.net"));
            mailMessage.To.Add(new MailAddress("admin@worma.ru"));
            mailMessage.Body = body;
            mailMessage.Subject = "live bets online service";
            client.Send(mailMessage);
        }

        private void SendVK(string body)
        {
            string url = string.Format("https://api.vk.com/method/messages.send?message={0}&user_id=156208296&access_token=eddba78f1893ca8be780e1bff2dcc01f2328cb359a5a62c83a4ccc3fb335c3092fbff97b1607a2b947da8&v=5.37", body);

            this.webApiClient.GetData(url).Wait();

        }
    }
}
