using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace VideoWorld.Features.StepDefinitions
{
    [Binding]
    public class StepDefinitions
    {
        [When(@"I go to the home page")]
        public void WhenIGoToTheHomePage()
        {
//            ScenarioContext.Current.Pending();
        }

        [Then(@"the list includes the movie ""(.*)""")]
        public void ThenTheListIncludesTheMovieAvatar(string movieName)
        {
            Console.WriteLine(movieName);
            //            ScenarioContext.Current.Pending();
        }

    }

}
