namespace KS.PizzaEmpire.Business.Test.StorageInformation
{
    using AutoMapper;
    using Business.Automapper;
    using Business.StorageInformation;
    using Common.BusinessObjects;
    using KS.PizzaEmpire.Business.Cache;
    using KS.PizzaEmpire.Business.TableStorage;
    using KS.PizzaEmpire.Services.Caching;
    using KS.PizzaEmpire.Services.Storage;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [TestClass]
    public class GamePlayerStorageInformationTest
    {
        [ClassInitialize]
        public static void InitAllTests(TestContext testContext)
        {
            AutoMapperConfiguration.Configure();
            Mapper.AssertConfigurationIsValid();
        }

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
                WorkInProgress = new List<WorkInProgress>
                {
                    new WorkInProgress 
                    {
                        Quantity = new ItemQuantity                        
                        {
                            ItemCode = BuildableItemEnum.Dough_Mixer,
                            Level = 1,
                            UnStoredQuantity = 2
                        },
                        Location = 0,                       
                        FinishTime = DateTime.UtcNow
                    }
                },        
                Locations = new List<BusinessLocation>
                {
                    new BusinessLocation
                    {
                        Storage = new LocationStorage
                        { 
                            Items = new Dictionary<BuildableItemEnum,ItemQuantity>
                            {
                                { 
                                    BuildableItemEnum.White_Flour, new ItemQuantity
                                    {
                                        ItemCode = BuildableItemEnum.White_Flour,
                                        Level = 0,
                                        UnStoredQuantity = 1,
                                        StoredQuantity = 2
                                    }
                                },
                                {
                                    BuildableItemEnum.Dirty_Dishes, new ItemQuantity
                                    {
                                        ItemCode = BuildableItemEnum.Dirty_Dishes,
                                        Level = 0,
                                        UnStoredQuantity = 2,
                                        StoredQuantity = 0
                                    }
                                }
                            }
                        }
                    }
                },
                TutorialStage = 10
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
            GamePlayerCacheable ts = (GamePlayerCacheable)storageInfo.ToCache(player);

            Assert.AreEqual(99, ts.Coins);
            Assert.AreEqual(66, ts.Coupons);
            Assert.AreEqual(1000, ts.Experience);
            Assert.AreEqual(4, ts.Level);
            Assert.AreEqual(10, ts.TutorialStage);
            Assert.AreEqual(false, ts.StateChanged);
            Assert.AreEqual(0, ts.WorkInProgress[0].Location);
            Assert.AreEqual(BuildableItemEnum.Dough_Mixer, ts.WorkInProgress[0].Quantity.ItemCode);
            Assert.AreEqual(1, ts.WorkInProgress[0].Quantity.Level);
            Assert.AreEqual(0, ts.WorkInProgress[0].Quantity.StoredQuantity);
            Assert.AreEqual(2, ts.WorkInProgress[0].Quantity.UnStoredQuantity);
            Assert.AreEqual(1, ts.Locations.Count);
            Assert.AreEqual(2, ts.Locations[0].Storage.Items.Count);
            Assert.AreEqual(BuildableItemEnum.White_Flour, ts.Locations[0].Storage.Items[BuildableItemEnum.White_Flour].ItemCode);
            Assert.AreEqual(0, ts.Locations[0].Storage.Items[BuildableItemEnum.White_Flour].Level);
            Assert.AreEqual(2, ts.Locations[0].Storage.Items[BuildableItemEnum.White_Flour].StoredQuantity);
            Assert.AreEqual(1, ts.Locations[0].Storage.Items[BuildableItemEnum.White_Flour].UnStoredQuantity);
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
            Assert.AreEqual(1, flip.WorkInProgress.Count);
            Assert.AreEqual(0, flip.WorkInProgress[0].Location);
            Assert.AreEqual(BuildableItemEnum.Dough_Mixer, flip.WorkInProgress[0].Quantity.ItemCode);
            Assert.AreEqual(1, flip.WorkInProgress[0].Quantity.Level);
            Assert.AreEqual(0, flip.WorkInProgress[0].Quantity.StoredQuantity);
            Assert.AreEqual(2, flip.WorkInProgress[0].Quantity.UnStoredQuantity);
            Assert.AreEqual(1, flip.Locations.Count);
            Assert.AreEqual(2, flip.Locations[0].Storage.Items.Count);
            Assert.AreEqual(BuildableItemEnum.White_Flour, flip.Locations[0].Storage.Items[BuildableItemEnum.White_Flour].ItemCode);
            Assert.AreEqual(0, flip.Locations[0].Storage.Items[BuildableItemEnum.White_Flour].Level);
            Assert.AreEqual(2, flip.Locations[0].Storage.Items[BuildableItemEnum.White_Flour].StoredQuantity);
            Assert.AreEqual(1, flip.Locations[0].Storage.Items[BuildableItemEnum.White_Flour].UnStoredQuantity);
            Assert.AreEqual(10, flip.TutorialStage);
        }

        [TestMethod]
        public async Task TestCacheServiceRoundTrip()
        {
            GamePlayerCacheable cacheable = (GamePlayerCacheable)storageInfo.ToCache(player);

            await RedisCache.Instance
                    .Set<GamePlayerCacheable>(storageInfo.CacheKey,
                    (GamePlayerCacheable)storageInfo.ToCache(player), 
                    TimeSpan.FromSeconds(60));

            GamePlayerCacheable cachedItem = await RedisCache.Instance.Get<GamePlayerCacheable>(storageInfo.CacheKey);
            GamePlayer flip = (GamePlayer)storageInfo.FromCache(cachedItem);

            Assert.AreEqual(99, flip.Coins);
            Assert.AreEqual(66, flip.Coupons);
            Assert.AreEqual(1000, flip.Experience);
            Assert.AreEqual(4, flip.Level);
            Assert.AreEqual(1, flip.WorkInProgress.Count);
            Assert.AreEqual(0, flip.WorkInProgress[0].Location);
            Assert.AreEqual(BuildableItemEnum.Dough_Mixer, flip.WorkInProgress[0].Quantity.ItemCode);
            Assert.AreEqual(1, flip.WorkInProgress[0].Quantity.Level);
            Assert.AreEqual(0, flip.WorkInProgress[0].Quantity.StoredQuantity);
            Assert.AreEqual(2, flip.WorkInProgress[0].Quantity.UnStoredQuantity);
            Assert.AreEqual(1, flip.Locations.Count);
            Assert.AreEqual(2, flip.Locations[0].Storage.Items.Count);
            Assert.AreEqual(BuildableItemEnum.White_Flour, flip.Locations[0].Storage.Items[BuildableItemEnum.White_Flour].ItemCode);
            Assert.AreEqual(0, flip.Locations[0].Storage.Items[BuildableItemEnum.White_Flour].Level);
            Assert.AreEqual(2, flip.Locations[0].Storage.Items[BuildableItemEnum.White_Flour].StoredQuantity);
            Assert.AreEqual(1, flip.Locations[0].Storage.Items[BuildableItemEnum.White_Flour].UnStoredQuantity);
            Assert.AreEqual(10, flip.TutorialStage);
        }       

        [TestMethod]
        public void TestToTableStorage()
        {
            GamePlayerTableStorage ts = (GamePlayerTableStorage)storageInfo.ToTableStorage(player);

            Assert.AreEqual(99, ts.Coins);
            Assert.AreEqual(66, ts.Coupons);
            Assert.AreEqual(1000, ts.Experience);
            Assert.AreEqual(4, ts.Level);
            Assert.AreEqual(23, ts.WorkInProgress.Length);
            Assert.AreEqual(26, ts.Locations.Length);
            Assert.AreEqual(10, ts.TutorialStage);
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
            Assert.AreEqual(1, flip.WorkInProgress.Count);
            Assert.AreEqual(0, flip.WorkInProgress[0].Location);
            Assert.AreEqual(BuildableItemEnum.Dough_Mixer, flip.WorkInProgress[0].Quantity.ItemCode);
            Assert.AreEqual(1, flip.WorkInProgress[0].Quantity.Level);
            Assert.AreEqual(0, flip.WorkInProgress[0].Quantity.StoredQuantity);
            Assert.AreEqual(2, flip.WorkInProgress[0].Quantity.UnStoredQuantity);
            Assert.AreEqual(1, flip.Locations.Count);
            Assert.AreEqual(2, flip.Locations[0].Storage.Items.Count);
            Assert.AreEqual(BuildableItemEnum.White_Flour, flip.Locations[0].Storage.Items[BuildableItemEnum.White_Flour].ItemCode);
            Assert.AreEqual(0, flip.Locations[0].Storage.Items[BuildableItemEnum.White_Flour].Level);
            Assert.AreEqual(2, flip.Locations[0].Storage.Items[BuildableItemEnum.White_Flour].StoredQuantity);
            Assert.AreEqual(1, flip.Locations[0].Storage.Items[BuildableItemEnum.White_Flour].UnStoredQuantity);
            Assert.AreEqual(10, flip.TutorialStage);
        }

        [TestMethod]
        public async Task TestTableStorageServiceRoundTrip()
        {
            GamePlayerTableStorage ts = (GamePlayerTableStorage)storageInfo.ToTableStorage(player);

            AzureTableStorage storage = new AzureTableStorage();
            await storage.SetTable(storageInfo.TableName);
            await storage.InsertOrReplace<GamePlayerTableStorage>(ts);

            GamePlayerTableStorage storageItem = await storage.Get<GamePlayerTableStorage>(storageInfo.PartitionKey, storageInfo.RowKey);

            GamePlayer flip = (GamePlayer)storageInfo.FromTableStorage(storageItem);

            Assert.AreEqual(99, flip.Coins);
            Assert.AreEqual(66, flip.Coupons);
            Assert.AreEqual(1000, flip.Experience);
            Assert.AreEqual(4, flip.Level);
            Assert.AreEqual(1, flip.WorkInProgress.Count);
            Assert.AreEqual(0, flip.WorkInProgress[0].Location);
            Assert.AreEqual(BuildableItemEnum.Dough_Mixer, flip.WorkInProgress[0].Quantity.ItemCode);
            Assert.AreEqual(1, flip.WorkInProgress[0].Quantity.Level);
            Assert.AreEqual(0, flip.WorkInProgress[0].Quantity.StoredQuantity);
            Assert.AreEqual(2, flip.WorkInProgress[0].Quantity.UnStoredQuantity);
            Assert.AreEqual(1, flip.Locations.Count);
            Assert.AreEqual(2, flip.Locations[0].Storage.Items.Count);
            Assert.AreEqual(BuildableItemEnum.White_Flour, flip.Locations[0].Storage.Items[BuildableItemEnum.White_Flour].ItemCode);
            Assert.AreEqual(0, flip.Locations[0].Storage.Items[BuildableItemEnum.White_Flour].Level);
            Assert.AreEqual(2, flip.Locations[0].Storage.Items[BuildableItemEnum.White_Flour].StoredQuantity);
            Assert.AreEqual(1, flip.Locations[0].Storage.Items[BuildableItemEnum.White_Flour].UnStoredQuantity);
            Assert.AreEqual(10, flip.TutorialStage);
        }        
    }
}