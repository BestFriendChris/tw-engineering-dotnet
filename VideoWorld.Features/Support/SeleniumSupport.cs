using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using TechTalk.SpecFlow;

namespace VideoWorld.Features.Support
{
    [Binding]
    public static class SeleniumSupport
    {
        private static IWebDriver driver;

        [BeforeScenario("web")]
        public static void BeforeWebScenario()
        {
            driver = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), DesiredCapabilities.HtmlUnit());
            ScenarioContext.Current.SetWebDriver(driver);
//            driver = new FirefoxDriver();
            ScenarioContext.Current.SetWebDriver(driver);

            driver.Navigate().GoToUrl("http://localhost:49785");
        }

        [AfterScenario("web")]
        public static void AfterWebScenario()
        {
            driver.Quit();
            ScenarioContext.Current.SetWebDriver(null);
        }

        [AfterFeature]
        public static void AfterWebFeature()
        {
        }

    }
}