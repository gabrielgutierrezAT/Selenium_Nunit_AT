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
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class Tests
    {
        

        [SetUp]
        public void Setup()
        {
        }

        
        [Test]
        [Category("Model Airplanes")]
          public void Model_Airplanes()
          {
            IWebDriver driver;
            //create Chrome Driver
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://tailspintoys.azurewebsites.net/");

            //Select Item
            driver.FindElement(By.CssSelector("a[href='/?slug=model'")).Click();
            driver.FindElement(By.CssSelector("a[href='/home/show?sku=modfcf'")).Click();

            //Fill Form
            driver.FindElement(By.ClassName("add-cart")).Click();
            driver.FindElement(By.XPath("//input[@value='Checkout']")).Click();
            driver.FindElement(By.Id("FirstName")).SendKeys("Gabriel");
            driver.FindElement(By.Id("LastName")).SendKeys("Gutierrez");
            driver.FindElement(By.Id("Email")).SendKeys("gabriel@emial.com");
            driver.FindElement(By.Id("Street1")).SendKeys("Street 123");
            driver.FindElement(By.Id("Street2")).SendKeys("Street 423");
            driver.FindElement(By.Id("City")).SendKeys("Ocotlan");
            SelectElement country_ddl = new SelectElement(driver.FindElement(By.Id("countrySelect")));
            country_ddl.SelectByValue("US");
            SelectElement state_ddl = new SelectElement(driver.FindElement(By.Id("stateSelect")));
            state_ddl.SelectByValue("WA");
            driver.FindElement(By.Id("Zip")).SendKeys("47829");

            //Review & PLace Order
            driver.FindElement(By.XPath("//input[@value='Review Order']")).Click();
            driver.FindElement(By.XPath("//input[@value='Place Order']")).Click();

            //Check Final Order Receipt
            string receipt = driver.Url;
            receipt = receipt.Replace("http://tailspintoys.azurewebsites.net/Order/Receipt/", ""); 
            Assert.That(receipt, Is.Not.Empty);

            driver.Quit();
        }

        [Test]
        [Category("Paper Airplanes")]
        public void Paper_Airplanes()
        {
            IWebDriver driver;
            //create Chrome Driver
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://tailspintoys.azurewebsites.net/");

            //Select Item
            driver.FindElement(By.CssSelector("a[href='/?slug=paper'")).Click();
            driver.FindElement(By.CssSelector("a[href='/home/show?sku=papcce'")).Click();

            //Fill Form
            driver.FindElement(By.ClassName("add-cart")).Click();
            driver.FindElement(By.XPath("//input[@value='Checkout']")).Click();
            driver.FindElement(By.Id("FirstName")).SendKeys("Gabriel");
            driver.FindElement(By.Id("LastName")).SendKeys("Gutierrez");
            driver.FindElement(By.Id("Email")).SendKeys("gabriel@emial.com");
            driver.FindElement(By.Id("Street1")).SendKeys("Street 123");
            driver.FindElement(By.Id("Street2")).SendKeys("Street 423");
            driver.FindElement(By.Id("City")).SendKeys("Ocotlan");
            SelectElement country_ddl = new SelectElement(driver.FindElement(By.Id("countrySelect")));
            country_ddl.SelectByValue("US");
            SelectElement state_ddl = new SelectElement(driver.FindElement(By.Id("stateSelect")));
            state_ddl.SelectByValue("WA");
            driver.FindElement(By.Id("Zip")).SendKeys("47829");

            //Review & PLace Order
            driver.FindElement(By.XPath("//input[@value='Review Order']")).Click();
            driver.FindElement(By.XPath("//input[@value='Place Order']")).Click();

            //Check Final Order Receipt
            string receipt = driver.Url;
            receipt = receipt.Replace("http://tailspintoys.azurewebsites.net/Order/Receipt/", ""); 
            Assert.That(receipt, Is.Not.Empty);

            driver.Quit();
        }




        [TearDown]
        public void closeBrowser()
        {
            
        }


    }
}