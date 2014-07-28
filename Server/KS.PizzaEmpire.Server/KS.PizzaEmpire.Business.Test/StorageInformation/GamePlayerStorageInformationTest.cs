namespace KS.PizzaEmpire.Business.Test.StorageInformation
{
    using Business.Cache;
    using Business.StorageInformation;
    using Business.TableStorage;
    using Common.BusinessObjects;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;

    [TestClass]
    public class GamePlayerStorageInformationTest
    {
        public GamePlayerStorageInformation storageInfo;
        public GamePlayer player;

        [TestInitialize]
        public void Initialize()
        {
            storageInfo = new GamePlayerStorageInformation("KEVIN");

            player = new GamePlayer
            {
                Coins = 99,
                Coupons = 66,
                Experience = 1000,
                Level = 4,
                StateChanged = false,
                WorkItems = new List<WorkItem>
                {
                    new WorkItem 
                    {
                         ItemCode = BuildableItemEnum.White_Flour,
                         FinishTime = DateTime.UtcNow
                    }
                },
                BuildableItems = new Dictionary<BuildableItemEnum, int>
                {
                    { BuildableItemEnum.White_Flour, 1 }, 
                    { BuildableItemEnum.Dry_Goods_Delivery_Truck_L1, 1 }
                }
            };
        }

        [TestMethod]
        public void TestInstantiate()
        {
            Assert.AreEqual("KEVIN", storageInfo.UniqueKey);
            Assert.AreEqual("GamePlayer", storageInfo.TableName);
            Assert.AreEqual("KE", storageInfo.PartitionKey);
            Assert.AreEqual("KEVIN", storageInfo.RowKey);
            Assert.AreEqual("GP_KEVIN", storageInfo.CacheKey);
        }

        [TestMethod]
        public void TestToCache()
        {
            GamePlayerCacheable cacheable = (GamePlayerCacheable)storageInfo.ToCache(player);

            Assert.AreEqual(99, cacheable.Coins);
            Assert.AreEqual(66, cacheable.Coupons);
            Assert.AreEqual(1000, cacheable.Experience);
            Assert.AreEqual(4, cacheable.Level);
            Assert.AreEqual(1, cacheable.WorkItems.Count);
            Assert.AreEqual(2, cacheable.BuildableItems.Count);
        }

        [TestMethod]
        public void TestFromCache()
        {
            GamePlayerCacheable cacheable = (GamePlayerCacheable)storageInfo.ToCache(player);
            GamePlayer flip = (GamePlayer)storageInfo.FromCache(cacheable);

            Assert.AreEqual(99, flip.Coins);
            Assert.AreEqual(66, flip.Coupons);
            Assert.AreEqual(1000, flip.Experience);
            Assert.AreEqual(4, flip.Level);
            Assert.AreEqual(1, flip.WorkItems.Count);
            Assert.AreEqual(2, flip.BuildableItems.Count);
        }

        [TestMethod]
        public void TestToTableStorage()
        {
            GamePlayerTableStorage ts = (GamePlayerTableStorage)storageInfo.ToTableStorage(player);

            Assert.AreEqual(99, ts.Coins);
            Assert.AreEqual(66, ts.Coupons);
            Assert.AreEqual(1000, ts.Experience);
            Assert.AreEqual(4, ts.Level);
            Assert.AreEqual(17, ts.WorkItemsSerialized.Length);
            Assert.AreEqual(12, ts.BuildableItemsSerialized.Length);
        }

        [TestMethod]
        public void TestFromTableStorage()
        {
            GamePlayerTableStorage ts = (GamePlayerTableStorage)storageInfo.ToTableStorage(player);
            GamePlayer flip = (GamePlayer)storageInfo.FromTableStorage(ts);

            Assert.AreEqual(99, flip.Coins);
            Assert.AreEqual(66, flip.Coupons);
            Assert.AreEqual(1000, flip.Experience);
            Assert.AreEqual(4, flip.Level);
            Assert.AreEqual(1, flip.WorkItems.Count);
            Assert.AreEqual(2, flip.BuildableItems.Count);
        }        
    }
}
