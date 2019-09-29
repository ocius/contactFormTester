using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.IO;
using System.Net.Mail;
using System.Threading;

namespace TestContactForm
{
    public class Email
    {
        static readonly string[] Scopes = { GmailService.Scope.GmailSend };
        static readonly string ApplicationName = "Ocius Contact Form Tester";

        public static void SendFailureEmail(string contactFormResponse)
        {
            UserCredential credential;

            using (var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleWebAuthorizationBroker
                    .AuthorizeAsync(GoogleClientSecrets
                    .Load(stream).Secrets, Scopes, "user", CancellationToken.None, new FileDataStore("token.json", true))
                    .Result;
            }

            var message = CreateMessage(contactFormResponse);

            SendMessage(credential, message);
        }

        private static Message CreateMessage(string contactFormResponse)
        {
            MailMessage mail = new MailMessage
            {
                Subject = "FAILURE: Could not submit contact form",
                Body = contactFormResponse,
                From = new MailAddress("tjdane@gmail.com"),
                IsBodyHtml = true
            };
            mail.To.Add(new MailAddress("tjdane@gmail.com"));

            MimeKit.MimeMessage mimeMessage = MimeKit.MimeMessage.CreateFromMailMessage(mail);

            return new Message { Raw = Base64UrlEncode(mimeMessage.ToString()) };
        }

        private static string Base64UrlEncode(string input)
        {
            var inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(inputBytes)
              .Replace('+', '-')
              .Replace('/', '_')
              .Replace("=", "");
        }

        private static void SendMessage(UserCredential credential, Message message)
        {
            var service = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            using (service)
            {
                service.Users.Messages.Send(message, "tjdane@gmail.com").Execute();
            }
        }
    }
}
