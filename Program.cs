using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.IO;
using System.Threading;
using System.Net.Mail;

namespace GmailQuickstart
{
    class Program
    {
        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/gmail-dotnet-quickstart.json
        static string[] Scopes = { GmailService.Scope.GmailSend };
        static string ApplicationName = "Gmail API .NET Quickstart";

        static void Main()
        {
            SendFailureEmail();
        }

        private static void SendFailureEmail()
        {
            UserCredential credential;

            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Gmail API service.
            var service = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            //Create Message
            MailMessage mail = new MailMessage
            {
                Subject = "FAILURE: Could not submit contact form",
                Body = "The attempt to submit the form for Ocuis tech specs failed",
                From = new MailAddress("tjdane@gmail.com"),
                IsBodyHtml = true
            };
            mail.To.Add(new MailAddress("tom@ocius.com.au"));
            mail.To.Add(new MailAddress("tjdane@gmail.com"));

            MimeKit.MimeMessage mimeMessage = MimeKit.MimeMessage.CreateFromMailMessage(mail);

            Message message = new Message
            {
                Raw = Base64UrlEncode(mimeMessage.ToString())
            };

            service.Users.Messages.Send(message, "tjdane@gmail.com").Execute();
        }

        private static string Base64UrlEncode(string input)
        {
            var inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(inputBytes)
              .Replace('+', '-')
              .Replace('/', '_')
              .Replace("=", "");
        }
    }
}