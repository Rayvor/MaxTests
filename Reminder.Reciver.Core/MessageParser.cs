using System;
using System.Collections.Generic;
using System.Text;
using Hors;
using Reminder.Core.Models;
using Telegram.Bot.Types;

namespace Reminder.Reciver.Core
{
    public static class MessageParser
    {

        public static AddReminderItemModel ParseMessage(Message message)
        {
            var hors = new HorsTextParser();
            var result = hors.Parse(message.Text, DateTime.Now);
            if (result.Dates != null && result.Dates.Count > 0)
            {
                Console.WriteLine($"do: { result.Text}; when:{result.Dates[0].DateFrom.ToString("dd MMMM yyyy:HH-mm-ss")}");
                return new AddReminderItemModel() { contactId = message.Chat.Id, date = result.Dates[0].DateFrom, Message = result.Text };
            }

            Console.WriteLine($"do: { result.Text};");
            return new AddReminderItemModel() { contactId = message.Chat.Id, date = DateTimeOffset.Now, Message = result.Text };
        }

    }
}
