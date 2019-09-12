using OpenQA.Selenium.Chrome;
using System;

namespace contactFormTests
{
    class Program
    {
        static void Main(string[] args)
        {
            GetToken();
            Console.WriteLine("Hello World!");
        }

        private static void GetToken()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("headless");

            var directory = Environment.CurrentDirectory;
            var driver = new ChromeDriver(directory, chromeOptions);

            driver.Navigate().GoToUrl("https://ocius.com.au");
            driver.Quit();
        }
    }
}
