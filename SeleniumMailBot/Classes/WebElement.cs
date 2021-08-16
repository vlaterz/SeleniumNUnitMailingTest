using System;
using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Exception = System.Exception;

namespace SeleniumMailBot.Classes
{
    internal class WebElement
    {
        public readonly By Path;
        private readonly WebDriverWait _wait;
        private readonly IWebDriver _driver;
        public Exception Exception { get; private set; }
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public WebElement(IWebDriver webDriver, By path)
        {
            Exception = null;
            Path = path;
            _driver = webDriver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
        }
        
        /// <summary>
        /// Метод вписывания текста в элемент (с ожиданием)
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public WebElement TrySendKeysWithWait(string text)
        {
            Execute(() => 
                _wait.Until(ExpectedConditions
                    .ElementIsVisible(Path))
                    .SendKeys(text)
            );
            return this;
        }

        /// <summary>
        /// Метод нажатия на элемент (с ожиданием)
        /// </summary>
        /// <returns></returns>
        public WebElement TryClickWithWait()
        {
            Execute(() =>
                _wait.Until(ExpectedConditions
                        .ElementIsVisible(Path))
                        .Click()
            );
            return this;
        }

        /// <summary>
        /// Метод ожидания появления элемента
        /// </summary>
        /// <returns></returns>
        public WebElement WaitForBeingVisible()
        {
            Execute(() =>
                _wait.Until(ExpectedConditions
                        .ElementIsVisible(Path))
            );
            return this;
        }

        /// <summary>
        /// Метод получения текста из элемента (с ожиданием)
        /// </summary>
        /// <returns></returns>
        public string TryGetText()
        {
            try
            {
                return _wait
                    .Until(ExpectedConditions.ElementIsVisible(Path))
                    .Text;
            }
            catch(Exception e)
            {
                Logger.Error(e);
                return null;
            }
        }

        public bool IsVisible()
        {
            try
            {
                _driver.FindElement(Path);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Метод выполнения действий с элементом
        /// </summary>
        /// <param name="foo"></param>
        private void Execute(Action foo)
        {
            try
            {
                foo();
            }
            catch (Exception e)
            {
                Logger.Error(e);
                Exception = e;
            }
        }
    }
}
