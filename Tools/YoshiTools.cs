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
    class YoshiTools : JupiterTestBase
    {
        public static void LaunchFunction(string[] hierarchy, int num)
        {
            toolBar.FindElementByName(hierarchy[0]).Click();
            for (int i = 1; i < hierarchy.Length; i++)
            {
                jupiter.FindElementByName(hierarchy[i]).Click();
                Thread.Sleep(500);
            }

            action = new Actions(Driver);
            Assert.IsNotNull(action);
            for (int i = 0; i < num; i++)
            {
                action.SendKeys(Keys.Down);
            }
            action.SendKeys(Keys.Enter);
            action.Perform();
        }

        public static bool ImportResult(string pathFile, int pos, int wait = 500)
        {
            string[] path = { "Home", "Import Results" };
            LaunchFunction(path, pos);
            Thread.Sleep(500);

            action = new Actions(Driver);
            action.SendKeys(pathFile);
            action.SendKeys(Keys.Enter);
            action.Perform();

            Thread.Sleep(wait);

            return true;
        }

        public static void ResetView()
        {
            action = new Actions(Driver);
            action.MoveToElement(jupiter, 0, 0).MoveByOffset(900, 500).Click();
            action.ContextClick();
            action.SendKeys(Keys.Down);
            action.SendKeys(Keys.Down);
            action.SendKeys(Keys.Down);
            action.SendKeys(Keys.Down);
            action.SendKeys(Keys.Enter);
            action.Perform();
        }

        public static void DialogAppear(int wait = 500)
        {
            Thread.Sleep(wait);
            try
            {
                jupiter.FindElementByName("Yes").Click();
            }
            catch
            {
                logger.Warn("Confirm Dialog Not Appeared");
            }
        }

        public static void SaveAs(string path, int wait = 500)
        {
            Driver.FindElementByXPath("//Button[@Name='Application menu']").Click();
            Driver.FindElementByName("Save As...").Click();

            Thread.Sleep(wait);
            action = new Actions(Driver);
            action.SendKeys(path);
            action.SendKeys(Keys.Enter);
            action.Perform();

            DialogAppear();
        }
    }
}
