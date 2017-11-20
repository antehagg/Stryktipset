using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;

namespace Stryktipset.Core.Data
{
    public class Result
    {
        public string[] ResultList;

        public Result(string[] resultList)
        {
            ResultList = resultList;
        }

        public bool Validate()
        {
            return ResultList.Length == 13 &&
                   ResultList.All(s => !string.IsNullOrEmpty(s) && (s == "1" || s.ToLower() == "x" || s == "2"));
        }
    }
}
