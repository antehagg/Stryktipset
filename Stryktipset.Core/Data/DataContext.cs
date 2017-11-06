using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stryktipset.Core.Data
{
    public class DataContext
    {
        public List<Week> Weeks { get; }

        public DataContext(List<Week> weeks)
        {
            Weeks = weeks;
        }

        public int GetWeekCount()
        {
            return Weeks.Count;
        }

        public void AddWeek(Week week)
        {
            Weeks.Add(week);
        }
    }
}
