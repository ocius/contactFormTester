using System;

namespace TestContactForm
{
    class Program
    {
        static void Main()
        {
            try
            {
                var isSuccess = Selenium.SubmitForm();
                if (!isSuccess) Email.SendFailureEmail("Form submission failed");
            }
            catch(Exception e)
            {
                Email.SendFailureEmail(e.Message);
            }
        }
    }
}