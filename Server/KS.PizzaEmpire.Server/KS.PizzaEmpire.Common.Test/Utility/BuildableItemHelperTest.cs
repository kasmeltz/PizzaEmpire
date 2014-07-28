namespace KS.PizzaEmpire.Common.Test.Utility
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using BusinessObjects;
    using Common.Utility;

    [TestClass]
    public class BuildableItemHelperTest
    {
        [TestMethod]
        public void TestToJSONEmptyDict()
        {
            // Arrange
            Dictionary<BuildableItemEnum, BuildableItem> dict = new Dictionary<BuildableItemEnum, BuildableItem>();

            // Act
            string json = BuildableItemHelper.ToJSON(dict);

            // Assert
            Assert.AreEqual("[]", json);
        }

        [TestMethod]
        public void TestToJSON()
        {
            // Arrange
            Dictionary<BuildableItemEnum, BuildableItem> dict = new Dictionary<BuildableItemEnum, BuildableItem>();
            dict[BuildableItemEnum.Dry_Goods_Delivery_Truck_L1] = new BuildableItem
            {
                ItemCode = BuildableItemEnum.Dry_Goods_Delivery_Truck_L1,
                CoinCost = 0, IsConsumable = false
            };
            dict[BuildableItemEnum.White_Flour] = new BuildableItem
            {
                ItemCode = BuildableItemEnum.White_Flour,
                CoinCost = 100,
                IsConsumable = true,
                BuildSeconds = 180,
                ProductionItem = BuildableItemEnum.Dry_Goods_Delivery_Truck_L1,
                RequiredItems = new List<ItemQuantity>
                {
                     new ItemQuantity 
                     {
                          ItemCode = BuildableItemEnum.Dough_Mixer_L1,
                          Quantity = 4
                    }
                }
            };
            string expectedJson = @"[{""ItemCode"":35,""RequiredLevel"":0,""CoinCost"":0,""ProductionItem"":0,""ProductionCapacity"":0,""BaseProduction"":0,""StorageCapacity"":0,""StorageItem"":0,""IsStorage"":false,""IsConsumable"":false,""IsImmediate"":false,""IsWorkSubtracted"":false,""Experience"":0,""BuildSeconds"":0,""CouponCost"":0,""SpeedUpCoupons"":0,""SpeedUpSeconds"":0,""RequiredItems"":null},{""ItemCode"":1,""RequiredLevel"":0,""CoinCost"":100,""ProductionItem"":35,""ProductionCapacity"":0,""BaseProduction"":0,""StorageCapacity"":0,""StorageItem"":0,""IsStorage"":false,""IsConsumable"":true,""IsImmediate"":false,""IsWorkSubtracted"":false,""Experience"":0,""BuildSeconds"":180,""CouponCost"":0,""SpeedUpCoupons"":0,""SpeedUpSeconds"":0,""RequiredItems"":[{""ItemCode"":48,""Quantity"":4}]}]";

            // Act
            string json = BuildableItemHelper.ToJSON(dict);

            // Assert
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void TestFromJSON()
        {
            // Arrange
            string expectedJson = @"[{""ItemCode"":35,""RequiredLevel"":0,""CoinCost"":0,""ProductionItem"":0,""ProductionCapacity"":0,""BaseProduction"":0,""StorageCapacity"":0,""StorageItem"":0,""IsStorage"":false,""IsConsumable"":false,""IsImmediate"":false,""IsWorkSubtracted"":false,""Experience"":0,""BuildSeconds"":0,""CouponCost"":0,""SpeedUpCoupons"":0,""SpeedUpSeconds"":0,""RequiredItems"":null},{""ItemCode"":1,""RequiredLevel"":0,""CoinCost"":100,""ProductionItem"":35,""ProductionCapacity"":0,""BaseProduction"":0,""StorageCapacity"":0,""StorageItem"":0,""IsStorage"":false,""IsConsumable"":true,""IsImmediate"":false,""IsWorkSubtracted"":false,""Experience"":0,""BuildSeconds"":180,""CouponCost"":0,""SpeedUpCoupons"":0,""SpeedUpSeconds"":0,""RequiredItems"":[{""ItemCode"":48,""Quantity"":4}]}]";

            // Act
            Dictionary<BuildableItemEnum, BuildableItem> dict = BuildableItemHelper.FromJSON(expectedJson);

            // Assert
            Assert.AreEqual(2, dict.Values.Count);
            Assert.AreEqual(BuildableItemEnum.Dry_Goods_Delivery_Truck_L1, dict[BuildableItemEnum.Dry_Goods_Delivery_Truck_L1].ItemCode);
            Assert.AreEqual(0, dict[BuildableItemEnum.Dry_Goods_Delivery_Truck_L1].CoinCost);
            Assert.AreEqual(false, dict[BuildableItemEnum.Dry_Goods_Delivery_Truck_L1].IsConsumable);

            Assert.AreEqual(BuildableItemEnum.White_Flour, dict[BuildableItemEnum.White_Flour].ItemCode);
            Assert.AreEqual(100, dict[BuildableItemEnum.White_Flour].CoinCost);
            Assert.AreEqual(true, dict[BuildableItemEnum.White_Flour].IsConsumable);
            Assert.AreEqual(180, dict[BuildableItemEnum.White_Flour].BuildSeconds);
            Assert.AreEqual(BuildableItemEnum.Dry_Goods_Delivery_Truck_L1, dict[BuildableItemEnum.White_Flour].ProductionItem);
            Assert.AreEqual(1, dict[BuildableItemEnum.White_Flour].RequiredItems.Count);
            Assert.AreEqual(BuildableItemEnum.Dough_Mixer_L1, dict[BuildableItemEnum.White_Flour].RequiredItems[0].ItemCode);
            Assert.AreEqual(4, dict[BuildableItemEnum.White_Flour].RequiredItems[0].Quantity);
        }
    }
}
