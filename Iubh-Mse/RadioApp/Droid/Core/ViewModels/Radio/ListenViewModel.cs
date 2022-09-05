using Iubh.RadioApp.Core.Clients;
using Iubh.RadioApp.Core.Models;
using MvvmCross.Commands;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Iubh.RadioApp.Core.ViewModels
{
    public class ListenViewModel: BaseViewModel
    {
        public readonly string StreamUrl = "https://scdn.nrjaudio.fm/de/33001/mp3_128.mp3?origine=wlan&cdn_path=adswizz_lbs9&adws_out_a1&access_token=051b27d744224c22a1273ebaa9b2c557";
        private readonly string songApiUrl = "https://api.nrjnet.de/webradio/energy/current/berlin.json?time={0}";
        private bool isTaskRunning;
        private bool isReloadNeccessary;

        private bool isPlaying;
        public bool IsPlaying
        {
            get { return this.isPlaying; }
            set
            {
                this.isPlaying = value;
                this.RaisePropertyChanged(() => this.IsPlaying);
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

        private bool isSongInfoReady;
        public bool IsSongInfoReady
        {
            get { return this.isSongInfoReady; }
            set
            {
                this.isSongInfoReady = value;
                this.RaisePropertyChanged(() => this.IsSongInfoReady);
            }
        }

        private string title;
        public string Title
        {
            get { return this.title; }
            set
            {
                this.title = value;
                this.RaisePropertyChanged(() => this.Title);
            }
        }

        private string interpreter;
        public string Interpreter
        {
            get { return this.interpreter; }
            set
            {
                this.interpreter = value;
                this.RaisePropertyChanged(() => this.Interpreter);
            }
        }


        private int currentVolume;
        public int CurrentVolume
        {
            get { return this.currentVolume; }
            set
            {
                this.currentVolume = value;
                this.RaisePropertyChanged(() => this.CurrentVolume);
            }
        }

        public IMvxAsyncCommand ReloadSongInfoCommand { get; private set; }

        public  ListenViewModel() : base("Radio hören", false, false)
        {
            this.ReloadSongInfoCommand = new MvxAsyncCommand(this.ReloadSongInfo);
            this.isReloadNeccessary = true;
            this.isTaskRunning = true;

            var thread = new Thread(async () =>
            {
                await InitSongInfoContinous();
            });
            thread.Start();
        }

        private async Task InitSongInfoContinous()
        {
            await Task.Run(async () => {
                while (isTaskRunning == true)
                {
                    await ReloadSongInfo();
                    await Task.Delay(TimeSpan.FromSeconds(10));
                }   
            });
        }

        private async Task ReloadSongInfo()
        {
            string url = string.Format(this.songApiUrl, DateTime.Now.Ticks);
            using (var client = new NoCacheHttpClient())
            {                
                string response = await client.GetStringAsync(url);
                var songInfo = JsonConvert.DeserializeObject<SongInfo>(response);

                if (string.IsNullOrEmpty(this.Title) == false && string.IsNullOrEmpty(this.Interpreter) == false && this.Title.Equals(songInfo.Title, StringComparison.CurrentCultureIgnoreCase) == false && this.Interpreter.Equals(songInfo.Interpreter, StringComparison.CurrentCultureIgnoreCase) == false)
                {
                    this.IsSongInfoReady = false;
                    await Task.Run(async () => {
                        while (isSongInfoReady == false)
                        {
                            await Task.Delay(TimeSpan.FromSeconds(10));                         
                            this.IsSongInfoReady = true;
                            this.isReloadNeccessary = true;
                        }
                    });
                }

                if (this.isReloadNeccessary == true)
                {
                    this.Title = songInfo.Title;
                    this.Interpreter = songInfo.Interpreter;
                    this.isReloadNeccessary = false;
                }
                this.IsSongInfoReady = true;
            }
        }

        public override void ViewDisappeared()
        {
            this.isTaskRunning = false;
            base.ViewDisappeared();
        }
    }    
}
