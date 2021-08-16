using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;
using NLog;
using SeleniumMailBot.Classes;

namespace UnitTests
{
    /// <summary>
    /// Класс хелпер для валидации аргументов и подгрузки конфигурации
    /// </summary>
    static class TestsHelper
    {
        private static string GetPathFromArgs(string[] args) => 
            args.Length == 1
            ? args[0]
            : Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\\Configuration.xml";

        public static Configuration ApplyParametersAndGetConfig(string[] args)
        {
            ValidateParameters(args);
            string path = GetPathFromArgs(args);
            var config = GetTestsConfig(path);
            ConfigureLogging(config.LoggingEnabled);
            return config;
        }

        private static void ValidateParameters(string[] args)
        {
            if (args.Length > 1)
                throw new Exception($"Invalid number of params: {args.Length}. Expected: 0 or 1. The only parameter possible is path to configuration xml");

            if(args.Length == 0) return;

            if(!File.Exists(args[0]))
                throw new Exception($"Path to xml file: {args[0]} is invalid");
        }

        public static Configuration GetConfig => new Configuration()
        {
            Login = "-",
            MailService = "Mail.ru",
            Password = "-",
            LoggingEnabled = true,
            TestsToRun = new []
            {
                "UnitTests.SeleniumMailBotTests.SendMessageTest",
                "UnitTests.SeleniumMailBotTests.ReceiveMessageTest"
            },
        };

        public static void CreateConfig()
        {
            var fullPath = GetPathFromArgs(new String[]{});
            XmlSerializer serializer = new XmlSerializer(typeof(Configuration));
            StreamWriter writer = new StreamWriter(fullPath);
            serializer.Serialize(writer, GetConfig);
            writer.Close();
        }

        public static Configuration GetTestsConfig(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Configuration));
            StreamReader reader = new StreamReader(path);
            return (Configuration)serializer.Deserialize(reader);
        }

        private static void ConfigureLogging(bool isEnabled)
        {
            if (isEnabled)
                LogManager.EnableLogging();
            else
                LogManager.DisableLogging();
        }

        public static string[] GetTestArgsFromConfig(Configuration config)
        {
            var str = new StringBuilder();
            str.Append("--test=");
            foreach (var testName in config.TestsToRun)
                str.Append(testName).Append(",");
            return new[] {str.ToString()};
        }
    }
}
