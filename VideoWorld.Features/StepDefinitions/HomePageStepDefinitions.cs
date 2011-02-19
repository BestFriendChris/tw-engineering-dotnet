using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using TechTalk.SpecFlow;
using VideoWorld.Features.Support;

namespace VideoWorld.Features.StepDefinitions
{
    [Binding]
    public class HomePageStepDefinitions : SeleniumStepsBase
    {
        [When(@"I go to the home page")]
        public void WhenIGoToTheHomePage()
        {            
            WebDriver.Navigate().GoToUrl("http://localhost:49785/");
        }

        [Then(@"the list includes the movie ""(.*)""")]
        public void ThenTheListIncludesTheMovieAvatar(string movieName)
        {
            var element = WebDriver.FindElement(By.ClassName("movie_title"), e => e.Text.Contains(movieName));
            Assert.IsNotNull(element);
        }

    }


}

