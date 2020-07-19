using OpenQA.Selenium;
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
            Thread.Sleep((int)(time_wait_for_import_s*1000));
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

            Collection<string[]> cstrAllData = CSVTool.ReadCSVFile(strCSVFile, ',');
            double dFirstValue = Convert.ToDouble(cstrAllData[0][0]);

            SaveAs(strActualFolder + "cube_actual.op2");
        }
    }

}
