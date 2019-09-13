using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

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
            driver.FindElementByName("fullName").SendKeys("Test - Tom Dane");
            driver.FindElementByName("email").SendKeys("ocius@tomdane.com");
            driver.FindElementByName("interest").SendKeys("Testing");
            driver.FindElementByXPath("//*[@id='___gatsby']/div/div/div/div[3]/form/button").Click();

            var messageXPath = "//*[@id='___gatsby']/div/div/div/div[3]/form/span";

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            var element = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(messageXPath)));

            var message = element.Text;
            driver.Quit();

            return message;

        }
    }
}
