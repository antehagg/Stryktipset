using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net.Config;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Stryktipset.Core.Data;

namespace Stryktipset.Core.Storage.JSON
{
    public class LoadDataJson
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private string _path;

        public LoadDataJson()
        {
            //To get the location the assembly normally resides on disk or the install directory
            var path = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;

            //once you have the path you get the directory with:
            var directory = Path.GetDirectoryName(path);
            var basePath = new Uri(directory ?? throw new InvalidOperationException()).LocalPath;

            _path = basePath + @"\JsonData.txt";
        }

        public DataContext ReadData()
        {
            var dc = new DataContext(new List<Week>());
            try
            {
                using (var file = File.OpenText(_path))
                {
                    var serializer = new JsonSerializer();
                    dc = (DataContext)serializer.Deserialize(file, typeof(DataContext));
                }
            }
            catch (Exception e)
            {
               Log.Error(e.Message);
                return dc;
            }

            return dc;
        }
    }
}
