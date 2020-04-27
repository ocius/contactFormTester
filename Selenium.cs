using OpenQA.Selenium.Chrome;
using System;

namespace TestContactForm
{
    public class Selenium
    {
        public static bool SubmitForm()
        {
            using (var driver = GetDriver())
            {
                driver.Navigate().GoToUrl("https://ocius.com.au/usv#technical");
                return CanSubmitForm(driver);
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

        private static bool CanSubmitForm(ChromeDriver driver)
        {
            var completedForm = FillOutForm(driver);
            return IsSuccessTextDisplayed(completedForm);
        }

        private static ChromeDriver FillOutForm(ChromeDriver driver)
        {
            WaitForPageLoad(driver);
            driver.FindElementByName("fullName").SendKeys("Test - Tom Dane");
            driver.FindElementByName("email").SendKeys("ocius@tomdane.com");
            driver.FindElementByName("interest").SendKeys("Testing");
            driver.FindElementByClassName("Button").Click();
            return driver;
        }

        private static void WaitForPageLoad(ChromeDriver driver)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        private static bool IsSuccessTextDisplayed(ChromeDriver driver)
        {
            WaitForPageLoad(driver);

            var body = driver.FindElementByTagName("span").Text;

            return body.Contains("Your message was sent successfully");
        }
    }
}
