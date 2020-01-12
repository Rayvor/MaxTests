using System;
using System.Collections.Generic;
using System.Text;

namespace Reminder.Core.Models
{
    public class SendReminderItemModel
    {
        public Guid id { get; set; }
        public long contactId { get; set; }
        public string Message { get; set; }
    }
}
