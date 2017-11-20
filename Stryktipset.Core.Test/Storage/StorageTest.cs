using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Stryktipset.Core.Data;
using Stryktipset.Core.Storage.JSON;

namespace Stryktipset.Core.Test.Storage
{
    [TestFixture]
    public class StorageTest
    {
        private StoreDataAsJson _storeDataAsJson;
        private LoadDataJson _loadDataJson;

        [SetUp]
        public void Init()
        {
            _loadDataJson = new LoadDataJson();
            _storeDataAsJson = new StoreDataAsJson();

        }
    }
}
