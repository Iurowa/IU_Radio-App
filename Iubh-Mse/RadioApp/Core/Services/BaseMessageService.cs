using System;
using Iubh.RadioApp.Core.Messages;
using MvvmCross.Plugin.Messenger;

namespace Iubh.RadioApp.Core.Services
{
    public abstract class BaseMessageService : IMessageService
    {
        private MvxSubscriptionToken shareMessageToken;

        private MvxSubscriptionToken mailMessageToken;

        protected IMvxMessenger Messenger { get;  set; }

        protected abstract void OnShareMessage(ShareMessage message);

        protected abstract void OnMailMessage(MailMessage message);


        public BaseMessageService(IMvxMessenger messenger)
        {
            this.Messenger = messenger;
        }


        public void Start ()
        {
            this.shareMessageToken = this.Messenger.SubscribeOnMainThread<ShareMessage>(this.OnShareMessage);
            this.mailMessageToken = this.Messenger.SubscribeOnMainThread<MailMessage>(this.OnMailMessage);
        }

        ~BaseMessageService()
        {
            this.Messenger.Unsubscribe<ShareMessage>(this.shareMessageToken);
            this.Messenger.Unsubscribe<MailMessage>(this.mailMessageToken);
        }
    }
}
