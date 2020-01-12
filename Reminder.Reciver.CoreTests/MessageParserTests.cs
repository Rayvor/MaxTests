using Hors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reminder.Reciver.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reminder.Reciver.Core.Tests
{
    [TestClass()]
    public class MessageParserTests
    {
        [TestMethod()]
        public void ParseMessageTest()
        {
            var hors = new HorsTextParser();
            var result = hors.Parse("Встреча 2 января", DateTime.Now);
            var result2 = hors.Parse("Встреча 3 апреля", DateTime.Now);
            Assert.AreEqual(new DateTime(2021, 01, 02), result.Dates[0].DateFrom);
            Assert.AreEqual(new DateTime(2020, 04, 03), result2.Dates[0].DateFrom);
        }
    }
}