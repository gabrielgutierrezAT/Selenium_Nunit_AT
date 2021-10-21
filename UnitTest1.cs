using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;

namespace Selenium_Nunit_AT
{
    [TestFixture]
    public class Tests
    {
        IWebDriver driver;

       [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Cookies.DeleteAllCookies();
            driver.Navigate().GoToUrl("https://candidatex:qa-is-cool@qa-task.backbasecloud.com/");
            login(driver);
        }

        [Test]
        [Category("Smoke Test")]
        [Category("1 - CRUD Article")]
        [Description("1.1 - CRUD Articles - User is Able to Create an Article Filling all Fields")]
        public void Create_article_1_1()
        {
            String article = createNewArticle(driver,"title","description","body","tags");
            Assert.That(article.Contains("article"));
        }

        [Test]
        [Category("Smoke Test")]
        [Category("1 - CRUD Article")]
        [Description("1.2 - CRUD Articles - User is Able to Delete an Article after Creation (Banner Button)")]
        public void Delete_article_1_2()
        {
            String article = createNewArticle(driver, "title", "description", "body", "tags");
            bool deleted = deleteFromBanner(driver);
            Assert.That(deleted, Is.True);
        }

        [Test]
        [Category("Smoke Test")]
        [Category("1 - CRUD Article")]
        [Description("1.3 - CRUD Articles - User is Able to Delete an Article after Creation (Article Button)")]
        public void Delete_article_1_3()
        {
            String article = createNewArticle(driver, "title", "description", "body", "tags");
            bool deleted = deleteFromArticle(driver);
            Assert.That(deleted, Is.True);
        }

        [Test]
        [Category("Smoke Test")]
        [Category("1 - CRUD Article")]
        [Description("1.4 - CRUD Articles - User is Able to Edit Title after Article Creation (Banner Button)")]
        public void edit_article_1_4()
        {
            String article = createNewArticle(driver, "", "description", "body", "tags");
            String updated = updateFromBanner(driver,"title");
            Assert.That(updated.Contains("article"));
        }

        [Test]
        [Category("Smoke Test")]
        [Category("1 - CRUD Article")]
        [Description("1.5 - CRUD Articles - User is Able to Edit What's this article about field after Article Creation (Banner Button)")]
        public void edit_article_1_5()
        {
            String article = createNewArticle(driver, "Title", "", "body", "tags");
            String updated = updateFromBanner(driver,"description");
            Assert.That(updated.Contains("article"));
        }

        [Test]
        [Category("Smoke Test")]
        [Category("1 - CRUD Article")]
        [Description("1.6 - CRUD Articles - User is Able to Edit Write your article(in markdown) field after Article Creation (Article Button)")]
        public void edit_article_1_6()
        {
            String article = createNewArticle(driver, "Title", "Description", "", "tags");
            String updated = updateFromArticle(driver, "body");
            Assert.That(updated.Contains("article"));
        }

        [Test]
        [Category("Smoke Test")]
        [Category("1 - CRUD Article")]
        [Description("1.7 - CRUD Articles - User is Able to Edit Tags field after Article Creation (Article Button)")]
        public void edit_article_1_7()
        {
            String article = createNewArticle(driver, "Title", "Description", "Body", "");
            String updated = updateFromArticle(driver, "tags");
            Assert.That(updated.Contains("article"));
        }


        [Test]
        [Category("Smoke Test")]
        [Category("2 - CRD comments on Articles")]
        [Description("2.1 - CRD Comments on Articles - User is Able to Post a Comment after Create an Article")]
        public void post_comment_2_1()
        {
            String article = createNewArticle(driver, "Title", "Description", "Body", "tags");
            bool comment = postComment(driver, "Comment on Article from Articles Page");
            Assert.That(comment, Is.True);
        }


        [Test]
        [Category("Smoke Test")]
        [Category("2 - CRD comments on Articles")]
        [Description("2.2 - CRD Comments on Articles - User is Able to Post an Empty Comment after Create an Article")]
        public void post_comment_2_2()
        {
            String article = createNewArticle(driver, "Title", "Description", "Body", "tags");
            bool comment = postComment(driver,"");
            Assert.That(comment, Is.True);
        }

        [Test]
        [Category("Smoke Test")]
        [Category("2 - CRD comments on Articles")]
        [Description("2.3 - CRD Comments on Articles - User is Able to Remove a Posted Comment after Creation")]
        public void remove_comment_2_3()
        {
            String article = createNewArticle(driver, "Title", "Description", "Body", "tags");
            bool comment = postComment(driver, "This is a Comment");
            comment = deleteComment(driver);
            Assert.That(comment, Is.True);
        }

        [Test]
        [Category("Smoke Test")]
        [Category("2 - CRD comments on Articles")]
        [Description("2.4 - CRD Comments on Articles - User is Able to Remove all Posted Comments after Creation")]
        public void remove_comment_2_4()
        {
            String article = createNewArticle(driver, "Title", "Description", "Body", "tags");
            bool comment = postComment(driver, "This is a Comment");
            comment = postComment(driver, "This is a Comment2");
            comment = postComment(driver, "This is a Comment3");
            comment = deleteallComments(driver);
            Assert.That(comment, Is.True);
        }

        [Test]
        [Category("Smoke Test")]
        [Category("2 - CRD comments on Articles")]
        [Description("2.5 - CRD Comments on Articles - User is Able to Post a Comment on Other user's Article")]
        public void post_comment_other_2_5()
        {
            string others = checkOthersArticle(driver);
            bool comment = postComment(driver, "This is a Comment");
            Assert.That(comment, Is.True);
        }

        [Test]
        [Category("Smoke Test")]
        [Category("2 - CRD comments on Articles")]
        [Description("2.6 - CRD Comments on Articles - User is Able to Remove a Comment on Other user's Article")]
        public void remove_comment_other_2_6()
        {
            string others = checkOthersArticle(driver);
            driver.Navigate().GoToUrl(others);
            bool comment = postComment(driver, "This is a Comment");
            comment = removeOthersComment(driver);
            Assert.That(comment, Is.True);
        }

        [Test]
        [Category("Smoke Test")]
        [Category("2 - CRD comments on Articles")]
        [Description("2.7 - CRD Comments on Articles - User is Able to Post a Comment on an Article shown after select any of the Popular Tags")]
        public void post_comment_other_2_7()
        {
            bool others = checkOthersbytags(driver);
            bool comment = postComment(driver, "This is a Comment");
            Assert.That(comment, Is.True);
        }

        [Test]
        [Category("Smoke Test")]
        [Category("3 - Favorite Articles")]
        [Description("3.1 - Favorite Articles - User is Able to Mark as Favorite first listed Article")]
        public void mark_favorite_3_1()
        {
            bool favorite = markFavorite(driver,1);
            Assert.That(favorite, Is.True);
        }

        [Test]
        [Category("Smoke Test")]
        [Category("3 - Favorite Articles")]
        [Description("3.2 - Favorite Articles - User is Able to Unmark as Favorite  previously Marked Article")]
        public void Unmark_favorite_3_2()
        {
            bool favorite = markFavorite(driver,1);
            favorite = unmarkFavorite(driver);
            Assert.That(favorite, Is.True);
        }

        [Test]
        [Category("Smoke Test")]
        [Category("3 - Favorite Articles")]
        [Description("3.3 - Favorite Articles - User is Able to Mark as Favorite an Article from his profile")]
        public void Mark_favorite_3_3()
        {
            bool userprofile = user_profile(driver);
            bool favorite = markFavorite(driver,0);
            Assert.That(favorite, Is.True);
        }

        [Test]
        [Category("Smoke Test")]
        [Category("3 - Favorite Articles")]
        [Description("3.4 - Favorite Articles - User is Able to Unmark as Favorite an Article from his profile")]
        public void Unmark_favorite_3_4()
        {
            bool userprofile = user_profile(driver);
            bool favorite = markFavorite(driver, 0);
            favorite = markFavorite(driver, 0);
            Assert.That(favorite, Is.True);
        }

        [Test]
        [Category("Smoke Test")]
        [Category("3 - Favorite Articles")]
        [Description("3.5 - Favorite Articles - User is Able to Mark all Articles as Favorite from his profile")]
        public void mark_all_favorite_3_5()
        {
            bool userprofile = user_profile(driver);
            bool favorite = marksallFavorite(driver);
            Assert.That(favorite, Is.True);
        }

        [Test]
        [Category("Smoke Test")]
        [Category("3 - Favorite Articles")]
        [Description("3.6 - Favorite Articles - User is Able to Mark other User's Article as Favorite from Article Section (Banner Section)")]
        public void mark_others_favorite_3_6()
        {
            string others = checkOthersArticle(driver);
            bool favorite = markFavorite(driver, 2);
            Assert.That(favorite, Is.True);
        }

        [Test]
        [Category("Smoke Test")]
        [Category("3 - Favorite Articles")]
        [Description("3.7 - Favorite Articles - User is Able to UnMark other User's Article as Favorite from Article Section (Comments Section)")]
        public void mark_others_favorite_3_7()
        {
            string others = checkOthersArticle(driver);
            bool favorite = markFavorite(driver, 2);
            favorite = markFavorite(driver, 3);
            Assert.That(favorite, Is.True);
        }

        [Test]
        [Category("Smoke Test")]
        [Category("3 - Favorite Articles")]
        [Description("3.8 - Favorite Articles - User is Able to Mark an Article Listed after select any of the Tags shown on the Popular Tags Section")]
        public void mark_tags_favorite_3_8()
        {
            bool others = checkOthersbytags(driver);
            bool favorite = markFavorite(driver, 0);
            Assert.That(favorite, Is.True);
        }




        [TearDown]
        public void closeBrowser()
        {
            Thread.Sleep(2000);
            driver.Quit();
        }

        //Methods
        public void login(IWebDriver driver) {
            driver.FindElement(By.CssSelector("a[href='/login'")).Click();
            driver.FindElement(By.XPath("//input[@formcontrolname='email']")).SendKeys("asd@asd.com");
            driver.FindElement(By.XPath("//input[@formcontrolname='password']")).SendKeys("asd");
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
        }

        public void waitCSSSelector(IWebDriver driver,String selector) {
            IWait<IWebDriver> wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector(selector)));
        }

        public void waitXpath(IWebDriver driver, String xpath)
        {
            IWait<IWebDriver> wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(xpath)));
        }

        public String createNewArticle(IWebDriver driver,String title, String description, String body, String tags) {
            waitCSSSelector(driver, "a[href='/editor'");
            driver.FindElement(By.CssSelector("a[href='/editor'")).Click();
            driver.FindElement(By.XPath("//input[@formcontrolname='title']")).SendKeys(title);
            driver.FindElement(By.XPath("//input[@formcontrolname='description']")).SendKeys(description);
            driver.FindElement(By.XPath("//textarea[@formcontrolname='body']")).SendKeys(body);
            driver.FindElement(By.XPath("//input[@placeholder='Enter tags']")).SendKeys(tags);
            driver.FindElement(By.XPath("//button[@type='button']")).Click();
            Thread.Sleep(2000);
            return (driver.Url);
        }

        public bool deleteFromBanner(IWebDriver driver) {
            try
            {
                waitXpath(driver, "//div[@class='banner']//div[@class='article-meta']//span//button[text()=' Delete Article ']");
                driver.FindElement(By.XPath("//div[@class='banner']//div[@class='article-meta']//span//button[text()=' Delete Article ']")).Click();
                return (true);
            }
            catch (Exception e) {
                return (false);
            }
            
        }

        public bool deleteFromArticle(IWebDriver driver)
        {
            try
            {
                waitXpath(driver, "//div[@class='article-actions']//div[@class='article-meta']//span//button[text()=' Delete Article ']");
                driver.FindElement(By.XPath("//div[@class='article-actions']//div[@class='article-meta']//span//button[text()=' Delete Article ']")).Click();
                Thread.Sleep(2000);
                return (true);
            }
            catch (Exception e)
            {
                return (false);
            }

        }

        public String updateFromBanner(IWebDriver driver, String update)
        {

            try
            {
                waitXpath(driver, "//div[@class='banner']//div[@class='article-meta']//span//a[text()=' Edit Article ']");
                driver.FindElement(By.XPath("//div[@class='banner']//div[@class='article-meta']//span//a[text()=' Edit Article ']")).Click();
                switch (update)
                {
                    case "title":
                        driver.FindElement(By.XPath("//input[@formcontrolname='title']")).SendKeys("Updated Title");
                        break;

                    case "description":
                        driver.FindElement(By.XPath("//input[@formcontrolname='description']")).SendKeys("updated description");
                        break;

                    case "body":
                        driver.FindElement(By.XPath("//textarea[@formcontrolname='body']")).SendKeys("updated Body");
                        break;

                    case "tags":
                        driver.FindElement(By.XPath("//input[@placeholder='Enter tags']")).SendKeys("Updated Tags");
                        break;

                    default:
                        return ("");
                }

                
                driver.FindElement(By.XPath("//button[@type='button']")).Click();
                Thread.Sleep(3000);
                return (driver.Url);
            }
            catch (Exception e)
            {
                return ("");
            }

        }

        public String updateFromArticle(IWebDriver driver, String update)
        {

            try
            {
                waitXpath(driver, "//div[@class='article-actions']//div[@class='article-meta']//span//a[text()=' Edit Article ']");
                driver.FindElement(By.XPath("//div[@class='article-actions']//div[@class='article-meta']//span//a[text()=' Edit Article ']")).Click();
                
                switch (update)
                {
                    case "title":
                        driver.FindElement(By.XPath("//input[@formcontrolname='title']")).SendKeys("Updated Title");
                        break;

                    case "description":
                        driver.FindElement(By.XPath("//input[@formcontrolname='description']")).SendKeys("updated description");
                        break;

                    case "body":
                        driver.FindElement(By.XPath("//textarea[@formcontrolname='body']")).SendKeys("updated Body");
                        break;

                    case "tags":
                        driver.FindElement(By.XPath("//input[@placeholder='Enter tags']")).SendKeys("Updated Tags");
                        break;

                    default:
                        Console.WriteLine("Fail");
                        break;
                }


                driver.FindElement(By.XPath("//button[@type='button']")).Click();
                Thread.Sleep(3000);
                return (driver.Url);
            }
            catch (Exception e)
            {
                return ("");
            }

        }

        public bool postComment(IWebDriver driver,String comment) {
            try
            {
                driver.FindElement(By.XPath("//textarea[@placeholder='Write a comment...']")).SendKeys(comment);
                driver.FindElement(By.XPath("//button[@type='submit']")).Click();
                return (true);
            }
            catch (Exception e)
            {
                return (false);
            }

        }


        public bool deleteComment(IWebDriver driver)
        {
            try
            {
                List<IWebElement> comments = new List<IWebElement>(driver.FindElements(By.XPath("//app-article-comment")));
                comments[0].FindElement(By.XPath("//div[@class='card']//i[@class='ion-trash-a']")).Click();
                return (true);
            }
            catch (Exception e)
            {
                return (false);
            }

        }

        public bool deleteallComments(IWebDriver driver)
        {
            try
            {
                List<IWebElement> comments = new List<IWebElement>(driver.FindElements(By.XPath("//app-article-comment")));
                foreach (var el in comments)
                {
                    el.FindElement(By.XPath("//div[@class='card']//i[@class='ion-trash-a']")).Click();
                }
                return (true);
            }
            catch (Exception e)
            {
                return (false);
            }

        }


        public String checkOthersArticle(IWebDriver driver)
        {
            try
            {
                driver.FindElement(By.XPath("//a[text()=' Global Feed ']")).Click();
                waitXpath(driver, "//app-article-list//app-article-preview");
                List<IWebElement> articles = new List<IWebElement>(driver.FindElements(By.XPath("//app-article-list//app-article-preview")));
                for (int i = 0; i < articles.Count; i++)
                {
                    String[] author = articles[i].Text.Split("\r\n", StringSplitOptions.None);
                    if (author[0] != driver.FindElement(By.XPath("//ul[@class='nav navbar-nav pull-xs-right']//li[4]")).Text)
                    {
                        driver.FindElement(By.XPath("//app-article-list//app-article-preview[" + (i + 1) + "]//span[text()='Read more...']")).Click();
                        break;

                    }
                }
                return (driver.Url);
            }
            catch (Exception e)
            {
                return ("");
            }

        }

        public bool removeOthersComment(IWebDriver driver)
        {
            try
            {
                List<IWebElement> comments = new List<IWebElement>(driver.FindElements(By.XPath("//app-article-comment")));
                comments[0].FindElement(By.XPath("//div[@class='card']//i[@class='ion-trash-a']")).Click();
                return (true);
            }
            catch (Exception e)
            {
                return (false);
            }

        }

        public bool checkOthersbytags(IWebDriver driver)
        {
            try
            {
                waitXpath(driver, "//div[@class='tag-list']");
                List<IWebElement> s_tags = new List<IWebElement>(driver.FindElements(By.XPath("//div[@class='tag-list']")));
                String[] t_tag = s_tags[0].Text.Split(" ", StringSplitOptions.None);
                s_tags[0].FindElement(By.XPath("//a[text()=' " + t_tag[0] + " ']")).Click();
                driver.FindElement(By.XPath("//app-article-list//app-article-preview//span[text()='Read more...']")).Click();
                return (true);
            }
            catch (Exception e)
            {
                return (false);
            }

        }

        public bool markFavorite(IWebDriver driver, int globalfeed)
        {
            try
            {
                if (globalfeed == 1) //Check Global Feed
                {
                    driver.FindElement(By.XPath("//a[text()=' Global Feed ']")).Click();
                    Thread.Sleep(1000);
                }
                if (globalfeed == 2) //from Banner
                {
                    driver.FindElement(By.XPath("//div[@class='banner']//div[@class='article-meta']//span//button[text()=' Favorite Article ']")).Click();
                    return (true);
                }
                if (globalfeed == 3) //Unfavorite from Others Article
                {
                    driver.FindElement(By.XPath("//div[@class='article-actions']//div[@class='article-meta']//span//button[text()=' Unfavorite Article ']")).Click();
                    return (true);
                }


                driver.FindElement(By.XPath("//app-article-list//app-article-preview[1]//app-favorite-button[@class='pull-xs-right']")).Click();
                return (true);
            }
            catch (Exception e)
            {
                return (false);
            }

        }

        public bool unmarkFavorite(IWebDriver driver)
        {
            try
            {
                driver.FindElement(By.XPath("//app-article-list//app-article-preview[1]//app-favorite-button[@class='pull-xs-right']")).Click();
                return (true);
            }
            catch (Exception e)
            {
                return (false);
            }

        }

        public bool user_profile(IWebDriver driver)
        {
            try
            {
                waitXpath(driver,"//ul[@class='nav navbar-nav pull-xs-right']//li[4]");
                driver.FindElement(By.XPath("//ul[@class='nav navbar-nav pull-xs-right']//li[4]")).Click();
                return (true);
            }
            catch (Exception e)
            {
                return (false);
            }

        }

        public bool marksallFavorite(IWebDriver driver)
        {
            try
            {
                waitXpath(driver, "//app-article-list//app-article-preview//app-favorite-button[@class='pull-xs-right']");
                int arts = driver.FindElements(By.XPath("//app-article-list//app-article-preview//app-favorite-button[@class='pull-xs-right']")).Count;
                for (int i = 1; i <= arts; i++)
                {
                    driver.FindElement(By.XPath("//app-article-list//app-article-preview[" + i + "]//app-favorite-button[@class='pull-xs-right']")).Click();
                }
                return (true);
            }
            catch (Exception e)
            {
                return (false);
            }

        }



    }
}