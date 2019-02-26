using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheBackEndLayer.DbModels
{
    public class HandiCap
    {
        public HandiCap()
        {
            PlayerScore = new HashSet<PlayerScores>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<PlayerScores> PlayerScore { get; set; }
    }
}