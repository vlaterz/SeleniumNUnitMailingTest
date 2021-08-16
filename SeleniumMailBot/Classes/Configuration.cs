using System;
using System.Xml.Serialization;

namespace SeleniumMailBot.Classes
{
    /// <summary>
    /// Сериализуемый класс конфигурации
    /// </summary>
    [Serializable]
    public class Configuration
    {
        [XmlElement("LoggingEnabled")]
        public bool LoggingEnabled;

        [XmlElement("MailService")]
        public string MailService;

        [XmlElement("Login")]
        public string Login;

        [XmlElement("Password")]
        public string Password;

        [XmlElement("TestsToRun")]
        public string[] TestsToRun;
    }
}
