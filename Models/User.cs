using System.Collections.Generic;
using Newtonsoft.Json;

namespace TheTowerAPI.Models
{
    public class User
    {
        public string Nickname { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public string Email { get; set; }
        public int Role { get; set; }
        [JsonIgnore]
        public ICollection<Record> Records { get; set; }
    }
}