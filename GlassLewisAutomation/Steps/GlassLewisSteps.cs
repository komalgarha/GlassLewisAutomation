using GlassLewisAutomation.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace GlassLewisAutomation.Steps
{
    [Binding]
     public class GlassLewisSteps
   {
       
        WebDriverWait wait = null;     
        IWebDriver webDriver = null;
        Homepage homepage = null;

       
        [Given(@"I am using ""(.*)""")]
        public void GivenIAmUsing(string browser)
        {
            
            if (browser.Equals("chrome"))
            {
                var chromeOptions = new ChromeOptions();
                chromeOptions.AddArgument("start-maximized");
                chromeOptions.AddArgument("--disable-extensions");
                chromeOptions.AddArguments("--headless");
                webDriver = new ChromeDriver(chromeOptions);
            }

            else if(browser.Equals("firefox"))
            {
                webDriver = new FirefoxDriver();
            }
            else if(browser.Equals("edge"))
            {
                EdgeOptions edgeOptions = new EdgeOptions();
                edgeOptions.UseChromium = true;
                webDriver = new EdgeDriver(edgeOptions);
            }
            else
            {
                throw new Exception("Browser not Identified");
            }
            webDriver.Navigate().GoToUrl("https://viewpoint.glasslewis.com/WD/?siteId=DemoClient");
            webDriver.Manage().Window.Maximize();
            Thread.Sleep(5000);
            homepage = new Homepage(webDriver, wait);
        }

        
        [When(@"the Belgium country is selected under country filter")]
        public void WhenTheBelgiumCountryIsSelectedUnderCountryFilter()
        {
            homepage.waitForBelgiumLabelAndClick(30);
        }
       
        [When(@"click on update button")]
        public void WhenClickOnUpdateButton()
        {
           
                homepage.Update();
                Thread.Sleep(5000);        
        }
    
        [When(@"User writes following Company Name in search field")]
        public void WhenUserWritesFollowingCompanyNameInSearchField(Table table)
        {
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            dynamic data = table.CreateDynamicInstance();
            homepage.SearchElement((string)data.CompanyName);
        }

        [Then(@"grid displays all meetings for the following country")]
        public void ThenGridDisplaysAllMeetingsForTheFollowingCountry(Table table)
        {
            dynamic data = table.CreateDynamicInstance();
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            homepage.Traverse((string)data.Country, 30);
        }

        [Then(@"No other country's meeting is listed on the page")]
        public void ThenNoOtherCountrySMeetingIsListedOnThePage()
        {
            homepage.TakeScreenshot("Country");
            webDriver.Quit();
        }

        [Then(@"the user lands onto the “Activision Blizzard Inc” vote card page\.")]
        public void ThenTheUserLandsOntoTheActivisionBlizzardIncVoteCardPage_()
        {
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            homepage.HeaderElement();             
        }
        [Then(@"Company Name should appear in the top banner")]
        public void ThenCompanyNameShouldAppearInTheTopBanner()
        {
            Thread.Sleep(3000);
            string actualCompanyName = homepage.HeaderElement();
            string expectedCompanyName = "Activision Blizzard Inc";
            Assert.IsTrue(actualCompanyName.Contains(expectedCompanyName));
            homepage.TakeScreenshot("Company");
            webDriver.Quit();
        }
    }
}