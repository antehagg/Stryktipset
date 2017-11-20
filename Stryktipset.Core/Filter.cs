using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stryktipset.Core.Data;

namespace Stryktipset.Core
{
    public class Filter
    {
        private static int _minTurnOut;
        private static int _maxTurnOut;
        private static DateTime _startDate;
        private static DateTime _endDate;
        private static DataContext _dc;
        private static int _minPermille;
        private static int _maxPermille;

        public Filter(DataContext dc, DateTime startDate, DateTime endDate, int minPermille = 0, int maxPermille = 0, int minTurnout = 0, int maxTurnout = 0)
        {
            _startDate = startDate;
            _endDate = endDate;
            _minPermille = minPermille;
            _maxPermille = maxPermille;
            _minTurnOut = minTurnout;
            _maxTurnOut = maxTurnout;
            _dc = dc;
        }

        public static DataContext GetContent()
        {
            var filteredWeeks = FilterTurnOut(_dc.Weeks);
            filteredWeeks = FilterOnDate(filteredWeeks);
            filteredWeeks = FilterOnPermille(filteredWeeks);

            return new DataContext(filteredWeeks);
        }

        private static List<Week> FilterTurnOut(List<Week> weeks)
        {
            var filteredWeeks = new List<Week>();

            if (_minTurnOut != 0 && _maxTurnOut != 0)
                filteredWeeks = weeks.Where(x => x.TurnOut >= _minTurnOut && x.TurnOut <= _maxTurnOut).ToList();
            else if(_minTurnOut != 0 && _maxTurnOut == 0)
                filteredWeeks = weeks.Where(x => x.TurnOut >= _minTurnOut).ToList();
            else if(_minTurnOut == 0 && _maxTurnOut != 0)
                filteredWeeks = weeks.Where(x => x.TurnOut <= _maxTurnOut).ToList();
            else if (_minTurnOut == 0 && _maxTurnOut == 0)
                filteredWeeks = weeks;

            return filteredWeeks;
        }

        private static List<Week> FilterOnDate(List<Week> weeks)
        {
            var filteredWeeks= weeks.Where(x => x.DateTime >= _startDate && x.DateTime <= _endDate).ToList();

            return filteredWeeks;
        }

        private static List<Week> FilterOnPermille(List<Week> weeks)
        {
            var filteredWeeks = new List<Week>();

            if (_minPermille != 0 && _maxPermille != 0)
                filteredWeeks = weeks.Where(x => x.TotalPermille >= _minPermille && x.TotalPermille <= _maxPermille).ToList();
            else if (_minPermille != 0 && _maxPermille == 0)
                filteredWeeks = weeks.Where(x => x.TotalPermille >= _minPermille).ToList();
            else if (_minPermille == 0 && _maxPermille != 0)
                filteredWeeks = weeks.Where(x => x.TotalPermille <= _maxPermille).ToList();
            else if (_minPermille == 0 && _maxPermille == 0)
                filteredWeeks = weeks;

            return filteredWeeks;
        }
    }
}
