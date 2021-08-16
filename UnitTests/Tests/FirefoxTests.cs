using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using SeleniumMailBot.Classes.BrowserLayer;
using SeleniumMailBot.Classes.PageLayer.MailRu;

namespace UnitTests.Tests
{
    [TestFixture]
    public class FirefoxTests
    {
        private static IWebDriver _driver;
        private static string _randomGuidMessage;

        [OneTimeSetUp]
        public void Init()
        {
            _randomGuidMessage = Guid.NewGuid().ToString();
        }

        [Test]
        [Order(2)]
        public void SendMessageTest()
        {
            try
            {
                _driver = new FirefoxDriver();
                var firefox = new Browser<MailRuLoginPageObject>(_driver, SeleniumMailBotTests.Config);
                firefox
                    .OpenMailingLoginPage()
                    .Login()
                    .SendMessage(SeleniumMailBotTests.Config.Login, _randomGuidMessage)
                    .Logout();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Assert.Fail();
            }
            finally
            {
                _driver?.Quit();
            }
        }

        [Test]
        [Order(3)]
        public void ReceiveMessageTest()
        {
            try
            {
                _driver = new FirefoxDriver();
                var firefox = new Browser<MailRuLoginPageObject>(_driver, SeleniumMailBotTests.Config);
                var mailingPage = firefox
                    .OpenMailingLoginPage()
                    .Login();
                var resultMessageText = mailingPage
                    .GetMessageText(0)
                    .Split(new[] { "\r\n" }, StringSplitOptions.None);
                mailingPage.Logout();
                Assert.AreEqual(_randomGuidMessage, resultMessageText[0], $"Got wrong message");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Assert.Fail();
            }
            finally
            {
                _driver?.Quit();
            }
        }
    }
}