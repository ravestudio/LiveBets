using System;
using System.Collections.Generic;
using System.Text;
using CommonLib.Objects;
using Newtonsoft.Json.Linq;

namespace EventCollector
{
    public class EventCreator
    {
        public Event CreateEvent(JToken token)
        {
            Event _event = new Event();
            _event.id = token["id"].ToObject<int>();
            _event.eventTitle = token["event"]["eventTitle"].ToString();
            _event.status = token["event"]["status"].ToString();
            _event.matchTime = token["event"]["matchTime"].ToString();
            _event.team1 = token["event"]["team1"].ToString();
            _event.team2 = token["event"]["team2"].ToString();

            var outcomes = token["outcomesWinner"]["main"]["outcomes"];
            _event.outcomesWinner = new List<Outcomes>();
            _event.outcomesWinner.Add(new Outcomes()
            {
                name = "main",
                win1 = outcomes["_1"] != null ? outcomes["_1"]["value"].ToObject<decimal?>() : null,
                win2 = outcomes["_2"] != null ? outcomes["_2"]["value"].ToObject<decimal?>() : null,
                X = outcomes["x"] != null ? outcomes["x"]["value"].ToObject<decimal?>() : null,

            });

            var scores = token["scores"]["total"];
            _event.scores = new List<Scores>();
            _event.scores.Add(new Scores()
            {
                name = "total",
                team1 = scores["ScoreTeam1"].ToObject<int>(),
                team2 = scores["ScoreTeam2"].ToObject<int>()
            });

            return _event;
        }
    }
}
