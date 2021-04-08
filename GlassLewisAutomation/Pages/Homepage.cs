using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace GlassLewisAutomation.Pages
{
    public class Homepage
    {
        public Homepage(IWebDriver driver, WebDriverWait wait)
        {
            WebDriver = driver;
            Wait = wait;
        }
        public IWebDriver WebDriver { get; }
        public WebDriverWait Wait { get; }

        public void waitForBelgiumLabelAndClick(int seconds)
        {        
            WebDriverWait wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(seconds));
            IWebElement selectCountry = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//label[@id='Belgium-cb-label-CountryFilter']")));
            selectCountry.Click();
        }

        public void Update() =>  WebDriver.FindElement(By.Id("btn-update")).Click();

        public void TakeScreenshot(string fileName)
        {
            ITakesScreenshot screenshotDriver = WebDriver as ITakesScreenshot;
            Screenshot screenshot = screenshotDriver.GetScreenshot();
            screenshot.SaveAsFile(fileName+"_test.png", ScreenshotImageFormat.Png);
        }

        public IWebElement searchElement => WebDriver.FindElement(By.XPath("//input[@id='kendo-Search-for-company']"));
        public void SearchElement(string CompanyName)
        {
           searchElement.SendKeys(CompanyName);
           Thread.Sleep(3000);
           searchElement.SendKeys(Keys.ArrowDown);
        } 
   
        public string HeaderElement() => WebDriver.FindElement(By.XPath("//h2[@id='detail-issuer-name']")).Text;

        public IWebElement ElemTable => WebDriver.FindElement(By.XPath("/html/body/div[2]/div[2]/article/div[2]/div[2]/table"));
      
        public  List<IWebElement> lstTrElem => new List<IWebElement>(ElemTable.FindElements(By.TagName("tr")));

        public void Traverse(string Country, int seconds)
        {
            WebDriverWait wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(seconds));
            IWebElement selectCountry = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='grid']/div[2]/table/tbody/tr[1]/td[4]")));

            for (int i = 1; i < lstTrElem.Count; i++)
            {
                IWebElement countryElem = WebDriver.FindElement(By.XPath("//*[@id='grid']/div[2]/table/tbody/tr[" + i + "]/td[4]"));
                Assert.IsTrue(Country == countryElem.Text);
             
            }
        }

   
    }
}
