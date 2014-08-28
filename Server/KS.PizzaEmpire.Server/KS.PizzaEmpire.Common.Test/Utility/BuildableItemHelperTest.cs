namespace KS.PizzaEmpire.Common.Test.Utility
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using BusinessObjects;
    using Common.Utility;
    using KS.PizzaEmpire.GameLogic.ItemLogic;

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
            List<BuildableItem> items = ItemManager.Instance.CreateItemList();
            Dictionary<BuildableItemEnum, BuildableItem> dict = new Dictionary<BuildableItemEnum, BuildableItem>();
            foreach(BuildableItem item in items)
            {
                dict[item.ItemCode] = item;
            }

            string expectedJson =
                @"[{""ItemCode"":46,""WorkItem"":null,""ProductionItem"":null,""StorageItem"":{""StorageStats"":[{""Capacity"":10}],""ItemCode"":46,""Stats"":[{""RequiredLevel"":0,""CoinCost"":0,""Experience"":0,""BuildSeconds"":0,""CouponCost"":0,""SpeedUpCoupons"":0,""SpeedUpSeconds"":0,""RequiredItems"":[]}]},""ConsumableItem"":null},{""ItemCode"":27,""WorkItem"":null,""ProductionItem"":{""ProductionStats"":[{""Capacity"":2},{""Capacity"":2},{""Capacity"":2}],""ItemCode"":27,""Stats"":[{""RequiredLevel"":0,""CoinCost"":0,""Experience"":0,""BuildSeconds"":0,""CouponCost"":0,""SpeedUpCoupons"":0,""SpeedUpSeconds"":0,""RequiredItems"":[]}]},""StorageItem"":null,""ConsumableItem"":null},{""ItemCode"":28,""WorkItem"":null,""ProductionItem"":{""ProductionStats"":[{""Capacity"":2}],""ItemCode"":28,""Stats"":[{""RequiredLevel"":0,""CoinCost"":0,""Experience"":0,""BuildSeconds"":0,""CouponCost"":0,""SpeedUpCoupons"":0,""SpeedUpSeconds"":0,""RequiredItems"":[]}]},""StorageItem"":null,""ConsumableItem"":null},{""ItemCode"":48,""WorkItem"":{""WorkStats"":[{}],""ItemCode"":48,""Stats"":[{""RequiredLevel"":0,""CoinCost"":0,""Experience"":100,""BuildSeconds"":-1000,""CouponCost"":0,""SpeedUpCoupons"":0,""SpeedUpSeconds"":0,""RequiredItems"":[]}]},""ProductionItem"":null,""StorageItem"":null,""ConsumableItem"":null},{""ItemCode"":47,""WorkItem"":{""WorkStats"":[{}],""ItemCode"":47,""Stats"":[{""RequiredLevel"":0,""CoinCost"":0,""Experience"":100,""BuildSeconds"":-1000,""CouponCost"":0,""SpeedUpCoupons"":0,""SpeedUpSeconds"":0,""RequiredItems"":[]}]},""ProductionItem"":null,""StorageItem"":null,""ConsumableItem"":null},{""ItemCode"":49,""WorkItem"":{""WorkStats"":[{}],""ItemCode"":49,""Stats"":[{""RequiredLevel"":0,""CoinCost"":0,""Experience"":100,""BuildSeconds"":-1000,""CouponCost"":0,""SpeedUpCoupons"":0,""SpeedUpSeconds"":0,""RequiredItems"":[]}]},""ProductionItem"":null,""StorageItem"":null,""ConsumableItem"":null},{""ItemCode"":1,""WorkItem"":null,""ProductionItem"":null,""StorageItem"":null,""ConsumableItem"":{""ProducedWith"":27,""StoredIn"":46,""ConsumableStats"":[{""ProductionQuantity"":1}],""ItemCode"":1,""Stats"":[{""RequiredLevel"":1,""CoinCost"":50,""Experience"":100,""BuildSeconds"":30,""CouponCost"":0,""SpeedUpCoupons"":0,""SpeedUpSeconds"":0,""RequiredItems"":[]}]}},{""ItemCode"":6,""WorkItem"":null,""ProductionItem"":null,""StorageItem"":null,""ConsumableItem"":{""ProducedWith"":27,""StoredIn"":46,""ConsumableStats"":[{""ProductionQuantity"":1}],""ItemCode"":6,""Stats"":[{""RequiredLevel"":1,""CoinCost"":50,""Experience"":100,""BuildSeconds"":30,""CouponCost"":0,""SpeedUpCoupons"":0,""SpeedUpSeconds"":0,""RequiredItems"":[]}]}},{""ItemCode"":4,""WorkItem"":null,""ProductionItem"":null,""StorageItem"":null,""ConsumableItem"":{""ProducedWith"":27,""StoredIn"":46,""ConsumableStats"":[{""ProductionQuantity"":1}],""ItemCode"":4,""Stats"":[{""RequiredLevel"":1,""CoinCost"":50,""Experience"":100,""BuildSeconds"":30,""CouponCost"":0,""SpeedUpCoupons"":0,""SpeedUpSeconds"":0,""RequiredItems"":[]}]}},{""ItemCode"":7,""WorkItem"":null,""ProductionItem"":null,""StorageItem"":null,""ConsumableItem"":{""ProducedWith"":28,""StoredIn"":42,""ConsumableStats"":[{""ProductionQuantity"":1}],""ItemCode"":7,""Stats"":[{""RequiredLevel"":1,""CoinCost"":50,""Experience"":100,""BuildSeconds"":30,""CouponCost"":0,""SpeedUpCoupons"":0,""SpeedUpSeconds"":0,""RequiredItems"":[]}]}},{""ItemCode"":15,""WorkItem"":null,""ProductionItem"":null,""StorageItem"":null,""ConsumableItem"":{""ProducedWith"":40,""StoredIn"":42,""ConsumableStats"":[{""ProductionQuantity"":1}],""ItemCode"":15,""Stats"":[{""RequiredLevel"":3,""CoinCost"":50,""Experience"":100,""BuildSeconds"":30,""CouponCost"":0,""SpeedUpCoupons"":0,""SpeedUpSeconds"":0,""RequiredItems"":[{""ItemCode"":1,""StoredQuantity"":1,""UnStoredQuantity"":0,""Level"":0},{""ItemCode"":6,""StoredQuantity"":1,""UnStoredQuantity"":0,""Level"":0},{""ItemCode"":4,""StoredQuantity"":1,""UnStoredQuantity"":0,""Level"":0}]}]}}]";

            // Act
            string json = BuildableItemHelper.ToJSON(dict);

            // Assert
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void TestFromJSON()
        {
            // Arrange
            string expectedJson =
                @"[{""ItemCode"":46,""WorkItem"":null,""ProductionItem"":null,""StorageItem"":{""StorageStats"":[{""Capacity"":10}],""ItemCode"":46,""Stats"":[{""RequiredLevel"":0,""CoinCost"":0,""Experience"":0,""BuildSeconds"":0,""CouponCost"":0,""SpeedUpCoupons"":0,""SpeedUpSeconds"":0,""RequiredItems"":[]}]},""ConsumableItem"":null},{""ItemCode"":27,""WorkItem"":null,""ProductionItem"":{""ProductionStats"":[{""Capacity"":2},{""Capacity"":2},{""Capacity"":2}],""ItemCode"":27,""Stats"":[{""RequiredLevel"":0,""CoinCost"":0,""Experience"":0,""BuildSeconds"":0,""CouponCost"":0,""SpeedUpCoupons"":0,""SpeedUpSeconds"":0,""RequiredItems"":[]}]},""StorageItem"":null,""ConsumableItem"":null},{""ItemCode"":28,""WorkItem"":null,""ProductionItem"":{""ProductionStats"":[{""Capacity"":2}],""ItemCode"":28,""Stats"":[{""RequiredLevel"":0,""CoinCost"":0,""Experience"":0,""BuildSeconds"":0,""CouponCost"":0,""SpeedUpCoupons"":0,""SpeedUpSeconds"":0,""RequiredItems"":[]}]},""StorageItem"":null,""ConsumableItem"":null},{""ItemCode"":48,""WorkItem"":{""WorkStats"":[{}],""ItemCode"":48,""Stats"":[{""RequiredLevel"":0,""CoinCost"":0,""Experience"":100,""BuildSeconds"":-1000,""CouponCost"":0,""SpeedUpCoupons"":0,""SpeedUpSeconds"":0,""RequiredItems"":[]}]},""ProductionItem"":null,""StorageItem"":null,""ConsumableItem"":null},{""ItemCode"":47,""WorkItem"":{""WorkStats"":[{}],""ItemCode"":47,""Stats"":[{""RequiredLevel"":0,""CoinCost"":0,""Experience"":100,""BuildSeconds"":-1000,""CouponCost"":0,""SpeedUpCoupons"":0,""SpeedUpSeconds"":0,""RequiredItems"":[]}]},""ProductionItem"":null,""StorageItem"":null,""ConsumableItem"":null},{""ItemCode"":49,""WorkItem"":{""WorkStats"":[{}],""ItemCode"":49,""Stats"":[{""RequiredLevel"":0,""CoinCost"":0,""Experience"":100,""BuildSeconds"":-1000,""CouponCost"":0,""SpeedUpCoupons"":0,""SpeedUpSeconds"":0,""RequiredItems"":[]}]},""ProductionItem"":null,""StorageItem"":null,""ConsumableItem"":null},{""ItemCode"":1,""WorkItem"":null,""ProductionItem"":null,""StorageItem"":null,""ConsumableItem"":{""ProducedWith"":27,""StoredIn"":46,""ConsumableStats"":[{""ProductionQuantity"":1}],""ItemCode"":1,""Stats"":[{""RequiredLevel"":1,""CoinCost"":50,""Experience"":100,""BuildSeconds"":30,""CouponCost"":0,""SpeedUpCoupons"":0,""SpeedUpSeconds"":0,""RequiredItems"":[]}]}},{""ItemCode"":6,""WorkItem"":null,""ProductionItem"":null,""StorageItem"":null,""ConsumableItem"":{""ProducedWith"":27,""StoredIn"":46,""ConsumableStats"":[{""ProductionQuantity"":1}],""ItemCode"":6,""Stats"":[{""RequiredLevel"":1,""CoinCost"":50,""Experience"":100,""BuildSeconds"":30,""CouponCost"":0,""SpeedUpCoupons"":0,""SpeedUpSeconds"":0,""RequiredItems"":[]}]}},{""ItemCode"":4,""WorkItem"":null,""ProductionItem"":null,""StorageItem"":null,""ConsumableItem"":{""ProducedWith"":27,""StoredIn"":46,""ConsumableStats"":[{""ProductionQuantity"":1}],""ItemCode"":4,""Stats"":[{""RequiredLevel"":1,""CoinCost"":50,""Experience"":100,""BuildSeconds"":30,""CouponCost"":0,""SpeedUpCoupons"":0,""SpeedUpSeconds"":0,""RequiredItems"":[]}]}},{""ItemCode"":7,""WorkItem"":null,""ProductionItem"":null,""StorageItem"":null,""ConsumableItem"":{""ProducedWith"":28,""StoredIn"":42,""ConsumableStats"":[{""ProductionQuantity"":1}],""ItemCode"":7,""Stats"":[{""RequiredLevel"":1,""CoinCost"":50,""Experience"":100,""BuildSeconds"":30,""CouponCost"":0,""SpeedUpCoupons"":0,""SpeedUpSeconds"":0,""RequiredItems"":[]}]}},{""ItemCode"":15,""WorkItem"":null,""ProductionItem"":null,""StorageItem"":null,""ConsumableItem"":{""ProducedWith"":40,""StoredIn"":42,""ConsumableStats"":[{""ProductionQuantity"":1}],""ItemCode"":15,""Stats"":[{""RequiredLevel"":3,""CoinCost"":50,""Experience"":100,""BuildSeconds"":30,""CouponCost"":0,""SpeedUpCoupons"":0,""SpeedUpSeconds"":0,""RequiredItems"":[{""ItemCode"":1,""StoredQuantity"":1,""UnStoredQuantity"":0,""Level"":0},{""ItemCode"":6,""StoredQuantity"":1,""UnStoredQuantity"":0,""Level"":0},{""ItemCode"":4,""StoredQuantity"":1,""UnStoredQuantity"":0,""Level"":0}]}]}}]";

            // Act
            Dictionary<BuildableItemEnum, BuildableItem> dict = BuildableItemHelper.FromJSON(expectedJson);

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
