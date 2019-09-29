namespace TestContactForm
{
    class Program
    {
        static void Main()
        {
            var contactFormResponse = Selenium.SubmitForm();

            if (!contactFormResponse.Contains("success")) Email.SendFailureEmail(contactFormResponse);
        }
    }
}