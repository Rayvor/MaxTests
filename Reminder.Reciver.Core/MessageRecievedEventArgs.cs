using System;
using System.Collections.Generic;
using System.Text;
using Reminder.Core.Models;

namespace Reminder.Reciver.Core
{
    public class MessageRecievedEventArgs
    {
        public AddReminderItemModel addModel { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contactid">Идентификатор пользователя</param>
        /// <param name="message">Пришедшее сообщение</param>
        public MessageRecievedEventArgs(AddReminderItemModel reminder)
        {
            addModel = reminder;
        }
    }

    public class MessageFailedRecievedEventArgs
    {
        public string Message { get; set; }

        public Exception Exception { get; set; }

        public MessageFailedRecievedEventArgs(string message, Exception e)
        {
            Message = message;
            Exception = e;
        }
    }
}
