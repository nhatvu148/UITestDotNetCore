using System;
using System.Collections.Generic;
// using System.Configuration;
using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLog;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace UITest1
{
    public class JupiterTestBase
    {
        protected static WindowsDriver<WindowsElement> Driver;
        protected static TestContext objTestContext;
        protected static Actions action;
        protected static WebDriverWait wait;
        protected static WindowsElement allParts;
        protected static WindowsElement toolBar;
        protected static WindowsElement jupiter;
        protected string pathDirectory = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\..\\..\\..\\..";
        protected static Logger logger = LogManager.GetCurrentClassLogger();

        protected static Dictionary<string, string> appSettings = new Dictionary<string, string>() {
            { "DCAD_Path", "C:\\Program Files\\TechnoStar\\Jupiter-Pre_4.1.3\\DCAD_main.exe" } ,
            { "PCAD_Path", "C:\\Program Files\\TechnoStar\\Jupiter-Post_4.1.3\\PCAD_main.exe" },
            { "HIBIYA2.DCAD_Path", "D:\\jenkins\\jobs\\JPT-Pre\\DCAD\\bin\\Release\\x64\\DCAD_main.exe" },
            { "HIBIYA2.PCAD_Path", "D:\\jenkins\\jobs\\JPT-Post\\bin\\Release\\x64\\PCAD_main.exe" },
         };

        public static void Setup(string path)
        {
            AppiumOptions appOptions = new AppiumOptions();
            Assert.IsNotNull(appOptions);
            appOptions.AddAdditionalCapability("app", path);
            logger.Info("Opened Jupiter");

            Driver = new WindowsDriver<WindowsElement>(
                new Uri("http://127.0.0.1:4723"),
                appOptions,
                TimeSpan.FromMinutes(5)
                );
            Assert.IsNotNull(Driver);
            Assert.IsNotNull(Driver.SessionId);
        }

        public static void TearDown()
        {
            if (Driver != null)
            {
                Driver.Quit();
                Driver = null;
                logger.Info("Closed Jupiter");
            }
        }

        public static string GetConfiguration(string key)
        {
            // return ConfigurationManager.AppSettings[Environment.MachineName + "." + key] ?? ConfigurationManager.AppSettings[key];
            return appSettings.ContainsKey(Environment.MachineName + "." + key) ? appSettings[Environment.MachineName + "." + key] : appSettings[key];
        }
    }
}
