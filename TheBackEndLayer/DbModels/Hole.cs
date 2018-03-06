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
            PlayerScores = new HashSet<Scores>();
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public ICollection<Scores> PlayerScores { get; set; }
    }
}
