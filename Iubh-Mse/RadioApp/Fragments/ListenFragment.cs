using System;
using Android.OS;
using Android.Views;
using Iubh.RadioApp.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Support.V4;
using Iubh.RadioApp.Droid.Views;
using Android.Media;
using Android.Widget;
using Android.Content;
using Android.Database;

namespace Iubh.RadioApp.Droid.Fragments
{
    public class ListenFragment : MvxFragment<ListenViewModel>
    {
        private HeaderView header;
        private Button play;
        private Button pause;
        private MediaPlayer player;
        private bool isPrepared;
        private AudioManager audioManager;
        private SeekBar volumeBar;
        private Button volumeUpper;
        private Button volumeLower;
        private ProgressBar progressBar;
        private TextView interpreter;
        private TextView title;
        private RelativeLayout loadingSongInfo;

        public ListenFragment()
        {
            this.isPrepared = false;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.fragment_listen, container, false);

            this.header = view.FindViewById<HeaderView>(Resource.Id.Listen_Header);
            this.play = view.FindViewById<Button>(Resource.Id.Listen_Play);
            this.pause = view.FindViewById<Button>(Resource.Id.Listen_Pause);
            this.volumeUpper = view.FindViewById<Button>(Resource.Id.Listen_VolumeUpper);
            this.volumeLower = view.FindViewById<Button>(Resource.Id.Listen_VolumeLower);
            this.volumeBar = view.FindViewById<SeekBar>(Resource.Id.Listen_VolumeBar);
            this.progressBar = view.FindViewById<ProgressBar>(Resource.Id.Listen_ProgressBar);
            this.interpreter = view.FindViewById<TextView>(Resource.Id.Listen_Interpret);
            this.title = view.FindViewById<TextView>(Resource.Id.Listen_Title);
            this.loadingSongInfo = view.FindViewById<RelativeLayout>(Resource.Id.Listen_SongInfo_Loading);
            
            var set = this.CreateBindingSet<ListenFragment, ListenViewModel>();
            set.Bind(this.play).For(x => x.Visibility).To(x =>x.IsPlaying).WithConversion(Converters.Converters.InvertedVisibilityConverter);
            set.Bind(this.play).For(x => x.Visibility).To(x => x.IsLoading).WithConversion(Converters.Converters.InvertedVisibilityConverter);
            set.Bind(this.play).To(x => x.ReloadSongInfoCommand);
            set.Bind(this.interpreter).For(x => x.Text).To(x => x.Interpreter);
            set.Bind(this.title).For(x => x.Text).To(x => x.Title);
            set.Bind(this.loadingSongInfo).For(x => x.Visibility).To(x => x.IsSongInfoReady).WithConversion(Converters.Converters.InvertedVisibilityConverter);

            set.Bind(this.progressBar).For(x => x.Visibility).To(x => x.IsLoading).WithConversion(Converters.Converters.VisibilityConverter);

            set.Bind(this.pause).For(x => x.Visibility).To(x => x.IsPlaying).WithConversion(Converters.Converters.VisibilityConverter);

            set.Bind(this.volumeBar).For(x => x.Progress).To(x => x.CurrentVolume);
            set.Bind(this.header).For(x => x.ActionButtonImage).To(x => x.HeaderActionImage).WithConversion(Converters.Converters.DrawableConverter);
            set.Bind(this.header).For(x => x.HeaderTitle).To(x => x.HeaderTitle);
            set.Bind(this.header).For(x => x.ShowHeaderButton).To(x => x.ShowHeaderButton).WithConversion(Converters.Converters.VisibilityConverter);
            set.Bind(this.header).For(x => x.ShowBack).To(x => x.ShowBack).WithConversion(Converters.Converters.VisibilityConverter);
            set.Apply();
         
            this.audioManager = Context.GetSystemService(Android.Content.Context.AudioService) as AudioManager;
            this.ViewModel.CurrentVolume = audioManager.GetStreamVolume(Stream.Music);
         
            this.play.Click += Play_Click;
            this.pause.Click += Pause_Click;
            this.volumeUpper.Click += VolumeUpper_Click;
            this.volumeLower.Click += VolumeLower_Click;
            this.volumeBar.ProgressChanged += VolumeBar_ProgressChanged;

            this.Context.ContentResolver.RegisterContentObserver(Android.Provider.Settings.System.ContentUri, true, new SettingsContentObserver(this.ViewModel, this.audioManager, this.Context, new Handler()));

            return view;
        }

        private void VolumeBar_ProgressChanged(object sender, SeekBar.ProgressChangedEventArgs e)
        {
            if (e.FromUser == true)
            {
                audioManager.SetStreamVolume(Stream.Music, e.Progress, VolumeNotificationFlags.PlaySound);
                this.ViewModel.CurrentVolume = e.Progress;
            }
        }

        private void VolumeLower_Click(object sender, EventArgs e)
        {
            audioManager.AdjustVolume(Adjust.Lower, VolumeNotificationFlags.PlaySound);
            this.ViewModel.CurrentVolume --;
        }

        private void VolumeUpper_Click(object sender, EventArgs e)
        {
            audioManager.AdjustVolume(Adjust.Raise, VolumeNotificationFlags.PlaySound);
            this.ViewModel.CurrentVolume++;
        }

        private void Pause_Click(object sender, EventArgs e)
        {
            this.player.Stop();
            this.isPrepared = false;
            this.ViewModel.IsPlaying = false;
        }

        private void Play_Click(object sender, EventArgs e)
        {
            this.ViewModel.IsLoading = true;

            this.Play();
        }

        public void Play()
        {
            if (this.isPrepared == false)
            {
                if (this.player == null)
                    this.player = new MediaPlayer();
                else
                    this.player.Reset();

                this.player.SetDataSource(this.ViewModel.StreamUrl);
                this.player.Prepared += (sender, args) =>
                {
                    this.player.Start();
                    this.isPrepared = true;
                    this.ViewModel.IsLoading = false;
                    this.ViewModel.IsPlaying = true;
                };

                player.PrepareAsync();
            }
        }

        public override void OnPause()
        {
            base.OnPause();
            if (this.player != null)
                this.player.Stop();
        }
    }

    public class SettingsContentObserver: ContentObserver
    {
        private AudioManager audioManager;
        ListenViewModel model;

        public SettingsContentObserver(ListenViewModel model, AudioManager audioManager, Context context, Handler handler) : base(handler)
        {
            this.audioManager = audioManager;
            this.model = model;
        }

        public override void OnChange(bool selfChange)
        {
            base.OnChange(selfChange);
            var volume = audioManager.GetStreamVolume(Stream.Music);
            this.audioManager.SetStreamVolume(Stream.Music, audioManager.GetStreamVolume(Stream.Music), VolumeNotificationFlags.PlaySound);
            this.model.CurrentVolume = volume;
        }
    }
}