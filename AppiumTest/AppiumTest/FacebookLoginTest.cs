using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Remote;
using NUnit.Framework;

namespace AppiumTest
{
    [TestClass]
    public class FacebookLoginTest
    {
        // Create Appium Drive Instances

        AppiumDriver<AndroidElement> _driver;
        DesiredCapabilities cap;

        [SetUp]
        public void initializeDriver()
        {
            cap = new DesiredCapabilities();
            cap.SetCapability("deviceame", "[Your Device Name]");
            cap.SetCapability("apppackage", "[Your App Packahe]");


            // Launch the android drive on the simulator
            _driver = new AndroidDriver<AndroidElement>(new Uri(""), cap);
        }

        [Test]
        public void TestMethod1()
        {

        }

        [TearDown]
        public void closeDriver()
        {
            _driver.Quit();
        }
    }
}
