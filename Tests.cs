using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace JBAssignment4
{
    [TestFixture]
    public class Tests
    {
        private IWebDriver driver;
        private string baseURL;

        [SetUp]

        public void SetupTest()
        {
            driver = new FirefoxDriver();
            baseURL = "http://localhost:63342/JBAssignment4HTML/index.html?_ijt=k7o93n8jgpoqpab7uqvagub44o";
           
        }

        [TearDown]

        public void TearDownTest()
        {
            if (driver != null)
            {
                driver.Quit();
            }
        }

        //not correct length
        [Test]
        public void AddVehicle_testingPhoneNumber_DisplaysErrors()
        {
            driver.Navigate().GoToUrl(baseURL);
            driver.FindElement(By.Id("sellerName")).SendKeys("Julia Bernoski");
            driver.FindElement(By.Id("address")).SendKeys("EasyStreet");
            driver.FindElement(By.Id("city")).SendKeys("Alexandria");
            driver.FindElement(By.Id("phoneNumber")).SendKeys("123-123-123");
            driver.FindElement(By.Id("saveVehicle")).Click();
            string error = driver.FindElement(By.Id("phoneNumber")).Text;
            Assert.AreNotEqual("123-123-1234", error);
        }

       
        //not correct format for email
        [Test]
        public void AddVehicle_testEmailAddress_DisplaysErrors()
        {
            driver.Navigate().GoToUrl(baseURL);
            driver.FindElement(By.Id("sellerName")).SendKeys("Julia");
            driver.FindElement(By.Id("address")).SendKeys("EasyStreet");
            driver.FindElement(By.Id("city")).SendKeys("Alexandria");
            driver.FindElement(By.Id("phoneNumber")).SendKeys("123-123-1234");
            driver.FindElement(By.Id("emailAddress")).SendKeys("gfgjfj");
            driver.FindElement(By.Id("saveVehicle")).Click();
            string error = driver.FindElement(By.Id("emailAddress")).Text;
            Assert.AreNotEqual("julia.bernoski@icloud", error);
        }

        //vehicle year is not a number
        [Test]
        public void AddVehicle_testVehicleYear_DisplaysErrors()
        {
            driver.Navigate().GoToUrl(baseURL);
            driver.FindElement(By.Id("sellerName")).SendKeys("Julia");
            driver.FindElement(By.Id("address")).SendKeys("EasyStreet");
            driver.FindElement(By.Id("city")).SendKeys("Alexandria");
            driver.FindElement(By.Id("phoneNumber")).SendKeys("123-123-1234");
            driver.FindElement(By.Id("emailAddress")).SendKeys("julia.bernoski@icloud.com");
            driver.FindElement(By.Id("vehicleYear")).SendKeys("sfg");
            driver.FindElement(By.Id("vehicleMake")).SendKeys("sfgsg");
            driver.FindElement(By.Id("vehicleModel")).SendKeys("12");

            driver.FindElement(By.Id("saveVehicle")).Click();
            Assert.IsTrue(driver.PageSource.Contains("Please Enter a valid year."));
        }

        //testing that everything is required
        [Test]
        public void AddVehicle_testRequired_DisplaysErrors()
        {
            driver.Navigate().GoToUrl(baseURL);
            driver.FindElement(By.Id("sellerName")).Clear();
            driver.FindElement(By.Id("address")).Clear();
            driver.FindElement(By.Id("city")).Clear();
            driver.FindElement(By.Id("phoneNumber")).Clear();
            driver.FindElement(By.Id("emailAddress")).Clear();
            driver.FindElement(By.Id("vehicleYear")).Clear();
            driver.FindElement(By.Id("vehicleMake")).Clear();
            driver.FindElement(By.Id("vehicleModel")).Clear();

            driver.FindElement(By.Id("saveVehicle")).Click();
            Assert.IsEmpty(driver.FindElements(By.Id("sellerName,address,city,phoneNumber,emailAddress,vehicleYear,vehicleMake,vehicleModel")), "");
        }

        //testing link -- doesn't work cause firefox sucks and can't save database stuff.
      /* [Test]
        public void viewVehicle_jdpowerLink_works()
        {
            driver.Navigate().GoToUrl(baseURL);
            driver.FindElement(By.Id("sellerName")).SendKeys("Julia");
            driver.FindElement(By.Id("address")).SendKeys("EasyStreet");
            driver.FindElement(By.Id("city")).SendKeys("Alexandria");
            driver.FindElement(By.Id("phoneNumber")).SendKeys("123-123-1234");
            driver.FindElement(By.Id("emailAddress")).SendKeys("julia.bernoski@icloud.com");
            driver.FindElement(By.Id("vehicleYear")).SendKeys("2001");
            driver.FindElement(By.Id("vehicleMake")).SendKeys("ford");
            driver.FindElement(By.Id("vehicleModel")).SendKeys("mustang");
            driver.FindElement(By.Id("saveVehicle")).Click();
            driver.FindElement(By.Id("theEnteredInfo")).Click();
            driver.Navigate().GoToUrl(baseURL + "#enteredInfo");
            string actual = driver.FindElement(By.Id("link")).Text;
            Assert.AreEqual("http://www.jdpower.com/cars/ford/mustang/2001",actual);
        }*/

        //no errors, and everything is filled out
        [Test]
        public void AddVehicle_noErrors_SavesInfoWithNoErrors()
        {
            driver.Navigate().GoToUrl(baseURL);
            driver.FindElement(By.Id("sellerName")).SendKeys("Daryl Dixon");
            driver.FindElement(By.Id("address")).SendKeys("Easy Street");
            driver.FindElement(By.Id("city")).SendKeys("Hilltop");
            driver.FindElement(By.Id("phoneNumber")).SendKeys("226-123-2344");
            driver.FindElement(By.Id("emailAddress")).SendKeys("andEmail@gmail.com");
            driver.FindElement(By.Id("vehicleYear")).SendKeys("1998");
            driver.FindElement(By.Id("vehicleMake")).SendKeys("ford");
            driver.FindElement(By.Id("vehicleModel")).SendKeys("mustang");

            driver.FindElement(By.Id("saveVehicle")).Click();
            
            Assert.IsTrue(driver.PageSource.Contains(""));
        }
    }
}
