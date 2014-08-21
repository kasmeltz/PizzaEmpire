﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KS.PizzaEmpire.Common.GameLogic;
using KS.PizzaEmpire.Common.BusinessObjects;
using System.Collections.Generic;
using KS.PizzaEmpire.GameLogic.ItemLogic;

namespace KS.PizzaEmpire.Common.Test.GameLogic
{
    [TestClass]
    public class GamePlayerLogicTest
    {
        public GamePlayer Player { get; set; }

        [ClassInitialize]
        public static void InitializeAllTests(TestContext testContent)
        {
            List<BuildableItem> items = ItemManager.Instance.CreateItemList();
            Dictionary<BuildableItemEnum, BuildableItem> bitems = new Dictionary<BuildableItemEnum, BuildableItem>();
            foreach (BuildableItem item in items)
            {
                bitems[item.ItemCode] = item;
            }

            GamePlayerLogic.Instance.BuildableItems = bitems;

            Dictionary<int, ExperienceLevel>elevels = new Dictionary<int, ExperienceLevel>();
            ExperienceLevel exl;

            exl = new ExperienceLevel
            {
                Level = 1,
                ExperienceRequired = 0,
            };
            elevels[exl.Level] = exl;

            exl = new ExperienceLevel
            {
                Level = 2,
                ExperienceRequired = 100,
            };
            elevels[exl.Level] = exl;
            exl = new ExperienceLevel
            {
                Level = 3,
                ExperienceRequired = 300,
            };
            elevels[exl.Level] = exl;

            exl = new ExperienceLevel
            {
                Level = 4,
                ExperienceRequired = 700,
            };
            elevels[exl.Level] = exl;

            GamePlayerLogic.Instance.ExperienceLevels = elevels;
        }

        [TestInitialize]
        public void InitializeTest()
        {
            Player = GamePlayerLogic.Instance.CreateNewGamePlayer();
        }

        [TestMethod]
        public void TestNewGamePlayerHasProperCoins()
        {
            Assert.AreEqual(1000, Player.Coins);
        }

        [TestMethod]
        public void TestNewGamePlayerHasProperCoupons()
        {
            Assert.AreEqual(5, Player.Coupons);
        }

        [TestMethod]
        public void TestNewGamePlayerHasProperLevel()
        {
            Assert.AreEqual(1, Player.Level);
        }

        [TestMethod]
        public void TestNewGamePlayerHasProperExperience()
        {
            Assert.AreEqual(0, Player.Experience);
        }

        [TestMethod]
        public void TestNewGamePlayerHasProperLocations()
        {
            Assert.AreEqual(1, Player.Locations.Count);
        }

        [TestMethod]
        public void TestNewGamePlayerHasProperItemCount()
        {
            Assert.AreEqual(5, Player.Locations[0].Storage.Items.Count);
        }

        [TestMethod]
        public void TestNewGamePlayerHasProperDeliveryTruck()
        {
            Assert.AreEqual(1, Player.Locations[0].Storage.Items[BuildableItemEnum.Dry_Goods_Delivery_Truck].Level);
            Assert.AreEqual(0, Player.Locations[0].Storage.Items[BuildableItemEnum.Dry_Goods_Delivery_Truck].StoredQuantity);
            Assert.AreEqual(1, Player.Locations[0].Storage.Items[BuildableItemEnum.Dry_Goods_Delivery_Truck].UnStoredQuantity);
        }

        [TestMethod]
        public void TestNewGamePlayerHasProperRestaurantStorage()
        {
            Assert.AreEqual(1, Player.Locations[0].Storage.Items[BuildableItemEnum.Restaurant_Storage].Level);
            Assert.AreEqual(0, Player.Locations[0].Storage.Items[BuildableItemEnum.Restaurant_Storage].StoredQuantity);
            Assert.AreEqual(1, Player.Locations[0].Storage.Items[BuildableItemEnum.Restaurant_Storage].UnStoredQuantity);
        }

        [TestMethod]
        public void TestNewGamePlayerHasProperDirtyTables()
        {
            Assert.AreEqual(1, Player.Locations[0].Storage.Items[BuildableItemEnum.Dirty_Table].Level);
            Assert.AreEqual(0, Player.Locations[0].Storage.Items[BuildableItemEnum.Dirty_Table].StoredQuantity);
            Assert.AreEqual(1, Player.Locations[0].Storage.Items[BuildableItemEnum.Dirty_Table].UnStoredQuantity);
        }

        [TestMethod]
        public void TestNewGamePlayerHasProperDirtyDishes()
        {
            Assert.AreEqual(1, Player.Locations[0].Storage.Items[BuildableItemEnum.Dirty_Dishes].Level);
            Assert.AreEqual(0, Player.Locations[0].Storage.Items[BuildableItemEnum.Dirty_Dishes].StoredQuantity);
            Assert.AreEqual(1, Player.Locations[0].Storage.Items[BuildableItemEnum.Dirty_Dishes].UnStoredQuantity);
        }

        [TestMethod]
        public void TestNewGamePlayerHasProperDirtyFloors()
        {
            Assert.AreEqual(1, Player.Locations[0].Storage.Items[BuildableItemEnum.Dirty_Floor].Level);
            Assert.AreEqual(0, Player.Locations[0].Storage.Items[BuildableItemEnum.Dirty_Floor].StoredQuantity);
            Assert.AreEqual(1, Player.Locations[0].Storage.Items[BuildableItemEnum.Dirty_Floor].UnStoredQuantity);
        }

        [TestMethod]
        public void TestNewGamePlayerHasProperWorkItems()
        {
            Assert.AreEqual(0, Player.WorkInProgress.Count);            
        }

        [TestMethod]
        public void TestNewGamePlayerStateChanged()
        {
            Assert.AreEqual(true, Player.StateChanged);
        }

        [TestMethod]
        public void TestSetLevel()
        {
            Assert.AreEqual(1, Player.Level);
            GamePlayerLogic.Instance.SetLevel(Player, 2);
            Assert.AreEqual(2, Player.Level);
            Assert.AreEqual(true, Player.StateChanged);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestSetLevelDoesntExist()
        {
            GamePlayerLogic.Instance.SetLevel(Player, -1);
            Assert.AreEqual(false, Player.StateChanged);
        }

        [TestMethod]
        public void TestAddExperienceNoLevels()
        {
            Assert.AreEqual(1, Player.Level);
            GamePlayerLogic.Instance.AddExperience(Player, 50);
            Assert.AreEqual(50, Player.Experience);
            Assert.AreEqual(1, Player.Level);
            Assert.AreEqual(true, Player.StateChanged);
        }

        [TestMethod]
        public void TestAddExperienceOneLevelExact()
        {
            Assert.AreEqual(1, Player.Level);
            GamePlayerLogic.Instance.AddExperience(Player, 100);
            Assert.AreEqual(100, Player.Experience);
            Assert.AreEqual(2, Player.Level);
            Assert.AreEqual(true, Player.StateChanged);
        }

        [TestMethod]
        public void TestAddExperienceOneLevel()
        {
            Assert.AreEqual(1, Player.Level);
            GamePlayerLogic.Instance.AddExperience(Player, 150);
            Assert.AreEqual(150, Player.Experience);
            Assert.AreEqual(2, Player.Level);
            Assert.AreEqual(true, Player.StateChanged);
        }

        [TestMethod]
        public void TestAddExperienceTwoLevelsExact()
        {
            Assert.AreEqual(1, Player.Level);
            GamePlayerLogic.Instance.AddExperience(Player, 300);
            Assert.AreEqual(300, Player.Experience);
            Assert.AreEqual(3, Player.Level);
            Assert.AreEqual(true, Player.StateChanged);
        }

        [TestMethod]
        public void TestAddExperienceTwoLevels()
        {
            Assert.AreEqual(1, Player.Level);
            GamePlayerLogic.Instance.AddExperience(Player, 400);
            Assert.AreEqual(400, Player.Experience);
            Assert.AreEqual(3, Player.Level);
            Assert.AreEqual(true, Player.StateChanged);
        }

        [TestMethod]
        public void TestAddExperienceMaxLevels()
        {
            Assert.AreEqual(1, Player.Level);
            GamePlayerLogic.Instance.AddExperience(Player, 40000);
            Assert.AreEqual(40000, Player.Experience);
            Assert.AreEqual(4, Player.Level);
            Assert.AreEqual(true, Player.StateChanged);
        }

        [TestMethod]
        public void TestDoesPlayerHaveProductionCapacityTrue()
        {
            Assert.Fail("Not implemented");

            /*
            Assert.AreEqual(true,
                GamePlayerLogic.Instance.DoesPlayerHaveProductionCapacity(Player, BuildableItemEnum.White_Flour));
             * */
        }

        [TestMethod]
        public void TestDoesPlayerHaveProductionCapacityFalse()
        {
            Assert.Fail("Not implemented");

            /*
            Player.WorkItems.Add(new WorkInProgress { ItemCode = BuildableItemEnum.White_Flour });
            Player.WorkItems.Add(new WorkInProgress { ItemCode = BuildableItemEnum.White_Flour });
            Assert.AreEqual(false,
                GamePlayerLogic.Instance.DoesPlayerHaveProductionCapacity(Player, BuildableItemEnum.White_Flour));
             * */
        }

        [TestMethod]
        public void TestDoesPlayeraHaveStorageCapacityTrue()
        {
            Assert.Fail("Not implemented");

            /*
            Assert.AreEqual(true,
                GamePlayerLogic.Instance.DoesPlayeraHaveStorageCapacity(Player, BuildableItemEnum.White_Flour));
             * */
        }

        [TestMethod]
         public void TestDoesPlayeraHaveStorageCapacityFalse()
        {
            Assert.Fail("Not implemented");

            /*
            Player.BuildableItems[BuildableItemEnum.White_Flour] = 10;
            Assert.AreEqual(false,
                GamePlayerLogic.Instance.DoesPlayeraHaveStorageCapacity(Player, BuildableItemEnum.White_Flour));
             * */
        }

        [TestMethod]
        public void TestAddItem()
        {
            Assert.Fail("Not implemented");

            /*
            Assert.AreEqual(false, Player.BuildableItems.ContainsKey(BuildableItemEnum.White_Flour));
            GamePlayerLogic.Instance.AddItem(Player, new WorkInProgress { ItemCode = BuildableItemEnum.White_Flour });
            Assert.AreEqual(1, Player.BuildableItems[BuildableItemEnum.White_Flour]);
            Assert.AreEqual(true, Player.StateChanged);
             * */
        }

        [TestMethod]
        public void TestAddItemNonConsumable()
        {
            Assert.Fail("Not implemented");

            /*
            GamePlayerLogic.Instance.AddItem(Player, new WorkInProgress { ItemCode = BuildableItemEnum.Dry_Goods_Delivery_Truck_L1 });
            Assert.AreEqual(1, Player.BuildableItems[BuildableItemEnum.Dry_Goods_Delivery_Truck_L1]);
            GamePlayerLogic.Instance.AddItem(Player, new WorkInProgress { ItemCode = BuildableItemEnum.Dry_Goods_Delivery_Truck_L1 });
            Assert.AreEqual(1, Player.BuildableItems[BuildableItemEnum.Dry_Goods_Delivery_Truck_L1]);
            Assert.AreEqual(true, Player.StateChanged);
             * */
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestFinishWorkNone()
        {
            Assert.Fail("Not implemented");

            /*
            List<WorkInProgress> finishedItems = GamePlayerLogic.Instance.FinishWork(Player, DateTime.UtcNow);
            Assert.AreEqual(0, finishedItems.Count);
            Assert.AreEqual(true, Player.StateChanged);
             * */
        }

        [TestMethod]
        public void TestFinishWorkNoneReady()
        {
            Assert.Fail("Not implemented");

            /*
            Player.WorkItems.Add(new WorkInProgress { ItemCode = BuildableItemEnum.White_Flour, FinishTime = DateTime.UtcNow.AddHours(1) });
            List<WorkInProgress> finishedItems = GamePlayerLogic.Instance.FinishWork(Player, DateTime.UtcNow);
            Assert.AreEqual(0, finishedItems.Count);
            Assert.AreEqual(true, Player.StateChanged);
             * */
        }

        [TestMethod]
        public void TestFinishWorkOneReady()
        {
            Assert.Fail("Not implemented");

            /*
            Player.WorkItems.Add(new WorkInProgress { ItemCode = BuildableItemEnum.White_Flour, FinishTime = DateTime.UtcNow.AddHours(1) });
            Player.WorkItems.Add(new WorkInProgress { ItemCode = BuildableItemEnum.White_Flour, FinishTime = DateTime.UtcNow.AddHours(-1) });
            List<WorkInProgress> finishedItems = GamePlayerLogic.Instance.FinishWork(Player, DateTime.UtcNow);
            Assert.AreEqual(1, finishedItems.Count);
            Assert.AreEqual(BuildableItemEnum.White_Flour, finishedItems[0].ItemCode);
            Assert.AreEqual(true, Player.StateChanged);
             * */
        }

        [TestMethod]
        public void TestFinishWorkTwoReady()
        {
            Assert.Fail("Not implemented");
            
            /*
            Player.WorkItems.Add(new WorkInProgress { ItemCode = BuildableItemEnum.White_Flour, FinishTime = DateTime.UtcNow.AddHours(1) });
            Player.WorkItems.Add(new WorkInProgress { ItemCode = BuildableItemEnum.Yeast, FinishTime = DateTime.UtcNow.AddHours(-1) });
            Player.WorkItems.Add(new WorkInProgress { ItemCode = BuildableItemEnum.Salt, FinishTime = DateTime.UtcNow.AddHours(-1) });
            List<WorkInProgress> finishedItems = GamePlayerLogic.Instance.FinishWork(Player, DateTime.UtcNow);
            Assert.AreEqual(2, finishedItems.Count);
            Assert.AreEqual(BuildableItemEnum.Yeast, finishedItems[0].ItemCode);
            Assert.AreEqual(BuildableItemEnum.Salt, finishedItems[1].ItemCode);
            Assert.AreEqual(true, Player.StateChanged);
             * */
        }

        [TestMethod]
        public void TestDeductResources()
        {
            Assert.Fail("Not implemented");

            /*
            Player.BuildableItems[BuildableItemEnum.White_Flour] = 3;
            Player.BuildableItems[BuildableItemEnum.Yeast] = 2;
            Player.BuildableItems[BuildableItemEnum.Salt] = 1;
            Player.BuildableItems[BuildableItemEnum.Dough_Mixer_L1] = 1;
            GamePlayerLogic.Instance.DeductResources(Player, BuildableItemEnum.White_Pizza_Dough);
            Assert.AreEqual(2, Player.BuildableItems[BuildableItemEnum.White_Flour]);
            Assert.AreEqual(1, Player.BuildableItems[BuildableItemEnum.Yeast]);
            Assert.AreEqual(0, Player.BuildableItems[BuildableItemEnum.Salt]);
            Assert.AreEqual(1, Player.BuildableItems[BuildableItemEnum.Dough_Mixer_L1]);
            Assert.AreEqual(true, Player.StateChanged);
             * */
        }

        [TestMethod]
        public void TestStartWork()
        {
            WorkInProgress workItem = GamePlayerLogic.Instance.StartWork(Player, 0, 0, BuildableItemEnum.White_Flour);
            Assert.AreEqual(BuildableItemEnum.White_Flour, workItem.Quantity.ItemCode);
            Assert.AreEqual(1, workItem.Quantity.UnStoredQuantity);
            Assert.AreEqual(0, workItem.Location);            
            Assert.AreEqual(true, Player.StateChanged);
            Assert.AreEqual(950, Player.Coins);
            Assert.AreEqual(5, Player.Coupons);
        }

        [TestMethod]
        public void TestCanBuildItemFailItemType()
        {
            Assert.Fail("Not implemented");

            /*
            ErrorCode ec = GamePlayerLogic.Instance.CanBuildItem(Player, (BuildableItemEnum)5000);

            Assert.AreEqual(ErrorCode.START_WORK_INVALID_ITEM, ec);
             * */
        }

        [TestMethod]
        public void TestCanBuildItemFailLevel()
        {
            Assert.Fail("Not implemented");

            /*
            GamePlayer player = GamePlayerLogic.Instance.CreateNewGamePlayer();

            ErrorCode ec = GamePlayerLogic.Instance.CanBuildItem(player, BuildableItemEnum.White_Pizza_Dough);

            Assert.AreEqual(ErrorCode.START_WORK_ITEM_NOT_AVAILABLE, ec);
             * */
        }

        [TestMethod]
        public void TestCanBuildItemFailCoins()
        {
            Assert.Fail("Not implemented");

            /*
            Player.Coins = 0;

            ErrorCode ec = GamePlayerLogic.Instance.CanBuildItem(Player, BuildableItemEnum.White_Flour);

            Assert.AreEqual(ErrorCode.START_WORK_INSUFFICIENT_COINS, ec);
             * */
        }

        [TestMethod]
        public void TestCanBuildItemFailCoupons()
        {
            Assert.Fail("Not implemented");

            /*
            Player.Coupons = 0;

            ErrorCode ec = GamePlayerLogic.Instance.CanBuildItem(Player, BuildableItemEnum.Tomatoes);

            Assert.AreEqual(ErrorCode.START_WORK_INSUFFICIENT_COUPONS, ec);
             * */
        }

        [TestMethod]
        public void TestCanBuildItemFailImproperIngredients()
        {
            Assert.Fail("Not implemented");

            /*
            GamePlayerLogic.Instance.SetLevel(Player, 3);

            ErrorCode ec = GamePlayerLogic.Instance.CanBuildItem(Player, BuildableItemEnum.White_Pizza_Dough);

            Assert.AreEqual(ErrorCode.START_WORK_INVALID_INGREDIENTS, ec);
             * */
        }

        [TestMethod]
        public void TestCanBuildItemFailInsufficeintIngredients()
        {
            Assert.Fail("Not implemented");

            /*
            GamePlayerLogic.Instance.SetLevel(Player, 3);
            Player.BuildableItems[BuildableItemEnum.Salt] = 0;
            Player.BuildableItems[BuildableItemEnum.White_Flour] = 0;
            Player.BuildableItems[BuildableItemEnum.Yeast] = 0;

            ErrorCode ec = GamePlayerLogic.Instance.CanBuildItem(Player, BuildableItemEnum.White_Pizza_Dough);
            
            Assert.AreEqual(ErrorCode.START_WORK_INSUFFICIENT_INGREDIENTS, ec);
             * */
        }

        [TestMethod]
        public void TestCanBuildItemFailEquipmentFull()
        {
            Assert.Fail("Not implemented");

            /*
            GamePlayerLogic.Instance.StartWork(Player, BuildableItemEnum.White_Flour);
            GamePlayerLogic.Instance.StartWork(Player, BuildableItemEnum.White_Flour);

            ErrorCode ec = GamePlayerLogic.Instance.CanBuildItem(Player, BuildableItemEnum.White_Flour);

            Assert.AreEqual(ErrorCode.START_WORK_NO_PRODUCTION_CAPACITY, ec);
             * */
        }

        [TestMethod]
        public void TestCanBuildItemFailStorageFull()
        {
            Assert.Fail("Not implemented");

            /*

            Player.BuildableItems[BuildableItemEnum.White_Flour] = 10;

            ErrorCode ec = GamePlayerLogic.Instance.CanBuildItem(Player, BuildableItemEnum.White_Flour);

            Assert.AreEqual(ErrorCode.START_WORK_NO_STORAGE_CAPACITY, ec);
             * */
        }

        [TestMethod]
        public void TestCanBuildItemFailNotEnoughNeg()
        {
            Assert.Fail("Not implemented");

            /*
            Player.BuildableItems[BuildableItemEnum.Dirty_Dishes] = 2;

            ErrorCode ec = GamePlayerLogic.Instance.CanBuildItem(Player, BuildableItemEnum.Dirty_Dishes);

            Assert.AreEqual(ErrorCode.START_WORK_INSUFFICIENT_NEGATIVE, ec);
             * */
        }

        [TestMethod]
        public void TestCanBuildItemFailNonConsumable()
        {
            Assert.Fail("Not implemented");

            /*
            ErrorCode ec = GamePlayerLogic.Instance.CanBuildItem(Player, BuildableItemEnum.Dry_Goods_Delivery_Truck_L1);

            Assert.AreEqual(ErrorCode.START_WORK_MULTIPLE_NON_CONSUMABLE, ec);
             * */
        }

        [TestMethod]
        public void TestStartWorkImmediate()
        {
            Assert.Fail("Not implemented");

            /*
            Player.BuildableItems[BuildableItemEnum.Dirty_Dishes] = 3;

            GamePlayerLogic.Instance.StartWork(Player, BuildableItemEnum.Dirty_Dishes);

            Assert.AreEqual(0, Player.BuildableItems[BuildableItemEnum.Dirty_Dishes]);
            Assert.AreEqual(0, Player.WorkItems.Count);
            Assert.AreEqual(true, Player.StateChanged);
            Assert.AreEqual(100, Player.Experience);
             * */
        }

        [TestMethod]
        public void TestGetCurrentWorkItemsForProductionItemItemsInProduction()
        {
            Assert.Fail("Not implemented");

            /*
            GamePlayerLogic.Instance.StartWork(Player, BuildableItemEnum.White_Flour);
            GamePlayerLogic.Instance.StartWork(Player, BuildableItemEnum.White_Flour);

            List<WorkInProgress> wis = GamePlayerLogic.Instance.GetCurrentWorkItemsForProductionItem(Player, BuildableItemEnum.Dry_Goods_Delivery_Truck_L1);

            Assert.AreEqual(2, wis.Count);
             * */
        }

        [TestMethod]
        public void TestGetCurrentWorkItemsForProductionItemNoItemsInProduction()
        {
            List<WorkInProgress> wis = 
                GamePlayerLogic
                    .Instance
                    .GetCurrentWorkItemsForProductionItem(Player, BuildableItemEnum.Dry_Goods_Delivery_Truck);

            Assert.AreEqual(0, wis.Count);
        }

        [TestMethod]
        public void TestGetPercentageCompleteForWorkItem()
        {
            Assert.Fail("Not implemented");

            /*
            GamePlayerLogic.Instance.StartWork(Player, BuildableItemEnum.White_Flour);
            GamePlayerLogic.Instance.StartWork(Player, BuildableItemEnum.White_Flour);

            Player.WorkItems[0].FinishTime = DateTime.UtcNow.AddSeconds(2);

            double ratio = GamePlayerLogic.Instance.GetPercentageCompleteForWorkItem(Player.WorkItems[0]);

            Assert.IsTrue(ratio < 1.2);
            Assert.IsTrue(ratio > 50.0 / 60.0);
             * */
        }

        [TestMethod]
        public void TestLevelChangedEvent()
        {
            bool levelChanged = false;
            int newLevel = -1;

            GamePlayerLogic.Instance.LevelChanged += (o, e) =>
            {
                levelChanged = true;
                newLevel = e.Value.Value;
            };

            GamePlayerLogic.Instance.SetLevel(Player, 3);

            Assert.AreEqual(3, newLevel);
            Assert.AreEqual(true, levelChanged);
        }

        [TestMethod]
        public void TestExperienceChangedEvent()
        {
            bool experienceChanged = false;
            int addedExperience = -1;

            GamePlayerLogic.Instance.ExperienceChanged += (o, e) =>
            {
                experienceChanged = true;
                addedExperience = e.Value.Value;
            };

            GamePlayerLogic.Instance.AddExperience(Player, 150);

            Assert.AreEqual(150, addedExperience);
            Assert.AreEqual(true, experienceChanged);
        }

        [TestMethod]
        public void TestCoinsChangedEvent()
        {
            bool coinsChanged = false;
            int coinChangeAmount = -1;

            GamePlayerLogic.Instance.CoinsChanged += (o, e) =>
            {
                coinsChanged = true;
                coinChangeAmount = e.Value.Value;
            };

            GamePlayerLogic.Instance.ModifyCoins(Player, 150);

            Assert.AreEqual(150, coinChangeAmount);
            Assert.AreEqual(true, coinsChanged);
        }

        [TestMethod]
        public void TestCouponsChangedEvent()
        {
            bool couponsChanged = false;
            int couponChangeAmount = -1;

            GamePlayerLogic.Instance.CouponsChanged += (o, e) =>
            {
                couponsChanged = true;
                couponChangeAmount = e.Value.Value;
            };

            GamePlayerLogic.Instance.ModifyCoupons(Player, -3);

            Assert.AreEqual(-3, couponChangeAmount);
            Assert.AreEqual(true, couponsChanged);
        }

        [TestMethod]
        public void TestItemConsumedEvent()
        {
            Assert.Fail("Not implemented");

            /*
            Player.BuildableItems[BuildableItemEnum.White_Flour] = 3;
            Player.BuildableItems[BuildableItemEnum.Yeast] = 2;
            Player.BuildableItems[BuildableItemEnum.Salt] = 1;
            Player.BuildableItems[BuildableItemEnum.Dough_Mixer_L1] = 1;

            bool itemConsumed = false;
            List<ItemQuantity> items = new List<ItemQuantity>();

            GamePlayerLogic.Instance.ItemConsumed += (o, e) =>
            {
                itemConsumed = true;
                items.Add(e.ItemQuantity);
            };

            GamePlayerLogic.Instance.DeductResources(Player, BuildableItemEnum.White_Pizza_Dough);

            Assert.AreEqual(3, items.Count);
            Assert.AreEqual(BuildableItemEnum.White_Flour, items[0].ItemCode);
            Assert.AreEqual(1, items[0].Quantity);
            Assert.AreEqual(BuildableItemEnum.Salt, items[1].ItemCode);
            Assert.AreEqual(1, items[1].Quantity);
            Assert.AreEqual(BuildableItemEnum.Yeast, items[2].ItemCode);
            Assert.AreEqual(1, items[2].Quantity);
            Assert.AreEqual(true, itemConsumed);
             * */
        }

        [TestMethod]
        public void TestItemSubtractedEvent()
        {
            Assert.Fail("Not implemented");

            /*
            Player.BuildableItems[BuildableItemEnum.White_Flour] = 3;

            bool itemSubtracted = false;
            List<ItemQuantity> items = new List<ItemQuantity>();

            GamePlayerLogic.Instance.ItemSubtracted += (o, e) =>
            {
                itemSubtracted = true;
                items.Add(e.ItemQuantity);
            };

            GamePlayerLogic.Instance.SubtractItem(Player, 
                new WorkInProgress { ItemCode = BuildableItemEnum.White_Flour });

            Assert.AreEqual(1, items.Count);
            Assert.AreEqual(BuildableItemEnum.White_Flour, items[0].ItemCode);
            Assert.AreEqual(1, items[0].Quantity);
            Assert.AreEqual(true, itemSubtracted);
             * */
        }

        [TestMethod]
        public void TestItemAddedEvent()
        {
            Assert.Fail("Not implemented");

            /*
            Player.BuildableItems[BuildableItemEnum.White_Flour] = 3;

            bool itemAdded = false;
            List<ItemQuantity> items = new List<ItemQuantity>();

            GamePlayerLogic.Instance.ItemAdded += (o, e) =>
            {
                itemAdded = true;
                items.Add(e.ItemQuantity);
            };

            GamePlayerLogic.Instance.AddItem(Player,
                new WorkInProgress { ItemCode = BuildableItemEnum.White_Flour });

            Assert.AreEqual(1, items.Count);
            Assert.AreEqual(BuildableItemEnum.White_Flour, items[0].ItemCode);
            Assert.AreEqual(1, items[0].Quantity);
            Assert.AreEqual(true, itemAdded);
             * */
        }

        [TestMethod]
        public void TestWorkStartedEvent()
        {
            bool workStarted = false;
            List<WorkInProgress> items = new List<WorkInProgress>();

            GamePlayerLogic.Instance.WorkStarted += (o, e) =>
            {
                workStarted = true;
                items.Add(e.WorkItem);
            };

            GamePlayerLogic.Instance.StartWork(Player, 0, 0, BuildableItemEnum.White_Flour);

            Assert.AreEqual(1, items.Count);
            Assert.AreEqual(BuildableItemEnum.White_Flour, items[0].Quantity.ItemCode);
            Assert.AreEqual(true, workStarted);
        }

        [TestMethod]
        public void TestWorkFinishedEvent()
        {
            Assert.Fail("Not implemented");

            /*
            Player.BuildableItems[BuildableItemEnum.Dirty_Dishes] = 3;

            bool workFinished = false;
            List<WorkInProgress> items = new List<WorkInProgress>();

            GamePlayerLogic.Instance.WorkFinished += (o, e) =>
            {
                workFinished = true;
                items.Add(e.WorkItem);
            };

            GamePlayerLogic.Instance.StartWork(Player, BuildableItemEnum.Dirty_Dishes);

            Assert.AreEqual(1, items.Count);
            Assert.AreEqual(BuildableItemEnum.Dirty_Dishes, items[0].ItemCode);
            Assert.AreEqual(true, workFinished);
             * */
        }
    }
}
