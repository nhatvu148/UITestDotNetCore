using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;

namespace UITest1
{
    class AkiyamaTool : JupiterTestBase
    {
        public static void InputId(int faceId, WindowsElement idBox, Actions action,
                           WindowsDriver<WindowsElement> sessionJpt, WindowsElement find)
        {
            idBox.SendKeys(Keys.Control + "a" + Keys.Control);
            idBox.SendKeys(Convert.ToString(faceId));
            action = new Actions(sessionJpt);
            action.MoveToElement(find);
            action.MoveToElement(find, find.Size.Width / 2 - 30, find.Size.Height / 3 - 4).Click().Perform();
        }

        public static void InputId(string faceId, WindowsElement idBox, Actions action,
                            WindowsDriver<WindowsElement> sessionJpt, WindowsElement find)
        {
            idBox.SendKeys(Keys.Control + "a" + Keys.Control);
            idBox.SendKeys(faceId);
            action = new Actions(sessionJpt);
            action.MoveToElement(find);
            action.MoveToElement(find, find.Size.Width / 2 - 30, find.Size.Height / 3 - 4).Click().Perform();
        }

        public static void SaveAs(string path)
        {
            jupiter.FindElementByName("Application menu").Click();
            jupiter.FindElementByName("Save As...").Click();

            Thread.Sleep(1000);
            action = new Actions(Driver);
            action.SendKeys(path);
            action.SendKeys(Keys.Enter);
            action.Perform();

            Thread.Sleep(1000);
            try
            {
                jupiter.FindElementByName("Yes").Click();
            }
            catch
            {
                logger.Warn("Confirm Dialog Not Appeared");
            }
        }

        public static void OpenJtdb(string path)
        {
            jupiter.FindElementByName("Application menu").Click();
            action = new Actions(Driver);
            Assert.IsNotNull(action);
            action.SendKeys(Keys.Down + Keys.Down);
            action.SendKeys(Keys.Enter);
            action.Perform();

            Thread.Sleep(1000);

            action = new Actions(Driver);
            Assert.IsNotNull(action);
            var open = jupiter.FindElementByName("Open");
            open.FindElementByAccessibilityId("1148").Click();
            action.SendKeys(path);
            action.SendKeys(Keys.Enter);
            action.Perform();
        }

        public static void SetDialog(out AppiumWebElement dialog, string dialogName, bool isCheckSelect)
        {
            dialog = jupiter.FindElementByName(dialogName);

            if (isCheckSelect)
            {
                try
                {
                    dialog.FindElementByName(">").Click();
                }
                catch
                {
                    logger.Info("Selection List Already Opened");
                }
            }
        }

        public static void SetCheckBox(AppiumWebElement dialog, string checkBoxId, bool isToCheck)
        {
            var checkBox = dialog.FindElementByAccessibilityId(checkBoxId);
            if (isToCheck)
            {
                if (!checkBox.Selected) checkBox.Click();
            } else
            {
                if (checkBox.Selected) checkBox.Click();
            }
        }

        public static void InputTextBox(AppiumWebElement dialog, string textBoxId, string inputText)
        {
            var textBox = dialog.FindElementByAccessibilityId(textBoxId);

            textBox.Clear();
            textBox.SendKeys(inputText);
        }
    }
}
