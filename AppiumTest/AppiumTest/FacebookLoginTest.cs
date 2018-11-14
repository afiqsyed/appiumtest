using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Remote;
using NUnit.Framework;
using OpenQA.Selenium;

namespace AppiumTest
{
    [TestClass]
    public class FacebookLoginTest
    {
        // Create Appium Drive Instances

        AppiumDriver<AndroidElement> _driver;
        DesiredCapabilities cap;

        string username, password;

        [SetUp]
        public void initializeDriver()
        {
            cap = new DesiredCapabilities();
            cap.SetCapability("deviceame", "[Your Device Name]");
            cap.SetCapability("apppackage", "[Your App Packahe]");
            cap.SetCapability("platformVersion", "11.0");


            // Launch the android drive on the simulator
            _driver = new AndroidDriver<AndroidElement>(new Uri(""), cap);
        }

        [Test]
        public void LoginUsingFacebook_CorrectUsernamePassowod_Successful()
        {
            var username = "[Facebook Usernama]";
            var password = "[Facebook Paassword]";

            GivenIHaveAUsernameAndPassword(username, password);
            WhenIClickLoginButton();
            AndIInsertUserNameAndPassword();
            UserIsSuccesfullyLogin();
        }

        [Test]
        public void LoginUsingFacebook_DenyPermission_LoginUnsuccesful()
        {
            // Using Invalid Account
            var username = "[Facebook Usernama]";
            var password = "[Facebook Paassword]";

            GivenIHaveAUsernameAndPassword(username, password);
            WhenIClickLoginButton();
            AndIInsertUserNameAndPassword();

            // Assert That Wrong Username and Password Messages Appear
            NUnit.Framework.Assert.IsTrue(VerifyThatElementIsPresent("[Element]"));
        }

        [Test]
        public void LoginUsingFacebook_AppPermissionDeniedOnFacebook_PermissionPageShownUp()
        {
            // Using Account That Don't Have Permission Setting
            var username = "[Facebook Usernama]";
            var password = "[Facebook Paassword]";

            GivenIHaveAUsernameAndPassword(username, password);
            WhenIClickLoginButton();
            AndIInsertUserNameAndPassword();

            // Assert That The Permission Page Appear
            NUnit.Framework.Assert.IsTrue(VerifyThatElementIsPresent("[Element]"));
        }

        private void GivenIHaveAUsernameAndPassword(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        public void WhenIClickLoginButton()
        {
            _driver.FindElementById("[Login Button Id]").Click();
        }

        public void AndIInsertUserNameAndPassword()
        {
            _driver.FindElementById("[Username Text Field]").SendKeys(this.username);
            _driver.FindElementById("[Password Text Field]").SendKeys(this.password);
            hideKeyBoard();
        }

        private void UserIsSuccesfullyLogin()
        {
            // Assert That The Welcome Page Is Shown
            NUnit.Framework.Assert.IsTrue(VerifyThatElementIsPresent("Welcome Page Element Id"));
        }


        public void hideKeyBoard()
        {
            try { _driver.HideKeyboard(); }
            catch (Exception e) { }
        }

        public bool VerifyThatElementIsPresent(string id)
        {
            try
            {
                _driver.FindElement(By.Id("id"));
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        [TearDown]
        public void closeDriver()
        {
            _driver.Quit();
        }

    }
}
