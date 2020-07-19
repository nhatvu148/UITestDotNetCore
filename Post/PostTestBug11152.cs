using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Support.UI;
using System.IO;
using System.Threading;
using System.Security.Permissions;

namespace UITest1
{
    class PostTestBug11152 : JupiterTestBase
    {
        static void ImportADVC(string pathDirectory)
        {
            YoshiTools.ImportResult($"{pathDirectory}\\TestResults\\JPT-Post\\TestBug11152\\bracket_result", 11, 1000);

            var dialogOpen = Driver.FindElementByName("Please select folder");
            dialogOpen.FindElementByName("Select Folder").Click();
            Thread.Sleep(1000);

        }

        static void Processing(string pathDirectory)
        {
            string[] path = { "Tools", "Note" };
            YoshiTools.LaunchFunction(path, 3);
            
            var dialogNote = jupiter.FindElementByName("Note");
            dialogNote.FindElementByAccessibilityId("5196").Clear();
            dialogNote.SendKeys("0.01");
            dialogNote.FindElementByAccessibilityId("2171").Click();

            var dialogPositions = Driver.FindElementByName("Positions");
            dialogPositions.FindElementByAccessibilityId("5512").Click();

            var dialogCSV = jupiter.FindElementByName("Open");
            dialogCSV.Clear();
            dialogCSV.SendKeys($"{pathDirectory}\\TestResults\\JPT-Post\\TestBug11152\\bracket_position.csv");
            dialogCSV.FindElementByAccessibilityId("1").Click();

            dialogPositions.FindElementByAccessibilityId("1").Click();
            dialogNote.FindElementByName("Close").Click();
            Thread.Sleep(1000);
        }

        static void Comparison(string pathDirectory)
        {
            //Export CSV Bracket_position for comparison
            string[] path = { "Home" };
            YoshiTools.LaunchFunction(path, 0);
            Thread.Sleep(1000);

            var WatchWindow = jupiter.FindElementByName("Watch");
            action = new Actions(Driver);
            action.MoveToElement(WatchWindow, 0, 0).MoveByOffset(16, 44).Click().Perform();
            Thread.Sleep(500);

            action = new Actions(Driver);
            action.SendKeys($"{pathDirectory}\\TestResults\\JPT-Post\\TestBug11152\\bracket_pos_actual.csv").Perform();
            Thread.Sleep(1000);
            jupiter.FindElementByName("Save").Click();
            YoshiTools.DialogAppear();

            //Output CSV comparison
            string BracketPosExpected = File.ReadAllText($"{pathDirectory}\\TestResults\\JPT-Post\\TestBug11152\\bracket_pos_expected.csv");
            string BracketPosActual = File.ReadAllText($"{pathDirectory}\\TestResults\\JPT-Post\\TestBug11152\\bracket_pos_actual.csv");

            try
            {
                Assert.AreEqual(BracketPosExpected, BracketPosActual);
            }
            catch
            {
                logger.Error("Assert.AreEqual failed - Output CSV \"bracket_pos\" Not Identical");
            }

        }

        public static void TestBug11152(string pathDirectory)
        {
            ImportADVC(pathDirectory);
            Processing(pathDirectory);
            Comparison(pathDirectory);
            YoshiTools.SaveAs($"{pathDirectory}\\TestResults\\JPT-Post\\TestBug11152\\bracket_result.tsdb");
            

        }


    }
}
