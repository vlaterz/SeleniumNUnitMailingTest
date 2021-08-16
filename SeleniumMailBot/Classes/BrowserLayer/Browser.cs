using System;
using NLog;
using OpenQA.Selenium;
using SeleniumMailBot.Classes.PageLayer.MailRu;
using SeleniumMailBot.Interfaces.Page;

namespace SeleniumMailBot.Classes.BrowserLayer
{
    /// <summary>
    /// Класс браузера
    /// </summary>
    /// <typeparam name="T">Страница логина почты</typeparam>
    public class Browser<T> where T : ILoginPageObject
    {
        private readonly IWebDriver _driver;
        private readonly ILoginPageObject _mailService;

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public Browser(IWebDriver driver, Configuration configs)
        {
            _driver = driver;

            switch (typeof(T))
            {
                case Type t when t == typeof(MailRuLoginPageObject):
                    _mailService = new MailRuLoginPageObject(_driver, configs);
                    break;
                //Add new MailingServices here
            }
           
        }

        /// <summary>
        /// Метод открытия страницы ввода логина и пароля
        /// </summary>
        /// <returns></returns>
        public ILoginPageObject OpenMailingLoginPage()
        {
            Logger.Info($"Navigated to {_mailService.Url}");
            _driver.Navigate().GoToUrl(_mailService.Url);
            return _mailService;
        }
    }
}
