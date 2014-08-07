﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KS.PizzaEmpire.Common.Test.APITransfer
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Common.BusinessObjects;
    using Common.APITransfer;
    using System.Collections.Generic;

    [TestClass]
    public class BuildableItemAPIMorphTest
    {
        public BuildableItemAPIMorph morph;
        public BuildableItem bi;

        [TestInitialize]
        public void Initialize()
        {
            morph = new BuildableItemAPIMorph();

            bi = new BuildableItem
            {
                ItemCode = BuildableItemEnum.White_Flour,
                RequiredLevel = 1,
                CoinCost = 50,
                ProductionItem = BuildableItemEnum.Dry_Goods_Delivery_Truck_L1,
                ProductionCapacity = 0,
                BaseProduction = 1,
                StorageCapacity = 0,
                StorageItem = BuildableItemEnum.Restaurant_Storage,
                IsStorage = false,
                IsConsumable = true,
                IsImmediate = false,
                IsWorkSubtracted = false,
                Experience = 100,
                BuildSeconds = 10,
                CouponCost = 0,
                SpeedUpCoupons = 1,
                SpeedUpSeconds = 60,
            };
        }

        [TestMethod]
        public void TestToAPIFormat()
        {
            BuildableItemAPI itemAPI = (BuildableItemAPI)morph.ToAPIFormat(bi);

            Assert.AreEqual(BuildableItemEnum.White_Flour, itemAPI.ItemCode);
            Assert.AreEqual(1, itemAPI.RequiredLevel);
            Assert.AreEqual(50, itemAPI.CoinCost);
            Assert.AreEqual(BuildableItemEnum.Dry_Goods_Delivery_Truck_L1, itemAPI.ProductionItem);
            Assert.AreEqual(0, itemAPI.ProductionCapacity);
            Assert.AreEqual(1, itemAPI.BaseProduction);
            Assert.AreEqual(0, itemAPI.StorageCapacity);
            Assert.AreEqual(BuildableItemEnum.Restaurant_Storage, itemAPI.StorageItem);
            Assert.AreEqual(false, itemAPI.IsStorage);
            Assert.AreEqual(true, itemAPI.IsConsumable);
            Assert.AreEqual(false, itemAPI.IsImmediate);
            Assert.AreEqual(false, itemAPI.IsWorkSubtracted);
            Assert.AreEqual(100, itemAPI.Experience);
            Assert.AreEqual(10, itemAPI.BuildSeconds);
            Assert.AreEqual(0, itemAPI.CouponCost);
            Assert.AreEqual(1, itemAPI.SpeedUpCoupons);
            Assert.AreEqual(60, itemAPI.SpeedUpSeconds);
        }

        [TestMethod]
        public void TestFromAPIFormat()
        {
            BuildableItemAPI itemAPI = (BuildableItemAPI)morph.ToAPIFormat(bi);
            BuildableItem flip = (BuildableItem)morph.ToBusinessFormat(itemAPI);

            Assert.AreEqual(BuildableItemEnum.White_Flour, flip.ItemCode);
            Assert.AreEqual(1, flip.RequiredLevel);
            Assert.AreEqual(50, flip.CoinCost);
            Assert.AreEqual(BuildableItemEnum.Dry_Goods_Delivery_Truck_L1, flip.ProductionItem);
            Assert.AreEqual(0, flip.ProductionCapacity);
            Assert.AreEqual(1, flip.BaseProduction);
            Assert.AreEqual(0, flip.StorageCapacity);
            Assert.AreEqual(BuildableItemEnum.Restaurant_Storage, flip.StorageItem);
            Assert.AreEqual(false, flip.IsStorage);
            Assert.AreEqual(true, flip.IsConsumable);
            Assert.AreEqual(false, flip.IsImmediate);
            Assert.AreEqual(false, flip.IsWorkSubtracted);
            Assert.AreEqual(100, flip.Experience);
            Assert.AreEqual(10, flip.BuildSeconds);
            Assert.AreEqual(0, flip.CouponCost);
            Assert.AreEqual(1, flip.SpeedUpCoupons);
            Assert.AreEqual(60, flip.SpeedUpSeconds);
        }
    }
}
