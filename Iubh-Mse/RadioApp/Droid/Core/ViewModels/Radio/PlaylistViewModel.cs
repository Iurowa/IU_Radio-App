using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using Iubh.RadioApp.Core.Clients;
using Iubh.RadioApp.Core.Models;
using MvvmCross.Commands;
using Newtonsoft.Json;

namespace Iubh.RadioApp.Core.ViewModels
{
    public class PlaylistViewModel : BaseViewModel
    {
        private readonly string playlistApiUrl = "https://api.nrjnet.de/webradio/playlist/energy/berlin?day={0}&hour={1}";

        public string SelectedTime { get; set; }

        public string SelectedDate { get; set; }

        public List<string> Dates { get; set; }

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

        private List<string> times;
        public List<string> Times
        {
            get { return this.times; }
            set
            {
                this.times = value;
                this.RaisePropertyChanged(() => this.Times);
            }
        }

        private List<PlaylistTableViewModel> items;
        public List<PlaylistTableViewModel> Items
        {
            get { return this.items; }
            set
            {
                this.items = value;
                this.RaisePropertyChanged(() => this.Items);
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

        public MvxCommand ReloadCommand { get; private set; }
        
        public PlaylistViewModel() : base("Playlist", false, false, null, null, null, false)
        {
            this.ReloadCommand = new MvxCommand(this.ReloadPlaylist);

            this.Dates =  new List<string>();
            this.Times = new List<string>();
            this.Items = new List<PlaylistTableViewModel>();
           
            var now = DateTime.Now;

            this.Dates.Add("Auswählen ...");
            this.Dates.Add("heute");
            this.Dates.Add("gestern");
            this.Dates.Add("vorgestern");

            this.Times.Add("Auswählen ...");
            var hour = now.ToLocalTime().Hour;
            for (int i = 0; i <= hour; i++)
            {
                this.Times.Add($"{i} Uhr");
            }
 
            if (this.Items.Any() == true)
            {
                this.IsEmpty = false;
            }
            else
            {
                this.IsEmpty = true;
            }
        }
                
        public void UpdateDate(string selectedItem)
        {
            this.SelectedDate = selectedItem;

            this.Times.Clear();
            this.Times.Add("Auswählen ...");
            if (selectedItem.Equals("heute", StringComparison.CurrentCultureIgnoreCase))
            {
                var now = DateTime.Now.ToLocalTime();
                for (int i = 0; i <= now.Hour; i++)
                {
                    this.Times.Add($"{i} Uhr");
                }
            }
            else
            {
                for (int i = 0; i < 24; i++)
                {
                    this.Times.Add($"{i} Uhr");
                }
            }
            
        }

        public void UpdateTime(string selectedItem)
        {
            this.SelectedTime = selectedItem;
        }

        public async void ReloadPlaylist()
        {
            if (this.SelectedDate.Equals("Auswählen ...") == true || this.SelectedDate.Equals("Auswählen ...") == true)
                return;

            this.IsLoading = true;
            this.Items.Clear();

            var date = -2;
            var time = this.SelectedTime.Split(' ').First();
            if (this.SelectedDate.Equals("heute", StringComparison.CurrentCultureIgnoreCase))
            {
                date = 0;
            }
            else if (this.SelectedDate.Equals("gestern", StringComparison.CurrentCultureIgnoreCase))
            {
                date = -1;
            }
            
            string url = string.Format(this.playlistApiUrl, date, time);
            using (var client = new NoCacheHttpClient())
            {
                string response = await client.GetStringAsync(url);
                var playlistObject = JsonConvert.DeserializeObject<PlaylistObject>(response);
                var songs = playlistObject.Playlist.Songs;
                foreach (var song in songs)
                {
                    this.Items.Add(new PlaylistTableViewModel { Title = song.Title, Interpreter = song.Interpreter, Time = song.Start.ToLocalTime().ToString("HH:mm") });
                }
                this.Items = this.Items.OrderByDescending(x => x.Time).ToList();

                if (this.Items.Any() == true)
                {
                    this.IsEmpty = false;
                }
                else
                {
                    this.IsEmpty = true;
                }
            }
            
            this.IsLoading = false;
        }
    }
}
