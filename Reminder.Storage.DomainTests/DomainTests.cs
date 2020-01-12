using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reminder.Reciever.TelegrammReciever;
using Reminder.Sender.TelegramSender;
using Reminder.Storage.Domain;
using ReminderStorage.InMemory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reminder.Storage.Domain.Tests
{
    [TestClass()]
    public class DomainTests
    {
        [TestMethod()]
        public void InitializeTest()
        {
            InMemoryStorage storage = new InMemoryStorage();
            TelegramSender sender = new TelegramSender("1028662742:AAFo3RAhaGwvh2zlxVEXGTFrOeufbnOZ9z4");
            TelegrammReciever reciever = new TelegrammReciever("1028662742:AAFo3RAhaGwvh2zlxVEXGTFrOeufbnOZ9z4");
            Domain d = new Domain(storage, sender, reciever, 50);
            d.Start();
            d.Stop();

        }
    }
}