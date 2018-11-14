using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Remote;
using NUnit.Framework;
using OpenQA.Selenium;

namespace AppiumTest
{
    class PaymentGatewayTest
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
        public void PaymentUsingPaypal_CorrectUsernamePassoword_EnoughBalance_Successful()
        {
            var username = "[PayPal Usernama]";
            var password = "[PayPal Paassword]";

            GivenIHavePayPalUsernameAndPassword(username, password);
            WhenIClickPayButton();
            AndIInsertPayPalUserNameAndPasswordAndClickPay();
            ThenPaymentIsSuccessful();
        }

        [Test]
        public void PaymentUsingPaypal_CorrectUsernamePassoword_EnoughBalance_Unsuccessful()
        {
            var username = "[PayPal Usernama]";
            var password = "[PayPal Paassword]";

            GivenIHavePayPalUsernameAndPassword(username, password);
            WhenIClickPayButton();
            AndIInsertPayPalUserNameAndPasswordAndClickPay();
            ThenPaymentIsUnsuccessful();
        }

        [Test]
        public void PaymentUsingPaypal_InCorrectUsernamePassoword_Unsuccessful()
        {
            var username = "[PayPal Usernama]";
            var password = "[PayPal Paassword]";

            GivenIHavePayPalUsernameAndPassword(username, password);
            WhenIClickPayButton();
            AndIInsertPayPalUserNameAndPasswordAndClickPay();
            TheWrongPasswordMessagesIsAppeared();
        }

        [Test]
        public void PaymentUsingPaypal_ClickBackButton_Unsuccessful()
        {
            var username = "[PayPal Usernama]";
            var password = "[PayPal Paassword]";

            GivenIHavePayPalUsernameAndPassword(username, password);
            WhenIClickPayButton();

            // Trigger Back Button
            _driver.pressKeyCode(AndroidKeyCode.BACK);

            ThenPaymentIsUnsuccessful();
        }

        private void GivenIHavePayPalUsernameAndPassword(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        public void WhenIClickPayButton()
        {
            _driver.FindElementById("[Login Button Id]").Click();
        }

        public void AndIInsertPayPalUserNameAndPasswordAndClickPay()
        {
            _driver.FindElementById("[Username Text Field]").SendKeys(this.username);
            _driver.FindElementById("[Password Text Field]").SendKeys(this.password);
            hideKeyBoard();
            _driver.FindElementById("[Pay Button]").Click();
        }

        private void ThenPaymentIsSuccessful()
        {
            // Assert That The Payment Success Page Is Shown
            Assert.IsTrue(VerifyThatElementIsPresent("[PageId]"));
        }

        private void ThenPaymentIsUnsuccessful()
        {
            // Assert That The Payment Not Success Page Is Shown
            Assert.IsTrue(VerifyThatElementIsPresent("[PageId]"));
        }

        private void TheWrongPasswordMessagesIsAppeared()
        {
            // Assert That The Wrong Password Messages Page Is Shown
            Assert.IsTrue(VerifyThatElementIsPresent("[PageId]"));
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
