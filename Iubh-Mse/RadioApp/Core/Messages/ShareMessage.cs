using System;
using MvvmCross.Plugin.Messenger;

namespace Iubh.RadioApp.Core.Messages
{
    public class ShareMessage : MvxMessage
    {
        public string Text { get; private set; }

        public ShareMessage(object sender, string text)
            : base(sender)
        {
            this.Text = text;
        }
    }
}
