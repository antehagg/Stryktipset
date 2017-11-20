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
        public DateTime? DateTime;
        public int? TurnOut;
        public int TotalPermille;

        public Week()
        {
            
        }

        public Week(Result result, Permille permille, Rank rank, DateTime? dateTime, int? turnOut)
        {
            Permille = permille;
            Rank = rank;
            DateTime = dateTime;
            TurnOut = turnOut;
            Result = result;
        }

        private int CalculateTotalPermille()
        {
            var totalPermille = 0;
            for (var i = 0; i <= 12; i++)
            {
                var result = Result.ResultList[i];
                if (result == "1")
                    totalPermille += Permille.PermilleList[i, 0];
                else if (result.ToLower() == "x")
                    totalPermille += Permille.PermilleList[i, 1];
                else if (result == "2")
                    totalPermille += Permille.PermilleList[i, 2];
            }
            return totalPermille * 10;
        }

        public bool ValidateWeekAndSave(out string errorMessage)
        {
            errorMessage = null;

            var resultValidated = ValidateResult(out var resultErrorMessage);
            var permilleValidated = ValidatePermille(out var  permilleErrorMessage);
            var rankValidated = ValidateRank(out var rankErrorMessage);
            var turnoutValidated = ValidateTurnout(out var turnoutErrorMessage);
            var dateValidated = ValidateDate(out var dateErrorMessage);

            if (resultValidated && permilleValidated && rankValidated && turnoutValidated && dateValidated)
            {
                TotalPermille = CalculateTotalPermille();
                return true;
            }

            errorMessage = resultErrorMessage + "\n" + permilleErrorMessage + "\n" + 
                rankErrorMessage + "\n" + turnoutErrorMessage + "\n" + dateErrorMessage; 

            return false;
        }

        private bool ValidateDate(out string dateErrorMessage)
        {
            if (DateTime > System.DateTime.Now)
            {
                dateErrorMessage = "Datefield are not correct!";
                return false;
            }

            dateErrorMessage = "";
            return true;
        }

        private bool ValidateTurnout(out string turnoutErrorMessage)
        {
            turnoutErrorMessage = null;

            var validated = TurnOut > 0;

            if (!validated)
            {
                turnoutErrorMessage = "Turnoutfield are not correct!";
            }

            return validated;
        }

        private bool ValidateRank(out string rankErrorMessage)
        {
            rankErrorMessage = null;

            var validated = Rank.Validate();

            if (!validated)
            {
                rankErrorMessage = "Rankfields are not correct!";
            }

            return validated;
        }

        private bool ValidatePermille(out string permilleErrorMessage)
        {
            permilleErrorMessage = null;

            var validated = Permille.Validate();

            if (!validated)
            {
                permilleErrorMessage = "Permillefields are not correct!";
            }

            return validated;
        }

        private bool ValidateResult(out string resultErrorMessage)
        {
            resultErrorMessage = null;

            var validated = Result.Validate();

            if (!validated)
            {
                resultErrorMessage = "Resultfields are not correct!";
            }

            return validated;
        }
    }
}
