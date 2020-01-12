using System;
using System.Threading;
using Reminder.Storage.Core;
using ReminderStorage.InMemory;
using Reminder.Core;
using Reminder.Storage.Domain;
using Reminder.Reciever.TelegrammReciever;
using Reminder.Sender.TelegramSender;
using Hors;
using System.Threading.Tasks;

namespace Reinder
{
    class Program
    {

        static void Main(string[] args)
        {
            InMemoryStorage storage = new InMemoryStorage();
            TelegramSender sender = new TelegramSender("1028662742:AAFo3RAhaGwvh2zlxVEXGTFrOeufbnOZ9z4");
            TelegrammReciever reciever = new TelegrammReciever("1028662742:AAFo3RAhaGwvh2zlxVEXGTFrOeufbnOZ9z4");
            Domain d = new Domain(storage, sender, reciever, 50);
            d.OnSuccesAdd += D_OnSuccesAdd;
            d.OnFailedSend += D_OnFailedSend;
            d.OnSuccesSend += D_OnSuccesSend;
            reciever.OnFailedMessageRecieved += Reciever_OnFailedMessageRecieved;

            d.Start();

            while (true)
            {
                string msg = Console.ReadLine();
                if (msg == "/stop")
                {
                    d.Stop();
                    Console.WriteLine("Прослушивание завершено");
                    break;
                }
                else
                {
                    switch (msg)
                    {
                        case "/awaiting":
                            var awaitingItems = storage.Get(ReminderStatus.Awaiting);
                            foreach (ReminderItem item in awaitingItems)
                                Console.WriteLine($"{item.Id};\ntimeToAlarm:\t{item.date.ToString("dd MMMM yyyy HH:mm:ss")};\nmessage:\t{item.Message}");
                            break;
                        default:
                            var id = Guid.NewGuid();
                            var newItem = new ReminderItem { Message = msg, date = DateTimeOffset.Now.AddSeconds(1), Id = id, _status = ReminderStatus.Awaiting, contactId = 778740583 };
                            storage.Add(id, newItem);
                            break;
                    }
                }
                
            }

            Console.ReadLine();
        }

        private static void Reciever_OnFailedMessageRecieved(object sender, Reminder.Reciver.Core.MessageFailedRecievedEventArgs e)
        {
            Console.WriteLine(e.Message, e.Exception.Message);
        }

        private static void D_OnSuccesAdd(object sender, Reminder.Core.Models.AddReminderItemModel e)
        {
            Console.WriteLine($"Reminder was succes add. Contact {e.contactId}");
        }

        private static void D_OnSuccesSend(object sender, Reminder.Core.EventArgs.SendSuccesEventArgs e)
        {
            Console.WriteLine($"Message to {e.item.contactId} wass succes send");
        }
        
        private static void D_OnFailedSend(object sender, Reminder.Core.EventArgs.SendFailedEventArgs e)
        {
            Console.WriteLine($"{e.exception.Message}, {e.reminderItem.Message} to {e.reminderItem.contactId}");
        }
    }
}
