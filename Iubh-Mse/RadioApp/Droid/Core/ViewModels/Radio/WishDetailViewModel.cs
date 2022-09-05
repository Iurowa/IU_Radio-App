using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Iubh.RadioApp.Core.ViewModels
{
    public class WishDetailViewModel : MvxViewModel<string>
    {
        public MvxCommand NavigateBackCommand { get; private set; }

        private readonly IMvxNavigationService navigationService;

        private string name;
        public string Name
        {
            get { return this.name; }
            set
            {
                this.name = value;
                this.RaisePropertyChanged(() => this.Name);
            }
        }

        private string musicWish;
        public string MusicWish
        {
            get { return this.musicWish; }
            set
            {
                this.musicWish = value;
                this.RaisePropertyChanged(() => this.MusicWish);
            }
        }

        public int AlertStyleId { get; set; }

        public MvxCommand SaveCommand { get; private set; }

        public MvxCommand NavigateLogoutCommand { get; private set; }

        public string HeaderTitle { get; set; }

        public bool ShowBack { get; set; }

        public bool ShowHeaderButton { get; set; }

        public string Key { get; set; }

        public WishDetailViewModel(IMvxNavigationService navigationService)
        {
            this.navigationService = navigationService;
            this.NavigateBackCommand = new MvxCommand(this.NavigateBack);

            this.SaveCommand = new MvxCommand(this.Save);

            this.HeaderTitle = "Musikwunsch";
            this.ShowHeaderButton = false;
            this.ShowBack = true;
        }

        protected void Save()
        {
            App.Db.RemoveWish(this.Key);

            UserDialogs.Instance.Alert(new AlertConfig { Message = "Der Musikwunsch wurde erledigt.", Title = "Erledigt", OkText = "Ok", AndroidStyleId = this.AlertStyleId });
            this.navigationService.Close(this, System.Threading.CancellationToken.None);
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
            var wish = App.Db.GetWish(key);
            this.MusicWish = wish.MusicWish;
            if (string.IsNullOrEmpty(wish.Name) == false)
            {
                this.Name = wish.Name;
            }
            else
            {
                this.Name = "-";
            }
        }
    }
}
