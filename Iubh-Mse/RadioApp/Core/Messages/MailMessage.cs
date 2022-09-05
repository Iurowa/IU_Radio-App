using System;
using MvvmCross.Plugin.Messenger;

namespace Iubh.RadioApp.Core.Messages
{
    public class MailMessage : MvxMessage
    {
        public string Address { get; private set; }

        public MailMessage(object sender, string address)
            : base(sender)
        {
            this.Address = address;
        }
    }
}
