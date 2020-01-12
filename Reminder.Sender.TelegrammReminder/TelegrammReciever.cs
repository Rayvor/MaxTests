using System;
using System.Collections.Generic;
using System.Text;
using Reminder.Reciver.Core;
using Reminder.Core.Models;
using System.Threading;

namespace Reminder.Reciever.TelegrammReciever
{
    public class TelegrammReciever : Reminder.Reciver.Core.IReminderReciever
    {
        public event EventHandler<MessageRecievedEventArgs> OnMessagerecieved;
        public event EventHandler<MessageFailedRecievedEventArgs> OnFailedMessageRecieved;

        private Telegram.Bot.TelegramBotClient botClient;

        public TelegrammReciever(string key)
        {
            if (key == null)
                throw new NullReferenceException();

            botClient = new Telegram.Bot.TelegramBotClient(key);
        }

        public void Run()
        {
            botClient.OnMessage += BotClient_OnMessage;
            botClient.StartReceiving();
        }

        private void BotClient_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            Console.WriteLine(e.Message.Text);
            try
            {
                AddReminderItemModel addModel = MessageParser.ParseMessage(e.Message);
                OnMessagerecieved?.Invoke(this, new MessageRecievedEventArgs(addModel));
            }
            catch (Exception ex)
            {
                OnFailedMessageRecieved?.Invoke(this, new MessageFailedRecievedEventArgs("Ошибка при получении сообщения", ex));
            }
        }

        public void Stop()
        {
            botClient.StopReceiving();
        }
    }
}
