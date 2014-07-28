namespace KS.PizzaEmpire.Business.Test.StorageInformation
{
    using Business.Cache;
    using Business.StorageInformation;
    using Business.TableStorage;
    using Common.BusinessObjects;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public class ExperienceLevelStorageInformationTest
    {
        public ExperienceLevelStorageInformation storageInfo;
        public ExperienceLevel level;

        [TestInitialize]
        public void Initialize()
        {
            storageInfo = new ExperienceLevelStorageInformation("1");

            level = new ExperienceLevel
            {
                ExperienceRequired = 100,
                Level = 1
            };
        }

        [TestMethod]
        public void TestInstantiate()
        {
            Assert.AreEqual("1", storageInfo.UniqueKey);
            Assert.AreEqual("ExperienceLevel", storageInfo.TableName);
            Assert.AreEqual("Version1", storageInfo.PartitionKey);
            Assert.AreEqual("1", storageInfo.RowKey);
            Assert.AreEqual("EL_1", storageInfo.CacheKey);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void TestToCache()
        {
            storageInfo.ToCache(level);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void TestFromCache()
        {
            ICacheEntity k = null;
            storageInfo.FromCache(k);
        }

        [TestMethod]
        public void TestToTableStorage()
        {
            ExperienceLevelTableStorage ts = (ExperienceLevelTableStorage)storageInfo.ToTableStorage(level);

            Assert.AreEqual(100, ts.ExperienceRequired);
            Assert.AreEqual(1, ts.Level);
        }

        [TestMethod]
        public void TestFromTableStorage()
        {
            ExperienceLevelTableStorage ts = (ExperienceLevelTableStorage)storageInfo.ToTableStorage(level);
            ExperienceLevel flip = (ExperienceLevel)storageInfo.FromTableStorage(ts);

            Assert.AreEqual(100, flip.ExperienceRequired);
            Assert.AreEqual(1, flip.Level);
        }        
    }
}
