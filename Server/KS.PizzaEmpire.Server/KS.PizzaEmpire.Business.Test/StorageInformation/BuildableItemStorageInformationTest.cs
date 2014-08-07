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
    public class BuildableItemStorageInformationTest
    {
        public BuildableItemStorageInformation storageInfo;
        public BuildableItem bitem;

        [TestInitialize]
        public void Initialize()
        {
            storageInfo = new BuildableItemStorageInformation(BuildableItemEnum.White_Flour.ToString());

            bitem = new BuildableItem
            {
                ItemCode = BuildableItemEnum.White_Pizza_Dough,
                RequiredLevel = 2,
                CoinCost = 50,
                ProductionItem = BuildableItemEnum.Dough_Mixer_L1,
                ProductionCapacity = 0,
                BaseProduction = 1,
                StorageCapacity = 0,
                StorageItem = BuildableItemEnum.Fridge_L1,
                IsStorage = false,
                IsConsumable = true,
                IsImmediate = false,
                IsWorkSubtracted = false,
                Experience = 100,
                BuildSeconds = 60,
                CouponCost = 0,
                SpeedUpCoupons = 1,
                SpeedUpSeconds = 60,
                RequiredItems = new List<ItemQuantity>
                {
                    new ItemQuantity 
                    {
                         ItemCode = BuildableItemEnum.White_Flour,
                         Quantity = 1
                    },
                    new ItemQuantity
                    {
                         ItemCode = BuildableItemEnum.Salt,
                         Quantity = 1
                    },
                    new ItemQuantity
                    {
                         ItemCode = BuildableItemEnum.Yeast,
                         Quantity = 1                    
                    }
                }
            };
        }

        [TestMethod]
        public void TestInstantiate()
        {
            Assert.AreEqual("White_Flour", storageInfo.UniqueKey);
            Assert.AreEqual("BuildableItem", storageInfo.TableName);
            Assert.AreEqual("Version1", storageInfo.PartitionKey);
            Assert.AreEqual("White_Flour", storageInfo.RowKey);
            Assert.AreEqual("BI_White_Flour", storageInfo.CacheKey);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void TestToCache()
        {
            storageInfo.ToCache(bitem);
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
            BuildableItemTableStorage ts = (BuildableItemTableStorage)storageInfo.ToTableStorage(bitem);

            Assert.AreEqual(17, ts.ItemCode);
            Assert.AreEqual(2, ts.RequiredLevel);
            Assert.AreEqual(50, ts.CoinCost);
            Assert.AreEqual(48, ts.ProductionItem);
            Assert.AreEqual(0, ts.ProductionCapacity);
            Assert.AreEqual(1, ts.BaseProduction);
            Assert.AreEqual(0, ts.StorageCapacity);
            Assert.AreEqual(50, ts.StorageItem);
            Assert.AreEqual(false, ts.IsStorage);
            Assert.AreEqual(true, ts.IsConsumable);
            Assert.AreEqual(false, ts.IsImmediate);
            Assert.AreEqual(false, ts.IsWorkSubtracted);
            Assert.AreEqual(100, ts.Experience);
            Assert.AreEqual(60, ts.BuildSeconds);
            Assert.AreEqual(0, ts.CouponCost);
            Assert.AreEqual(1, ts.SpeedUpCoupons);
            Assert.AreEqual(60, ts.SpeedUpSeconds);
            Assert.AreEqual(18, ts.RequiredItemsSerialized.Length);            
        }

        [TestMethod]
        public void TestFromTableStorage()
        {
            BuildableItemTableStorage ts = (BuildableItemTableStorage)storageInfo.ToTableStorage(bitem);
            BuildableItem flip = (BuildableItem)storageInfo.FromTableStorage(ts);

            Assert.AreEqual(BuildableItemEnum.White_Pizza_Dough, flip.ItemCode);
            Assert.AreEqual(2, flip.RequiredLevel);
            Assert.AreEqual(50, flip.CoinCost);
            Assert.AreEqual(BuildableItemEnum.Dough_Mixer_L1, flip.ProductionItem);
            Assert.AreEqual(0, flip.ProductionCapacity);
            Assert.AreEqual(1, flip.BaseProduction);
            Assert.AreEqual(0, flip.StorageCapacity);
            Assert.AreEqual(BuildableItemEnum.Fridge_L1, flip.StorageItem);
            Assert.AreEqual(false, flip.IsStorage);
            Assert.AreEqual(true, flip.IsConsumable);
            Assert.AreEqual(false, flip.IsImmediate);
            Assert.AreEqual(false, flip.IsWorkSubtracted);
            Assert.AreEqual(100, flip.Experience);
            Assert.AreEqual(60, flip.BuildSeconds);
            Assert.AreEqual(0, flip.CouponCost);
            Assert.AreEqual(1, flip.SpeedUpCoupons);
            Assert.AreEqual(60, flip.SpeedUpSeconds);
            Assert.AreEqual(3, flip.RequiredItems.Count);
            Assert.AreEqual(BuildableItemEnum.White_Flour, flip.RequiredItems[0].ItemCode);
            Assert.AreEqual(1, flip.RequiredItems[0].Quantity);
            Assert.AreEqual(BuildableItemEnum.Salt, flip.RequiredItems[1].ItemCode);
            Assert.AreEqual(1, flip.RequiredItems[1].Quantity);
            Assert.AreEqual(BuildableItemEnum.Yeast, flip.RequiredItems[2].ItemCode);
            Assert.AreEqual(1, flip.RequiredItems[2].Quantity);
        }        
    }
}