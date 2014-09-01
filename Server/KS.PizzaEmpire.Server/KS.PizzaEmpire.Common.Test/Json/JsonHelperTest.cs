namespace KS.PizzaEmpire.Common.Test.Json
{
    using Common.BusinessObjects;
    using Common.Utility;
    using KS.PizzaEmpire.GameLogic.ItemLogic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Services.Json;
    using System.Collections.Generic;

    [TestClass]
    public class JsonHelperTest
    {
        [ClassInitialize]
        public static void InitTests(TestContext tc)
        {
            JsonHelper.Instance.Initialize(new NewtonsoftJsonConverter());
        }

        [TestMethod]
        public void TestItemsToJSONEmptyDict()
        {
            // Arrange
            Dictionary<BuildableItemEnum, BuildableItem> dict = new Dictionary<BuildableItemEnum, BuildableItem>();
            // Act
            string json = JsonHelper.Instance.ItemsToJSON(dict);
            // Assert
            Assert.AreEqual("[]", json);
        }

        [TestMethod]
        public void TestItemsToJSON()
        {
            // Arrange
            List<BuildableItem> items = ItemManager.Instance.CreateItemList();
            Dictionary<BuildableItemEnum, BuildableItem> dict = new Dictionary<BuildableItemEnum, BuildableItem>();
            foreach (BuildableItem item in items)
            {
                dict[item.ItemCode] = item;
            }

            string expectedJson =
                @"[{""ItemCode"":46,""WorkItem"":null,""ProductionItem"":null,""StorageItem"":{""StorageStats"":[{""Capacity"":10}],""ItemCode"":46,""Stats"":[{""RequiredLevel"":0,""CoinCost"":0,""Experience"":0,""BuildSeconds"":0,""CouponCost"":0,""SpeedUpCoupons"":0,""SpeedUpSeconds"":0,""RequiredItems"":[]}]},""ConsumableItem"":null},{""ItemCode"":27,""WorkItem"":null,""ProductionItem"":{""ProductionStats"":[{""Capacity"":2},{""Capacity"":2},{""Capacity"":2}],""ItemCode"":27,""Stats"":[{""RequiredLevel"":0,""CoinCost"":0,""Experience"":0,""BuildSeconds"":0,""CouponCost"":0,""SpeedUpCoupons"":0,""SpeedUpSeconds"":0,""RequiredItems"":[]}]},""StorageItem"":null,""ConsumableItem"":null},{""ItemCode"":28,""WorkItem"":null,""ProductionItem"":{""ProductionStats"":[{""Capacity"":2}],""ItemCode"":28,""Stats"":[{""RequiredLevel"":0,""CoinCost"":0,""Experience"":0,""BuildSeconds"":0,""CouponCost"":0,""SpeedUpCoupons"":0,""SpeedUpSeconds"":0,""RequiredItems"":[]}]},""StorageItem"":null,""ConsumableItem"":null},{""ItemCode"":48,""WorkItem"":{""WorkStats"":[{}],""ItemCode"":48,""Stats"":[{""RequiredLevel"":0,""CoinCost"":0,""Experience"":100,""BuildSeconds"":-1000,""CouponCost"":0,""SpeedUpCoupons"":0,""SpeedUpSeconds"":0,""RequiredItems"":[]}]},""ProductionItem"":null,""StorageItem"":null,""ConsumableItem"":null},{""ItemCode"":47,""WorkItem"":{""WorkStats"":[{}],""ItemCode"":47,""Stats"":[{""RequiredLevel"":0,""CoinCost"":0,""Experience"":100,""BuildSeconds"":-1000,""CouponCost"":0,""SpeedUpCoupons"":0,""SpeedUpSeconds"":0,""RequiredItems"":[]}]},""ProductionItem"":null,""StorageItem"":null,""ConsumableItem"":null},{""ItemCode"":49,""WorkItem"":{""WorkStats"":[{}],""ItemCode"":49,""Stats"":[{""RequiredLevel"":0,""CoinCost"":0,""Experience"":100,""BuildSeconds"":-1000,""CouponCost"":0,""SpeedUpCoupons"":0,""SpeedUpSeconds"":0,""RequiredItems"":[]}]},""ProductionItem"":null,""StorageItem"":null,""ConsumableItem"":null},{""ItemCode"":1,""WorkItem"":null,""ProductionItem"":null,""StorageItem"":null,""ConsumableItem"":{""ProducedWith"":27,""StoredIn"":46,""ConsumableStats"":[{""ProductionQuantity"":1}],""ItemCode"":1,""Stats"":[{""RequiredLevel"":1,""CoinCost"":50,""Experience"":100,""BuildSeconds"":30,""CouponCost"":0,""SpeedUpCoupons"":0,""SpeedUpSeconds"":0,""RequiredItems"":[]}]}},{""ItemCode"":6,""WorkItem"":null,""ProductionItem"":null,""StorageItem"":null,""ConsumableItem"":{""ProducedWith"":27,""StoredIn"":46,""ConsumableStats"":[{""ProductionQuantity"":1}],""ItemCode"":6,""Stats"":[{""RequiredLevel"":1,""CoinCost"":50,""Experience"":100,""BuildSeconds"":30,""CouponCost"":0,""SpeedUpCoupons"":0,""SpeedUpSeconds"":0,""RequiredItems"":[]}]}},{""ItemCode"":4,""WorkItem"":null,""ProductionItem"":null,""StorageItem"":null,""ConsumableItem"":{""ProducedWith"":27,""StoredIn"":46,""ConsumableStats"":[{""ProductionQuantity"":1}],""ItemCode"":4,""Stats"":[{""RequiredLevel"":1,""CoinCost"":50,""Experience"":100,""BuildSeconds"":30,""CouponCost"":0,""SpeedUpCoupons"":0,""SpeedUpSeconds"":0,""RequiredItems"":[]}]}},{""ItemCode"":7,""WorkItem"":null,""ProductionItem"":null,""StorageItem"":null,""ConsumableItem"":{""ProducedWith"":28,""StoredIn"":42,""ConsumableStats"":[{""ProductionQuantity"":1}],""ItemCode"":7,""Stats"":[{""RequiredLevel"":1,""CoinCost"":50,""Experience"":100,""BuildSeconds"":30,""CouponCost"":0,""SpeedUpCoupons"":0,""SpeedUpSeconds"":0,""RequiredItems"":[]}]}},{""ItemCode"":15,""WorkItem"":null,""ProductionItem"":null,""StorageItem"":null,""ConsumableItem"":{""ProducedWith"":40,""StoredIn"":42,""ConsumableStats"":[{""ProductionQuantity"":1}],""ItemCode"":15,""Stats"":[{""RequiredLevel"":3,""CoinCost"":50,""Experience"":100,""BuildSeconds"":30,""CouponCost"":0,""SpeedUpCoupons"":0,""SpeedUpSeconds"":0,""RequiredItems"":[{""ItemCode"":1,""StoredQuantity"":1,""UnStoredQuantity"":0,""Level"":0},{""ItemCode"":6,""StoredQuantity"":1,""UnStoredQuantity"":0,""Level"":0},{""ItemCode"":4,""StoredQuantity"":1,""UnStoredQuantity"":0,""Level"":0}]}]}}]";

            // Act
            string json = JsonHelper.Instance.ItemsToJSON(dict);

            // Assert
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void TestItemsFromJSON()
        {
            // Arrange
            string expectedJson =
                @"[{""ItemCode"":46,""WorkItem"":null,""ProductionItem"":null,""StorageItem"":{""StorageStats"":[{""Capacity"":10}],""ItemCode"":46,""Stats"":[{""RequiredLevel"":0,""CoinCost"":0,""Experience"":0,""BuildSeconds"":0,""CouponCost"":0,""SpeedUpCoupons"":0,""SpeedUpSeconds"":0,""RequiredItems"":[]}]},""ConsumableItem"":null},{""ItemCode"":27,""WorkItem"":null,""ProductionItem"":{""ProductionStats"":[{""Capacity"":2},{""Capacity"":2},{""Capacity"":2}],""ItemCode"":27,""Stats"":[{""RequiredLevel"":0,""CoinCost"":0,""Experience"":0,""BuildSeconds"":0,""CouponCost"":0,""SpeedUpCoupons"":0,""SpeedUpSeconds"":0,""RequiredItems"":[]}]},""StorageItem"":null,""ConsumableItem"":null},{""ItemCode"":28,""WorkItem"":null,""ProductionItem"":{""ProductionStats"":[{""Capacity"":2}],""ItemCode"":28,""Stats"":[{""RequiredLevel"":0,""CoinCost"":0,""Experience"":0,""BuildSeconds"":0,""CouponCost"":0,""SpeedUpCoupons"":0,""SpeedUpSeconds"":0,""RequiredItems"":[]}]},""StorageItem"":null,""ConsumableItem"":null},{""ItemCode"":48,""WorkItem"":{""WorkStats"":[{}],""ItemCode"":48,""Stats"":[{""RequiredLevel"":0,""CoinCost"":0,""Experience"":100,""BuildSeconds"":-1000,""CouponCost"":0,""SpeedUpCoupons"":0,""SpeedUpSeconds"":0,""RequiredItems"":[]}]},""ProductionItem"":null,""StorageItem"":null,""ConsumableItem"":null},{""ItemCode"":47,""WorkItem"":{""WorkStats"":[{}],""ItemCode"":47,""Stats"":[{""RequiredLevel"":0,""CoinCost"":0,""Experience"":100,""BuildSeconds"":-1000,""CouponCost"":0,""SpeedUpCoupons"":0,""SpeedUpSeconds"":0,""RequiredItems"":[]}]},""ProductionItem"":null,""StorageItem"":null,""ConsumableItem"":null},{""ItemCode"":49,""WorkItem"":{""WorkStats"":[{}],""ItemCode"":49,""Stats"":[{""RequiredLevel"":0,""CoinCost"":0,""Experience"":100,""BuildSeconds"":-1000,""CouponCost"":0,""SpeedUpCoupons"":0,""SpeedUpSeconds"":0,""RequiredItems"":[]}]},""ProductionItem"":null,""StorageItem"":null,""ConsumableItem"":null},{""ItemCode"":1,""WorkItem"":null,""ProductionItem"":null,""StorageItem"":null,""ConsumableItem"":{""ProducedWith"":27,""StoredIn"":46,""ConsumableStats"":[{""ProductionQuantity"":1}],""ItemCode"":1,""Stats"":[{""RequiredLevel"":1,""CoinCost"":50,""Experience"":100,""BuildSeconds"":30,""CouponCost"":0,""SpeedUpCoupons"":0,""SpeedUpSeconds"":0,""RequiredItems"":[]}]}},{""ItemCode"":6,""WorkItem"":null,""ProductionItem"":null,""StorageItem"":null,""ConsumableItem"":{""ProducedWith"":27,""StoredIn"":46,""ConsumableStats"":[{""ProductionQuantity"":1}],""ItemCode"":6,""Stats"":[{""RequiredLevel"":1,""CoinCost"":50,""Experience"":100,""BuildSeconds"":30,""CouponCost"":0,""SpeedUpCoupons"":0,""SpeedUpSeconds"":0,""RequiredItems"":[]}]}},{""ItemCode"":4,""WorkItem"":null,""ProductionItem"":null,""StorageItem"":null,""ConsumableItem"":{""ProducedWith"":27,""StoredIn"":46,""ConsumableStats"":[{""ProductionQuantity"":1}],""ItemCode"":4,""Stats"":[{""RequiredLevel"":1,""CoinCost"":50,""Experience"":100,""BuildSeconds"":30,""CouponCost"":0,""SpeedUpCoupons"":0,""SpeedUpSeconds"":0,""RequiredItems"":[]}]}},{""ItemCode"":7,""WorkItem"":null,""ProductionItem"":null,""StorageItem"":null,""ConsumableItem"":{""ProducedWith"":28,""StoredIn"":42,""ConsumableStats"":[{""ProductionQuantity"":1}],""ItemCode"":7,""Stats"":[{""RequiredLevel"":1,""CoinCost"":50,""Experience"":100,""BuildSeconds"":30,""CouponCost"":0,""SpeedUpCoupons"":0,""SpeedUpSeconds"":0,""RequiredItems"":[]}]}},{""ItemCode"":15,""WorkItem"":null,""ProductionItem"":null,""StorageItem"":null,""ConsumableItem"":{""ProducedWith"":40,""StoredIn"":42,""ConsumableStats"":[{""ProductionQuantity"":1}],""ItemCode"":15,""Stats"":[{""RequiredLevel"":3,""CoinCost"":50,""Experience"":100,""BuildSeconds"":30,""CouponCost"":0,""SpeedUpCoupons"":0,""SpeedUpSeconds"":0,""RequiredItems"":[{""ItemCode"":1,""StoredQuantity"":1,""UnStoredQuantity"":0,""Level"":0},{""ItemCode"":6,""StoredQuantity"":1,""UnStoredQuantity"":0,""Level"":0},{""ItemCode"":4,""StoredQuantity"":1,""UnStoredQuantity"":0,""Level"":0}]}]}}]";

            // Act
            Dictionary<BuildableItemEnum, BuildableItem> dict = JsonHelper.Instance.ItemsFromJSON(expectedJson);

            // Assert
            Assert.AreEqual(11, dict.Values.Count);
            ProductionItem deliveryTruck = dict[BuildableItemEnum.Dry_Goods_Delivery_Truck] as ProductionItem;
            Assert.AreEqual(BuildableItemEnum.Dry_Goods_Delivery_Truck, deliveryTruck.ItemCode);
            Assert.AreEqual(3, deliveryTruck.ProductionStats.Count);
            Assert.AreEqual(2, deliveryTruck.ProductionStats[0].Capacity);
            Assert.AreEqual(2, deliveryTruck.ProductionStats[1].Capacity);
            Assert.AreEqual(2, deliveryTruck.ProductionStats[2].Capacity);
        }
    }
}

/*
namespace KS.PizzaEmpire.Common.Test.Utility
{
    using BusinessObjects;
    using Common.Utility;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;

    [TestClass]
    public class ExperienceLevelHelperTest
    {
        [TestMethod]
        public void TestToJSONEmptyDict()
        {
            // Arrange
            Dictionary<int, ExperienceLevel> dict = new Dictionary<int, ExperienceLevel>();
            // Act
            string json = ExperienceLevelHelper.ToJSON(dict);
            // Assert
            Assert.AreEqual("[]", json);
        }

        [TestMethod]
        public void TestToJSON()
        {
            // Arrange
            Dictionary<int, ExperienceLevel> dict = new Dictionary<int, ExperienceLevel>();
            dict[1] = new ExperienceLevel { Level = 1, ExperienceRequired = 100 };
            dict[2] = new ExperienceLevel { Level = 2, ExperienceRequired = 300 };
            string expectedJson = 
                @"[{""Level"":1,""ExperienceRequired"":100},{""Level"":2,""ExperienceRequired"":300}]";
            
            // Act
            string json = ExperienceLevelHelper.ToJSON(dict);

            // Assert
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void TestFromJSON()
        {
            // Arrange
            string expectedJson =
                @"[{""Level"":1,""ExperienceRequired"":100},{""Level"":2,""ExperienceRequired"":300}]";
            
            // Act
            Dictionary<int, ExperienceLevel> dict = ExperienceLevelHelper.FromJSON(expectedJson);

            // Assert
            Assert.AreEqual(2, dict.Values.Count);
            Assert.AreEqual(1, dict[1].Level);
            Assert.AreEqual(100, dict[1].ExperienceRequired);
            Assert.AreEqual(2, dict[2].Level);
            Assert.AreEqual(300, dict[2].ExperienceRequired);
        }
    }
}

*/

/*
namespace KS.PizzaEmpire.Common.Test.Utility
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using BusinessObjects;
    using Common.Utility;
    using KS.PizzaEmpire.GameLogic.ItemLogic;

    [TestClass]
    public class GamePlayerHelperTest
    {
        [TestMethod]
        public void TestToJSON()
        {
            // Arrange
            GamePlayer Player = new GamePlayer
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
                        FinishTime = new DateTime(2014, 12, 01, 0, 0, 0)
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

            string expectedJson =
                @"{""Coins"":99,""Coupons"":66,""Experience"":1000,""Level"":4,""TutorialStage"":10,""StateChanged"":false,""Locations"":[{""Storage"":{""Items"":{""White_Flour"":{""ItemCode"":1,""StoredQuantity"":2,""UnStoredQuantity"":1,""Level"":0},""Dirty_Dishes"":{""ItemCode"":47,""StoredQuantity"":0,""UnStoredQuantity"":2,""Level"":0}}}}],""WorkInProgress"":[{""Quantity"":{""ItemCode"":40,""StoredQuantity"":0,""UnStoredQuantity"":2,""Level"":1},""Location"":0,""FinishTime"":""2014-12-01T00:00:00""}]}";

            // Act
            string json = GamePlayerHelper.ToJSON(Player);

            // Assert
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void TestFromJSON()
        {
            // Arrange
            string expectedJson =
                @"{""Coins"":99,""Coupons"":66,""Experience"":1000,""Level"":4,""TutorialStage"":10,""StateChanged"":false,""Locations"":[{""Storage"":{""Items"":{""White_Flour"":{""ItemCode"":1,""StoredQuantity"":2,""UnStoredQuantity"":1,""Level"":0},""Dirty_Dishes"":{""ItemCode"":47,""StoredQuantity"":0,""UnStoredQuantity"":2,""Level"":0}}}}],""WorkInProgress"":[{""Quantity"":{""ItemCode"":40,""StoredQuantity"":0,""UnStoredQuantity"":2,""Level"":1},""Location"":0,""FinishTime"":""2014-12-01T00:00:00""}]}";

            // Act
            GamePlayer flip = GamePlayerHelper.FromJSON(expectedJson);

            // Assert
            // Assert
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
*/