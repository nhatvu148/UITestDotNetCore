using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLog.Fluent;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
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

                Setup("C:\\Program Files\\TechnoStar\\Jupiter-Post_4.1.3\\PCAD_main.exe");
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
                Setup("C:\\Program Files\\TechnoStar\\Jupiter-Post_4.1.3\\PCAD_main.exe");
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
        public void TestMyTest()
        {
            string strOp2File = $"{pathDirectory}\\TestResults\\JPT-Post\\TestBug8618\\Input\\cube.op2";
            string strCSVFile = $"{pathDirectory}\\TestResults\\JPT-Post\\TestBug8618\\Input\\2_Save.csv";
            string strActualFolder = $"{pathDirectory}\\TestResults\\JPT-Post\\TestBug8618\\Actual\\";

            //Open file
            PostTool.ImportOp2(strOp2File, 1.5);
            Thread.Sleep(1000);

            //Navigate to dialog
            string[] strRibbon = { "Tools", "Note", "Point" };
            PostTool.OpenRibbon(strRibbon);

            //Open from csv
            Driver.FindElementByName("Import from CSV").Click();
            Thread.Sleep(1000);
            var DialogPositions = Driver.FindElementByName("Positions");
            DialogPositions.FindElementByAccessibilityId("5512").Click();
            Thread.Sleep(1000);



            action = new Actions(Driver);
            action.SendKeys(strCSVFile);
            action.SendKeys(Keys.Enter).Perform();
            Thread.Sleep(1000);

            action = new Actions(Driver);
            action.MoveToElement(DialogPositions, 0, 0);
            action.MoveByOffset(110, 110);
            action.Click().Perform();

            var bigTable = DialogPositions.FindElementByAccessibilityId("5511");

            var rows = bigTable.FindElementsByXPath("//child::*");
            //var firstRow1 = bigTable.FindElementByXPath("//*[1]/following-sibling::*[1]");

            foreach (var child in rows)
            {
                logger.Info(child.Text);
            }

            rows[8].Click();

            Assert.AreEqual("1", rows[8].Text);
            //Driver.FindElementByXPath("//Button[@Name='Application menu']").Click();

            Collection<string[]> cstrAllData = CSVTool.ReadCSVFile(strCSVFile, ',');
            double dFirstValue = Convert.ToDouble(cstrAllData[0][0]);
        }
    }

    class CSVTool : JupiterTestBase
    {
        public static Collection<string[]> ReadCSVFile(string strFilePath, char charDelimiter)
        {
            Collection<string[]> cstrReturn = new Collection<string[]>();
            StreamReader sr = new StreamReader(strFilePath);
            string line;
            string[] row;
            while ((line = sr.ReadLine()) != null)
            {
                row = line.Split(charDelimiter);
                cstrReturn.Add(row);
            }
            return cstrReturn;
        }
    }

    class PostTool : JupiterTestBase
    {
        public static void ImportOp2(string strFilePath, double time_wait_for_import_s)
        {
            jupiter.FindElementByName("Import Results").Click();
            Thread.Sleep(1000);
            jupiter.FindElementByName("Nastran Op2").Click();
            action = new Actions(Driver);
            action.SendKeys(strFilePath);
            action.SendKeys(Keys.Enter).Perform();
            Thread.Sleep((int)(time_wait_for_import_s * 1000));
        }

        public static void OpenRibbon(string[] strRibbon)
        {
            int nCnt = 0;
            foreach (string strName in strRibbon)
            {
                if (nCnt++ == 0)
                    toolBar.FindElementByName(strName).Click();
                else
                    jupiter.FindElementByName(strName).Click();
                Thread.Sleep(1000);
            }
        }
    }
}
