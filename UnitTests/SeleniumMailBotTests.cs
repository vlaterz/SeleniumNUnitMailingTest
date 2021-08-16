using System;
using System.Reflection;
using NUnit.Framework;
using NUnitLite;
using SeleniumMailBot.Classes;

namespace UnitTests
{
    [TestFixture]
    public class SeleniumMailBotTests
    {
        public static Configuration Config;

        public static int Main(string[] args)
        {
            Config = TestsHelper.ApplyParametersAndGetConfig(args);
            var testArgs = TestsHelper.GetTestArgsFromConfig(Config);
            var res = new AutoRun(Assembly.GetExecutingAssembly()).Execute(testArgs);
            Console.ReadKey();
            return res;
        }
    }
}
