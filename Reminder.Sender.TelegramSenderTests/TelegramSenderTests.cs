using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reminder.Sender.TelegramSender;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Reminder.Sender.TelegramSender.Tests
{
    [TestClass()]
    public class TelegramSenderTests
    {
        [TestMethod()]
        public async Task SendTest()
        {
            var telegramSender = new TelegramSender("1028662742:AAFo3RAhaGwvh2zlxVEXGTFrOeufbnOZ9z4");
            await telegramSender.Send(778740583, "Hi!");
        }
    }
}