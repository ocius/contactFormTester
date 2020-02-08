using System;

namespace TestContactForm
{
    class Program
    {
        static void Main()
        {
            string response;

            try
            {
                response = Selenium.SubmitForm();
            }
            catch(Exception e)
            {
                response = e.Message;
            }

            if (!response.Contains("success")) Email.SendFailureEmail(response);
        }
    }
}