using System;

namespace Reminder.Storage.Core
{
    public class ReminderItem
    {
        public Guid Id { get; set; }
        public DateTimeOffset date { get; set; }
        public string Message { get; set; }
        public long contactId { get; set; }
        public ReminderStatus _status { get; set; } = ReminderStatus.Awaiting;
        public bool  IStimeToAlarm => date<DateTime.UtcNow;
    }
}
