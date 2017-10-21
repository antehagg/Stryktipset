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
        private Week _week;
        [SetUp]
        public void Init()
        {
            _storeDataAsJson = new StoreDataAsJson();
            _week = new Week(new Result(new List<string> { "1", "2", "X", "1", "X", "2", "1", "2", "X", "1", "1", "2", "X" }), new Permille(), new Rank(), DateTime.Now, 0, 0);
        }

        [Test]
        public void SaveDataShouldReturnTrueIfFileIsWritten()
        {
            var fileWritten = _storeDataAsJson.SaveData(_week);
            Assert.AreEqual(true, fileWritten);
        }
    }
}
