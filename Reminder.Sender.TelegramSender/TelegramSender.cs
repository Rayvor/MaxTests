using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace Reminder.Sender.TelegramSender
{
    
    public class TelegramSender : Core.IReminderSender
    {
        private TelegramBotClient botClient;


        public TelegramSender(string key)
        {
            botClient = new TelegramBotClient(key);
        }

        public async Task Send(long contactID, string message)
        {
            await botClient.SendTextMessageAsync(chatId: contactID, text: message);
        }
    }
}
