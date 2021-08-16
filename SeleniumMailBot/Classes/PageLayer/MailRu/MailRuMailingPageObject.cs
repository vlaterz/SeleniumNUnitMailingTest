using NLog;
using OpenQA.Selenium;
using SeleniumMailBot.Interfaces.Page;

namespace SeleniumMailBot.Classes.PageLayer.MailRu
{
    /// <summary>
    /// Класс взаимодействия с почтой сайта Mail.ru
    /// </summary>
    public class MailRuMailingPageObject : IMailPageObject
    {
        public string Url => "https://e.mail.ru/inbox";
        private readonly Configuration _configuration;
        private readonly IWebDriver _driver;

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly WebElement _writeLetterBtn;
        private readonly WebElement _mailInputField;
        private readonly WebElement _mailSendBtn;
        private readonly WebElement _mailSendConfirmBtn;
        private readonly WebElement _dropdown;
        private readonly WebElement _logoutBtn;
        private readonly WebElement _letterTextInput;
        private readonly WebElement _inboxLettersBtn;
        private readonly WebElement _letterListElements;
        private readonly WebElement _letterElement;
        private readonly WebElement _letterTextStringsParent;

        public MailRuMailingPageObject(IWebDriver webDriver, Configuration config)
        {
            _driver = webDriver;
            _configuration = config;

            _writeLetterBtn = new WebElement(_driver, By.XPath("//a[@title='Написать письмо']"));
            _mailInputField = new WebElement(_driver, By.XPath("//div[@class='inputContainer--nsqFu']/input"));
            _mailSendBtn = new WebElement(_driver, By.XPath("//span[@title='Отправить']"));
            _mailSendConfirmBtn = new WebElement(_driver, By.XPath("//span[text()='Отправить']/parent::button"));
            _dropdown = new WebElement(_driver, By.XPath("//div[@data-testid='whiteline-account']"));
            _logoutBtn = new WebElement(_driver, By.XPath("//div[text()='Выйти']"));
            _letterTextInput = new WebElement(_driver, By.XPath("//div[@role='textbox']"));
            _inboxLettersBtn = new WebElement(_driver, By.XPath("//a[contains(@title,'Входящие')]"));
            _letterListElements = new WebElement(_driver, By.XPath("//div[@class='dataset__items']"));
            _letterElement = new WebElement(_driver, By.XPath("//a[@class='llc js-tooltip-direction_letter-bottom js-letter-list-item llc_normal']"));
            _letterTextStringsParent = new WebElement(_driver, By.XPath("//div[@class='letter-body']//div[contains(@class,'cl')]"));
        }

        /// <summary>
        /// Метод отправки сообщения
        /// </summary>
        /// <param name="targetMailAdress"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public IMailPageObject SendMessage(string targetMailAdress, string text)
        {
            Logger.Info($"Trying to send message {text} to {targetMailAdress}..");

            Logger.Debug("Waiting for 'Write Letter' button to become visible..");
            if (_writeLetterBtn.WaitForBeingVisible().Exception != null)
            {
                Logger.Error("Write Letter Button is not visible");
                return null;
            }

            Logger.Debug("Clicking on 'Write Letter' button..");
            if (_writeLetterBtn.TryClickWithWait().Exception != null) return this;

            Logger.Debug($"Typing {targetMailAdress} into target mail adress field..");
            if (_mailInputField.TrySendKeysWithWait(targetMailAdress).Exception != null) return this;

            Logger.Debug($"Typing {text} into letter message field..");
            if (_letterTextInput.TrySendKeysWithWait(text).Exception != null) return this;

            Logger.Debug("Clicking Send button..");
            if (_mailSendBtn.TryClickWithWait().Exception != null) return this;

            Logger.Debug("Checking if confirm button is visible..");
            if (_mailSendConfirmBtn.IsVisible())
            {
                Logger.Debug("Confirm button is visible.");
                Logger.Debug("Clicking confirm button..");
                _mailSendConfirmBtn.TryClickWithWait();
            }
            else
                Logger.Debug("Confirm button is not visible, finishing.");
            

            Logger.Info($"Message sent successfully.");
            return this;
        }

        /// <summary>
        /// Метод выхода из акка
        /// </summary>
        /// <returns></returns>
        public ILoginPageObject Logout()
        {
            Logger.Info($"Trying to log out from account..");

            Logger.Debug($"Trying to click on menu dropdown..");
            if (_dropdown.TryClickWithWait().Exception != null) return null;

            Logger.Debug($"Trying to click on Logout button..");
            if (_logoutBtn.TryClickWithWait().Exception != null) return null;

            Logger.Info($"Logged out successfully.");
            return new MailRuLoginPageObject(_driver, _configuration);
        }

        /// <summary>
        /// Метод получения письма
        /// </summary>
        /// <param name="index">Индекс получаемого письма (начиная сверху)</param>
        /// <returns></returns>
        public string GetMessageText(int index = 0)
        {
            Logger.Info($"Trying to get message by index ({index})..");

            Logger.Debug("Clicking Inbox button..");
            if (_inboxLettersBtn.TryClickWithWait().Exception != null) return null;

            Logger.Debug("Checking if letters list is visible..");
            if (!_letterListElements.IsVisible()) return null;

            Logger.Debug($"Getting letter by index ({index})");
            var letter = _driver.FindElements(_letterElement.Path)[index];
            letter.Click();

            return _letterTextStringsParent.TryGetText();
        }
    }
}