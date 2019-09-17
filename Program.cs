using TestContactForm;

namespace GmailQuickstart
{
    class Program
    {
        static void Main()
        {
            var contactFormResponse = Selenium.SubmitForm();

            if (contactFormResponse.Contains("success")) return;

            Email.SendFailureEmail();
        }
    }
}