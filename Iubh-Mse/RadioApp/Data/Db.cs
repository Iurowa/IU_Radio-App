using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using Iubh.RadioApp.Data.Database;
using Iubh.RadioApp.Data.Models;

namespace Iubh.RadioApp.Data
{
    public class Db : IDb
    {
        private FirebaseClient firebaseClient;
        private FirebaseAuthProvider firebaseAuth;

        public Db()
        {
            this.firebaseClient = new FirebaseClient("https://iu-radioapp-default-rtdb.firebaseio.com/");
            this.firebaseAuth = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyArzLH5KCOkLXppiAty_repcQRcbBO9KiE"));                      
        }

        ~Db()
        {
            this.firebaseClient.Dispose();
        }

        public bool IsModeratorLoginSuccessfully(string mail, string password)
        {
            try
            {
                var auth = this.firebaseAuth.SignInWithEmailAndPasswordAsync(mail, password).Result;
                if (auth != null && auth.User != null)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public void AddWish(Wish wish)
        {
            if (string.IsNullOrEmpty(wish.MusicWish) == false)
            {
                wish.DateCreated = DateTime.Now.ToUniversalTime();

                this.firebaseClient.Child("Wishes").PostAsync(wish);
            }
        }

        public List<Wish> GetWishes()
        {
            var wishes = new List<Wish>();
            var wishesDb = this.firebaseClient.Child("Wishes").OnceAsync<Wish>().Result;

            foreach (var wishDb in wishesDb)
            {
                wishes.Add(new Wish { DateCreated = wishDb.Object.DateCreated, Key = wishDb.Key, MusicWish = wishDb.Object.MusicWish, Name = wishDb.Object.Name });
            }
            return wishes.OrderByDescending(x => x.DateCreated).ToList();
        }

        public Wish GetWish(string key)
        {
            return this.firebaseClient.Child("Wishes").Child(key).OnceSingleAsync<Wish>().Result;
        }

        public async void RemoveWish(string key)
        {
            await this.firebaseClient.Child("Wishes").Child(key).DeleteAsync();
        }

        public void AddPlaylistRate(PlaylistRating rate)
        {
            rate.DateCreated = DateTime.Now.ToUniversalTime();
            this.firebaseClient.Child("PlaylistRatings").PostAsync(rate);
        }

        public PlaylistRating GetPlaylistRate(string key)
        {
            return this.firebaseClient.Child("PlaylistRatings").Child(key).OnceSingleAsync<PlaylistRating>().Result;
        }

        public List<BaseRating> GetPlaylistRatings()
        {
            var playlist = new List<BaseRating>();
            var playlistsDb = this.firebaseClient.Child("PlaylistRatings").OnceAsync<BaseRating>().Result;

            foreach (var playlistDb in playlistsDb)
            {
                playlist.Add(new BaseRating { DateCreated = playlistDb.Object.DateCreated, Key = playlistDb.Key, Text = playlistDb.Object.Text, Rating = playlistDb.Object.Rating });
            }
            return playlist.OrderByDescending(x => x.DateCreated).ToList();
        }

        public void AddModeratorRate(ModeratorRating rate)
        {
            rate.DateCreated = DateTime.Now.ToUniversalTime();
            this.firebaseClient.Child("ModeratorRatings").PostAsync(rate);
        }

        public ModeratorRating GetModeratorRate(string key)
        {
            return this.firebaseClient.Child("ModeratorRatings").Child(key).OnceSingleAsync<ModeratorRating>().Result;
        }

        public List<BaseRating> GetModeratorRatings()
        {
            var moderatorList = new List<BaseRating>();
            var moderatorListDb = this.firebaseClient.Child("ModeratorRatings").OnceAsync<BaseRating>().Result;

            foreach (var moderatorDb in moderatorListDb)
            {
                moderatorList.Add(new BaseRating { DateCreated = moderatorDb.Object.DateCreated, Key = moderatorDb.Key, Text = moderatorDb.Object.Text, Rating = moderatorDb.Object.Rating });
            }
            return moderatorList.OrderByDescending(x => x.DateCreated).ToList();
        }
    }
}
