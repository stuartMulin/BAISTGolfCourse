using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBackEndLayer.ViewModels.Scores
{
   public  class PlayerScoreViewModel
    {
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string Score { get; set; }
        public string Handicap { get; set; }
        public string Date { get; set; }
    }
}
