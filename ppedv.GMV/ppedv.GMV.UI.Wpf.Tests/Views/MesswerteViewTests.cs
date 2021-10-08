using FluentAssertions;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ppedv.GMV.UI.Wpf.Tests.Views
{
    public class MesswerteViewTests
    {

        private const string WpfAppId = @"C:\Users\Fred\source\repos\Tests_10_2021\ppedv.GMV\ppedv.GMV.UI.Wpf\bin\Debug\net6.0-windows\ppedv.GMV.UI.Wpf.exe";
        protected const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723";

        protected static WindowsDriver<WindowsElement> session;

        public MesswerteViewTests()
        {
            if (session == null)
            {
                var appiumOptions = new AppiumOptions();
                appiumOptions.AddAdditionalCapability("app", WpfAppId);
                appiumOptions.AddAdditionalCapability("deviceName", "WindowsPC");
                session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appiumOptions);

            }
        }

        [Fact]
        public void MesswerteView_Should_Say_hello_when_testbutton_is_clicked()
        {
            var btn = session.FindElementByAccessibilityId("testButton");
            btn.Click();
            var lbl = session.FindElementByAccessibilityId("testLabel");
            lbl.Text.Should().Be("Hello");
        }
    }
}
