using System;
using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Views;
using Android.Widget;
using Iubh.RadioApp.Core.ViewModels;
using MvvmCross.Droid.Support.V4;
using Iubh.RadioApp.Droid.Fragments;
using Iubh.RadioApp.Droid.Views;

namespace Iubh.RadioApp.Droid.Activities
{
    [Activity(Label = "ModeratorActivity", Theme = "@style/AppTheme", WindowSoftInputMode = SoftInput.AdjustPan, NoHistory = true)]
    public class ModeratorActivity : MvvmCross.Droid.Support.V4.MvxFragmentActivity<TabBarModeratorViewModel>
    {
        private TabViewModerator navigation;
        private LinearLayout wishTab;
        private LinearLayout rateTab;

        public TabbarItemOption SelectedIndex 
        { 
            set
            {
                this.Navigate(value);
            } 
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
          
            SetContentView(Resource.Layout.activity_moderator);

            UserDialogs.Init(this);

            
            this.navigation = FindViewById<TabViewModerator>(Resource.Id.moderator_navigation);

            this.wishTab = this.navigation.FindViewById<LinearLayout>(Resource.Id.tabbar_moderator_wish);
            this.wishTab.Click += TabClicked;

            this.rateTab = this.navigation.FindViewById<LinearLayout>(Resource.Id.tabbar_moderator_rate);
            this.rateTab.Click += TabClicked;
            
            this.Navigate(TabbarItemOption.Wish);
        }

        private void TabClicked(object sender, EventArgs e)
        {
            if (sender == this.wishTab)
            {
                this.Navigate(TabbarItemOption.Wish);
            }
            else if (sender == this.rateTab)
            {
                this.Navigate(TabbarItemOption.Rate);
            }
        }

        private void Navigate(TabbarItemOption item)
        {
            this.ResetTabbarImages();
            MvxFragment fragment = null;
            switch (item)
            {
                case TabbarItemOption.Wish:
                    fragment = new WishListFragment();
                    fragment.ViewModel = new WishListViewModel();
                    this.wishTab.FindViewById<ImageView>(Resource.Id.tabbar_moderator_wishImage).SetImageDrawable(Resources.GetDrawable(Resource.Drawable.Icon_Wish_selected, null));
                    break;
                case TabbarItemOption.Rate:
                    fragment = new RateListFragment();
                    fragment.ViewModel = new RateListViewModel();
                    this.rateTab.FindViewById<ImageView>(Resource.Id.tabbar_moderator_rateImage).SetImageDrawable(Resources.GetDrawable(Resource.Drawable.Icon_Rate_selected, null));
                    break;
            }
            this.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.moderator_fragment_container, fragment).Commit();
          
        }

        private void ResetTabbarImages ()
        {
            this.rateTab.FindViewById<ImageView>(Resource.Id.tabbar_moderator_rateImage).SetImageDrawable(Resources.GetDrawable(Resource.Drawable.Icon_Rate, null));
            this.wishTab.FindViewById<ImageView>(Resource.Id.tabbar_moderator_wishImage).SetImageDrawable(Resources.GetDrawable(Resource.Drawable.Icon_Wish, null));
        }
    }
}