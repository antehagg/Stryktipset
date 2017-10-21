using System;
using System.IO;
using Newtonsoft.Json;
using Stryktipset.Core.Data;

namespace Stryktipset.Core.Storage.JSON
{
    public class StoreDataAsJson : IStoreData
    {
        public bool SaveData(Week week)
        {
            try
            {
                //To get the location the assembly normally resides on disk or the install directory
                var path = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;

                //once you have the path you get the directory with:
                var directory = Path.GetDirectoryName(path);
                var basePath = new Uri(directory ?? throw new InvalidOperationException()).LocalPath;

                using (var file = File.CreateText(basePath + @"JsonData.txt"))
                {
                    var serializer = new JsonSerializer();
                    //serialize object directly into file stream
                    serializer.Serialize(file, week);
                }
                return true;
            }
            catch (Exception e)
            {
                var error = e.Message;
                // TODO: Log error here
                return false;
            }
        }
    }
}
