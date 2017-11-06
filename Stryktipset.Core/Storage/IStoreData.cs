using System.Collections.Generic;
using Stryktipset.Core.Data;

namespace Stryktipset.Core.Storage
{
    interface IStoreData
    {
        bool SaveData(DataContext dataContext);
    }
}