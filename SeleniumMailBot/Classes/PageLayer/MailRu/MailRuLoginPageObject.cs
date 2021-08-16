using NLog;
using OpenQA.Selenium;
using SeleniumMailBot.Interfaces.Page;

namespace SeleniumMailBot.Classes.PageLayer.MailRu
{
    /// <summary>
    /// Класс взаимодействия со страницей логина сайта Mail.ru
    /// </summary>
    public class MailRuLoginPageObject : ILoginPageObject
    {
        public string Url => "https://mail.ru/";
        private readonly Configuration _configuration;
        private readonly IWebDriver _driver;

        private readonly WebElement _loginField;
        private readonly WebElement _passwordField;
        private readonly WebElement _enterPasswordBtn;
        private readonly WebElement _confirmButton;

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public MailRuLoginPageObject(IWebDriver webDriver, Configuration config)
        {
            _driver = webDriver;
            _configuration = config;

            _loginField = new WebElement(_driver, By.XPath("//input[@name='login']"));
            _passwordField = new WebElement(_driver, By.XPath("//input[@name='password']"));
            _enterPasswordBtn = new WebElement(_driver, By.XPath("//button[@data-testid='enter-password']"));
            _confirmButton = new WebElement(_driver, By.XPath("//button[@data-testid='login-to-mail']"));
        }

        /// <summary>
        /// Метод входа в акк
        /// </summary>
        /// <returns></returns>
        public IMailPageObject Login()
        {
            Logger.Info("Trying to login into mail.ru account..");

            Logger.Debug("Entering login into field..");
            if (_loginField.TrySendKeysWithWait(_configuration.Login).Exception != null) return null;

            Logger.Debug("Clicking enter password button..");
            if (_enterPasswordBtn.TryClickWithWait().Exception != null) return null;

            Logger.Debug("Looking for password field..");
            if (_passwordField.WaitForBeingVisible().Exception != null) return null;

            Logger.Debug("Entering password into Password field..");
            if (_passwordField.TrySendKeysWithWait(_configuration.Password).Exception != null) return null;

            Logger.Debug("Clicking login into account..");
            if (_confirmButton.TryClickWithWait().Exception != null) return null;

            return new MailRuMailingPageObject(_driver, _configuration); 
        }
    }
}