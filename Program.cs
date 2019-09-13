using OpenQA.Selenium.Chrome;
using System;

namespace contactFormTests
{
    class Program
    {
        static void Main(string[] args)
        {
            var foo = GetToken();
            Console.WriteLine(foo);
        }

        private static string GetToken()
        {
            var chromeOptions = new ChromeOptions();
            //chromeOptions.AddArgument("headless");

            var directory = Environment.CurrentDirectory;
            var driver = new ChromeDriver(directory, chromeOptions);

            driver.Navigate().GoToUrl("https://ocius.com.au/usv#technical");
            var textInput = driver.FindElementByName("fullName").GetAttribute("placeholder");


            driver.Quit();
            return textInput;

        }
    }
}
