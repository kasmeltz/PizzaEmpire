using KS.PizzaEmpire.Business.Logic;
using KS.PizzaEmpire.GameLogic.ExperienceLevelLogic;
using KS.PizzaEmpire.GameLogic.GamePlayerLogic;
using KS.PizzaEmpire.GameLogic.ItemLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KS.PizzaEmpire.GameLogic.Test.GamePlayerLogic
{
    [TestClass]
    public class GamePlayerManagerTest
    {
        public GamePlayer Player { get; set; }

        [ClassInitialize]
        public static void InitializeAllTests(TestContext testContent)
        {
            Task.WaitAll(ItemManager.Instance.Initialize());
            Task.WaitAll(ExperienceLevelManager.Instance.Initialize());
        }

        [TestInitialize]
        public void InitializeTest()
        {
            Player = GamePlayerManager.CreateNewGamePlayer();
        }

        [TestMethod]
        public void TestCreateNewGamePlayer()
        {
            Assert.AreEqual(1000, Player.Coins);
            Assert.AreEqual(5, Player.Coupons);
            Assert.AreEqual(1, Player.Level);
            Assert.AreEqual(0, Player.Experience);
            Assert.AreEqual(1, Player.BuildableItems.Values.Count);
            Assert.AreEqual(1, Player.BuildableItems[BuildableItemEnum.Delivery_Truck_L1]);
            Assert.AreEqual(0, Player.WorkItems.Count);
        }

        [TestMethod]
        public void TestSetLevel()
        {
            Assert.AreEqual(1, Player.Level);
            GamePlayerManager.SetLevel(Player, 2);
            Assert.AreEqual(2, Player.Level);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestSetLevelDoesntExist()
        {
            GamePlayerManager.SetLevel(Player, -1);
        }

        [TestMethod]
        public void TestAddExperienceNoLevels()
        {
            Assert.AreEqual(1, Player.Level);
            GamePlayerManager.AddExperience(Player, 50);
            Assert.AreEqual(50, Player.Experience);
            Assert.AreEqual(1, Player.Level);
        }

        [TestMethod]
        public void TestAddExperienceOneLevelExact()
        {
            Assert.AreEqual(1, Player.Level);
            GamePlayerManager.AddExperience(Player, 100);
            Assert.AreEqual(100, Player.Experience);
            Assert.AreEqual(2, Player.Level);
        }

        [TestMethod]
        public void TestAddExperienceOneLevel()
        {
            Assert.AreEqual(1, Player.Level);
            GamePlayerManager.AddExperience(Player, 150);
            Assert.AreEqual(150, Player.Experience);
            Assert.AreEqual(2, Player.Level);
        }

        [TestMethod]
        public void TestAddExperienceTwoLevelsExact()
        {
            Assert.AreEqual(1, Player.Level);
            GamePlayerManager.AddExperience(Player, 300);
            Assert.AreEqual(300, Player.Experience);
            Assert.AreEqual(3, Player.Level);
        }

        [TestMethod]
        public void TestAddExperienceTwoLevels()
        {
            Assert.AreEqual(1, Player.Level);
            GamePlayerManager.AddExperience(Player, 400);
            Assert.AreEqual(400, Player.Experience);
            Assert.AreEqual(3, Player.Level);
        }

        [TestMethod]
        public void TestAddExperienceMaxLevels()
        {
            Assert.AreEqual(1, Player.Level);
            GamePlayerManager.AddExperience(Player, 40000);
            Assert.AreEqual(40000, Player.Experience);
            Assert.AreEqual(4, Player.Level);
        }

        [TestMethod]
        public void TestDoesPlayerHaveCapacityTrue()
        {
            Assert.AreEqual(true, 
                GamePlayerManager.DoesPlayerHaveCapacity(Player, BuildableItemEnum.White_Flour));
        }

        [TestMethod]
        public void TestDoesPlayerHaveCapacityFalse()
        {
            Player.WorkItems.Add(new WorkItem { ItemCode = BuildableItemEnum.White_Flour });
            Player.WorkItems.Add(new WorkItem { ItemCode = BuildableItemEnum.White_Flour });
            Assert.AreEqual(false, 
                GamePlayerManager.DoesPlayerHaveCapacity(Player, BuildableItemEnum.White_Flour));
        }

        [TestMethod]        
        public void TestAddItem()
        {
            Assert.AreEqual(false, Player.BuildableItems.ContainsKey(BuildableItemEnum.White_Flour));
            GamePlayerManager.AddItem(Player, new WorkItem { ItemCode = BuildableItemEnum.White_Flour });
            Assert.AreEqual(1, Player.BuildableItems[BuildableItemEnum.White_Flour]);
        }

        [TestMethod]
        public void TestAddItemMaxQuantity()
        {
            GamePlayerManager.AddItem(Player, new WorkItem { ItemCode = BuildableItemEnum.Delivery_Truck_L1 });
            Assert.AreEqual(1, Player.BuildableItems[BuildableItemEnum.Delivery_Truck_L1]);
            GamePlayerManager.AddItem(Player, new WorkItem { ItemCode = BuildableItemEnum.Delivery_Truck_L1 });
            Assert.AreEqual(1, Player.BuildableItems[BuildableItemEnum.Delivery_Truck_L1]);
        }                

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestFinishWorkNone()
        {
            List<WorkItem> finishedItems = GamePlayerManager.FinishWork(Player);
            Assert.AreEqual(0, finishedItems.Count);
        }

        [TestMethod]
        public void TestFinishWorkNoneReady()
        {
            Player.WorkItems.Add(new WorkItem { ItemCode = BuildableItemEnum.White_Flour, FinishTime = DateTime.UtcNow.AddHours(1) });
            List<WorkItem> finishedItems = GamePlayerManager.FinishWork(Player);
            Assert.AreEqual(0, finishedItems.Count);
        }

        [TestMethod]
        public void TestFinishWorkOneReady()
        {
            Player.WorkItems.Add(new WorkItem { ItemCode = BuildableItemEnum.White_Flour, FinishTime = DateTime.UtcNow.AddHours(1) });
            Player.WorkItems.Add(new WorkItem { ItemCode = BuildableItemEnum.White_Flour, FinishTime = DateTime.UtcNow.AddHours(-1) });
            List<WorkItem> finishedItems = GamePlayerManager.FinishWork(Player);
            Assert.AreEqual(1, finishedItems.Count);
            Assert.AreEqual(BuildableItemEnum.White_Flour, finishedItems[0].ItemCode);
        }

        [TestMethod]
        public void TestFinishWorkTwoReady()
        {
            Player.WorkItems.Add(new WorkItem { ItemCode = BuildableItemEnum.White_Flour, FinishTime = DateTime.UtcNow.AddHours(1) });
            Player.WorkItems.Add(new WorkItem { ItemCode = BuildableItemEnum.Yeast, FinishTime = DateTime.UtcNow.AddHours(-1) });
            Player.WorkItems.Add(new WorkItem { ItemCode = BuildableItemEnum.Salt, FinishTime = DateTime.UtcNow.AddHours(-1) });
            List<WorkItem> finishedItems = GamePlayerManager.FinishWork(Player);
            Assert.AreEqual(2, finishedItems.Count);
            Assert.AreEqual(BuildableItemEnum.Yeast, finishedItems[0].ItemCode);
            Assert.AreEqual(BuildableItemEnum.Salt, finishedItems[1].ItemCode);
        }

        [TestMethod]
        public void TestDeductResources()
        {
            Player.BuildableItems[BuildableItemEnum.White_Flour] = 3;
            Player.BuildableItems[BuildableItemEnum.Yeast] = 2;
            Player.BuildableItems[BuildableItemEnum.Salt] = 1;
            Player.BuildableItems[BuildableItemEnum.Dough_Mixer_L1] = 1;
            GamePlayerManager.DeductResources(Player, BuildableItemEnum.White_Pizza_Dough);
            Assert.AreEqual(2, Player.BuildableItems[BuildableItemEnum.White_Flour]);
            Assert.AreEqual(1, Player.BuildableItems[BuildableItemEnum.Yeast]);
            Assert.AreEqual(0, Player.BuildableItems[BuildableItemEnum.Salt]);
            Assert.AreEqual(1, Player.BuildableItems[BuildableItemEnum.Dough_Mixer_L1]);
        }

        [TestMethod]
        public void TestStartWork()
        {
            WorkItem workItem = GamePlayerManager.StartWork(Player, BuildableItemEnum.White_Flour);
            Assert.AreEqual(BuildableItemEnum.White_Flour, workItem.ItemCode);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCanBuildItemFailItemType()
        {
            WorkItem workItem = GamePlayerManager.StartWork(Player, (BuildableItemEnum)5000);
        }   

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCanBuildItemFailLevel()
        {
            GamePlayer player = GamePlayerManager.CreateNewGamePlayer();
            WorkItem workItem = GamePlayerManager.StartWork(player, BuildableItemEnum.White_Pizza_Dough);
        }   

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCanBuildItemFailCoins()
        {
            Player.Coins = 0;
            WorkItem workItem = GamePlayerManager.StartWork(Player, BuildableItemEnum.White_Flour);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCanBuildItemFailCapacity()
        {
            WorkItem workItem = GamePlayerManager.StartWork(Player, BuildableItemEnum.Delivery_Truck_L1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCanBuildItemFailImproperIngredients()
        {
            GamePlayerManager.SetLevel(Player, 3);
            WorkItem workItem = GamePlayerManager.StartWork(Player, BuildableItemEnum.White_Pizza_Dough);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCanBuildItemFailInsufficeintIngredients()
        {
            GamePlayerManager.SetLevel(Player, 3);
            Player.BuildableItems[BuildableItemEnum.Salt] = 0;
            Player.BuildableItems[BuildableItemEnum.White_Flour] = 0;
            Player.BuildableItems[BuildableItemEnum.Yeast] = 0;
            WorkItem workItem = GamePlayerManager.StartWork(Player, BuildableItemEnum.White_Pizza_Dough);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCanBuildItemFailEqupimentFull()
        {
            GamePlayerManager.StartWork(Player, BuildableItemEnum.White_Flour);
            GamePlayerManager.StartWork(Player, BuildableItemEnum.White_Flour);
            GamePlayerManager.StartWork(Player, BuildableItemEnum.White_Flour);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCanBuildItemFailMaxQuantity()
        {
            GamePlayerManager.StartWork(Player, BuildableItemEnum.Delivery_Truck_L1);
        }
    }
}
