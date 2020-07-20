using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static UITest1.AkiyamaTool;

namespace UITest1
{
    class PostTestBug8618 : JupiterTestBase
    {
        //Bug #8618:        Note > Point: Import from CSV has some errors.
        //Author:           Kitazato
        //Date created:     July 14, 2020
        //Redmine item:     #11164 - Software Test: Errors when importing csv
        public static void TestBug8618(string pathDirectory)
        {
            string strOp2File = $"{pathDirectory}\\TestResults\\JPT-Post\\TestBug8618\\Input\\cube.op2";
            string strCSVFile = $"{pathDirectory}\\TestResults\\JPT-Post\\TestBug8618\\Input\\2_Save.csv";
            string strActualFolder = $"{pathDirectory}\\TestResults\\JPT-Post\\TestBug8618\\Actual\\";

            //Open file
            PostTool.ImportOp2(strOp2File, 1.5);

            //Navigate to dialog
            string[] strRibbon = { "Tools", "Note", "Point" };
            var DialogNote = PostTool.OpenRibbon(strRibbon);
            Assert.IsNotNull(DialogNote);

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

            //Read data in table
            var bigTable = DialogPositions.FindElementByAccessibilityId("5511");
            var rows = bigTable.FindElementsByXPath("//child::*");
            //var firstRow1 = bigTable.FindElementByXPath("//*[1]/following-sibling::*[1]");
            foreach (var child in rows)
            {
                logger.Info(child.Text);
            }
            rows[8].Click();
            double dActualValue = Convert.ToDouble(rows[8].Text);

            //Close dialog
            DialogPositions.FindElementByName("OK").Click();
            Thread.Sleep(1000);
            var DialogNote2 = Driver.FindElementByXPath("//Window[@Name=\"Note\"][@ClassName=\"#32770\"]");
            Thread.Sleep(1000);
            DialogNote2.FindElementByName("Close").Click();
            Thread.Sleep(1000);

            //Read expected value in csv file
            Collection<string[]> cstrAllData = CSVTool.ReadCSVFile(strCSVFile, ',');
            double dFirstValue = Convert.ToDouble(cstrAllData[0][0]);

            //Compare the 2 values
            Assert.AreEqual(dActualValue, dFirstValue);

            SaveAs(strActualFolder + "cube_actual.op2");
        }
    }

}
