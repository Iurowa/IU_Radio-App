using System;
using Acr.UserDialogs;
using MvvmCross.Commands;

namespace Iubh.RadioApp.Core.ViewModels
{
    public class WishViewModel : BaseViewModel
    {
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

        public WishViewModel() : base("Musik wünschen", false, false, null, null, null, false)
        {
            this.SaveCommand = new MvxCommand(this.Save);
        }

        protected void Save()
        {
            if (string.IsNullOrEmpty(this.MusicWish) == true)
            {
                UserDialogs.Instance.Alert(new AlertConfig { Message = "Bitte füllen Sie alle Pflichtfelder aus.", Title = "Fehler", OkText = "Ok", AndroidStyleId = this.AlertStyleId });
                return;
            }

            var wish = new Data.Models.Wish
            {
                Name = this.Name,
                MusicWish = this.MusicWish
            };

            App.Db.AddWish(wish);

            this.MusicWish = string.Empty;
            this.Name = string.Empty;

            UserDialogs.Instance.Alert(new AlertConfig { Message = "Vielen Dank für Ihren Musikwunsch.", Title = "Danke!", OkText = "Ok", AndroidStyleId = this.AlertStyleId });
        }
    }
}
