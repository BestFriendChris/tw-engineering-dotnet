using System;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
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
            driver = new FirefoxDriver();
            ScenarioContext.Current.SetWebDriver(driver);
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