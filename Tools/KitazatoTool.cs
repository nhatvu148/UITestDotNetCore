using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
            Thread.Sleep((int)(time_wait_for_import_s * 1000));
        }

        public static AppiumWebElement OpenRibbon(string[] strRibbon)
        {
            int nCnt = 0;
            AppiumWebElement retDialog = jupiter.FindElementByName("Application menu");//to initialize;
            foreach (string strName in strRibbon)
            {
                if (nCnt++ == 0)
                {
                    retDialog = toolBar.FindElementByName(strName);
                }
                else
                {
                    retDialog = jupiter.FindElementByName(strName);
                }
                if (retDialog != null) { retDialog.Click(); }
                else { return null; }
                Thread.Sleep(1000);
            }
            return retDialog;
        }
    }

}
