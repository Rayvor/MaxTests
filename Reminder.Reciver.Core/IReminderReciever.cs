using System;
using System.Collections.Generic;
using System.Text;

namespace Reminder.Reciver.Core
{
    public interface IReminderReciever
    {
        event EventHandler<MessageRecievedEventArgs> OnMessagerecieved;

        void Run();

        void Stop();
    }
}
