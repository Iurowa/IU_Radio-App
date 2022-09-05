using System;
using System.Collections.Generic;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Iubh.RadioApp.Data.Models
{
    public class Configuration
    {
        [PrimaryKey]
        public Guid Id { get; set; }

        public string Value { get; set; }
    }
}
