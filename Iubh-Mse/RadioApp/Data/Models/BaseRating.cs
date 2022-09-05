using SQLite;
using System;

namespace Iubh.RadioApp.Data.Models
{
    public class BaseRating
    {
        public string Key { get; set; }

        public DateTime DateCreated { get; set; }

        public int Rating { get; set; }

        public string Text { get; set; }
    }
}
