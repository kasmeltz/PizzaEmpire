namespace KS.PizzaEmpire.Business.Test.StorageInformation
{
    using Automapper;
    using AutoMapper;
    using Business.StorageInformation;
    using Cache;
    using Common.BusinessObjects;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Services.Storage;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TableStorage;

    [TestClass]
    public class ConsumableItemStorageInformationTest
    {
        public BuildableItemStorageInformation storageInfo;
        public ConsumableItem bitem;

        [ClassInitialize]
        public static void InitAllTests(TestContext testContext)
        {
            AutoMapperConfiguration.Configure();
            Mapper.AssertConfigurationIsValid();
        }

        [TestInitialize]
        public void Initialize()
        {
            storageInfo = new BuildableItemStorageInformation(BuildableItemEnum.White_Flour.ToString());

            bitem = new ConsumableItem
            {
                ItemCode = BuildableItemEnum.White_Pizza_Dough,
                Stats = new List<BuildableItemStat>
                {
                    new BuildableItemStat
                    {
                        RequiredLevel = 2,
                        CoinCost = 50,
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
                                StoredQuantity = 1
                            },
                            new ItemQuantity
                            {
                                ItemCode = BuildableItemEnum.Salt,
                                StoredQuantity = 1
                            },
                            new ItemQuantity
                            {
                                ItemCode = BuildableItemEnum.Yeast,
                                StoredQuantity = 1                    
                            }
                        }
                    }
                },
                ConsumableStats = new List<ConsumableItemStat>
                {
                    new ConsumableItemStat
                    {
                        ProductionQuantity = 2
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

            Assert.AreEqual("Version1", ts.PartitionKey);
            Assert.AreEqual("White_Flour", ts.RowKey);
            Assert.AreEqual(15, ts.ItemCode);
            Assert.AreEqual(32, ts.Stats.Length);
            Assert.AreEqual(4, ts.ConsumableStats.Length);
        }

        [TestMethod]
        public void TestFromTableStorage()
        {
            BuildableItemTableStorage ts = (BuildableItemTableStorage)storageInfo.ToTableStorage(bitem);
            ConsumableItem flip = (ConsumableItem)storageInfo.FromTableStorage(ts);

            Assert.AreEqual(BuildableItemEnum.White_Pizza_Dough, flip.ItemCode);
            Assert.AreEqual(1, flip.Stats.Count);
            Assert.AreEqual(2, flip.Stats[0].RequiredLevel);
            Assert.AreEqual(50, flip.Stats[0].CoinCost);
            Assert.AreEqual(100, flip.Stats[0].Experience);
            Assert.AreEqual(60, flip.Stats[0].BuildSeconds);
            Assert.AreEqual(0, flip.Stats[0].CouponCost);
            Assert.AreEqual(1, flip.Stats[0].SpeedUpCoupons);
            Assert.AreEqual(60, flip.Stats[0].SpeedUpSeconds);
            Assert.AreEqual(3, flip.Stats[0].RequiredItems.Count);
            Assert.AreEqual(BuildableItemEnum.White_Flour, flip.Stats[0].RequiredItems[0].ItemCode);
            Assert.AreEqual(1, flip.Stats[0].RequiredItems[0].StoredQuantity);
            Assert.AreEqual(BuildableItemEnum.Salt, flip.Stats[0].RequiredItems[1].ItemCode);
            Assert.AreEqual(1, flip.Stats[0].RequiredItems[1].StoredQuantity);
            Assert.AreEqual(BuildableItemEnum.Yeast, flip.Stats[0].RequiredItems[2].ItemCode);
            Assert.AreEqual(1, flip.Stats[0].RequiredItems[2].StoredQuantity);
            Assert.AreEqual(1, flip.ConsumableStats.Count);
            Assert.AreEqual(2, flip.ConsumableStats[0].ProductionQuantity);
        }

        [TestMethod]
        public async Task TestTableStorageServiceRoundTrip()
        {
            BuildableItemTableStorage ts = (BuildableItemTableStorage)storageInfo.ToTableStorage(bitem);
            
            AzureTableStorage storage = new AzureTableStorage();
            await storage.SetTable(storageInfo.TableName);
            await storage.InsertOrReplace<BuildableItemTableStorage>(ts);

            BuildableItemTableStorage storageItem = await storage.Get<BuildableItemTableStorage>(storageInfo.PartitionKey, storageInfo.RowKey);

            ConsumableItem flip = (ConsumableItem)storageInfo.FromTableStorage(storageItem); 

            Assert.AreEqual(BuildableItemEnum.White_Pizza_Dough, flip.ItemCode);
            Assert.AreEqual(1, flip.Stats.Count);
            Assert.AreEqual(2, flip.Stats[0].RequiredLevel);
            Assert.AreEqual(50, flip.Stats[0].CoinCost);
            Assert.AreEqual(100, flip.Stats[0].Experience);
            Assert.AreEqual(60, flip.Stats[0].BuildSeconds);
            Assert.AreEqual(0, flip.Stats[0].CouponCost);
            Assert.AreEqual(1, flip.Stats[0].SpeedUpCoupons);
            Assert.AreEqual(60, flip.Stats[0].SpeedUpSeconds);
            Assert.AreEqual(3, flip.Stats[0].RequiredItems.Count);
            Assert.AreEqual(BuildableItemEnum.White_Flour, flip.Stats[0].RequiredItems[0].ItemCode);
            Assert.AreEqual(1, flip.Stats[0].RequiredItems[0].StoredQuantity);
            Assert.AreEqual(BuildableItemEnum.Salt, flip.Stats[0].RequiredItems[1].ItemCode);
            Assert.AreEqual(1, flip.Stats[0].RequiredItems[1].StoredQuantity);
            Assert.AreEqual(BuildableItemEnum.Yeast, flip.Stats[0].RequiredItems[2].ItemCode);
            Assert.AreEqual(1, flip.Stats[0].RequiredItems[2].StoredQuantity);
            Assert.AreEqual(1, flip.ConsumableStats.Count);
            Assert.AreEqual(2, flip.ConsumableStats[0].ProductionQuantity);
        }        
    }
}