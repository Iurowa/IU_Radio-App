using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Acr.UserDialogs;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Iubh.RadioApp.Core.ViewModels
{
    public class RateTeaserViewModel : MvxViewModel
    {
        public MvxCommand NavigateToDetailCommand { get; private set; }

        private readonly IMvxNavigationService navigationService;

        public string Key { get; set; }

        public string Rate { get; set; }

        public string Text { get; set; }

        public RateTeaserViewModel()
        {
            this.navigationService = Mvx.IoCProvider.Resolve<IMvxNavigationService>();
            this.NavigateToDetailCommand = new MvxCommand(this.NavigateToDetail);
        }

        protected void NavigateToDetail()
        {
            this.navigationService.Navigate(typeof(RateDetailViewModel), this.Key);
        }

    }
}
