using System.Collections.Generic;

namespace TheTowerAPI.Models
{
    public class Level
    {
        public string LevelName { get; set; }
        public ICollection<Record> Records { get; set; }
    }
}