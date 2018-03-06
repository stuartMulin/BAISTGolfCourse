using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBackEndLayer.ViewModels.Scores
{
   public  class ScoreCard
    {
        public List<string> DaysOfWeeks { get { return FillDays(); } set { FillDays(); } }
        public List<double> DaysOfWeekScores { get; set; }
        public List<string> MonthsOfYears { get { return PopulateMonths(); } set { PopulateMonths(); } }
        public List<double> MonthsOfYearAverageScores { get; set; }
        public List<string> Years { get; set; }
        public List<double> YearAverageScores { get; set; }

        public List<string> FillDays()
        {
            var days = new List<string>();
            days.Add("Sunday");
            days.Add("Monday");
            days.Add("Tuesday");
            days.Add("Wednesday");
            days.Add("Thursday");
            days.Add("Friday");
            days.Add("Saturday");

            return days;
        }
        public List<string> PopulateMonths()
        {
            var months = new List<string>();
            months.Add("Jan");
            months.Add("Feb");
            months.Add("Mar");
            months.Add("April");
            months.Add("May");
            months.Add("Jun");
            months.Add("Jul");
            months.Add("Aug");
            months.Add("Sep");
            months.Add("Oct");
            months.Add("Nov");
            months.Add("Dec");

            return months;
        }
    }
}
