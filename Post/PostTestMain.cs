using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Support.UI;

namespace UITest1
{
    [TestClass]
    public class PostTestMain : JupiterTestBase
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            if (Driver == null)
            {
                foreach (var process in Process.GetProcessesByName("PCAD_main"))
                {
                    process.Kill();
                }
                logger.Info(GetConfiguration("PCAD_Path"));

                Setup(GetConfiguration("PCAD_Path"));
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
                foreach (var process in Process.GetProcessesByName("PCAD_main"))
                {
                    process.Kill();
                }

                Thread.Sleep(4000);
                Setup(GetConfiguration("PCAD_Path"));
            }
            wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(50));
            Assert.IsNotNull(wait);
            Driver.Manage().Window.Maximize();
            jupiter = Driver.FindElementByXPath("//Window[starts-with(@Name,'Jupiter-Post 4.1.3')]");
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
            PostTestScenario3Boxes.TestScenario3Boxes(pathDirectory);
        }

        [TestMethod]
        public void TestBug10939()
        {
            PostTestBug10939.TestBug10939(pathDirectory);
        }

        [TestMethod]
        public void TestBug10763()
        {
            PostTestBug10763.TestBug10763();
        }

        [TestMethod]
        public void TestBug10752()
        {
            PostTestBug10752.TestBug10752(pathDirectory);
        }

        [TestMethod]
        public void TestBug10739()
        {
            PostTestBug10739.TestBug10739(pathDirectory);
        }

        [TestMethod]
        public void TestBug10606()
        {
            PostTestBug10606.TestBug10606(pathDirectory);
        }

        [TestMethod]
        public void TestBug10599()
        {
            PostTestBug10599.TestBug10599(pathDirectory);
        }

        [TestMethod]
        public void TestBug10143()
        {
            PostTestBug10143.TestBug10143(pathDirectory);
        }

        [TestMethod]
        public void TestBug10600()
        {
            PostTestBug10600.TestBug10600(pathDirectory);
        }

        [TestMethod]
        public void TestBug10894()
        {
            PostTestBug10894.TestBug10894(pathDirectory);
        }

        [TestMethod]
        public void TestBug10646()
        {
            PostTestBug10646.TestBug10646(pathDirectory);
        }

        [TestMethod]
        public void TestBug10617()
        {
            PostTestBug10617.TestBug10617(pathDirectory);
        }

        [TestMethod]
        public void TestBug9142()
        {
            PostTestBug9142.TestBug9142(pathDirectory);
        }

        [TestMethod]
        public void TestBug8618()
        {
            PostTestBug8618.TestBug8618(pathDirectory);
        }
    }
}
