using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Acr.UserDialogs;
using Iubh.RadioApp.Data.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Iubh.RadioApp.Core.ViewModels
{
    public class RateDetailViewModel : MvxViewModel<string>
    {
        public MvxCommand NavigateBackCommand { get; private set; }

        private readonly IMvxNavigationService navigationService;

        private string text;
        public string Text
        {
            get { return this.text; }
            set
            {
                this.text = value;
                this.RaisePropertyChanged(() => this.Text);
            }
        }

        private int rate;
        public int Rate
        {
            get { return this.rate; }
            set
            {
                this.rate = value;
                this.RaisePropertyChanged(() => this.Rate);
            }
        }

        public int AlertStyleId { get; set; }

        public MvxCommand NavigateLogoutCommand { get; private set; }

        public string HeaderTitle { get; set; }

        public bool ShowBack { get; set; }

        public bool ShowHeaderButton { get; set; }

        public string Key { get; set; }

        public RateDetailViewModel(IMvxNavigationService navigationService)
        {
            this.navigationService = navigationService;
            this.NavigateBackCommand = new MvxCommand(this.NavigateBack);

            this.HeaderTitle = "Bewertung";
            this.ShowHeaderButton = false;
            this.ShowBack = true;
        }

        protected void NavigateBack()
        {
            this.navigationService.Close(this, System.Threading.CancellationToken.None);
        }

        public override void Prepare()
        {
            base.Prepare();
        }

        public override void Prepare(string key)
        {
            this.Key = key;
            BaseRating rate = App.Db.GetPlaylistRate(key);
            if (rate == null)
            {
                rate = App.Db.GetModeratorRate(key);
                if (rate == null)
                    return;
            }
            this.Rate = rate.Rating;
            if (string.IsNullOrEmpty(rate.Text) == false)
            {
                this.Text = rate.Text;
            }
            else
            {
                this.Text = "-";
            }
        }
    }
}
