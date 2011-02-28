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
        private const string RootUrl = "http://localhost:49785";

        [When(@"I logout")]
        public void WhenILogout()
        {
            var element = WebDriver.FindElement(By.TagName("a"), e => e.Text == "Log off");
            Assert.IsNotNull(element);
            element.Click();
        }


        [Given(@"I am not logged in")]
        public void GivenIAmNotLoggedIn()
        {
            // ?
        }

        [Then(@"the system shows me the login page")]
        public void ThenTheSystemShowsMeTheLoginPage()
        {
            WebDriver.WaitForElement(By.TagName("h2"), e => e.Text == "Login");
        }

        [When(@"I navigate to the login page")]
        public void WhenINavigateToTheLoginPage()
        {
            WebDriver.Navigate().GoToUrl(RootUrl + "/login");
            ThenTheSystemShowsMeTheLoginPage();
        }

        [When(@"login as ""(.*)""")]
        public void WhenLoginAs(string username)
        {
            var textField = WebDriver.FindElement(By.TagName("input"), e => e.GetAttribute("name") == "username");
            Assert.IsNotNull(textField);
            textField.SendKeys(username + " " + new Random().NextDouble());

            var button = WebDriver.FindElement(By.TagName("input"), e => e.GetAttribute("name") == "login");
            Assert.IsNotNull(button);
            button.Click();

            WebDriver.WaitForElement(By.ClassName("movies"));
        }

        [Given(@"I am logged in as ""(.*)""")]
        public void GivenIAmLoggedInAs(string username)
        {
            WhenILogout();
            WhenINavigateToTheLoginPage();
            WhenLoginAs(username);
        }

        [Then(@"the system shows me the home page")]
        public void ThenTheSystemShowsMeTheHomePage()
        {
            var element = WebDriver.FindElement(By.TagName("h2"), e => e.Text == "Index");
            Assert.IsNotNull(element);
        }

        [When(@"I view the list of available movies")]
        public void WhenIGoToTheHomePage()
        {
            var element = WebDriver.FindElement(By.TagName("a"), e => e.Text == "View Movies");
            Assert.IsNotNull(element);
            element.Click();

            WebDriver.WaitForElement(By.ClassName("movies"));
        }

        [When(@"I try to navigate to the home page")]
        public void WhenITryToNavigateToTheHomePage()
        {
            WebDriver.Navigate().GoToUrl(RootUrl);
        }

        [When(@"I navigate to the home page")]
        public void WhenINavigateToTheHomePage()
        {
            WhenIGoToTheHomePage();
        }

        [Then(@"the list includes the movie ""(.*)""")]
        public void ThenTheListIncludesTheMovieAvatar(string movieName)
        {
            var element = WebDriver.FindElement(By.ClassName("movie"), e => e.Text.Contains(movieName));
            Assert.IsNotNull(element);
        }

        [When(@"I add the movie ""(.*)"" to my cart")]
        public void WhenIAddAMovieToMyCart(string movieName)
        {
            var element = WebDriver.FindElement(By.ClassName("movie"), e => e.Text.Contains(movieName));
            Assert.IsNotNull(element);
            var addButton = element.FindElement(By.ClassName("addToCart"));
            Assert.IsNotNull(addButton);

            addButton.Click();

            //Note: waiting for the page to reload after the movie is added
            WebDriver.WaitForElement(By.ClassName("cart"));
        }

        [Given(@"I have added the movie ""(.*)"" with a period of (.*) days")]
        public void GivenIHaveAddedTheMovieAvatarWithAPeriodOf2Days(string movieName, int numberOfDays)
        {
            var element = WebDriver.FindElement(By.ClassName("movie"), e => e.Text.Contains(movieName));
            Assert.IsNotNull(element);

            var periodElement = element.FindElements(By.TagName("select")).Where(e => e.GetAttribute("name").Equals("numberOfDays")).FirstOrDefault();
            Assert.IsNotNull(periodElement);

            var option = periodElement.FindElements(By.TagName("option")).Where(e => e.Value.Equals(numberOfDays.ToString())).FirstOrDefault();
            option.Select();

            var addButton = element.FindElement(By.ClassName("addToCart"));
            Assert.IsNotNull(addButton);

            addButton.Click();

            //Note: waiting for the page to reload after the movie is added
            WebDriver.WaitForElement(By.ClassName("cart"));
        }

        [Then(@"I see ""(.*) item in your cart""")]
        public void ThenISee1ItemInYourCart(int numberOfMovies)
        {
            var cartElement = WebDriver.FindElement(By.ClassName("cart"));

            Assert.IsNotNull(cartElement);

            Assert.That(cartElement.Text.Contains(string.Format("{0} item(s) in your cart", numberOfMovies)), 
                "Message was {0}", cartElement.Text);
        }

        [Given(@"I have added the movie ""(.*)""")]
        public void GivenIHaveAddedTheMovie(string movieName)
        {
            WhenIGoToTheHomePage();
            WhenIAddAMovieToMyCart(movieName);
        }

        [When(@"I view my Cart")]
        public void WhenIViewMyCart()
        {
            WhenINavigateToMyCart();
        }

        [When(@"I navigate to my Cart")]
        public void WhenINavigateToMyCart()
        {
            var element = WebDriver.FindElement(By.TagName("a"), e => e.Text == "View Cart");
            Assert.IsNotNull(element);
            element.Click();

            WebDriver.WaitForElement(By.ClassName("rentals"));
        }

        [Then(@"I should see the movie ""(.*)"" with a (\d+) day rental")]
        public void ThenIShouldSeeTheMovieAvatarWithA1DayRental(string movieName, int periodInDays)
        {
            var element = WebDriver.FindElement(By.ClassName("rental"), e => e.Text.Contains(movieName));
            var periodelement = element.FindElement(By.ClassName("period"));
            Assert.That(periodelement.Text, Is.EqualTo(periodInDays.ToString()));
        }

        [When(@"I check out")]
        public void WhenICheckOut()
        {
            WhenINavigateToMyCart();
            var element = WebDriver.FindElement(By.ClassName("checkout"));
            element.Click();

            WebDriver.WaitForElement(By.ClassName("statement"));
        }

        [Then(@"I should see my statement")]
        public void ThenIShouldSeeMyStatement()
        {
            var statementElement = WebDriver.FindElement(By.ClassName("statement"));
            Assert.That(statementElement.Text.Contains("Amount charged"));
        }

        [Given(@"I have rented the movie ""(.*)""")]
        public void GivenIHaveRentedTheMovieAvatar(string movieName)
        {
            WhenIGoToTheHomePage();
            WhenIAddAMovieToMyCart(movieName);
            WhenICheckOut();
        }

        [When(@"I navigate to my History")]
        public void WhenINavigateToMyHistory()
        {
            var element = WebDriver.FindElement(By.TagName("a"), e => e.Text == "View History");
            Assert.IsNotNull(element);
            element.Click();

            WebDriver.WaitForElement(By.ClassName("statements"));
        }

        [Then(@"I should see 1 history item")]
        public void ThenIShouldSee1HistoryItem()
        {
            var elements = WebDriver.FindElements(By.ClassName("statement"));
            Assert.That(elements.Count, Is.EqualTo(1), WebDriver.PageSource);
            
        }
    }


}

