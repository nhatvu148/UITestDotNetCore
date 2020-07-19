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
    class PostTestBug9220 : JupiterTestBase
    {
        static void ImportUnv(string pathDirectory)
        {
            YoshiTools.ImportResult($"{pathDirectory}\\TestResults\\JPT-Post\\TestBug11153\\PIN_ONLY.unv", 12);

            var dialogOpen = jupiter.FindElementByName("Open");
            dialogOpen.FindElementByAccessibilityId("1").Click();
            Thread.Sleep(12000);
        }

        public static void TestBug9220(string pathDirectory)
        {
            ImportUnv(pathDirectory);

        }
    }
}


