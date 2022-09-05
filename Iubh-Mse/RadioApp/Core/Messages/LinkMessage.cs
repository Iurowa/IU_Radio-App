using System;
using MvvmCross.Plugin.Messenger;

namespace Iubh.RadioApp.Core.Messages
{
    public class LinkMessage : MvxMessage
    {
        public string Link { get; private set; }

        public LinkMessage(object sender, string link)
            : base(sender)
        {
            this.Link = link;
        }
    }
}
