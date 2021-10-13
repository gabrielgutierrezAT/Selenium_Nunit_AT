using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.ObjectModel;
using System.Threading;

namespace Selenium_Nunit_AT
{
    public class Tests
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
        }

        
        [Test]
        [Category("Search Test")]
          public void Search_text()
          {              
              driver.Navigate().GoToUrl("http://www.google.com");
              string search_text = System.IO.File.ReadAllText("../../../TextFile1.txt");
              driver.FindElement(By.Name("q")).SendKeys(search_text + Keys.Enter);
              Assert.That(driver.Title, Is.EqualTo(search_text + " - Buscar con Google"));
        }
   
        [Test]
        [Category("Media Test")]
        [Category("Regression Test")]
        public void Linkedin()
        {
            driver.Navigate().GoToUrl("http://www.agilethought.com");
            IWebElement linkedin_btn = driver.FindElement(By.CssSelector("a[href='http://www.linkedin.com/company/agilethought']"));
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            jse.ExecuteScript("arguments[0].click()", linkedin_btn);
            string title = driver.SwitchTo().Window(driver.WindowHandles[1]).Title;
            Assert.That(title, Does.Contain("linkedin").IgnoreCase);
        }

        [Test]
        [Category("Media Test")]
        [Category("Regression Test")]
        public void Facebook()
        {
            driver.Navigate().GoToUrl("http://www.agilethought.com");
            IWebElement facebook_btn = driver.FindElement(By.CssSelector("a[href='https://www.facebook.com/AgileThought']"));
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            jse.ExecuteScript("arguments[0].click()", facebook_btn);
            string title = driver.SwitchTo().Window(driver.WindowHandles[1]).Title;
            Assert.That(title, Does.Contain("facebook").IgnoreCase);
        }

        [Test]
        [Category("Media Test")]
        [Category("Regression Test")]
        public void Twitter()
        {
            driver.Navigate().GoToUrl("http://www.agilethought.com");
            IWebElement twitter_btn = driver.FindElement(By.CssSelector("a[href='https://twitter.com/AgileThought']"));
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            jse.ExecuteScript("arguments[0].click()", twitter_btn);
            string title = driver.SwitchTo().Window(driver.WindowHandles[1]).Title;
            Assert.That(title, Does.Contain("twitter").IgnoreCase);
        }

        [Test]
        [Category("Media Test")]
        [Category("Regression Test")]
        public void Instagram()
        {
            driver.Navigate().GoToUrl("http://www.agilethought.com");
            IWebElement instagram_btn = driver.FindElement(By.CssSelector("a[href='https://www.instagram.com/agilethought/']"));
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            jse.ExecuteScript("arguments[0].click()", instagram_btn);
            string title = driver.SwitchTo().Window(driver.WindowHandles[1]).Title;
            Assert.That(title, Does.Contain("instagram").IgnoreCase);
        }
        

        [Test]
        [Category("Search Test")]
        [Category("Regression Test")]
        public void Amazon()
        {
            driver.Navigate().GoToUrl("http://www.amazon.com");
            string search_text = System.IO.File.ReadAllText("../../../Amazon.txt");
            driver.FindElement(By.Id("twotabsearchtextbox")).SendKeys(search_text + Keys.Enter);
            Assert.That(driver.Title, Is.EqualTo("Amazon.com : " + search_text));
            
        }

        [TearDown]
        public void closeBrowser()
        {
            driver.Quit();
        }


    }
}