using System;
using SQLite;

namespace Iubh.RadioApp.Data.Models
{
    public class Wish
    {
        public string Key { get; set; }

        public DateTime DateCreated { get; set; }

        public string MusicWish { get; set; }

        public string Name { get; set; }
    }
}
