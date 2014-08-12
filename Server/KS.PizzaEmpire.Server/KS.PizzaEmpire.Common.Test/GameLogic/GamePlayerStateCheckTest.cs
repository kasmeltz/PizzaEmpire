using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KS.PizzaEmpire.Common.BusinessObjects;
using KS.PizzaEmpire.Common.GameLogic;
using System.Collections.Generic;

namespace KS.PizzaEmpire.Common.Test.GameLogic
{
    [TestClass]
    public class GamePlayerStateCheckTest
    {
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

            Dictionary<int, ExperienceLevel> elevels = new Dictionary<int, ExperienceLevel>();
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
        
        [TestMethod]
        public void TestTutorialStageFalse()
        {
            // Arrange
            GamePlayer player = new GamePlayer { TutorialStage = 3 };
            GamePlayerStateCheck stateCheck = new GamePlayerStateCheck { TutorialStage = 4 };

            // Act
            bool tf = stateCheck.CheckAll(player);

            // Assert
            Assert.AreEqual(false, tf);
        }

        [TestMethod]
        public void TestTutorialStageTrue()
        {
            // Arrange
            GamePlayer player = new GamePlayer { TutorialStage = 4 };
            GamePlayerStateCheck stateCheck = new GamePlayerStateCheck { TutorialStage = 4 };

            // Act
            bool tf = stateCheck.CheckAll(player);

            // Assert
            Assert.AreEqual(true, tf);
        }

        [TestMethod]
        public void TestCoinsFalse()
        {
            // Arrange
            GamePlayer player = new GamePlayer { TutorialStage = 3 };
            GamePlayerStateCheck stateCheck = new GamePlayerStateCheck { Coins = 4 };

            // Act
            bool tf = stateCheck.CheckAll(player);

            // Assert
            Assert.AreEqual(false, tf);
        }

        [TestMethod]
        public void TestCoinsTrue()
        {
            // Arrange
            GamePlayer player = new GamePlayer { Coins = 4 };
            GamePlayerStateCheck stateCheck = new GamePlayerStateCheck { Coins = 4 };

            // Act
            bool tf = stateCheck.CheckAll(player);

            // Assert
            Assert.AreEqual(true, tf);
        }

        [TestMethod]
        public void TestRequiredLevelFalse()
        {
            // Arrange
            GamePlayer player = new GamePlayer { Level = 3 };
            GamePlayerStateCheck stateCheck = new GamePlayerStateCheck { RequiredLevel = 4 };

            // Act
            bool tf = stateCheck.CheckAll(player);

            // Assert
            Assert.AreEqual(false, tf);
        }

        [TestMethod]
        public void TestRequiredLevelTrue()
        {
            // Arrange
            GamePlayer player = new GamePlayer { Level = 4 };
            GamePlayerStateCheck stateCheck = new GamePlayerStateCheck { RequiredLevel = 4 };

            // Act
            bool tf = stateCheck.CheckAll(player);

            // Assert
            Assert.AreEqual(true, tf);
        }

        [TestMethod]
        public void TestItemQuantityGreaterThanFalse()
        {
            // Arrange
            GamePlayer player = new GamePlayer
            {
                BuildableItems = new Dictionary<BuildableItemEnum, int>()
            };

            GamePlayerStateCheck stateCheck = new GamePlayerStateCheck
            {
                ItemQuantityGreaterThan = new List<ItemQuantity> { 
                    new ItemQuantity { ItemCode = BuildableItemEnum.Sliced_Pepperoni, Quantity = 2 },
                    new ItemQuantity { ItemCode = BuildableItemEnum.Salt, Quantity = 3 }
                }
            };

            // Act
            bool tf = stateCheck.CheckAll(player);

            // Assert
            Assert.AreEqual(false, tf);
        }

        [TestMethod]
        public void TestItemQuantityGreaterThanTrue()
        {
            // Arrange
            GamePlayer player = new GamePlayer
            {
                BuildableItems = new Dictionary<BuildableItemEnum, int> 
                { 
                     { BuildableItemEnum.Sliced_Pepperoni, 3 },
                     { BuildableItemEnum.Salt, 4 } 
                }
            };

            GamePlayerStateCheck stateCheck = new GamePlayerStateCheck
            {
                ItemQuantityGreaterThan = new List<ItemQuantity> { 
                    new ItemQuantity { ItemCode = BuildableItemEnum.Sliced_Pepperoni, Quantity = 2 },
                    new ItemQuantity { ItemCode = BuildableItemEnum.Salt, Quantity = 3 }
                }
            };

            // Act
            bool tf = stateCheck.CheckAll(player);

            // Assert
            Assert.AreEqual(true, tf);
        }

        [TestMethod]
        public void TestItemQuantityLessThanFalse()
        {
            // Arrange
            GamePlayer player = new GamePlayer
            {
                BuildableItems = new Dictionary<BuildableItemEnum, int> 
                { 
                     { BuildableItemEnum.Sliced_Pepperoni, 4 },
                     { BuildableItemEnum.Salt, 3 } 
                }
            };

            GamePlayerStateCheck stateCheck = new GamePlayerStateCheck
            {
                ItemQuantityLessThan = new List<ItemQuantity> { 
                    new ItemQuantity { ItemCode = BuildableItemEnum.Sliced_Pepperoni, Quantity = 2 },
                    new ItemQuantity { ItemCode = BuildableItemEnum.Salt, Quantity = 3 }
                }
            };

            // Act
            bool tf = stateCheck.CheckAll(player);

            // Assert
            Assert.AreEqual(false, tf);
        }

        [TestMethod]
        public void TestItemQuantityLessThanTrue()
        {
            // Arrange
            GamePlayer player = new GamePlayer
            {
                BuildableItems = new Dictionary<BuildableItemEnum, int> 
                { 
                     { BuildableItemEnum.Sliced_Pepperoni, 1 },
                     { BuildableItemEnum.Salt, 1 } 
                }
            };

            GamePlayerStateCheck stateCheck = new GamePlayerStateCheck
            {
                ItemQuantityLessThan = new List<ItemQuantity> { 
                    new ItemQuantity { ItemCode = BuildableItemEnum.Sliced_Pepperoni, Quantity = 2 },
                    new ItemQuantity { ItemCode = BuildableItemEnum.Salt, Quantity = 3 }
                }
            };

            // Act
            bool tf = stateCheck.CheckAll(player);

            // Assert
            Assert.AreEqual(true, tf);
        }

        [TestMethod]
        public void TestWorkItemsInProgressFalse()
        {
            // Arrange
            GamePlayer player = new GamePlayer
            {
                WorkItems = new List<WorkItem> {
                     new WorkItem { ItemCode = BuildableItemEnum.Olive_Oil, FinishTime = DateTime.Now }
                }
            };

            GamePlayerStateCheck stateCheck = new GamePlayerStateCheck
            {
                WorkItemsInProgress = new List<ItemQuantity> {
                     new ItemQuantity { ItemCode = BuildableItemEnum.Mozzarella_Cheese, Quantity = 1 }
                }
            };

            // Act
            bool tf = stateCheck.CheckAll(player);

            // Assert
            Assert.AreEqual(false, tf);
        }

        [TestMethod]
        public void TestWorkItemsInProgressTrue()
        {
            // Arrange
            GamePlayer player = new GamePlayer
            {
                WorkItems = new List<WorkItem> {
                     new WorkItem { ItemCode = BuildableItemEnum.Mozzarella_Cheese, FinishTime = DateTime.Now }
                }
            };

            GamePlayerStateCheck stateCheck = new GamePlayerStateCheck
            {
                WorkItemsInProgress = new List<ItemQuantity> {
                     new ItemQuantity { ItemCode = BuildableItemEnum.Mozzarella_Cheese, Quantity = 1 }
                }
            };

            // Act
            bool tf = stateCheck.CheckAll(player);

            // Assert
            Assert.AreEqual(true, tf);
        }

        [TestMethod]
        public void TestCanBuildItemFalse()
        {
            // Arrange
            GamePlayer player = GamePlayerLogic.Instance.CreateNewGamePlayer();
            GamePlayerStateCheck stateCheck = new GamePlayerStateCheck
            {
                CanBuildItem = BuildableItemEnum.White_Pizza_Dough
            };

            // Act
            bool tf = stateCheck.CheckAll(player);

            // Assert
            Assert.AreEqual(false, tf);
        }

        [TestMethod]
        public void TestCanBuildItemTrue()
        {
            // Arrange
            GamePlayer player = GamePlayerLogic.Instance.CreateNewGamePlayer();
            GamePlayerStateCheck stateCheck = new GamePlayerStateCheck
            {
                CanBuildItem = BuildableItemEnum.White_Flour
            };

            // Act
            bool tf = stateCheck.CheckAll(player);

            // Assert
            Assert.AreEqual(true, tf);
        }

        [TestMethod]
        public void TestOneFailedFalse()
        {
            // Arrange
            GamePlayer player = new GamePlayer { Coins = 3, TutorialStage = 4 };
            GamePlayerStateCheck stateCheck = new GamePlayerStateCheck { Coins = 4, TutorialStage = 4 };

            // Act
            bool tf = stateCheck.CheckAll(player);

            // Assert
            Assert.AreEqual(false, tf);
        }

        [TestMethod]
        public void TestMultiplePassed()
        {
            // Arrange
            GamePlayer player = new GamePlayer { Coins = 4, TutorialStage = 4 };
            GamePlayerStateCheck stateCheck = new GamePlayerStateCheck { Coins = 4, TutorialStage = 4 };

            // Act
            bool tf = stateCheck.CheckAll(player);

            // Assert
            Assert.AreEqual(true, tf);
        }
    }
}
