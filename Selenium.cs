using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace TestContactForm
{
    public class Selenium
    {
        public static string SubmitForm()
        {
            using (var driver = GetDriver())
            {
                driver.Navigate().GoToUrl("https://ocius.com.au/usv#technical");
                return GetSubmitMessage(driver);
            }
        }

        private static ChromeDriver GetDriver()
        {
            var directory = AppDomain.CurrentDomain.BaseDirectory;
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("headless");
            var driver = new ChromeDriver(directory, chromeOptions);
            return driver;
        }

        private static string GetSubmitMessage(ChromeDriver driver)
        {
            try
            {
                var completedForm = FillOutForm(driver);
                return GetSuccessText(completedForm);
            }
            catch (Exception e)
            {
                return e.Message.ToString();
            }
        }

        private static ChromeDriver FillOutForm(ChromeDriver driver)
        {
            Thread.Sleep(2000); //Ensure page is loaded
            driver.FindElementByName("fullName").SendKeys("Test - Tom Dane");
            driver.FindElementByName("email").SendKeys("ocius@tomdane.com");
            driver.FindElementByName("interest").SendKeys("Testing");
            driver.FindElementByXPath("//*[@id='___gatsby']/div/div/div/div[3]/form/button").Click();
            return driver;
        }

        private static string GetSuccessText(ChromeDriver driver)
        {
            var messageXPath = "//*[@id='___gatsby']/div/div/div/div[3]/form/span";
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            var element = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(messageXPath)));
            var message = element.Text;
            return message;
        }
    }
}
