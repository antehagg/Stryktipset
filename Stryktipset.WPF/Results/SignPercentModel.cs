using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stryktipset.Core.Data;

namespace Stryktipset.WPF.Results
{
    public class SignPercentModel
    {
        public ObservableCollection<SignPercentRow> SignPercentRows;
        private int[] _oneHit;
        private int[] _xHit;
        private int[] _twoHit;

        public SignPercentModel(int totalWeeks, List<Week> weeks)
        {
            SignPercentRows = new ObservableCollection<SignPercentRow>();
            _oneHit = new int[13];
            _xHit = new int[13];
            _twoHit = new int[13];

            PopulateArrays(weeks);
            BuildSignExactRows(totalWeeks);
        }

        private void BuildSignExactRows(int totalWeeks)
        {
            for (var i = 0; i <= 12; i++)
            {
                var row = new SignPercentRow
                {
                    N = i + 1,
                    One = _oneHit[i] / totalWeeks * 100,
                    X = _xHit[i] / totalWeeks * 100,
                    Two = _twoHit[i] / totalWeeks * 100
                };
                SignPercentRows.Add(row);
            }
        }

        private void PopulateArrays(List<Week> weeks)
        {
            foreach (var w in weeks)
            {
                var totalOne = 0;
                var totalX = 0;
                var totalTwo = 0;

                foreach (var r in w.Result.ResultList)
                {
                    if (r == "1")
                        totalOne++;
                    if (r == "x")
                        totalX++;
                    if (r == "2")
                        totalTwo++;
                }
                _oneHit[totalOne - 1]++;
                _xHit[totalX - 1]++;
                _twoHit[totalTwo - 1]++;
            }
        }
    }

    public class SignPercentRow
    {
        public int N { get; set; }
        public int One { get; set; }
        public int X { get; set; }
        public int Two { get; set; }

    }
}
