using System;
using Acr.UserDialogs;
using Iubh.RadioApp.Data.Models;
using MvvmCross.Commands;

namespace Iubh.RadioApp.Core.ViewModels
{
    public class RateViewModel : BaseViewModel
    {
        public MvxCommand SaveCommand { get; private set; }

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

        private int? rating;
        public int? Rating
        {
            get { return this.rating; }
            set
            {
                this.rating = value;
                this.RaisePropertyChanged(() => this.Rating);
            }
        }


        private bool showModerator;
        public bool ShowModerator
        {
            get { return this.showModerator; }
            set
            {
                this.showModerator = value;
                this.RaisePropertyChanged(() => this.ShowModerator);
            }
        }

        private bool showPlaylist;
        public bool ShowPlaylist
        {
            get { return this.showPlaylist; }
            set
            {
                this.showPlaylist = value;
                this.RaisePropertyChanged(() => this.ShowPlaylist);
            }
        }

        public int AlertStyleId { get; set; }

        public RateViewModel() : base("Bewertungen", false, false, null, null, null, false)
        {
            this.SaveCommand = new MvxCommand(this.Save);
            this.ShowPlaylist = true;
        }

        private void Save()
        {
            if (this.Rating == null)
            {
                UserDialogs.Instance.Alert(new AlertConfig { Message = "Bitte geben Sie eine Bewertung ab.", Title = "Fehler", OkText = "Ok", AndroidStyleId = this.AlertStyleId });
                return;
            }

            if (this.ShowPlaylist == true)
            {
                var playlistRating = new PlaylistRating
                {
                    Text = this.Text,
                    Rating = this.Rating.Value
                };
             
                App.Db.AddPlaylistRate(playlistRating);

                UserDialogs.Instance.Alert(new AlertConfig { Message = $"Vielen Dank für die Bewertung der Playlist.", Title = "Danke!", OkText = "Ok", AndroidStyleId = this.AlertStyleId });
            }
            else if (this.ShowModerator == true)
            {
                var moderatorRating = new ModeratorRating
                {
                    Text = this.Text,
                    Rating = this.Rating.Value
                };

                App.Db.AddModeratorRate(moderatorRating);

                UserDialogs.Instance.Alert(new AlertConfig { Message = $"Vielen Dank für Ihre Bewertung der Moderator(inn)en.", Title = "Danke!", OkText = "Ok", AndroidStyleId = this.AlertStyleId });
            }

            this.Rating = null;
            this.Text = null;
        }
    }
}
