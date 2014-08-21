using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KS.PizzaEmpire.Common.BusinessObjects;
using KS.PizzaEmpire.Common.GameLogic;
using System.Collections.Generic;
using KS.PizzaEmpire.GameLogic.ItemLogic;
using KS.PizzaEmpire.Common.GameLogic.GamePlayerState;

namespace KS.PizzaEmpire.Common.Test.GameLogic
{
    [TestClass]
    public class GamePlayerStateCheckTest
    {
        protected GamePlayer Player;
        protected GamePlayerStateCheck StateCheck;

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

        [TestInitialize]
        public void Initialize()
        {
            Player = new GamePlayer
            {
                TutorialStage = 3,
                Coins = 10,
                Level = 3,
                WorkInProgress = new List<WorkInProgress>
                {
                    new WorkInProgress 
                    { 
                        Quantity = new ItemQuantity
                        {
                            ItemCode = BuildableItemEnum.White_Flour,
                            UnStoredQuantity = 1
                        }
                    },
                    new WorkInProgress 
                    {
                        Quantity = new ItemQuantity
                        {
                            ItemCode = BuildableItemEnum.Tomatoes,
                            UnStoredQuantity = 1
                        }                        
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
                                    BuildableItemEnum.White_Flour, 
                                    new ItemQuantity 
                                    { 
                                        Level = 1, ItemCode = BuildableItemEnum.White_Flour, StoredQuantity = 1, UnStoredQuantity = 1
                                    }
                                },
                                {
                                    BuildableItemEnum.Dry_Goods_Delivery_Truck,
                                    new ItemQuantity 
                                    { 
                                        Level = 2, ItemCode = BuildableItemEnum.Dry_Goods_Delivery_Truck, StoredQuantity = 2, UnStoredQuantity = 2
                                    }
                                }
                            }
                        }
                    }
                }
            };

            StateCheck = new GamePlayerStateCheck();
        }

        protected void DoTutorialStageAssert(int stage, ComparisonEnum compare, bool expected)
        {
            // Arrange
            StateCheck.Rules.Add(
                new TutorialStageCompareRule
                {
                    TutorialStage = stage,
                    ComparisonType = compare
                });

            // Act
            bool tf = StateCheck.IsValid(Player);

            // Assert
            Assert.AreEqual(expected, tf);
        }
        
        [TestMethod]
        public void TestTutorialStageEqualTrue()
        {
            DoTutorialStageAssert(3, ComparisonEnum.Equal, true);
        }

        [TestMethod]
        public void TestTutorialStageEqualFalse()
        {
            DoTutorialStageAssert(4, ComparisonEnum.Equal, false);
        }        

        [TestMethod]
        public void TestTutorialStageNotEqualTrue()
        {
            DoTutorialStageAssert(4, ComparisonEnum.NotEqual, true);
        }

        [TestMethod]
        public void TestTutorialStageNotEqualFalse()
        {
            DoTutorialStageAssert(3, ComparisonEnum.NotEqual, false);
        }

        [TestMethod]
        public void TestTutorialStageGreaterThanTrue()
        {
            DoTutorialStageAssert(2, ComparisonEnum.GreaterThan, true);
        }

        [TestMethod]
        public void TestTutorialStageGreaterThanFalse()
        {
            DoTutorialStageAssert(3, ComparisonEnum.GreaterThan, false);
        }

        [TestMethod]
        public void TestTutorialStageGreaterThanOrEqualTrue()
        {
            DoTutorialStageAssert(3, ComparisonEnum.GreaterThanOrEqual, true);
        }

        [TestMethod]
        public void TestTutorialStageGreaterThanOrEqualFalse()
        {
            DoTutorialStageAssert(4, ComparisonEnum.GreaterThanOrEqual, false);
        }

        [TestMethod]
        public void TestTutorialStageLessThanTrue()
        {
            DoTutorialStageAssert(4, ComparisonEnum.LessThan, true);
        }

        [TestMethod]
        public void TestTutorialStageLessThanFalse()
        {
            DoTutorialStageAssert(3, ComparisonEnum.LessThan, false);
        }

        [TestMethod]
        public void TestTutorialStageLessThanOrEqualTrue()
        {
            DoTutorialStageAssert(3, ComparisonEnum.LessThanOrEqual, true);
        }

        [TestMethod]
        public void TestTutorialStageLessThanOrEqualFalse()
        {
            DoTutorialStageAssert(2, ComparisonEnum.LessThanOrEqual, false);
        }

        protected void DoCoinAssert(int coins, ComparisonEnum compare, bool expected)
        {
            // Arrange
            StateCheck.Rules.Add(
                new CoinCompareRule
                {
                    Coins = coins,
                    ComparisonType = compare
                });

            // Act
            bool tf = StateCheck.IsValid(Player);

            // Assert
            Assert.AreEqual(expected, tf);
        }

        [TestMethod]
        public void TestCoinsEqualTrue()
        {
            DoCoinAssert(10, ComparisonEnum.Equal, true);
        }

        [TestMethod]
        public void TestCoinsEqualFalse()
        {
            DoCoinAssert(4, ComparisonEnum.Equal, false);
        }

        [TestMethod]
        public void TestCoinsNotEqualTrue()
        {
            DoCoinAssert(9, ComparisonEnum.NotEqual, true);
        }

        [TestMethod]
        public void TestCoinsNotEqualFalse()
        {
            DoCoinAssert(10, ComparisonEnum.NotEqual, false);
        }

        [TestMethod]
        public void TestCoinsGreaterThanTrue()
        {
            DoCoinAssert(9, ComparisonEnum.GreaterThan, true);
        }

        [TestMethod]
        public void TestCoinsGreaterThanFalse()
        {
            DoCoinAssert(10, ComparisonEnum.GreaterThan, false);
        }

        [TestMethod]
        public void TestCoinsGreaterThanOrEqualTrue()
        {
            DoCoinAssert(10, ComparisonEnum.GreaterThanOrEqual, true);
        }

        [TestMethod]
        public void TestCoinsGreaterThanOrEqualFalse()
        {
            DoCoinAssert(11, ComparisonEnum.GreaterThanOrEqual, false);
        }

        [TestMethod]
        public void TestCoinsLessThanTrue()
        {
            DoCoinAssert(11, ComparisonEnum.LessThan, true);
        }

        [TestMethod]
        public void TestCoinsLessThanFalse()
        {
            DoCoinAssert(10, ComparisonEnum.LessThan, false);
        }

        [TestMethod]
        public void TestCoinsLessThanOrEqualTrue()
        {
            DoCoinAssert(10, ComparisonEnum.LessThanOrEqual, true);
        }

        [TestMethod]
        public void TestCoinsLessThanOrEqualFalse()
        {
            DoCoinAssert(9, ComparisonEnum.LessThanOrEqual, false);
        }

        protected void DoLevelAssert(int level, ComparisonEnum compare, bool expected)
        {
            // Arrange
            StateCheck.Rules.Add(
                new LevelCompareRule
                {
                    Level = level,
                    ComparisonType = compare
                });

            // Act
            bool tf = StateCheck.IsValid(Player);

            // Assert
            Assert.AreEqual(expected, tf);
        }

        [TestMethod]
        public void TestLevelEqualTrue()
        {
            DoLevelAssert(3, ComparisonEnum.Equal, true);
        }

        [TestMethod]
        public void TestLevelEqualFalse()
        {
            DoLevelAssert(4, ComparisonEnum.Equal, false);
        }

        [TestMethod]
        public void TestLevelNotEqualTrue()
        {
            DoLevelAssert(4, ComparisonEnum.NotEqual, true);
        }

        [TestMethod]
        public void TestLevelNotEqualFalse()
        {
            DoLevelAssert(3, ComparisonEnum.NotEqual, false);
        }

        [TestMethod]
        public void TestLevelGreaterThanTrue()
        {
            DoLevelAssert(2, ComparisonEnum.GreaterThan, true);
        }

        [TestMethod]
        public void TestLevelGreaterThanFalse()
        {
            DoLevelAssert(3, ComparisonEnum.GreaterThan, false);
        }

        [TestMethod]
        public void TestLevelGreaterThanOrEqualTrue()
        {
            DoLevelAssert(3, ComparisonEnum.GreaterThanOrEqual, true);
        }

        [TestMethod]
        public void TestLevelGreaterThanOrEqualFalse()
        {
            DoLevelAssert(4, ComparisonEnum.GreaterThanOrEqual, false);
        }

        [TestMethod]
        public void TestLevelLessThanTrue()
        {
            DoLevelAssert(4, ComparisonEnum.LessThan, true);
        }

        [TestMethod]
        public void TestLevelLessThanFalse()
        {
            DoLevelAssert(3, ComparisonEnum.LessThan, false);
        }

        [TestMethod]
        public void TestLevelLessThanOrEqualTrue()
        {
            DoLevelAssert(3, ComparisonEnum.LessThanOrEqual, true);
        }

        [TestMethod]
        public void TestLevelLessThanOrEqualFalse()
        {
            DoLevelAssert(2, ComparisonEnum.LessThanOrEqual, false);
        }

        protected void DoWorkInProgressAssert(ComparisonEnum compare, BuildableItemEnum itemCode, int quantity, bool expected)
        {
            // Arrange
            StateCheck.Rules.Add(
                new WorkInProgressCompareRule
                {                    
                    ComparisonType = compare,
                    Item = new ItemQuantity 
                    {
                        ItemCode = itemCode,
                        UnStoredQuantity = quantity
                    }
                });

            // Act
            bool tf = StateCheck.IsValid(Player);

            // Assert
            Assert.AreEqual(expected, tf);
        }

        [TestMethod]
        public void TestWorkInProgressEqualTrueFirstItem()
        {
            DoWorkInProgressAssert(ComparisonEnum.Equal, BuildableItemEnum.White_Flour, 1, true);
        }

        [TestMethod]
        public void TestWorkInProgressEqualTrueSecondItem()
        {
            DoWorkInProgressAssert(ComparisonEnum.Equal, BuildableItemEnum.Tomatoes, 1, true);
        }

        [TestMethod]
        public void TestWorkInProgressEqualTrueNoWork()
        {
            DoWorkInProgressAssert(ComparisonEnum.Equal, BuildableItemEnum.Dry_Goods_Delivery_Truck, 0, true);
        }

        [TestMethod]
        public void TestWorkInProgressEqualFalseQuantity()
        {
            DoWorkInProgressAssert(ComparisonEnum.Equal, BuildableItemEnum.White_Flour, 2, false);
        }

        [TestMethod]
        public void TestWorkInProgressEqualFalseNoWork()
        {
            DoWorkInProgressAssert(ComparisonEnum.Equal, BuildableItemEnum.Dry_Goods_Delivery_Truck, 1, false);
        }

        [TestMethod]
        public void TestWorkInProgressNotEqualTrueFirstItem()
        {
            DoWorkInProgressAssert(ComparisonEnum.NotEqual, BuildableItemEnum.White_Flour, 0, true);
        }

        [TestMethod]
        public void TestWorkInProgressNotEqualTrueSecondItem()
        {
            DoWorkInProgressAssert(ComparisonEnum.NotEqual, BuildableItemEnum.Tomatoes, 2, true);
        }

        [TestMethod]
        public void TestWorkInProgressNotEqualTrueNoWork()
        {
            DoWorkInProgressAssert(ComparisonEnum.NotEqual, BuildableItemEnum.Dry_Goods_Delivery_Truck, 1, true);
        }

        [TestMethod]
        public void TestWorkInProgressNotEqualFalseQuantity()
        {
            DoWorkInProgressAssert(ComparisonEnum.NotEqual, BuildableItemEnum.White_Flour, 1, false);
        }

        [TestMethod]
        public void TestWorkInProgressNotEqualFalseNoWork()
        {
            DoWorkInProgressAssert(ComparisonEnum.NotEqual, BuildableItemEnum.Dry_Goods_Delivery_Truck, 0, false);
        }

        [TestMethod]
        public void TestWorkInProgressGreaterThanTrueFirstItem()
        {
            DoWorkInProgressAssert(ComparisonEnum.GreaterThan, BuildableItemEnum.White_Flour, 0, true);
        }

        [TestMethod]
        public void TestWorkInProgressGreaterThanTrueSecondItem()
        {
            DoWorkInProgressAssert(ComparisonEnum.GreaterThan, BuildableItemEnum.Tomatoes, 0, true);
        }

        [TestMethod]
        public void TestWorkInProgressGreaterThanTrueNoWork()
        {
            DoWorkInProgressAssert(ComparisonEnum.GreaterThan, BuildableItemEnum.Dry_Goods_Delivery_Truck, -1, true);
        }

        [TestMethod]
        public void TestWorkInProgressGreaterThanFalseQuantity()
        {
            DoWorkInProgressAssert(ComparisonEnum.GreaterThan, BuildableItemEnum.White_Flour, 2, false);
        }

        [TestMethod]
        public void TestWorkInProgressGreaterThanFalseNoWork()
        {
            DoWorkInProgressAssert(ComparisonEnum.GreaterThan, BuildableItemEnum.Dry_Goods_Delivery_Truck, 0, false);
        }

        [TestMethod]
        public void TestWorkInProgressGreaterThanOrEqualTrueFirstItem()
        {
            DoWorkInProgressAssert(ComparisonEnum.GreaterThanOrEqual, BuildableItemEnum.White_Flour, 1, true);
        }

        [TestMethod]
        public void TestWorkInProgressGreaterThanOrEqualTrueSecondItem()
        {
            DoWorkInProgressAssert(ComparisonEnum.GreaterThanOrEqual, BuildableItemEnum.Tomatoes, 1, true);
        }

        [TestMethod]
        public void TestWorkInProgressGreaterThanOrEqualTrueNoWork()
        {
            DoWorkInProgressAssert(ComparisonEnum.GreaterThanOrEqual, BuildableItemEnum.Dry_Goods_Delivery_Truck, 0, true);
        }

        [TestMethod]
        public void TestWorkInProgressGreaterThanOrEqualFalseQuantity()
        {
            DoWorkInProgressAssert(ComparisonEnum.GreaterThanOrEqual, BuildableItemEnum.White_Flour, 2, false);
        }

        [TestMethod]
        public void TestWorkInProgressGreaterThanOrEqualFalseNoWork()
        {
            DoWorkInProgressAssert(ComparisonEnum.GreaterThanOrEqual, BuildableItemEnum.Dry_Goods_Delivery_Truck, 1, false);
        }

        [TestMethod]
        public void TestWorkInProgressLessThanTrueFirstItem()
        {
            DoWorkInProgressAssert(ComparisonEnum.LessThan, BuildableItemEnum.White_Flour, 2, true);
        }

        [TestMethod]
        public void TestWorkInProgressLessThanTrueSecondItem()
        {
            DoWorkInProgressAssert(ComparisonEnum.LessThan, BuildableItemEnum.Tomatoes, 2, true);
        }

        [TestMethod]
        public void TestWorkInProgressLessThanTrueNoWork()
        {
            DoWorkInProgressAssert(ComparisonEnum.LessThan, BuildableItemEnum.Dry_Goods_Delivery_Truck, 1, true);
        }

        [TestMethod]
        public void TestWorkInProgressLessThanFalseQuantity()
        {
            DoWorkInProgressAssert(ComparisonEnum.LessThan, BuildableItemEnum.White_Flour, 1, false);
        }

        [TestMethod]
        public void TestWorkInProgressLessThanFalseNoWork()
        {
            DoWorkInProgressAssert(ComparisonEnum.LessThan, BuildableItemEnum.Dry_Goods_Delivery_Truck, 0, false);
        }

        [TestMethod]
        public void TestWorkInProgressLessThanOrEqualTrueFirstItem()
        {
            DoWorkInProgressAssert(ComparisonEnum.LessThanOrEqual, BuildableItemEnum.White_Flour, 1, true);
        }

        [TestMethod]
        public void TestWorkInProgressLessThanOrEqualTrueSecondItem()
        {
            DoWorkInProgressAssert(ComparisonEnum.LessThanOrEqual, BuildableItemEnum.Tomatoes, 1, true);
        }

        [TestMethod]
        public void TestWorkInProgressLessThanOrEqualTrueNoWork()
        {
            DoWorkInProgressAssert(ComparisonEnum.LessThanOrEqual, BuildableItemEnum.Dry_Goods_Delivery_Truck, 0, true);
        }

        [TestMethod]
        public void TestWorkInProgressLessThanOrEqualFalseQuantity()
        {
            DoWorkInProgressAssert(ComparisonEnum.LessThanOrEqual, BuildableItemEnum.White_Flour, 0, false);
        }

        [TestMethod]
        public void TestWorkInProgressLessThanOrEqualFalseNoWork()
        {
            DoWorkInProgressAssert(ComparisonEnum.LessThanOrEqual, BuildableItemEnum.Dry_Goods_Delivery_Truck, -1, false);
        }

        protected void DoStorageItemLevelAssert(ComparisonEnum compare, BuildableItemEnum itemCode, int level, bool expected)
        {
            // Arrange
            StateCheck.Rules.Add(
                new StorageItemLevelCompareRule
                { 
                    Level = level,
                    Location = 0,
                    ComparisonType = compare,
                    Item = itemCode
                });

            // Act
            bool tf = StateCheck.IsValid(Player);

            // Assert
            Assert.AreEqual(expected, tf);
        }

        [TestMethod]
        public void TestStorageItemLevelEqualTrueFirstItem()
        {
            DoStorageItemLevelAssert(ComparisonEnum.Equal, BuildableItemEnum.White_Flour, 1, true);
        }

        [TestMethod]
        public void TestStorageItemLevelEqualTrueSecondItem()
        {
            DoStorageItemLevelAssert(ComparisonEnum.Equal, BuildableItemEnum.Dry_Goods_Delivery_Truck, 2, true);
        }

        [TestMethod]
        public void TestStorageItemLevelEqualTrueNoWork()
        {
            DoStorageItemLevelAssert(ComparisonEnum.Equal, BuildableItemEnum.Tomatoes, 0, true);
        }

        [TestMethod]
        public void TestStorageItemLevelEqualFalseQuantity()
        {
            DoStorageItemLevelAssert(ComparisonEnum.Equal, BuildableItemEnum.White_Flour, 2, false);
        }

        [TestMethod]
        public void TestStorageItemLevelEqualFalseNoWork()
        {
            DoStorageItemLevelAssert(ComparisonEnum.Equal, BuildableItemEnum.Tomatoes, 1, false);
        }

        [TestMethod]
        public void TestStorageItemLevelNotEqualTrueFirstItem()
        {
            DoStorageItemLevelAssert(ComparisonEnum.NotEqual, BuildableItemEnum.White_Flour, 2, true);
        }

        [TestMethod]
        public void TestStorageItemLevelNotEqualTrueSecondItem()
        {
            DoStorageItemLevelAssert(ComparisonEnum.NotEqual, BuildableItemEnum.Dry_Goods_Delivery_Truck, 1, true);
        }

        [TestMethod]
        public void TestStorageItemLevelNotEqualTrueNoWork()
        {
            DoStorageItemLevelAssert(ComparisonEnum.NotEqual, BuildableItemEnum.Tomatoes, 1, true);
        }

        [TestMethod]
        public void TestStorageItemLevelNotEqualFalseQuantity()
        {
            DoStorageItemLevelAssert(ComparisonEnum.NotEqual, BuildableItemEnum.White_Flour, 1, false);
        }

        [TestMethod]
        public void TestStorageItemLevelNotEqualFalseNoWork()
        {
            DoStorageItemLevelAssert(ComparisonEnum.NotEqual, BuildableItemEnum.Tomatoes, 0, false);
        }

        [TestMethod]
        public void TestStorageItemLevelGreaterThanTrueFirstItem()
        {
            DoStorageItemLevelAssert(ComparisonEnum.GreaterThan, BuildableItemEnum.White_Flour, 0, true);
        }

        [TestMethod]
        public void TestStorageItemLevelGreaterThanTrueSecondItem()
        {
            DoStorageItemLevelAssert(ComparisonEnum.GreaterThan, BuildableItemEnum.Dry_Goods_Delivery_Truck, 1, true);
        }

        [TestMethod]
        public void TestStorageItemLevelGreaterThanTrueNoWork()
        {
            DoStorageItemLevelAssert(ComparisonEnum.GreaterThan, BuildableItemEnum.Tomatoes, -1, true);
        }

        [TestMethod]
        public void TestStorageItemLevelGreaterThanFalseQuantity()
        {
            DoStorageItemLevelAssert(ComparisonEnum.GreaterThan, BuildableItemEnum.White_Flour, 1, false);
        }

        [TestMethod]
        public void TestStorageItemLevelGreaterThanFalseNoWork()
        {
            DoStorageItemLevelAssert(ComparisonEnum.GreaterThan, BuildableItemEnum.Tomatoes, 0, false);
        }

        [TestMethod]
        public void TestStorageItemLevelGreaterThanOrEqualTrueFirstItem()
        {
            DoStorageItemLevelAssert(ComparisonEnum.GreaterThanOrEqual, BuildableItemEnum.White_Flour, 1, true);
        }

        [TestMethod]
        public void TestStorageItemLevelGreaterThanOrEqualTrueSecondItem()
        {
            DoStorageItemLevelAssert(ComparisonEnum.GreaterThanOrEqual, BuildableItemEnum.Dry_Goods_Delivery_Truck, 2, true);
        }

        [TestMethod]
        public void TestStorageItemLevelGreaterThanOrEqualTrueNoWork()
        {
            DoStorageItemLevelAssert(ComparisonEnum.GreaterThanOrEqual, BuildableItemEnum.Tomatoes, 0, true);
        }

        [TestMethod]
        public void TestStorageItemLevelGreaterThanOrEqualFalseQuantity()
        {
            DoStorageItemLevelAssert(ComparisonEnum.GreaterThanOrEqual, BuildableItemEnum.White_Flour, 2, false);
        }

        [TestMethod]
        public void TestStorageItemLevelGreaterThanOrEqualFalseNoWork()
        {
            DoStorageItemLevelAssert(ComparisonEnum.GreaterThanOrEqual, BuildableItemEnum.Tomatoes, 1, false);
        }

        [TestMethod]
        public void TestStorageItemLevelLessThanTrueFirstItem()
        {
            DoStorageItemLevelAssert(ComparisonEnum.LessThan, BuildableItemEnum.White_Flour, 2, true);
        }

        [TestMethod]
        public void TestStorageItemLevelLessThanTrueSecondItem()
        {
            DoStorageItemLevelAssert(ComparisonEnum.LessThan, BuildableItemEnum.Dry_Goods_Delivery_Truck, 3, true);
        }

        [TestMethod]
        public void TestStorageItemLevelLessThanTrueNoWork()
        {
            DoStorageItemLevelAssert(ComparisonEnum.LessThan, BuildableItemEnum.Tomatoes, 1, true);
        }

        [TestMethod]
        public void TestStorageItemLevelLessThanFalseQuantity()
        {
            DoStorageItemLevelAssert(ComparisonEnum.LessThan, BuildableItemEnum.White_Flour, 1, false);
        }

        [TestMethod]
        public void TestStorageItemLevelLessThanFalseNoWork()
        {
            DoStorageItemLevelAssert(ComparisonEnum.LessThan, BuildableItemEnum.Tomatoes, 0, false);
        }

        [TestMethod]
        public void TestStorageItemLevelLessThanOrEqualTrueFirstItem()
        {
            DoStorageItemLevelAssert(ComparisonEnum.LessThanOrEqual, BuildableItemEnum.White_Flour, 1, true);
        }

        [TestMethod]
        public void TestStorageItemLevelLessThanOrEqualTrueSecondItem()
        {
            DoStorageItemLevelAssert(ComparisonEnum.LessThanOrEqual, BuildableItemEnum.Dry_Goods_Delivery_Truck, 2, true);
        }

        [TestMethod]
        public void TestStorageItemLevelLessThanOrEqualTrueNoWork()
        {
            DoStorageItemLevelAssert(ComparisonEnum.LessThanOrEqual, BuildableItemEnum.Tomatoes, 0, true);
        }

        [TestMethod]
        public void TestStorageItemLevelLessThanOrEqualFalseQuantity()
        {
            DoStorageItemLevelAssert(ComparisonEnum.LessThanOrEqual, BuildableItemEnum.White_Flour, 0, false);
        }

        [TestMethod]
        public void TestStorageItemLevelLessThanOrEqualFalseNoWork()
        {
            DoStorageItemLevelAssert(ComparisonEnum.LessThanOrEqual, BuildableItemEnum.Tomatoes, -1, false);
        }

        protected void DoStorageItemStoredQuantityAssert(ComparisonEnum compare, BuildableItemEnum itemCode, int quantity, bool expected)
        {
            // Arrange
            StateCheck.Rules.Add(
                new StorageItemStoredQuantityCompareRule
                {
                    Quantity = quantity,
                    Location = 0,
                    ComparisonType = compare,
                    Item = itemCode
                });

            // Act
            bool tf = StateCheck.IsValid(Player);

            // Assert
            Assert.AreEqual(expected, tf);
        }

        [TestMethod]
        public void TestStorageItemStoredQuantityEqualTrueFirstItem()
        {
            DoStorageItemStoredQuantityAssert(ComparisonEnum.Equal, BuildableItemEnum.White_Flour, 1, true);
        }

        [TestMethod]
        public void TestStorageItemStoredQuantityEqualTrueSecondItem()
        {
            DoStorageItemStoredQuantityAssert(ComparisonEnum.Equal, BuildableItemEnum.Dry_Goods_Delivery_Truck, 2, true);
        }

        [TestMethod]
        public void TestStorageItemStoredQuantityEqualTrueNoWork()
        {
            DoStorageItemStoredQuantityAssert(ComparisonEnum.Equal, BuildableItemEnum.Tomatoes, 0, true);
        }

        [TestMethod]
        public void TestStorageItemStoredQuantityEqualFalseQuantity()
        {
            DoStorageItemStoredQuantityAssert(ComparisonEnum.Equal, BuildableItemEnum.White_Flour, 2, false);
        }

        [TestMethod]
        public void TestStorageItemStoredQuantityEqualFalseNoWork()
        {
            DoStorageItemStoredQuantityAssert(ComparisonEnum.Equal, BuildableItemEnum.Tomatoes, 1, false);
        }

        [TestMethod]
        public void TestStorageItemStoredQuantityNotEqualTrueFirstItem()
        {
            DoStorageItemStoredQuantityAssert(ComparisonEnum.NotEqual, BuildableItemEnum.White_Flour, 2, true);
        }

        [TestMethod]
        public void TestStorageItemStoredQuantityNotEqualTrueSecondItem()
        {
            DoStorageItemStoredQuantityAssert(ComparisonEnum.NotEqual, BuildableItemEnum.Dry_Goods_Delivery_Truck, 1, true);
        }

        [TestMethod]
        public void TestStorageItemStoredQuantityNotEqualTrueNoWork()
        {
            DoStorageItemStoredQuantityAssert(ComparisonEnum.NotEqual, BuildableItemEnum.Tomatoes, 1, true);
        }

        [TestMethod]
        public void TestStorageItemStoredQuantityNotEqualFalseQuantity()
        {
            DoStorageItemStoredQuantityAssert(ComparisonEnum.NotEqual, BuildableItemEnum.White_Flour, 1, false);
        }

        [TestMethod]
        public void TestStorageItemStoredQuantityNotEqualFalseNoWork()
        {
            DoStorageItemStoredQuantityAssert(ComparisonEnum.NotEqual, BuildableItemEnum.Tomatoes, 0, false);
        }

        [TestMethod]
        public void TestStorageItemStoredQuantityGreaterThanTrueFirstItem()
        {
            DoStorageItemStoredQuantityAssert(ComparisonEnum.GreaterThan, BuildableItemEnum.White_Flour, 0, true);
        }

        [TestMethod]
        public void TestStorageItemStoredQuantityGreaterThanTrueSecondItem()
        {
            DoStorageItemStoredQuantityAssert(ComparisonEnum.GreaterThan, BuildableItemEnum.Dry_Goods_Delivery_Truck, 1, true);
        }

        [TestMethod]
        public void TestStorageItemStoredQuantityGreaterThanTrueNoWork()
        {
            DoStorageItemStoredQuantityAssert(ComparisonEnum.GreaterThan, BuildableItemEnum.Tomatoes, -1, true);
        }

        [TestMethod]
        public void TestStorageItemStoredQuantityGreaterThanFalseQuantity()
        {
            DoStorageItemStoredQuantityAssert(ComparisonEnum.GreaterThan, BuildableItemEnum.White_Flour, 1, false);
        }

        [TestMethod]
        public void TestStorageItemStoredQuantityGreaterThanFalseNoWork()
        {
            DoStorageItemStoredQuantityAssert(ComparisonEnum.GreaterThan, BuildableItemEnum.Tomatoes, 0, false);
        }

        [TestMethod]
        public void TestStorageItemStoredQuantityGreaterThanOrEqualTrueFirstItem()
        {
            DoStorageItemStoredQuantityAssert(ComparisonEnum.GreaterThanOrEqual, BuildableItemEnum.White_Flour, 1, true);
        }

        [TestMethod]
        public void TestStorageItemStoredQuantityGreaterThanOrEqualTrueSecondItem()
        {
            DoStorageItemStoredQuantityAssert(ComparisonEnum.GreaterThanOrEqual, BuildableItemEnum.Dry_Goods_Delivery_Truck, 2, true);
        }

        [TestMethod]
        public void TestStorageItemStoredQuantityGreaterThanOrEqualTrueNoWork()
        {
            DoStorageItemStoredQuantityAssert(ComparisonEnum.GreaterThanOrEqual, BuildableItemEnum.Tomatoes, 0, true);
        }

        [TestMethod]
        public void TestStorageItemStoredQuantityGreaterThanOrEqualFalseQuantity()
        {
            DoStorageItemStoredQuantityAssert(ComparisonEnum.GreaterThanOrEqual, BuildableItemEnum.White_Flour, 2, false);
        }

        [TestMethod]
        public void TestStorageItemStoredQuantityGreaterThanOrEqualFalseNoWork()
        {
            DoStorageItemStoredQuantityAssert(ComparisonEnum.GreaterThanOrEqual, BuildableItemEnum.Tomatoes, 1, false);
        }

        [TestMethod]
        public void TestStorageItemStoredQuantityLessThanTrueFirstItem()
        {
            DoStorageItemStoredQuantityAssert(ComparisonEnum.LessThan, BuildableItemEnum.White_Flour, 2, true);
        }

        [TestMethod]
        public void TestStorageItemStoredQuantityLessThanTrueSecondItem()
        {
            DoStorageItemStoredQuantityAssert(ComparisonEnum.LessThan, BuildableItemEnum.Dry_Goods_Delivery_Truck, 3, true);
        }

        [TestMethod]
        public void TestStorageItemStoredQuantityLessThanTrueNoWork()
        {
            DoStorageItemStoredQuantityAssert(ComparisonEnum.LessThan, BuildableItemEnum.Tomatoes, 1, true);
        }

        [TestMethod]
        public void TestStorageItemStoredQuantityLessThanFalseQuantity()
        {
            DoStorageItemStoredQuantityAssert(ComparisonEnum.LessThan, BuildableItemEnum.White_Flour, 1, false);
        }

        [TestMethod]
        public void TestStorageItemStoredQuantityLessThanFalseNoWork()
        {
            DoStorageItemStoredQuantityAssert(ComparisonEnum.LessThan, BuildableItemEnum.Tomatoes, 0, false);
        }

        [TestMethod]
        public void TestStorageItemStoredQuantityLessThanOrEqualTrueFirstItem()
        {
            DoStorageItemStoredQuantityAssert(ComparisonEnum.LessThanOrEqual, BuildableItemEnum.White_Flour, 1, true);
        }

        [TestMethod]
        public void TestStorageItemStoredQuantityLessThanOrEqualTrueSecondItem()
        {
            DoStorageItemStoredQuantityAssert(ComparisonEnum.LessThanOrEqual, BuildableItemEnum.Dry_Goods_Delivery_Truck, 2, true);
        }

        [TestMethod]
        public void TestStorageItemStoredQuantityLessThanOrEqualTrueNoWork()
        {
            DoStorageItemStoredQuantityAssert(ComparisonEnum.LessThanOrEqual, BuildableItemEnum.Tomatoes, 0, true);
        }

        [TestMethod]
        public void TestStorageItemStoredQuantityLessThanOrEqualFalseQuantity()
        {
            DoStorageItemStoredQuantityAssert(ComparisonEnum.LessThanOrEqual, BuildableItemEnum.White_Flour, 0, false);
        }

        [TestMethod]
        public void TestStorageItemStoredQuantityLessThanOrEqualFalseNoWork()
        {
            DoStorageItemStoredQuantityAssert(ComparisonEnum.LessThanOrEqual, BuildableItemEnum.Tomatoes, -1, false);
        }

        protected void DoStorageItemUnstoredQuantityAssert(ComparisonEnum compare, BuildableItemEnum itemCode, int quantity, bool expected)
        {
            // Arrange
            StateCheck.Rules.Add(
                new StorageItemUnstoredQuantityCompareRule
                {
                    Quantity = quantity,
                    Location = 0,
                    ComparisonType = compare,
                    Item = itemCode
                });

            // Act
            bool tf = StateCheck.IsValid(Player);

            // Assert
            Assert.AreEqual(expected, tf);
        }

        [TestMethod]
        public void TestStorageItemUnstoredQuantityEqualTrueFirstItem()
        {
            DoStorageItemUnstoredQuantityAssert(ComparisonEnum.Equal, BuildableItemEnum.White_Flour, 1, true);
        }

        [TestMethod]
        public void TestStorageItemUnstoredQuantityEqualTrueSecondItem()
        {
            DoStorageItemUnstoredQuantityAssert(ComparisonEnum.Equal, BuildableItemEnum.Dry_Goods_Delivery_Truck, 2, true);
        }

        [TestMethod]
        public void TestStorageItemUnstoredQuantityEqualTrueNoWork()
        {
            DoStorageItemUnstoredQuantityAssert(ComparisonEnum.Equal, BuildableItemEnum.Tomatoes, 0, true);
        }

        [TestMethod]
        public void TestStorageItemUnstoredQuantityEqualFalseQuantity()
        {
            DoStorageItemUnstoredQuantityAssert(ComparisonEnum.Equal, BuildableItemEnum.White_Flour, 2, false);
        }

        [TestMethod]
        public void TestStorageItemUnstoredQuantityEqualFalseNoWork()
        {
            DoStorageItemUnstoredQuantityAssert(ComparisonEnum.Equal, BuildableItemEnum.Tomatoes, 1, false);
        }

        [TestMethod]
        public void TestStorageItemUnstoredQuantityNotEqualTrueFirstItem()
        {
            DoStorageItemUnstoredQuantityAssert(ComparisonEnum.NotEqual, BuildableItemEnum.White_Flour, 2, true);
        }

        [TestMethod]
        public void TestStorageItemUnstoredQuantityNotEqualTrueSecondItem()
        {
            DoStorageItemUnstoredQuantityAssert(ComparisonEnum.NotEqual, BuildableItemEnum.Dry_Goods_Delivery_Truck, 1, true);
        }

        [TestMethod]
        public void TestStorageItemUnstoredQuantityNotEqualTrueNoWork()
        {
            DoStorageItemUnstoredQuantityAssert(ComparisonEnum.NotEqual, BuildableItemEnum.Tomatoes, 1, true);
        }

        [TestMethod]
        public void TestStorageItemUnstoredQuantityNotEqualFalseQuantity()
        {
            DoStorageItemUnstoredQuantityAssert(ComparisonEnum.NotEqual, BuildableItemEnum.White_Flour, 1, false);
        }

        [TestMethod]
        public void TestStorageItemUnstoredQuantityNotEqualFalseNoWork()
        {
            DoStorageItemUnstoredQuantityAssert(ComparisonEnum.NotEqual, BuildableItemEnum.Tomatoes, 0, false);
        }

        [TestMethod]
        public void TestStorageItemUnstoredQuantityGreaterThanTrueFirstItem()
        {
            DoStorageItemUnstoredQuantityAssert(ComparisonEnum.GreaterThan, BuildableItemEnum.White_Flour, 0, true);
        }

        [TestMethod]
        public void TestStorageItemUnstoredQuantityGreaterThanTrueSecondItem()
        {
            DoStorageItemUnstoredQuantityAssert(ComparisonEnum.GreaterThan, BuildableItemEnum.Dry_Goods_Delivery_Truck, 1, true);
        }

        [TestMethod]
        public void TestStorageItemUnstoredQuantityGreaterThanTrueNoWork()
        {
            DoStorageItemUnstoredQuantityAssert(ComparisonEnum.GreaterThan, BuildableItemEnum.Tomatoes, -1, true);
        }

        [TestMethod]
        public void TestStorageItemUnstoredQuantityGreaterThanFalseQuantity()
        {
            DoStorageItemUnstoredQuantityAssert(ComparisonEnum.GreaterThan, BuildableItemEnum.White_Flour, 1, false);
        }

        [TestMethod]
        public void TestStorageItemUnstoredQuantityGreaterThanFalseNoWork()
        {
            DoStorageItemUnstoredQuantityAssert(ComparisonEnum.GreaterThan, BuildableItemEnum.Tomatoes, 0, false);
        }

        [TestMethod]
        public void TestStorageItemUnstoredQuantityGreaterThanOrEqualTrueFirstItem()
        {
            DoStorageItemUnstoredQuantityAssert(ComparisonEnum.GreaterThanOrEqual, BuildableItemEnum.White_Flour, 1, true);
        }

        [TestMethod]
        public void TestStorageItemUnstoredQuantityGreaterThanOrEqualTrueSecondItem()
        {
            DoStorageItemUnstoredQuantityAssert(ComparisonEnum.GreaterThanOrEqual, BuildableItemEnum.Dry_Goods_Delivery_Truck, 2, true);
        }

        [TestMethod]
        public void TestStorageItemUnstoredQuantityGreaterThanOrEqualTrueNoWork()
        {
            DoStorageItemUnstoredQuantityAssert(ComparisonEnum.GreaterThanOrEqual, BuildableItemEnum.Tomatoes, 0, true);
        }

        [TestMethod]
        public void TestStorageItemUnstoredQuantityGreaterThanOrEqualFalseQuantity()
        {
            DoStorageItemUnstoredQuantityAssert(ComparisonEnum.GreaterThanOrEqual, BuildableItemEnum.White_Flour, 2, false);
        }

        [TestMethod]
        public void TestStorageItemUnstoredQuantityGreaterThanOrEqualFalseNoWork()
        {
            DoStorageItemUnstoredQuantityAssert(ComparisonEnum.GreaterThanOrEqual, BuildableItemEnum.Tomatoes, 1, false);
        }

        [TestMethod]
        public void TestStorageItemUnstoredQuantityLessThanTrueFirstItem()
        {
            DoStorageItemUnstoredQuantityAssert(ComparisonEnum.LessThan, BuildableItemEnum.White_Flour, 2, true);
        }

        [TestMethod]
        public void TestStorageItemUnstoredQuantityLessThanTrueSecondItem()
        {
            DoStorageItemUnstoredQuantityAssert(ComparisonEnum.LessThan, BuildableItemEnum.Dry_Goods_Delivery_Truck, 3, true);
        }

        [TestMethod]
        public void TestStorageItemUnstoredQuantityLessThanTrueNoWork()
        {
            DoStorageItemUnstoredQuantityAssert(ComparisonEnum.LessThan, BuildableItemEnum.Tomatoes, 1, true);
        }

        [TestMethod]
        public void TestStorageItemUnstoredQuantityLessThanFalseQuantity()
        {
            DoStorageItemUnstoredQuantityAssert(ComparisonEnum.LessThan, BuildableItemEnum.White_Flour, 1, false);
        }

        [TestMethod]
        public void TestStorageItemUnstoredQuantityLessThanFalseNoWork()
        {
            DoStorageItemUnstoredQuantityAssert(ComparisonEnum.LessThan, BuildableItemEnum.Tomatoes, 0, false);
        }

        [TestMethod]
        public void TestStorageItemUnstoredQuantityLessThanOrEqualTrueFirstItem()
        {
            DoStorageItemUnstoredQuantityAssert(ComparisonEnum.LessThanOrEqual, BuildableItemEnum.White_Flour, 1, true);
        }

        [TestMethod]
        public void TestStorageItemUnstoredQuantityLessThanOrEqualTrueSecondItem()
        {
            DoStorageItemUnstoredQuantityAssert(ComparisonEnum.LessThanOrEqual, BuildableItemEnum.Dry_Goods_Delivery_Truck, 2, true);
        }

        [TestMethod]
        public void TestStorageItemUnstoredQuantityLessThanOrEqualTrueNoWork()
        {
            DoStorageItemUnstoredQuantityAssert(ComparisonEnum.LessThanOrEqual, BuildableItemEnum.Tomatoes, 0, true);
        }

        [TestMethod]
        public void TestStorageItemUnstoredQuantityLessThanOrEqualFalseQuantity()
        {
            DoStorageItemUnstoredQuantityAssert(ComparisonEnum.LessThanOrEqual, BuildableItemEnum.White_Flour, 0, false);
        }

        [TestMethod]
        public void TestStorageItemUnstoredQuantityLessThanOrEqualFalseNoWork()
        {
            DoStorageItemUnstoredQuantityAssert(ComparisonEnum.LessThanOrEqual, BuildableItemEnum.Tomatoes, -1, false);
        }

        [TestMethod]
        public void TestCanBuildItemFalse()
        {
            Assert.Fail("Not implemented");

            /*

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
             * */
        }

        [TestMethod]
        public void TestCanBuildItemTrue()
        {
            Assert.Fail("Not implemented");

            /*

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
             * */
        }

        [TestMethod]
        public void TestOneFailedFalse()
        {
            StateCheck.Rules.Add(new CoinCompareRule { Coins = 10, ComparisonType = ComparisonEnum.Equal });
            StateCheck.Rules.Add(new TutorialStageCompareRule { TutorialStage = 3, ComparisonType = ComparisonEnum.NotEqual });
                
            // Act
            bool tf = StateCheck.IsValid(Player);

            // Assert
            Assert.AreEqual(false, tf);
        }

        [TestMethod]
        public void TestMultiplePassed()
        {
            StateCheck.Rules.Add(new CoinCompareRule { Coins = 10, ComparisonType = ComparisonEnum.Equal });
            StateCheck.Rules.Add(new TutorialStageCompareRule { TutorialStage = 4, ComparisonType = ComparisonEnum.NotEqual });
            StateCheck.Rules.Add(new LevelCompareRule { Level = 21, ComparisonType = ComparisonEnum.LessThanOrEqual });

            // Act
            bool tf = StateCheck.IsValid(Player);

            // Assert
            Assert.AreEqual(true, tf);
        }
    }
}
