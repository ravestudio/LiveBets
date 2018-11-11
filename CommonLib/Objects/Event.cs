using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLib.Objects
{
    public class Event
    {
        public int id { get; set; }
        public int gameId { get; set; }
        public string eventTitle { get; set; }
        public string status { get; set; }
        public string team1 { get; set; }
        public string team2 { get; set; }

        public string matchTime { get; set; }

        public IList<Outcomes> outcomesWinner { get; set; }
        public IList<Scores> scores { get; set; } 

    }

    public class Outcomes
    {
        public string name { get; set; }
        public decimal? win1 { get; set; }
        public decimal? win2 { get; set; }
        public decimal? X { get; set; }
    }

    public class Scores
    {
        public string name { get; set; }
        public int team1 { get; set; }
        public int team2 { get; set; }
    }


}
