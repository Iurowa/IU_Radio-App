using System;
using System.Collections.Generic;
using System.Linq;
using Iubh.RadioApp.Data;
using MvvmCross.Commands;

namespace Iubh.RadioApp.Core.ViewModels
{
    public class RateListViewModel : BaseViewModel
    {
        private List<RateTeaserViewModel> playListItems;
        public List<RateTeaserViewModel> PlayListItems
        {
            get { return this.playListItems; }
            set
            {
                this.playListItems = value;
                this.RaisePropertyChanged(() => this.PlayListItems);
            }
        }

        private bool playListIsLoading;
        public bool PlayListIsLoading
        {
            get { return this.playListIsLoading; }
            set
            {
                this.playListIsLoading = value;
                this.RaisePropertyChanged(() => this.PlayListIsLoading);
            }
        }

        private bool playListIsEmpty;
        public bool PlayListIsEmpty
        {
            get { return this.playListIsEmpty; }
            set
            {
                this.playListIsEmpty = value;
                this.RaisePropertyChanged(() => this.PlayListIsEmpty);
            }
        }

        private decimal playlistRateSum;
        public decimal PlaylistRateSum
        {
            get { return this.playlistRateSum; }
            set
            {
                this.playlistRateSum = value;
                this.RaisePropertyChanged(() => this.PlaylistRateSum);
            }
        }

        private List<RateTeaserViewModel> moderatorItems;
        public List<RateTeaserViewModel> ModeratorItems
        {
            get { return this.moderatorItems; }
            set
            {
                this.moderatorItems = value;
                this.RaisePropertyChanged(() => this.ModeratorItems);
            }
        }

        private bool moderatorIsLoading;
        public bool ModeratorIsLoading
        {
            get { return this.moderatorIsLoading; }
            set
            {
                this.moderatorIsLoading = value;
                this.RaisePropertyChanged(() => this.ModeratorIsLoading);
            }
        }

        private bool moderatorIsEmpty;
        public bool ModeratorIsEmpty
        {
            get { return this.moderatorIsEmpty; }
            set
            {
                this.moderatorIsEmpty = value;
                this.RaisePropertyChanged(() => this.ModeratorIsEmpty);
            }
        }

        private decimal moderatorRateSum;
        public decimal ModeratorRateSum
        {
            get { return this.moderatorRateSum; }
            set
            {
                this.moderatorRateSum = value;
                this.RaisePropertyChanged(() => this.ModeratorRateSum);
            }
        }

        private bool iSShowModerator;
        public bool IsShowModerator
        {
            get { return this.iSShowModerator; }
            set
            {
                this.iSShowModerator = value;
                this.RaisePropertyChanged(() => this.IsShowModerator);
            }
        }

        private bool isLoadingModerator;
        public bool IsLoadingModerator
        {
            get { return this.isLoadingModerator; }
            set
            {
                this.isLoadingModerator = value;
                this.RaisePropertyChanged(() => this.IsLoadingModerator);
            }
        }

        private bool isLoadingPlaylist;
        public bool IsLoadingPlaylist
        {
            get { return this.isLoadingPlaylist; }
            set
            {
                this.isLoadingPlaylist = value;
                this.RaisePropertyChanged(() => this.IsLoadingPlaylist);
            }
        }

        public MvxCommand RefreshModeratorCommand { get; private set; }

        public MvxCommand RefreshPlaylistCommand { get; private set; }

        public MvxCommand NavigateLogoutCommand { get; private set; }

        public MvxCommand ShowModeratorCommand { get; private set; }

        public MvxCommand ShowPlaylistCommand { get; private set; }

        public IMvxCommand<RateTeaserViewModel> NavigateDetailCommand { get; private set; }

        public RateListViewModel() : base("Bewertungen", true, false, "Logout", "Logout", null, false)
        {
            this.NavigateDetailCommand = new MvxCommand<RateTeaserViewModel>(this.NavigateToDetail);
            this.NavigateLogoutCommand = new MvxCommand(this.Logout);
            this.ShowModeratorCommand = new MvxCommand(this.ShowModerator);
            this.ShowPlaylistCommand = new MvxCommand(this.ShowPlaylist);
            this.RefreshModeratorCommand = new MvxCommand(this.ShowModerator);
            this.RefreshPlaylistCommand = new MvxCommand(this.ShowPlaylist);

            this.PlayListItems = new List<RateTeaserViewModel>();
            this.ModeratorItems = new List<RateTeaserViewModel>();
            this.ShowPlaylist();
        }

        private void NavigateToDetail(RateTeaserViewModel item)
        {
            item.NavigateToDetailCommand.Execute();
        }

        private void ShowModerator()
        {
            this.IsLoadingModerator = true;
            var rates = App.Db.GetModeratorRatings().OrderBy(x => x.DateCreated).ToList();
            this.ModeratorItems.Clear();

            foreach (var rate in rates)
            {
                var text = string.IsNullOrEmpty(rate.Text) == false ? rate.Text : "-";
                this.ModeratorItems.Add(new RateTeaserViewModel { Key = rate.Key, Rate = rate.Rating.ToString(), Text = text });
            }

            if (this.ModeratorItems.Any() == false)
            {
                this.ModeratorIsEmpty = true;
            }
            else
            {
                this.ModeratorIsEmpty = false;
                var sum = this.ModeratorItems.Sum(x => Convert.ToDecimal(x.Rate));
                this.ModeratorRateSum = Math.Round(sum / this.ModeratorItems.Count, 1);
            }

            this.IsShowModerator = true;
            this.IsLoadingModerator = false;
        }

        private void ShowPlaylist()
        {
            this.IsLoadingPlaylist = true;
            var rates = App.Db.GetPlaylistRatings().OrderBy(x => x.DateCreated).ToList();
            this.PlayListItems.Clear();

            foreach (var rate in rates)
            {
                var text = string.IsNullOrEmpty(rate.Text) == false ? rate.Text : "-";
                this.PlayListItems.Add(new RateTeaserViewModel { Key = rate.Key, Rate = rate.Rating.ToString(), Text = text });
            }

            if (this.PlayListItems.Any() == false)
            {
                this.playListIsEmpty = true;
            }
            else
            {
                this.playListIsEmpty = false;
                var sum = this.PlayListItems.Sum(x => Convert.ToDecimal(x.Rate));
                this.PlaylistRateSum = Math.Round(sum / this.PlayListItems.Count, 1);
            }
            
            this.IsShowModerator = false;
            this.IsLoadingPlaylist = false;
        }
        
        private void Logout()
        {
            App.DbLocal.RemoveConfigValue(Config.Static.IsLoginId);
            this.NavigationService.Navigate<TabBarViewModel>();
        }
    }
}
