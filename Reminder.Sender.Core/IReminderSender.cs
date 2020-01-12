using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Reminder.Sender.Core
{
    public interface IReminderSender
    {
        Task Send(long contactID, string message);
    }
}
