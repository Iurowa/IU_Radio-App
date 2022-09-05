using System;
using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Views;
using Android.Widget;
using Iubh.RadioApp.Core.ViewModels;
using MvvmCross;
using MvvmCross.Droid.Support.V4;
using Iubh.RadioApp.Droid.Fragments;
using Iubh.RadioApp.Droid.Views;
using Iubh.RadioApp.Droid.Services;

namespace Iubh.RadioApp.Droid.Activities
{
    [Activity(Label = "MainActivity", Theme = "@style/AppTheme", WindowSoftInputMode = SoftInput.AdjustPan, NoHistory = false)]
    public class MainActivity : MvvmCross.Droid.Support.V4.MvxFragmentActivity<TabBarViewModel>
    {
        private TabView navigation;

        private LinearLayout listenTab;
        private LinearLayout playlistTab;
        private LinearLayout wishTab;
        private LinearLayout rateTab;
        private LinearLayout serviceTab;

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
          
            SetContentView(Resource.Layout.activity_main);

            UserDialogs.Init(this);

            
            this.navigation = FindViewById<TabView>(Resource.Id.bottom_navigation);

            this.listenTab = this.navigation.FindViewById<LinearLayout>(Resource.Id.tabbar_listen);
            this.listenTab.Click += TabClicked;

            this.playlistTab = this.navigation.FindViewById<LinearLayout>(Resource.Id.tabbar_playlist);
            this.playlistTab.Click += TabClicked;

            this.wishTab = this.navigation.FindViewById<LinearLayout>(Resource.Id.tabbar_wish);
            this.wishTab.Click += TabClicked;

            this.rateTab = this.navigation.FindViewById<LinearLayout>(Resource.Id.tabbar_rate);
            this.rateTab.Click += TabClicked;

            this.serviceTab = this.navigation.FindViewById<LinearLayout>(Resource.Id.tabbar_service);
            this.serviceTab.Click += TabClicked;
            
            this.Navigate(TabbarItemOption.Listen);
        }

        private void TabClicked(object sender, EventArgs e)
        {
            if (sender == this.listenTab)
            {
                this.Navigate(TabbarItemOption.Listen);
            }
            else if (sender == this.playlistTab)
            {
                this.Navigate(TabbarItemOption.Playlist);
            }
            else if(sender == this.wishTab)
            {
                this.Navigate(TabbarItemOption.Wish);
            }
            else if (sender == this.rateTab)
            {
                this.Navigate(TabbarItemOption.Rate);
            }
            else if (sender == this.serviceTab)
            {
                this.Navigate(TabbarItemOption.Service);
            }
        }

        private void Navigate(TabbarItemOption item)
        {
            this.ResetTabbarImages();
            MvxFragment fragment = null;
            switch (item)
            {
                case TabbarItemOption.Listen:
                    fragment = new ListenFragment();
                    fragment.ViewModel = new ListenViewModel();
                    this.listenTab.FindViewById<ImageView>(Resource.Id.tabbar_listenImage).SetImageDrawable(Resources.GetDrawable(Resource.Drawable.Icon_Play_selected, null));
                    break;
                case TabbarItemOption.Playlist:
                    fragment = new PlaylistFragment();
                    fragment.ViewModel = new PlaylistViewModel();
                    this.playlistTab.FindViewById<ImageView>(Resource.Id.tabbar_playlistImage).SetImageDrawable(Resources.GetDrawable(Resource.Drawable.Icon_Playlist_selected, null));
                    break;
                case TabbarItemOption.Wish:
                    fragment = new WishFragment();
                    fragment.ViewModel = new WishViewModel();
                    this.wishTab.FindViewById<ImageView>(Resource.Id.tabbar_wishImage).SetImageDrawable(Resources.GetDrawable(Resource.Drawable.Icon_Wish_selected, null));
                    break;
                case TabbarItemOption.Rate:
                    fragment = new RateFragment();
                    fragment.ViewModel = new RateViewModel();
                    this.rateTab.FindViewById<ImageView>(Resource.Id.tabbar_rateImage).SetImageDrawable(Resources.GetDrawable(Resource.Drawable.Icon_Rate_selected, null));
                    break;
                case TabbarItemOption.Service:
                    fragment = new ServiceFragment();
                    fragment.ViewModel = new ServiceViewModel();
                    this.serviceTab.FindViewById<ImageView>(Resource.Id.tabbar_serviceImage).SetImageDrawable(Resources.GetDrawable(Resource.Drawable.Icon_Service_selected, null));
                    break;
            }
            this.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.fragment_container, fragment).Commit();
          
        }

        private void ResetTabbarImages ()
        {
            this.listenTab.FindViewById<ImageView>(Resource.Id.tabbar_listenImage).SetImageDrawable(Resources.GetDrawable(Resource.Drawable.Icon_Play, null));
            this.playlistTab.FindViewById<ImageView>(Resource.Id.tabbar_playlistImage).SetImageDrawable(Resources.GetDrawable(Resource.Drawable.Icon_Playlist, null));
            this.rateTab.FindViewById<ImageView>(Resource.Id.tabbar_rateImage).SetImageDrawable(Resources.GetDrawable(Resource.Drawable.Icon_Rate, null));
            this.wishTab.FindViewById<ImageView>(Resource.Id.tabbar_wishImage).SetImageDrawable(Resources.GetDrawable(Resource.Drawable.Icon_Wish, null));
            this.serviceTab.FindViewById<ImageView>(Resource.Id.tabbar_serviceImage).SetImageDrawable(Resources.GetDrawable(Resource.Drawable.Icon_Service, null));
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
        }
    }
}