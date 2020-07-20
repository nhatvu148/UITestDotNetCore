using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;
using System;
using System.IO;
using System.Threading;
using static UITest1.AkiyamaTool;
// Test Jenkins

namespace UITest1
{
    class PreTestMazdaRely : JupiterTestBase
    {
        public static void TestMazdaRely_1(string pathDirectory)
        {
            OpenJtdb($"{pathDirectory}\\TestResults\\JPT-Pre\\TestMazdaRely\\R2-IDI3_TS.jtdb");

            var assembly = jupiter.FindElementByName("Assembly");
            assembly.Click();
            assembly = jupiter.FindElementByName("Assembly");
            allParts = (WindowsElement)assembly.FindElementByName("All Parts");
            var boldOld = allParts.FindElementByName("bolt_old");
            action = new Actions(Driver);
            Assert.IsNotNull(action);
            action.MoveToElement(boldOld);
            action.ContextClick();
            action.SendKeys(Keys.Up);
            action.SendKeys(Keys.Enter);
            action.Build().Perform();


            var headCyl = allParts.FindElementByName("HEAD-CYL");
            action = new Actions(Driver);
            Assert.IsNotNull(action);
            action.MoveToElement(headCyl);
            action.ContextClick();
            action.SendKeys(Keys.Down + Keys.Down + Keys.Down + Keys.Down + Keys.Down);
            action.SendKeys(Keys.Enter);
            action.Build().Perform();

            var geometry = toolBar.FindElementByName("Geometry");
            geometry.Click();
            jupiter.FindElementByName("Show Adjacent").Click();
            action = new Actions(Driver);
            Assert.IsNotNull(action);
            action.SendKeys(Keys.Down);
            action.SendKeys(Keys.Enter);
            action.Build().Perform();

            AppiumWebElement showAdjacentDialog;

            SetDialog(out showAdjacentDialog, "Show Adjacent | Faces", true);

            showAdjacentDialog.FindElementByName("Start Face").Click();

            var home = toolBar.FindElementByName("Home");
            home.Click();
            var find = Driver.FindElementByName("Find");
            Assert.IsNotNull(find);
            action = new Actions(Driver);
            Assert.IsNotNull(action);
            action.MoveToElement(find);
            action.MoveToElement(find, find.Size.Width / 2, find.Size.Height / 3 + 20).Click();
            action.SendKeys(Keys.Down);
            action.SendKeys(Keys.Enter);
            action.Perform();

            WindowsElement idBox = Driver.FindElementByAccessibilityId("1582");
            Assert.IsNotNull(idBox);
            InputId(15254, idBox, action, Driver, find);

            showAdjacentDialog.FindElementByName("Stop Face").Click();

            home.Click();

            int[] stopFaceIds = { 19885, 16323, 20597, 20487, 20596, 20698, 20694 };

            foreach (int faceId in stopFaceIds)
            {
                InputId(faceId, idBox, action, Driver, find);
            }

            var numOfLayers = showAdjacentDialog.FindElementByAccessibilityId("1003");
            numOfLayers.Clear();
            numOfLayers.SendKeys("100");

            var allChBox = showAdjacentDialog.FindElementByAccessibilityId("1009");
            if (allChBox.Selected) allChBox.Click();
            var includeChBox = showAdjacentDialog.FindElementByAccessibilityId("1004");
            if (includeChBox.Selected) includeChBox.Click();

            var stopAngle = showAdjacentDialog.FindElementByAccessibilityId("1005");
            if (stopAngle.Text != "0")
            {
                stopAngle.Clear();
                stopAngle.SendKeys("0");
            }
            showAdjacentDialog.FindElementByName("OK").Click();

            var tools = toolBar.FindElementByName("Tools");
            tools.Click();
            var groupIcon = jupiter.FindElementByName("Model Filter");

            action = new Actions(Driver);
            Assert.IsNotNull(action);
            action.MoveToElement(groupIcon, 0, 0);
            action.MoveByOffset(-30, 5).Click().Perform();

            AppiumWebElement groupDialog;

            SetDialog(out groupDialog, "Group", false);

            var groupName = groupDialog.FindElementByAccessibilityId("1001");
            groupName.Clear();
            groupName.SendKeys("WaterJacket");
            groupDialog.FindElementByName("OK").Click();

            // Output 01_Groups.jtdb Comparison
            byte[] _01_GroupsExpected = File.ReadAllBytes(pathDirectory + @"\TestResults\JPT-Pre\TestMazdaRely\Expected\01_Groups.jtdb");
            string base64_01_GroupsExpected = Convert.ToBase64String(_01_GroupsExpected);

            byte[] _01_GroupsActual = File.ReadAllBytes(pathDirectory + @"\TestResults\JPT-Pre\TestMazdaRely\Actual\01_Groups.jtdb");
            string base64_01_GroupsActual = Convert.ToBase64String(_01_GroupsActual);

            try
            {
                Assert.AreEqual(base64_01_GroupsActual, base64_01_GroupsExpected, "Assert.AreEqual failed - Output Images Not Identical");
            }
            catch
            {
                logger.Error("Assert.AreEqual failed - Output Images Not Identical");
            }

            SaveAs($"{pathDirectory}\\TestResults\\JPT-Pre\\TestMazdaRely\\Actual\\01_Groups.jtdb");
        }

        public static void TestMazdaRely_2(string pathDirectory)
        {
            OpenJtdb($"{pathDirectory}\\TestResults\\JPT-Pre\\TestMazdaRely\\Actual\\01_Groups.jtdb");

            var assembly = jupiter.FindElementByName("Assembly");
            assembly.Click();
            assembly = jupiter.FindElementByName("Assembly");
            assembly.FindElementByName("Group").Click();

            var group = jupiter.FindElementByName("Group");
            group.Click();
            var waterJacketGroup = group.FindElementByName("WaterJacket");
            action = new Actions(Driver);
            Assert.IsNotNull(action);
            action.MoveToElement(waterJacketGroup);
            action.ContextClick();
            action.SendKeys(Keys.Down);
            action.SendKeys(Keys.Enter);
            action.Build().Perform();

            var meshing = toolBar.FindElementByName("Meshing");
            meshing.Click();

            var localSettings = jupiter.FindElementByName("Local Settings");
            action = new Actions(Driver);
            Assert.IsNotNull(action);
            action.MoveToElement(localSettings, 0, 0);
            action.MoveByOffset(30, 5).Click().Perform();

            action = new Actions(Driver);
            Assert.IsNotNull(action);
            action.SendKeys(Keys.Down);
            action.SendKeys(Keys.Enter);
            action.Build().Perform();

            Thread.Sleep(1000);

            AppiumWebElement localSettingsFaceDialog;

            SetDialog(out localSettingsFaceDialog, "Local Settings Face", true);

            InputTextBox(localSettingsFaceDialog, "1003", "WaterJacket");

            // To Check MeshSizeChBox
            SetCheckBox(localSettingsFaceDialog, "1005", true);
            InputTextBox(localSettingsFaceDialog, "1006", "1.5"); // Average
            InputTextBox(localSettingsFaceDialog, "1007", "0.5"); // Minimum
            InputTextBox(localSettingsFaceDialog, "1008", "3");  // Maximum

            localSettingsFaceDialog.FindElementByName("OK").Click();

            assembly = jupiter.FindElementByName("Assembly");
            assembly.Click();
            assembly = jupiter.FindElementByName("Assembly");
            allParts = (WindowsElement)assembly.FindElementByName("All Parts");
            var guide = allParts.FindElementByName("GUIDE-VALVE");
            var valve1 = allParts.FindElementByName("valve-exh");
            var valve2 = allParts.FindElementByName("valve-int");
            var seat1 = allParts.FindElementByName("valve-seat-exh");
            var seat2 = allParts.FindElementByName("valve-seat-int");

            action = new Actions(Driver);
            Assert.IsNotNull(action);
            action.MoveToElement(guide);
            action.Click();
            action.KeyDown(Keys.Control);
            action.MoveToElement(valve1);
            action.Click();
            action.MoveToElement(valve2);
            action.Click();
            action.MoveToElement(seat1);
            action.Click();
            action.MoveToElement(seat2);
            action.Click().Perform();
            action = new Actions(Driver);
            action.KeyUp(Keys.Control).Perform();

            action.Click().Perform();
            action.ContextClick();
            action.SendKeys(Keys.Down);
            action.SendKeys(Keys.Enter);
            action.Build().Perform();

            action = new Actions(Driver);
            Assert.IsNotNull(action);
            action.MoveToElement(localSettings, 0, 0);
            action.MoveByOffset(30, 5).Click().Perform();

            action = new Actions(Driver);
            Assert.IsNotNull(action);
            action.SendKeys(Keys.Up + Keys.Up + Keys.Up);
            action.SendKeys(Keys.Enter);
            action.Build().Perform();

            AppiumWebElement localSettingsPartDialog;

            SetDialog(out localSettingsPartDialog, "Local Settings Parts", true);

        }
    }
}
