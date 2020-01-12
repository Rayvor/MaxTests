using System;
using System.Linq;
using Reminder.Storage.Core;
using Reminder.Core.Models;
using Reminder.Core.EventArgs;
using System.Threading;
using System.Threading.Tasks;
using Reminder.Reciver.Core;
using Reminder.Sender.Core;

namespace Reminder.Storage.Domain
{
    public class Domain
    {
        private IReminderStorage storage;
        private IReminderReciever reciever;
        private IReminderSender sender;
        private int timeToUpdate;
        public event EventHandler<AddReminderItemModel> OnSuccesAdd;
        public event EventHandler<SendSuccesEventArgs> OnSuccesSend;
        public event EventHandler<SendFailedEventArgs> OnFailedSend;
        private Task RunTask;
        private CancellationTokenSource cts = new CancellationTokenSource();

        /// <summary>
        /// Создание домен контроллера
        /// </summary>
        /// <param name="_storage">Хранилище напоминаний</param>
        public Domain(IReminderStorage _storage, IReminderSender sender, IReminderReciever reciever, int timetoUpdate=500) {
            storage = _storage;
            this.reciever = reciever;
            this.sender = sender;
            this.timeToUpdate = timetoUpdate;
            this.reciever.OnMessagerecieved += Reciever_OnMessagerecieved;
            this.reciever.Run();            
        }

        private void Reciever_OnMessagerecieved(object sender, MessageRecievedEventArgs e)
        {
            try
            {
                Add(e.addModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public void Start()
        {
            var t = new Task(Run, cts.Token);
            t.Start();
        }

        private void Run()
        {
            while (!cts.IsCancellationRequested)
            {
                CheckAwaitingReminders();
                SendReadyRemiders();
                Thread.Sleep(timeToUpdate);
            }
        }

        public void Add(AddReminderItemModel model)
        {
            ReminderItem item = new ReminderItem()
            {
                Id = Guid.NewGuid(),
                date = model.date,
                Message = model.Message,
                contactId = model.contactId,
                _status = ReminderStatus.Awaiting
            };
            storage.Add(item.Id, item);
            OnSuccesAdd?.Invoke(this, model);
        }


        public void CheckAwaitingReminders()
        {
            var ids = storage.Get(ReminderStatus.Awaiting, 0, 0).Where(r => r.IStimeToAlarm).Select(r=>r.Id);
            storage.UpdateStatus(ids, ReminderStatus.ReadyToSend);
        }

        public async Task SendReadyRemiders()
        {
            var reminders = storage.Get(ReminderStatus.ReadyToSend, 0, 0).Where(r => r.IStimeToAlarm).ToList();
            foreach(ReminderItem r_item in reminders)
            {
                SendReminderItemModel sendModel = new SendReminderItemModel()
                {
                    id = r_item.Id,
                    contactId = r_item.contactId,
                    Message = r_item.Message
                };

                try
                {
                    storage.UpdateStatus(sendModel.id, ReminderStatus.Sended);
                    await sender.Send(sendModel.contactId, sendModel.Message);
                    OnSuccesSend?.Invoke(this, new SendSuccesEventArgs(r_item));
                }
                catch (Exception e)
                {
                    storage.UpdateStatus(sendModel.id, ReminderStatus.Failed);
                    OnFailedSend?.Invoke(this, new SendFailedEventArgs(e, r_item));
                }
            }
        }

        public void Display()
        {
            foreach(ReminderItem item in storage.Get(0, 0))
            {
                Console.WriteLine($"ID:{item.Id};Contact:{item.contactId};Message:{item.Message}");
            }
        }

        public void Stop()
        {
            reciever.Stop();
            cts.Cancel();
        }
    }
}
