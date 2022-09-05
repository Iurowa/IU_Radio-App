using System;
using Iubh.RadioApp.Core.Messages;
using MvvmCross.Plugin.Messenger;

namespace Iubh.RadioApp.Core.Services
{
    public abstract class BaseShareService : IShareService
    {
        private MvxSubscriptionToken shareMessageToken;

        protected IMvxMessenger Messenger { get; private set; }

        protected abstract void OnShareMessage(ShareMessage message);
        

        public BaseShareService(IMvxMessenger messenger)
        {
            this.Messenger = messenger;
        }



        public void Start ()
        {
            this.shareMessageToken = this.Messenger.SubscribeOnMainThread<ShareMessage>(this.OnShareMessage);
        }

        ~BaseShareService()
        {
            this.Messenger.Unsubscribe<ShareMessage>(this.shareMessageToken);
        }
    }
}
