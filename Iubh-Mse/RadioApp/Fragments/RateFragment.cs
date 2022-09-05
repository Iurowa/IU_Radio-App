using System;
using Android.OS;
using Android.Views;
using Android.Widget;
using Iubh.RadioApp.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using Iubh.RadioApp.Droid.Views;
using Android.Graphics.Drawables;

namespace Iubh.RadioApp.Droid.Fragments
{
    public class RateFragment : BaseFormFragment<RateViewModel>
    {
        private HeaderView header;
        private Button rateOne;
        private Button rateTwo;
        private Button rateThree;
        private Button rateFour;
        private Button rateFive;
        private Button moderator;
        private Button playlist;
        private Drawable transparentButton;
        private Drawable blueButton;
        private TextView ratePlaylistInfoText;
        private TextView rateModeratorInfoText;
        private Button send;
        private EditText text;
        private ScrollView scrollView;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.fragment_rate, container, false);
            this.header = view.FindViewById<HeaderView>(Resource.Id.Rate_Header);
            this.rateOne = view.FindViewById<Button>(Resource.Id.Rate_One);
            this.rateTwo = view.FindViewById<Button>(Resource.Id.Rate_Two);
            this.rateThree = view.FindViewById<Button>(Resource.Id.Rate_Three);
            this.rateFour = view.FindViewById<Button>(Resource.Id.Rate_Four);
            this.rateFive = view.FindViewById<Button>(Resource.Id.Rate_Five);
            this.moderator = view.FindViewById<Button>(Resource.Id.Rate_Moderator);
            this.playlist = view.FindViewById<Button>(Resource.Id.Rate_Playlist);
            this.ratePlaylistInfoText = view.FindViewById<TextView>(Resource.Id.Rate_InfoText_Playlist);
            this.rateModeratorInfoText = view.FindViewById<TextView>(Resource.Id.Rate_InfoText_Moderator);
            this.send = view.FindViewById<Button>(Resource.Id.Rate_SendButton);
            this.text = view.FindViewById<EditText>(Resource.Id.Rate_EditText);
            this.scrollView = view.FindViewById<ScrollView>(Resource.Id.Rate_ScrollView);

            var set = this.CreateBindingSet<RateFragment, RateViewModel>();
            
            set.Bind(this.header).For(x => x.ActionButtonImage).To(x => x.HeaderActionImage).WithConversion(Converters.Converters.DrawableConverter);
            set.Bind(this.header).For(x => x.HeaderTitle).To(x => x.HeaderTitle);
            set.Bind(this.header).For(x => x.ShowHeaderButton).To(x => x.ShowHeaderButton).WithConversion(Converters.Converters.VisibilityConverter);
            set.Bind(this.header).For(x => x.ShowBack).To(x => x.ShowBack).WithConversion(Converters.Converters.VisibilityConverter);

            set.Bind(this.send).To(x => x.SaveCommand);
            set.Bind(this.text).For(x => x.Text).To(x => x.Text);

            set.Bind(this.ratePlaylistInfoText).For(x => x.Visibility).To(x => x.ShowPlaylist).WithConversion(Converters.Converters.VisibilityConverter);
            set.Bind(this.rateModeratorInfoText).For(x => x.Visibility).To(x => x.ShowModerator).WithConversion(Converters.Converters.VisibilityConverter);

            set.Apply();

            this.transparentButton = this.Context.GetDrawable(Resource.Drawable.round_corner_transparent_button);
            this.blueButton = this.Context.GetDrawable(Resource.Drawable.round_corner_button);

            this.moderator.Click += Moderator_Click;
            this.playlist.Click += Playlist_Click;
            this.rateOne.Click += RateClick;
            this.rateTwo.Click += RateClick;
            this.rateThree.Click += RateClick;
            this.rateFour.Click += RateClick;
            this.rateFive.Click += RateClick;
            this.send.Click += Send_Click;

            this.RegisterForHideKeyboard(this.scrollView);
            this.ViewModel.AlertStyleId = Resource.Style.IuAlertDialog;

            return view;
        }

        private void Send_Click(object sender, EventArgs e)
        {
            this.ResetRating();
            this.CloseKeboard(this.View);
        }

        private void ResetRating()
        {
            this.ViewModel.Rating = null;
            this.rateOne.Background = this.Context.GetDrawable(Resource.Drawable.smiley_1);
            this.rateTwo.Background = this.Context.GetDrawable(Resource.Drawable.smiley_2);
            this.rateThree.Background = this.Context.GetDrawable(Resource.Drawable.smiley_3);
            this.rateFour.Background = this.Context.GetDrawable(Resource.Drawable.smiley_4);
            this.rateFive.Background = this.Context.GetDrawable(Resource.Drawable.smiley_5);
        }

        private void RateClick(object sender, EventArgs e)
        {
            this.ResetRating();

            var btn = (Button)sender;
            Drawable btnSelected = null;
            switch (btn.Id)
            {
                case Resource.Id.Rate_One:
                    {
                        btnSelected = this.Context.GetDrawable(Resource.Drawable.smiley_1_selected);
                        this.ViewModel.Rating = 1;
                        break;
                    }
                case Resource.Id.Rate_Two:
                    {
                        btnSelected = this.Context.GetDrawable(Resource.Drawable.smiley_2_selected);
                        this.ViewModel.Rating = 2;
                        break;
                    }
                case Resource.Id.Rate_Three:
                    {
                        btnSelected = this.Context.GetDrawable(Resource.Drawable.smiley_3_selected);
                        this.ViewModel.Rating = 3;
                        break;
                    }
                case Resource.Id.Rate_Four:
                    {
                        btnSelected = this.Context.GetDrawable(Resource.Drawable.smiley_4_selected);
                        this.ViewModel.Rating = 4;
                        break;
                    }
                case Resource.Id.Rate_Five:
                    {
                        btnSelected = this.Context.GetDrawable(Resource.Drawable.smiley_5_selected);
                        this.ViewModel.Rating = 5;
                        break;
                    }
                default:
                    break;
            }

            btn.Background = btnSelected;
        }

        private void Moderator_Click(object sender, EventArgs e)
        {
            this.playlist.Background = blueButton;
            this.moderator.Background = transparentButton;

            this.ViewModel.ShowModerator = true;
            this.ViewModel.ShowPlaylist = false;

            this.ResetRating();
            this.ViewModel.Text = string.Empty;
        }

        private void Playlist_Click(object sender, EventArgs e)
        {
            this.playlist.Background = transparentButton;
            this.moderator.Background = blueButton;

            this.ViewModel.ShowModerator = false;
            this.ViewModel.ShowPlaylist = true;

            this.ResetRating();
            this.ViewModel.Text = string.Empty;
        }

        public override void OnDestroyView()
        {
            base.OnDestroyView();

            this.header.Dispose();
            this.header = null;
        }
    }
}