using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Iubh.RadioApp.Core.Messages;
using Iubh.RadioApp.Core.Services;
using MvvmCross.Platforms.Android;
using MvvmCross.Plugin.Messenger;

namespace Iubh.RadioApp.Droid.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMvxAndroidCurrentTopActivity topActivity;

        private MvxSubscriptionToken shareMessageToken;

        private MvxSubscriptionToken mailMessageToken;

        private MvxSubscriptionToken linkMessageToken;

        protected IMvxMessenger Messenger { get; private set; }

        public MessageService(IMvxMessenger messenger, IMvxAndroidCurrentTopActivity topActivity)
        {
            this.Messenger = messenger;
            this.topActivity = topActivity;
        }

        public void Start()
        {
            this.shareMessageToken = this.Messenger.SubscribeOnMainThread<ShareMessage>(this.OnShareMessage);
            this.mailMessageToken = this.Messenger.SubscribeOnMainThread<MailMessage>(this.OnMailMessage);
            this.linkMessageToken = this.Messenger.SubscribeOnMainThread<LinkMessage>(this.OnLinkMessage);
        }

        ~MessageService()
        {
            this.Messenger.Unsubscribe<ShareMessage>(this.shareMessageToken);
            this.Messenger.Unsubscribe<MailMessage>(this.mailMessageToken);
            this.Messenger.Unsubscribe<LinkMessage>(this.linkMessageToken);
        }

        protected void OnShareMessage(ShareMessage message)
        {
            Intent sendIntent = new Intent(Intent.ActionSend);
            sendIntent.PutExtra(Intent.ExtraText, message.Text);
            sendIntent.SetType("text/plain");

            Action showShareActivity = () => this.topActivity.Activity.StartActivity(Intent.CreateChooser(sendIntent, "Teilen ..."));
            showShareActivity();
        }

        protected void OnMailMessage(MailMessage message)
        {
            Intent sendIntent = new Intent(Intent.ActionSend, Android.Net.Uri.Parse($"mailto:{message.Address}"));
            sendIntent.PutExtra(Intent.ExtraEmail, new String[] { message.Address });
            sendIntent.SetType("text/plain");
            Action showShareActivity = () => this.topActivity.Activity.StartActivity(Intent.CreateChooser(sendIntent, "E-Mail App auswählen ..."));
            showShareActivity();
        }

        protected void OnLinkMessage(LinkMessage message)
        {
            Intent sendIntent = new Intent(Intent.ActionView, Android.Net.Uri.Parse(message.Link));
            Action showShareActivity = () => this.topActivity.Activity.StartActivity(sendIntent);
            showShareActivity();
        }
    }
}