using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBackEndLayer.DbModels
{
  public   class Hole
    {
        public Hole()
        {
            PlayerScores = new HashSet<PlayerScores>();
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public ICollection<PlayerScores> PlayerScores { get; set; }
    }
}
