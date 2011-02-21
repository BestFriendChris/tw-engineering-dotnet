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
        [When(@"I view the list of available movies")]
        public void WhenIGoToTheHomePage()
        {            
            WebDriver.Navigate().GoToUrl("http://localhost:49785/");
        }

        [Then(@"the list includes the movie ""(.*)""")]
        public void ThenTheListIncludesTheMovieAvatar(string movieName)
        {
            var element = WebDriver.FindElement(By.ClassName("movie"), e => e.Text.Contains(movieName));
            Assert.IsNotNull(element);
        }

        [When(@"I add the movie ""(.*)"" to my cart")]
        public void WhenIAddTheMovieAvatarToMyCart(string movieName)
        {
            var element = WebDriver.FindElement(By.ClassName("movie"), e => e.Text.Contains(movieName));
            Assert.IsNotNull(element);
            var addButton = element.FindElement(By.ClassName("addToCart"));
            Assert.IsNotNull(addButton);

            addButton.Click();
        }

        [Then(@"I see ""(.*) item in your cart""")]
        public void ThenISee1ItemInYourCart(int numberOfMovies)
        {
            var cartElement = WebDriver.FindElement(By.ClassName("cart"));

            Assert.IsNotNull(cartElement);

            Assert.That(cartElement.Text.Contains(string.Format("{0} item(s) in your cart", numberOfMovies)), 
                "Message was {0}", cartElement.Text);
        }

    }


}

