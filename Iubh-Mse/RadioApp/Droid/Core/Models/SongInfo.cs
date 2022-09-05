using Newtonsoft.Json;

namespace Iubh.RadioApp.Core.Models
{
    public class SongInfo
    {
        [JsonProperty(PropertyName="artist")]
        public string Interpreter { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
    }
}
