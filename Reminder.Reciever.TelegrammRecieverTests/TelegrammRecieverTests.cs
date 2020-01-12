using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reminder.Reciever.TelegrammReciever;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reminder.Reciever.TelegrammReciever.Tests
{
    [TestClass()]
    public class TelegrammRecieverTests
    {
        [TestMethod()]
        [ExpectedException(typeof(NullReferenceException))]
        public void TelegrammRecieverInstance()
        {
            var telegramReviever = new TelegrammReciever(null);
        }

        public void TelegrammRecieverBotInitialize()
        {
            var telegramReviever = new TelegrammReciever("1028662742:AAFo3RAhaGwvh2zlxVEXGTFrOeufbnOZ9z4");
            telegramReviever.Run();

        }
    }
}