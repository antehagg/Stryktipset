

using System;
using NUnit.Framework;
using Stryktipset.Core.Storage.JSON;

namespace Stryktipset.Core.Test.DataContext
{
    [TestFixture]
    public class FilterTests
    {
        private Data.DataContext _dataContext;

        [SetUp]
        public void Init()
        {
            _dataContext = new LoadDataJson().ReadData();
        }

        [Test]
        public void TestTurnOutFilter()
        {
            var filter = new Filter(_dataContext, DateTime.MinValue, DateTime.MaxValue, 0, 0, 450000, 900000);
            var filteredDc = Filter.GetContent();
            Assert.AreEqual(1, filteredDc.Weeks.Count);
        }

        [Test]
        public void TestDateTimeFilter()
        {
            var filter = new Filter(_dataContext, new DateTime(2017, 11, 10), new DateTime(2017, 11, 13));
            var filteredDc = Filter.GetContent();
            Assert.AreEqual(1, filteredDc.Weeks.Count);
        }

        [Test]
        public void TestPermilleWithNoValues()
        {
            var filter = new Filter(_dataContext, DateTime.MinValue, DateTime.MaxValue);
            var filteredDc = Filter.GetContent();
            Assert.AreEqual(3, filteredDc.Weeks.Count);
        }

        [Test]
        public void TestPermilleWithHighValue()
        {
            var filter = new Filter(_dataContext, DateTime.MinValue, DateTime.MaxValue, 6000, 7000);
            var filteredDc = Filter.GetContent();
            Assert.AreEqual(0, filteredDc.Weeks.Count);
        }

        [Test]
        public void TestPermilleWithLowValue()
        {
            var filter = new Filter(_dataContext, DateTime.MinValue, DateTime.MaxValue, 1000, 3000);
            var filteredDc = Filter.GetContent();
            Assert.AreEqual(0, filteredDc.Weeks.Count);
        }

        [Test]
        public void TestPermilleWithTwoValues()
        {
            var filter = new Filter(_dataContext, DateTime.MinValue, DateTime.MaxValue, 3000, 6000);
            var filteredDc = Filter.GetContent();
            Assert.AreEqual(3, filteredDc.Weeks.Count);
        }
    }
}