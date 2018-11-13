using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CommonLib.Analyzer
{
    public class DrawHockeyStrategy
    {
        public bool Check(Objects.Event _event)
        {
            bool res = true;

            //время больше 50м
            if (int.Parse(_event.matchTime) < 50)
                res = false;

            var score = _event.scores.SingleOrDefault(s => s.name == "total");

            //счет должен быть ничейным
            if (score == null || !(score.team1 == score.team2 && score.team1 > 0))
                res = false;

            var outcome_main = _event.outcomesWinner.SingleOrDefault(o => o.name == "main");
            var outcome_ot = _event.outcomesWinner.SingleOrDefault(o => o.name == "ot");

            //с ot не используем, и нужен высокий x
            if (outcome_ot != null || outcome_main == null || !(outcome_main.X.HasValue && outcome_main.X > 1.50m))
                res = false;

            return res;
        }
    }
}