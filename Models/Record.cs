using Newtonsoft.Json;

namespace TheTowerAPI.Models
{
    public class Record
    {
        public string Nickname { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        public string LevelName { get; set; }
        [JsonIgnore]
        public Level Level { get; set; }
        public long Time { get; set; }
        
    }
}