using Android.OS;
using Android.Views;
using Android.Widget;
using Iubh.RadioApp.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Iubh.RadioApp.Droid.Adapters;
using Iubh.RadioApp.Droid.Views;
using Iubh.RadioApp.Droid.Views.RecyclerView;
using MvvmCross.Droid.Support.V4;
using System;
using Android.Graphics.Drawables;

namespace Iubh.RadioApp.Droid.Fragments
{
    public class RateListFragment : MvxFragment<RateListViewModel>
    {
        private HeaderView header;
        
        private RelativeLayout playlistWrapper;
        private MvxRecyclerView playlistRecyclerView;
        private MvxSwipeRefreshLayout playlistRefreshRecyclerView;
        private RelativeLayout playlistLoading;
        private TextView playlistIsEmpty;
        private TextView playlistRateSum;

        private RelativeLayout moderatorWrapper;
        private MvxRecyclerView moderatorRecyclerView;
        private MvxSwipeRefreshLayout moderatorRefreshRecyclerView;
        private RelativeLayout moderatorLoading;
        private TextView moderatorIsEmpty;
        private TextView moderatorRateSum;
        
        private Button moderator;
        private Button playlist;
        private Drawable transparentButton;
        private Drawable blueButton;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.fragment_ratelist, container, false);
            this.header = view.FindViewById<HeaderView>(Resource.Id.Ratelist_Header);
            
            this.playlistWrapper = view.FindViewById<RelativeLayout>(Resource.Id.Ratelist_Playlist_Wrapper);
            this.playlistRecyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.Ratelist_Playlist_RecyclerView);
            this.playlistLoading = view.FindViewById<RelativeLayout>(Resource.Id.Ratelist_Playlist_Loading);
            this.playlistIsEmpty = view.FindViewById<TextView>(Resource.Id.Ratelist_Playlist_EmtpyItems);
            this.playlistRateSum = view.FindViewById<TextView>(Resource.Id.Ratelist_Playlist_RateSum);
            
            this.moderatorWrapper= view.FindViewById<RelativeLayout>(Resource.Id.Ratelist_Moderator_Wrapper);
            this.moderatorRecyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.Ratelist_Moderator_RecyclerView);
            this.moderatorLoading = view.FindViewById<RelativeLayout>(Resource.Id.Ratelist_Moderator_Loading);
            this.moderatorIsEmpty = view.FindViewById<TextView>(Resource.Id.Ratelist_Moderator_EmtpyItems);
            this.moderatorRateSum = view.FindViewById<TextView>(Resource.Id.Ratelist_Moderator_RateSum);
            
            this.moderator = view.FindViewById<Button>(Resource.Id.Ratelist_Moderator);
            this.playlist = view.FindViewById<Button>(Resource.Id.Ratelist_Playlist);

            this.moderatorRefreshRecyclerView = view.FindViewById<MvxSwipeRefreshLayout>(Resource.Id.Ratelist_Moderator_RecyclerViewRefresh);
            this.playlistRefreshRecyclerView = view.FindViewById<MvxSwipeRefreshLayout>(Resource.Id.Ratelist_Playlist_RecyclerViewRefresh);

            var playlistSource = new RateAdapter((IMvxAndroidBindingContext)this.BindingContext);
            this.playlistRecyclerView.AddItemDecoration(new LineDividerItemDecoration(this.Context));
            this.playlistRecyclerView.Adapter = playlistSource;
            
            var moderatorSource = new RateAdapter((IMvxAndroidBindingContext)this.BindingContext);
            this.moderatorRecyclerView.AddItemDecoration(new LineDividerItemDecoration(this.Context));
            this.moderatorRecyclerView.Adapter = moderatorSource;
            
            var set = this.CreateBindingSet<RateListFragment, RateListViewModel>();

            set.Bind(playlistSource).For(x => x.ItemsSource).To(x => x.PlayListItems);
            set.Bind(playlistSource).For(x => x.ItemClick).To(x => x.NavigateDetailCommand);
            set.Bind(this.playlistLoading).For(x => x.Visibility).To(x => x.PlayListIsLoading).WithConversion(Converters.Converters.VisibilityConverter);
            set.Bind(this.playlistIsEmpty).For(x => x.Visibility).To(x => x.PlayListIsEmpty).WithConversion(Converters.Converters.VisibilityConverter);
            set.Bind(this.playlistRateSum).For(x => x.Text).To(x => x.PlaylistRateSum);
            set.Bind(this.playlistRefreshRecyclerView).For(x => x.Refreshing).To(x => x.IsLoadingPlaylist);
            set.Bind(this.playlistRefreshRecyclerView).For(x => x.RefreshCommand).To(x => x.RefreshPlaylistCommand);

            set.Bind(moderatorSource).For(x => x.ItemsSource).To(x => x.ModeratorItems);
            set.Bind(moderatorSource).For(x => x.ItemClick).To(x => x.NavigateDetailCommand);
            set.Bind(this.moderatorLoading).For(x => x.Visibility).To(x => x.ModeratorIsLoading).WithConversion(Converters.Converters.VisibilityConverter);
            set.Bind(this.moderatorIsEmpty).For(x => x.Visibility).To(x => x.ModeratorIsEmpty).WithConversion(Converters.Converters.VisibilityConverter);
            set.Bind(this.moderatorRateSum).For(x => x.Text).To(x => x.ModeratorRateSum);
            set.Bind(this.moderatorRefreshRecyclerView).For(x => x.Refreshing).To(x => x.IsLoadingModerator);
            set.Bind(this.moderatorRefreshRecyclerView).For(x => x.RefreshCommand).To(x => x.RefreshModeratorCommand);

            set.Bind(this.moderator).To(x => x.ShowModeratorCommand);
            set.Bind(this.playlist).To(x => x.ShowPlaylistCommand);
            set.Bind(this.moderatorWrapper).For(x => x.Visibility).To(x => x.IsShowModerator).WithConversion(Converters.Converters.VisibilityConverter);
            set.Bind(this.playlistWrapper).For(x => x.Visibility).To(x => x.IsShowModerator).WithConversion(Converters.Converters.InvertedVisibilityConverter);
            
            set.Bind(this.header).For(x => x.ActionButtonImage).To(x => x.HeaderActionImage).WithConversion(Converters.Converters.DrawableConverter);
            set.Bind(this.header).For(x => x.HeaderTitle).To(x => x.HeaderTitle);
            set.Bind(this.header).For(x => x.ShowHeaderButton).To(x => x.ShowHeaderButton).WithConversion(Converters.Converters.VisibilityConverter);
            set.Bind(this.header).For(x => x.ShowBack).To(x => x.ShowBack).WithConversion(Converters.Converters.VisibilityConverter);
            set.Bind(this.header.HeaderButton).To(x => x.NavigateLogoutCommand);

            set.Apply();

            this.transparentButton = this.Context.GetDrawable(Resource.Drawable.round_corner_transparent_button);
            this.blueButton = this.Context.GetDrawable(Resource.Drawable.round_corner_button);

            this.moderator.Click += Moderator_Click;
            this.playlist.Click += Playlist_Click;

            this.moderatorRefreshRecyclerView.Refresh += ModeratorRefreshRecyclerView_Refresh;
            this.playlistRefreshRecyclerView.Refresh += PlaylistRefreshRecyclerView_Refresh;
            return view;
        }

        private void PlaylistRefreshRecyclerView_Refresh(object sender, EventArgs e)
        {
            var playlistSource = new RateAdapter((IMvxAndroidBindingContext)this.BindingContext);
            this.playlistRecyclerView.AddItemDecoration(new LineDividerItemDecoration(this.Context));
            this.playlistRecyclerView.Adapter = playlistSource;
            this.playlistRecyclerView.ItemsSource = this.ViewModel.PlayListItems;
        }

        private void ModeratorRefreshRecyclerView_Refresh(object sender, EventArgs e)
        {
            var moderatorSource = new RateAdapter((IMvxAndroidBindingContext)this.BindingContext);
            this.moderatorRecyclerView.AddItemDecoration(new LineDividerItemDecoration(this.Context));
            this.moderatorRecyclerView.Adapter = moderatorSource;
            this.moderatorRecyclerView.ItemsSource = this.ViewModel.ModeratorItems;
        }

        private void Moderator_Click(object sender, EventArgs e)
        {
            this.playlist.Background = blueButton;
            this.moderator.Background = transparentButton;
        }

        private void Playlist_Click(object sender, EventArgs e)
        {
            this.playlist.Background = transparentButton;
            this.moderator.Background = blueButton;
        }

        public override void OnResume()
        {
            if (this.ViewModel.IsShowModerator == true)
            {
                this.playlist.Background = blueButton;
                this.moderator.Background = transparentButton;
            }
            else
            {
                this.playlist.Background = transparentButton;
                this.moderator.Background = blueButton;
            }
            base.OnResume(); 
        }
    }
}