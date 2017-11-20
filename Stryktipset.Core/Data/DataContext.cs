using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stryktipset.Core.Storage.JSON;

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

        public void Save()
        {
            var storeData = new StoreDataAsJson();
            storeData.SaveData(this);
        }
    }
}
