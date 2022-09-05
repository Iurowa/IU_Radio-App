using Newtonsoft.Json;
using System;

namespace Iubh.RadioApp.Core.Models
{
    public class PlaylistObject
    {
        [JsonProperty(PropertyName="playlist")]
        public Playlist Playlist { get; set; }
    }

    public class Playlist
    {
        [JsonProperty(PropertyName="songs")]
        public Song[] Songs { get; set; }
    }

    public class Song
    {
        [JsonProperty(PropertyName = "artist")]
        public string Interpreter { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "start")]
        public DateTime Start { get; set; }
    }

}
