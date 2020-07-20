using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Support.UI;

namespace UITest1
{
    [TestClass]
    public class PreTestMain : JupiterTestBase
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            if (Driver == null)
            {
                foreach (var process in Process.GetProcessesByName("DCAD_main"))
                {
                    process.Kill();
                }
                logger.Info(GetConfiguration("DCAD_Path"));

                Setup(GetConfiguration("DCAD_Path"));
                objTestContext = testContext;
            }
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            TearDown();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            if (Driver == null)
            {
                foreach (var process in Process.GetProcessesByName("DCAD_main"))
                {
                    process.Kill();
                }

                Thread.Sleep(4000);
                Setup(GetConfiguration("DCAD_Path"));
            }

            wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(50));
            Assert.IsNotNull(wait);
            Driver.Manage().Window.Maximize();
            jupiter = Driver.FindElementByXPath("//Window[starts-with(@Name,'Jupiter-Pre 4.1.3')]");
            toolBar = Driver.FindElementByName("Ribbon Tabs");
        }

        [TestCleanup]
        public void TestCleanup()
        {
            TearDown();
        }

        [TestMethod]
        public void TestScenario3Boxes()
        {
            PreTestScenario3Boxes.TestScenario3Boxes(pathDirectory);
        }

        [TestMethod]
        public void TestMazdaRely_1()
        {
            PreTestMazdaRely.TestMazdaRely_1(pathDirectory);
        }

        [TestMethod]
        public void TestMazdaRely_2()
        {
            PreTestMazdaRely.TestMazdaRely_2(pathDirectory);
        }
    }
}