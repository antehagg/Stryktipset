using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Stryktipset.Core.Data;

namespace Stryktipset.Core.Storage.JSON
{
    public class StoreDataAsJson : IStoreData
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public bool SaveData(DataContext dataContext)
        {
            try
            {
                //To get the location the assembly normally resides on disk or the install directory
                var path = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;

                //once you have the path you get the directory with:
                var directory = Path.GetDirectoryName(path);
                var basePath = new Uri(directory ?? throw new InvalidOperationException()).LocalPath;

                using (var file = File.CreateText(basePath + @"\JsonData.txt"))
                {
                    var json = JsonConvert.SerializeObject(dataContext);
                    var serializer = new JsonSerializer();
                    //serialize object directly into file stream
                    serializer.Serialize(file, dataContext);
                }
                return true;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return false;
            }
        }
    }
}
