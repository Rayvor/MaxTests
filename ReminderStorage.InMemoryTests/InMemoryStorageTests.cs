using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reminder.Storage.Core;
using ReminderStorage.InMemory;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReminderStorage.InMemory.Tests
{
    [TestClass()]
    public class InMemoryStorageTests
    {
        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void GetReminderItemException()
        {
            var memoryStorage = new InMemoryStorage();
            var item = new ReminderItem();
            var id = Guid.NewGuid();
            memoryStorage.Add(id, item);

            var getItem = memoryStorage.Get(Guid.NewGuid());
        }

        public void InitializeTest()
        {
            var memoryStorage = new InMemoryStorage();
            memoryStorage.Clear();
            Assert.AreEqual(memoryStorage.Count(), 0);
        }

        public void AddTest()
        {
            var memoryStorage = new InMemoryStorage();
            var item = new ReminderItem();
            var id = Guid.NewGuid();
            memoryStorage.Add(id, item);

            Assert.AreEqual(memoryStorage.Count(), 1);
        }

        public void RemoveTest()
        {
            var memoryStorage = new InMemoryStorage();
            var item = new ReminderItem();
            var id = Guid.NewGuid();
            memoryStorage.Add(id, item);

            Assert.AreEqual(memoryStorage.Count(), 1);

            memoryStorage.Remove(id);

            Assert.AreEqual(memoryStorage.Count(), 0);
        }

        public void ClearTest()
        {
            var memoryStorage = new InMemoryStorage();
            var item = new ReminderItem();
            var id = Guid.NewGuid();
            memoryStorage.Add(id, item);
            var id2 = Guid.NewGuid();
            var item2 = new ReminderItem();
            memoryStorage.Add(id2, item2);

            Assert.AreEqual(memoryStorage.Count(), 2);

            memoryStorage.Clear();

            Assert.AreEqual(memoryStorage.Count(), 0);
        }
    }
}