using System.Collections.Generic;
using System.Threading.Tasks;
using Iubh.RadioApp.Data.Models;

namespace Iubh.RadioApp.Data.Database
{
    public interface IDb
    {
        bool IsModeratorLoginSuccessfully(string mail, string password);

        void AddWish(Wish wish);

        Wish GetWish(string key);

        List<Wish> GetWishes();

        void RemoveWish(string key);

        void AddPlaylistRate(PlaylistRating rate);

        PlaylistRating GetPlaylistRate(string key);

        List<BaseRating> GetPlaylistRatings();

        void AddModeratorRate(ModeratorRating rate);

        ModeratorRating GetModeratorRate(string key);

        List<BaseRating> GetModeratorRatings();
    }
}
