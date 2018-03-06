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
            PlayerScore = new HashSet<Scores>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Scores> PlayerScore { get; set; }
    }
}