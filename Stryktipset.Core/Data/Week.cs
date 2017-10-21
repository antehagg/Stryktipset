using System;
using Newtonsoft.Json;

namespace Stryktipset.Core.Data
{
    public class Week
    {
        [JsonProperty]
        public Result Result;
        public Permille Permille;
        public Rank Rank;
        public DateTime DateTime;
        public int TurnOut;
        public int TotalPermille;

        public Week()
        {
            
        }

        public Week(Result result, Permille permille, Rank rank, DateTime dateTime, int turnOut, int totalPermille)
        {
            Permille = permille;
            Rank = rank;
            DateTime = dateTime;
            TurnOut = turnOut;
            TotalPermille = totalPermille;
            Result = result;
        }
    }
}
