using System;
using System.Collections.Generic;

namespace Stryktipset.Core.Data
{
    public class Rank
    {
        public int[,] RankList;

        public Rank(int[,] rankList)
        {
            RankList = rankList;
        }

        public bool Validate()
        {
            var allExistValidated = true;
            var allExist = new bool[39];

            for (var i = 0; i <= 12; i++)
            {
                for (var j = 0; j <= 2; j++)
                {
                    if (RankList[i, j] >= 1 && RankList[i, j] <= 39)
                    {
                        if (!allExist[RankList[i, j] - 1])
                        {
                            allExist[RankList[i, j] - 1] = true;
                        }
                        else
                        {
                            allExistValidated = false;
                            break;
                        }
                    }
                    else
                    {
                        allExistValidated = false;
                        break;
                    }
                }
            }
            return allExistValidated;
        }
    }
}