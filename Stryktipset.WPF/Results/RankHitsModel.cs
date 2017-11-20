using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Stryktipset.Core.Data;

namespace Stryktipset.WPF.Results
{
    public class RankHitsModel
    {
        public ObservableCollection<RankHitRow> RankHitRows;
        public int TotalWeeks;
        public int[] RankHits;
        public int[] RankPercent;

        public RankHitsModel(int totalWeeks, List<Week> weeks)
        {
            TotalWeeks = totalWeeks;
            RankHits = new int[39];
            RankPercent = new int[39];
            CalculateRankHits(weeks);
            CalculatePercent();
            RankHitRows = new ObservableCollection<RankHitRow>();
            BuildRankHitRows();
        }

        private void BuildRankHitRows()
        {
            for (var i = 0; i <= 38; i++)
            {
                var rankHitRow = new RankHitRow
                {
                    Rank = i+1,
                    Hits = RankHits[i],
                    Percent = RankPercent[i],
                    Weeks = TotalWeeks
                };
                RankHitRows.Add(rankHitRow);
            }
        }

        private void CalculatePercent()
        {
            for (var i = 0; i < RankHits.Length; i++)
            {
                var percent = RankHits[i] / TotalWeeks * 100;
                RankPercent[i] = percent;
            }
        }

        private void CalculateRankHits(List<Week> weeks)
        {
            foreach (var w in weeks)
            {
                for (var i = 0; i < w.Result.ResultList.Length; i++)
                {
                    switch (w.Result.ResultList[i])
                    {
                        case "1":
                            RankHits[w.Rank.RankList[i, 0]]++;
                            break;
                        case "x":
                            RankHits[w.Rank.RankList[i, 1]]++;
                            break;
                        case "2":
                            RankHits[w.Rank.RankList[i, 2]]++;
                            break;
                    }
                }
            }
        }
    }

    public class RankHitRow : INotifyPropertyChanged
    {
        public int Rank { get; set; }
        public int Weeks { get; set; }
        public int Hits { get; set; }
        public int Percent { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
