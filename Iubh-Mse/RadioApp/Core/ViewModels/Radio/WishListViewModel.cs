using System.Collections.Generic;
using System.Linq;
using Iubh.RadioApp.Data;
using MvvmCross.Commands;

namespace Iubh.RadioApp.Core.ViewModels
{
    public class WishListViewModel : BaseViewModel
    {
        private List<WishTeaserViewModel> items;
        public List<WishTeaserViewModel> Items
        {
            get { return this.items; }
            set
            {
                this.items = value;
                this.RaisePropertyChanged(() => this.Items);
            }
        }

        private bool isLoading;
        public bool IsLoading
        {
            get { return this.isLoading; }
            set
            {
                this.isLoading = value;
                this.RaisePropertyChanged(() => this.IsLoading);
            }
        }

        private bool isEmpty;
        public bool IsEmpty
        {
            get { return this.isEmpty; }
            set
            {
                this.isEmpty = value;
                this.RaisePropertyChanged(() => this.IsEmpty);
            }
        }

        public MvxCommand NavigateLogoutCommand { get; private set; }

        public IMvxCommand<WishTeaserViewModel> NavigateDetailCommand { get; private set; }

        public MvxCommand RefreshCommand { get; private set; }
        

        public WishListViewModel() : base("Musikwünsche", true, false, "Logout", "Logout", null, false)
        {
            this.NavigateDetailCommand = new MvxCommand<WishTeaserViewModel>(this.NavigateToDetail);
            this.NavigateLogoutCommand = new MvxCommand(this.Logout);
            this.RefreshCommand = new MvxCommand(this.GetItems);

            this.Items = new List<WishTeaserViewModel>();
            //this.GetItems();
        }

        private void NavigateToDetail(WishTeaserViewModel item)
        {
            item.NavigateToDetailCommand.Execute();
        }

        private void Logout()
        {
            App.DbLocal.RemoveConfigValue(Config.Static.IsLoginId);
            this.NavigationService.Navigate<TabBarViewModel>();
        }

        private void GetItems()
        {
            this.IsLoading = true;
            this.Items.Clear();

            var wishs = App.Db.GetWishes().OrderBy(x => x.DateCreated).ToList();
            foreach (var wish in wishs)
            {
                var name = string.IsNullOrEmpty(wish.Name) == false ? wish.Name : "-";
                this.Items.Add(new WishTeaserViewModel { Key = wish.Key, Name = name, Text = wish.MusicWish });
            }

            if (this.Items.Any() == false)
            {
                this.isEmpty = true;
            }
            this.IsLoading = false;
        }

        public override void ViewAppearing()
        {
            if (this.Items != null)
            {
                this.GetItems();
            }
            base.ViewAppearing();
        }
    }
}
