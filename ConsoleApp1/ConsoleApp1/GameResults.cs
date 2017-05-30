using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class GameResults
    {
        public DateTime GameDate { get; set; }
        public String TeamName { get; set; }
        public HomeOrAway HomeOrAway { get; set; }
        public int Goals { get; set; }

        public int GoalAttempts { get; set; }

        public int ShotsOnGoal { get; set; }

        public int ShotsOffGoal { get; set; }

        public Double PossessionPercent { get; set; }

        public Double ConversionRate
        {
            get
            {
                return (double)Goals / (double)GoalAttempts;
            }
        }
    }

    public enum HomeOrAway
    {
        Home,
        Away
    }
}
