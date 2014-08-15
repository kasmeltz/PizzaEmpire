using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KS.PizzaEmpire.Common.GameLogic;
using KS.PizzaEmpire.Common.BusinessObjects;
using System.Collections.Generic;

namespace KS.PizzaEmpire.Common.Test.GameLogic
{
    [TestClass]
    public class GamePlayerLogicTest
    {
        public GamePlayer Player { get; set; }

        [ClassInitialize]
        public static void InitializeAllTests(TestContext testContent)
        {            
            Dictionary<BuildableItemEnum, BuildableItem> bitems = new Dictionary<BuildableItemEnum, BuildableItem>();
            BuildableItem bi;

            bi = new BuildableItem
            {
                ItemCode = BuildableItemEnum.Restaurant_Storage,
                RequiredLevel = 1,
                CoinCost = 0,
                ProductionItem = BuildableItemEnum.None,
                ProductionCapacity = 0,
                BaseProduction = 1,
                StorageCapacity = 5,
                StorageItem = BuildableItemEnum.None,
                IsStorage = true,
                IsConsumable = false,
                IsImmediate = false,
                IsWorkSubtracted = false,
                Experience = 0,
                BuildSeconds = 0,
                CouponCost = 0,
                SpeedUpCoupons = 0, 
                SpeedUpSeconds = 0
            };
            bitems[bi.ItemCode] = bi;

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
                BuildSeconds = 60,
                CouponCost = 0,
                SpeedUpCoupons = 1,
                SpeedUpSeconds = 60,
            };
            bitems[bi.ItemCode] = bi;

            bi = new BuildableItem
            {
                ItemCode = BuildableItemEnum.Yeast,
                RequiredLevel = 2,
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
                BuildSeconds = 60,
                CouponCost = 0,
                SpeedUpCoupons = 1, 
                SpeedUpSeconds = 60
            };
            bitems[bi.ItemCode] = bi;

            bi = new BuildableItem
            {
                ItemCode = BuildableItemEnum.Salt,
                RequiredLevel = 2,
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
                BuildSeconds = 60,
                CouponCost = 0,
                SpeedUpCoupons = 1, 
                SpeedUpSeconds = 60
            };
            bitems[bi.ItemCode] = bi;
            
            bi = new BuildableItem
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
            bitems[bi.ItemCode] = bi;

            bi = new BuildableItem
            {
                ItemCode = BuildableItemEnum.Dry_Goods_Delivery_Truck_L1,
                RequiredLevel = 1,
                CoinCost = 50,
                ProductionItem = BuildableItemEnum.None,
                ProductionCapacity = 2,
                BaseProduction = 1,
                StorageCapacity = 0,
                StorageItem = BuildableItemEnum.None,
                IsStorage = false,
                IsConsumable = false,
                IsImmediate = false,
                IsWorkSubtracted = false,
                Experience = 100,
                BuildSeconds = 60,
                CouponCost = 0,
                SpeedUpCoupons = 1,
                SpeedUpSeconds = 60,
            };
            bitems[bi.ItemCode] = bi;
            
            bi = new BuildableItem
            {
                ItemCode = BuildableItemEnum.Dough_Mixer_L1,
                RequiredLevel = 1,
                CoinCost = 50,
                ProductionItem = BuildableItemEnum.None,
                ProductionCapacity = 2,
                BaseProduction = 1,
                StorageCapacity = 0,
                StorageItem = BuildableItemEnum.None,
                IsStorage = false,
                IsConsumable = false,
                IsImmediate = false,
                IsWorkSubtracted = false,
                Experience = 100,
                BuildSeconds = 60,
                CouponCost = 0,
                SpeedUpCoupons = 1,
                SpeedUpSeconds = 60,
            };
            bitems[bi.ItemCode] = bi;

            bi = new BuildableItem
            {
                ItemCode = BuildableItemEnum.Tomatoes,
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
                BuildSeconds = 60,
                CouponCost = 1,
                SpeedUpCoupons = 1,
                SpeedUpSeconds = 60,
            };
            bitems[bi.ItemCode] = bi;

            bi = new BuildableItem
            {
                ItemCode = BuildableItemEnum.Dirty_Dishes,
                RequiredLevel = 1,
                CoinCost = 50,
                ProductionItem = BuildableItemEnum.None,
                ProductionCapacity = 0,
                BaseProduction = 3,
                StorageCapacity = 0,
                StorageItem = BuildableItemEnum.None,
                IsStorage = false,
                IsConsumable = true,
                IsImmediate = true,
                IsWorkSubtracted = true,
                Experience = 100,
                BuildSeconds = -1,
                CouponCost = 1,
                SpeedUpCoupons = 1,
                SpeedUpSeconds = 60,
            };
            bitems[bi.ItemCode] = bi;

            bi = new BuildableItem
            {
                ItemCode = BuildableItemEnum.Dirty_Table,
                RequiredLevel = 1,
                CoinCost = 50,
                ProductionItem = BuildableItemEnum.None,
                ProductionCapacity = 0,
                BaseProduction = 3,
                StorageCapacity = 0,
                StorageItem = BuildableItemEnum.None,
                IsStorage = false,
                IsConsumable = true,
                IsImmediate = true,
                IsWorkSubtracted = true,
                Experience = 100,
                BuildSeconds = -1,
                CouponCost = 1,
                SpeedUpCoupons = 1,
                SpeedUpSeconds = 60,
            };
            bitems[bi.ItemCode] = bi;

            bi = new BuildableItem
            {
                ItemCode = BuildableItemEnum.Dirty_Floor,
                RequiredLevel = 1,
                CoinCost = 50,
                ProductionItem = BuildableItemEnum.None,
                ProductionCapacity = 0,
                BaseProduction = 3,
                StorageCapacity = 0,
                StorageItem = BuildableItemEnum.None,
                IsStorage = false,
                IsConsumable = true,
                IsImmediate = true,
                IsWorkSubtracted = true,
                Experience = 100,
                BuildSeconds = -1,
                CouponCost = 1,
                SpeedUpCoupons = 1,
                SpeedUpSeconds = 60,
            };
            bitems[bi.ItemCode] = bi;

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
        public void TestCreateNewGamePlayer()
        {
            Assert.AreEqual(1000, Player.Coins);
            Assert.AreEqual(5, Player.Coupons);
            Assert.AreEqual(1, Player.Level);
            Assert.AreEqual(0, Player.Experience);
            Assert.AreEqual(5, Player.BuildableItems.Values.Count);
            Assert.AreEqual(1, Player.BuildableItems[BuildableItemEnum.Dry_Goods_Delivery_Truck_L1]);
            Assert.AreEqual(1, Player.BuildableItems[BuildableItemEnum.Restaurant_Storage]);
            Assert.AreEqual(0, Player.WorkItems.Count);
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
            Assert.AreEqual(true,
                GamePlayerLogic.Instance.DoesPlayerHaveProductionCapacity(Player, BuildableItemEnum.White_Flour));
        }

        [TestMethod]
        public void TestDoesPlayerHaveProductionCapacityFalse()
        {
            Player.WorkItems.Add(new WorkItem { ItemCode = BuildableItemEnum.White_Flour });
            Player.WorkItems.Add(new WorkItem { ItemCode = BuildableItemEnum.White_Flour });
            Assert.AreEqual(false,
                GamePlayerLogic.Instance.DoesPlayerHaveProductionCapacity(Player, BuildableItemEnum.White_Flour));
        }

        [TestMethod]
        public void TestDoesPlayeraHaveStorageCapacityTrue()
        {
            Assert.AreEqual(true,
                GamePlayerLogic.Instance.DoesPlayeraHaveStorageCapacity(Player, BuildableItemEnum.White_Flour));
        }

        [TestMethod]
         public void TestDoesPlayeraHaveStorageCapacityFalse()
        {
            Player.BuildableItems[BuildableItemEnum.White_Flour] = 10;
            Assert.AreEqual(false,
                GamePlayerLogic.Instance.DoesPlayeraHaveStorageCapacity(Player, BuildableItemEnum.White_Flour));
        }

        [TestMethod]
        public void TestAddItem()
        {
            Assert.AreEqual(false, Player.BuildableItems.ContainsKey(BuildableItemEnum.White_Flour));
            GamePlayerLogic.Instance.AddItem(Player, new WorkItem { ItemCode = BuildableItemEnum.White_Flour });
            Assert.AreEqual(1, Player.BuildableItems[BuildableItemEnum.White_Flour]);
            Assert.AreEqual(true, Player.StateChanged);
        }

        [TestMethod]
        public void TestAddItemNonConsumable()
        {
            GamePlayerLogic.Instance.AddItem(Player, new WorkItem { ItemCode = BuildableItemEnum.Dry_Goods_Delivery_Truck_L1 });
            Assert.AreEqual(1, Player.BuildableItems[BuildableItemEnum.Dry_Goods_Delivery_Truck_L1]);
            GamePlayerLogic.Instance.AddItem(Player, new WorkItem { ItemCode = BuildableItemEnum.Dry_Goods_Delivery_Truck_L1 });
            Assert.AreEqual(1, Player.BuildableItems[BuildableItemEnum.Dry_Goods_Delivery_Truck_L1]);
            Assert.AreEqual(true, Player.StateChanged);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestFinishWorkNone()
        {
            List<WorkItem> finishedItems = GamePlayerLogic.Instance.FinishWork(Player, DateTime.UtcNow);
            Assert.AreEqual(0, finishedItems.Count);
            Assert.AreEqual(true, Player.StateChanged);
        }

        [TestMethod]
        public void TestFinishWorkNoneReady()
        {
            Player.WorkItems.Add(new WorkItem { ItemCode = BuildableItemEnum.White_Flour, FinishTime = DateTime.UtcNow.AddHours(1) });
            List<WorkItem> finishedItems = GamePlayerLogic.Instance.FinishWork(Player, DateTime.UtcNow);
            Assert.AreEqual(0, finishedItems.Count);
            Assert.AreEqual(true, Player.StateChanged);
        }

        [TestMethod]
        public void TestFinishWorkOneReady()
        {
            Player.WorkItems.Add(new WorkItem { ItemCode = BuildableItemEnum.White_Flour, FinishTime = DateTime.UtcNow.AddHours(1) });
            Player.WorkItems.Add(new WorkItem { ItemCode = BuildableItemEnum.White_Flour, FinishTime = DateTime.UtcNow.AddHours(-1) });
            List<WorkItem> finishedItems = GamePlayerLogic.Instance.FinishWork(Player, DateTime.UtcNow);
            Assert.AreEqual(1, finishedItems.Count);
            Assert.AreEqual(BuildableItemEnum.White_Flour, finishedItems[0].ItemCode);
            Assert.AreEqual(true, Player.StateChanged);
        }

        [TestMethod]
        public void TestFinishWorkTwoReady()
        {
            Player.WorkItems.Add(new WorkItem { ItemCode = BuildableItemEnum.White_Flour, FinishTime = DateTime.UtcNow.AddHours(1) });
            Player.WorkItems.Add(new WorkItem { ItemCode = BuildableItemEnum.Yeast, FinishTime = DateTime.UtcNow.AddHours(-1) });
            Player.WorkItems.Add(new WorkItem { ItemCode = BuildableItemEnum.Salt, FinishTime = DateTime.UtcNow.AddHours(-1) });
            List<WorkItem> finishedItems = GamePlayerLogic.Instance.FinishWork(Player, DateTime.UtcNow);
            Assert.AreEqual(2, finishedItems.Count);
            Assert.AreEqual(BuildableItemEnum.Yeast, finishedItems[0].ItemCode);
            Assert.AreEqual(BuildableItemEnum.Salt, finishedItems[1].ItemCode);
            Assert.AreEqual(true, Player.StateChanged);
        }

        [TestMethod]
        public void TestDeductResources()
        {
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
        }

        [TestMethod]
        public void TestStartWork()
        {
            WorkItem workItem = GamePlayerLogic.Instance.StartWork(Player, BuildableItemEnum.White_Flour);
            Assert.AreEqual(BuildableItemEnum.White_Flour, workItem.ItemCode);
            Assert.AreEqual(true, Player.StateChanged);
            Assert.AreEqual(950, Player.Coins);
            Assert.AreEqual(5, Player.Coupons);
        }

        [TestMethod]
        public void TestCanBuildItemFailItemType()
        {
            ErrorCode ec = GamePlayerLogic.Instance.CanBuildItem(Player, (BuildableItemEnum)5000);

            Assert.AreEqual(ErrorCode.START_WORK_INVALID_ITEM, ec);
        }

        [TestMethod]
        public void TestCanBuildItemFailLevel()
        {
            GamePlayer player = GamePlayerLogic.Instance.CreateNewGamePlayer();

            ErrorCode ec = GamePlayerLogic.Instance.CanBuildItem(player, BuildableItemEnum.White_Pizza_Dough);

            Assert.AreEqual(ErrorCode.START_WORK_ITEM_NOT_AVAILABLE, ec);
        }

        [TestMethod]
        public void TestCanBuildItemFailCoins()
        {
            Player.Coins = 0;

            ErrorCode ec = GamePlayerLogic.Instance.CanBuildItem(Player, BuildableItemEnum.White_Flour);

            Assert.AreEqual(ErrorCode.START_WORK_INSUFFICIENT_COINS, ec);
        }

        [TestMethod]
        public void TestCanBuildItemFailCoupons()
        {
            Player.Coupons = 0;

            ErrorCode ec = GamePlayerLogic.Instance.CanBuildItem(Player, BuildableItemEnum.Tomatoes);

            Assert.AreEqual(ErrorCode.START_WORK_INSUFFICIENT_COUPONS, ec);
        }

        [TestMethod]
        public void TestCanBuildItemFailImproperIngredients()
        {
            GamePlayerLogic.Instance.SetLevel(Player, 3);

            ErrorCode ec = GamePlayerLogic.Instance.CanBuildItem(Player, BuildableItemEnum.White_Pizza_Dough);

            Assert.AreEqual(ErrorCode.START_WORK_INVALID_INGREDIENTS, ec);
        }

        [TestMethod]
        public void TestCanBuildItemFailInsufficeintIngredients()
        {
            GamePlayerLogic.Instance.SetLevel(Player, 3);
            Player.BuildableItems[BuildableItemEnum.Salt] = 0;
            Player.BuildableItems[BuildableItemEnum.White_Flour] = 0;
            Player.BuildableItems[BuildableItemEnum.Yeast] = 0;

            ErrorCode ec = GamePlayerLogic.Instance.CanBuildItem(Player, BuildableItemEnum.White_Pizza_Dough);
            
            Assert.AreEqual(ErrorCode.START_WORK_INSUFFICIENT_INGREDIENTS, ec);
        }

        [TestMethod]
        public void TestCanBuildItemFailEquipmentFull()
        {
            GamePlayerLogic.Instance.StartWork(Player, BuildableItemEnum.White_Flour);
            GamePlayerLogic.Instance.StartWork(Player, BuildableItemEnum.White_Flour);

            ErrorCode ec = GamePlayerLogic.Instance.CanBuildItem(Player, BuildableItemEnum.White_Flour);

            Assert.AreEqual(ErrorCode.START_WORK_NO_PRODUCTION_CAPACITY, ec);
        }

        [TestMethod]
        public void TestCanBuildItemFailStorageFull()
        {
            Player.BuildableItems[BuildableItemEnum.White_Flour] = 10;

            ErrorCode ec = GamePlayerLogic.Instance.CanBuildItem(Player, BuildableItemEnum.White_Flour);

            Assert.AreEqual(ErrorCode.START_WORK_NO_STORAGE_CAPACITY, ec);
        }

        [TestMethod]
        public void TestCanBuildItemFailNotEnoughNeg()
        {
            Player.BuildableItems[BuildableItemEnum.Dirty_Dishes] = 2;

            ErrorCode ec = GamePlayerLogic.Instance.CanBuildItem(Player, BuildableItemEnum.Dirty_Dishes);

            Assert.AreEqual(ErrorCode.START_WORK_INSUFFICIENT_NEGATIVE, ec);
        }

        [TestMethod]
        public void TestCanBuildItemFailNonConsumable()
        {
            ErrorCode ec = GamePlayerLogic.Instance.CanBuildItem(Player, BuildableItemEnum.Dry_Goods_Delivery_Truck_L1);

            Assert.AreEqual(ErrorCode.START_WORK_MULTIPLE_NON_CONSUMABLE, ec);
        }

        [TestMethod]
        public void TestStartWorkImmediate()
        {
            Player.BuildableItems[BuildableItemEnum.Dirty_Dishes] = 3;

            GamePlayerLogic.Instance.StartWork(Player, BuildableItemEnum.Dirty_Dishes);

            Assert.AreEqual(0, Player.BuildableItems[BuildableItemEnum.Dirty_Dishes]);
            Assert.AreEqual(0, Player.WorkItems.Count);
            Assert.AreEqual(true, Player.StateChanged);
            Assert.AreEqual(100, Player.Experience);
        }

        [TestMethod]
        public void TestGetCurrentWorkItemsForProductionItemItemsInProduction()
        {
            GamePlayerLogic.Instance.StartWork(Player, BuildableItemEnum.White_Flour);
            GamePlayerLogic.Instance.StartWork(Player, BuildableItemEnum.White_Flour);

            List<WorkItem> wis = GamePlayerLogic.Instance.GetCurrentWorkItemsForProductionItem(Player, BuildableItemEnum.Dry_Goods_Delivery_Truck_L1);

            Assert.AreEqual(2, wis.Count);
        }

        [TestMethod]
        public void TestGetCurrentWorkItemsForProductionItemNoItemsInProduction()
        {
            List<WorkItem> wis = GamePlayerLogic.Instance.GetCurrentWorkItemsForProductionItem(Player, BuildableItemEnum.Dry_Goods_Delivery_Truck_L1);

            Assert.AreEqual(0, wis.Count);
        }

        [TestMethod]
        public void TestGetPercentageCompleteForWorkItem()
        {
            GamePlayerLogic.Instance.StartWork(Player, BuildableItemEnum.White_Flour);
            GamePlayerLogic.Instance.StartWork(Player, BuildableItemEnum.White_Flour);

            Player.WorkItems[0].FinishTime = DateTime.UtcNow.AddSeconds(2);

            double ratio = GamePlayerLogic.Instance.GetPercentageCompleteForWorkItem(Player.WorkItems[0]);

            Assert.IsTrue(ratio < 1.2);
            Assert.IsTrue(ratio > 50.0 / 60.0);
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
        }

        [TestMethod]
        public void TestItemSubtractedEvent()
        {
            Player.BuildableItems[BuildableItemEnum.White_Flour] = 3;

            bool itemSubtracted = false;
            List<ItemQuantity> items = new List<ItemQuantity>();

            GamePlayerLogic.Instance.ItemSubtracted += (o, e) =>
            {
                itemSubtracted = true;
                items.Add(e.ItemQuantity);
            };

            GamePlayerLogic.Instance.SubtractItem(Player, 
                new WorkItem { ItemCode = BuildableItemEnum.White_Flour });

            Assert.AreEqual(1, items.Count);
            Assert.AreEqual(BuildableItemEnum.White_Flour, items[0].ItemCode);
            Assert.AreEqual(1, items[0].Quantity);
            Assert.AreEqual(true, itemSubtracted);
        }

        [TestMethod]
        public void TestItemAddedEvent()
        {
            Player.BuildableItems[BuildableItemEnum.White_Flour] = 3;

            bool itemAdded = false;
            List<ItemQuantity> items = new List<ItemQuantity>();

            GamePlayerLogic.Instance.ItemAdded += (o, e) =>
            {
                itemAdded = true;
                items.Add(e.ItemQuantity);
            };

            GamePlayerLogic.Instance.AddItem(Player,
                new WorkItem { ItemCode = BuildableItemEnum.White_Flour });

            Assert.AreEqual(1, items.Count);
            Assert.AreEqual(BuildableItemEnum.White_Flour, items[0].ItemCode);
            Assert.AreEqual(1, items[0].Quantity);
            Assert.AreEqual(true, itemAdded);
        }

        [TestMethod]
        public void TestWorkStartedEvent()
        {
            bool workStarted = false;
            List<WorkItem> items = new List<WorkItem>();

            GamePlayerLogic.Instance.WorkStarted += (o, e) =>
            {
                workStarted = true;
                items.Add(e.WorkItem);
            };

            GamePlayerLogic.Instance.StartWork(Player, BuildableItemEnum.White_Flour);

            Assert.AreEqual(1, items.Count);
            Assert.AreEqual(BuildableItemEnum.White_Flour, items[0].ItemCode);
            Assert.AreEqual(true, workStarted);
        }

        [TestMethod]
        public void TestWorkFinishedEvent()
        {
            Player.BuildableItems[BuildableItemEnum.Dirty_Dishes] = 3;

            bool workFinished = false;
            List<WorkItem> items = new List<WorkItem>();

            GamePlayerLogic.Instance.WorkFinished += (o, e) =>
            {
                workFinished = true;
                items.Add(e.WorkItem);
            };

            GamePlayerLogic.Instance.StartWork(Player, BuildableItemEnum.Dirty_Dishes);

            Assert.AreEqual(1, items.Count);
            Assert.AreEqual(BuildableItemEnum.Dirty_Dishes, items[0].ItemCode);
            Assert.AreEqual(true, workFinished);
        }
    }
}
