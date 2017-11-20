using System.Collections.Generic;
using System.Linq;

namespace Stryktipset.Core.Data
{
    public class Permille
    {
        public int[,] PermilleList;

        public Permille(int[,] permilleList)
        {
            PermilleList = permilleList;
        }

        public bool Validate()
        {
            var totalValidated = true;
            for (var i = 0; i <= 12; i++)
            {
                var total = 0;
                for (var j = 0; j <= 2; j++)
                {
                    if(PermilleList[i, j] >= 1 && PermilleList[i, j] <= 99)
                        total += PermilleList[i, j];
                    else
                    {
                        totalValidated = false;
                        break;
                    }
                }

                if (total == 100)
                    continue;

                totalValidated = false;
                break;
            }
            return totalValidated;
        }
    }
}