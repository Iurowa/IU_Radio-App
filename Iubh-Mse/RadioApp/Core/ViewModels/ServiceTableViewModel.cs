using System;
using Acr.UserDialogs;
using Iubh.RadioApp.Core.Messages;
using Iubh.RadioApp.Core.Options;
using Microsoft.AppCenter.Analytics;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using MvvmCross.ViewModels;

namespace Iubh.RadioApp.Core.ViewModels
{
    public class ServiceTableViewModel : MvxViewModel, IServiceTableViewModel
    {
        private readonly IMvxNavigationService navigationService;

        private IMvxMessenger messenger;

        public MvxCommand NavigateToDetailCommand { get; private set; }

        public ServiceOption Type { get; set; }

        public string Title
        {
            get;
            set;
        }

        public string Message { get; set; }

        public string Url { get; set; }

        public Type Model { get; set; }

        public string MailAddress { get; set; }

        public ServiceTableViewModel()
        {
            this.navigationService = Mvx.IoCProvider.Resolve<IMvxNavigationService>();
            this.NavigateToDetailCommand = new MvxCommand(this.NavigateToDetail);
            this.messenger = Mvx.IoCProvider.Resolve<IMvxMessenger>();
        }


        protected void NavigateToDetail()
        {
            switch (this.Type)
            {
                case ServiceOption.Alert:
                    UserDialogs.Instance.Alert(new AlertConfig { Message = this.Message, Title = "Info", OkText = "Ok" });
                    break;
                case ServiceOption.Onboarding:
                    this.navigationService.Navigate(typeof(OnboardingViewModel), true );
                    break;
                case ServiceOption.View:
                    this.navigationService.Navigate(this.Model);
                    break;
                case ServiceOption.Mail:
                    this.messenger.Publish(new MailMessage(this, this.MailAddress));
                    break;
                case ServiceOption.Share:
                    this.messenger.Publish(new ShareMessage(this, this.Message));
                    break;
                case ServiceOption.External:
                    this.messenger.Publish(new LinkMessage(this, this.Url));
                    break;
                default:
                    break;
            }

        }
    }
}
