namespace KS.PizzaEmpire.Common.Test.APITransfer
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using KS.PizzaEmpire.Common.BusinessObjects;
    using KS.PizzaEmpire.Common.APITransfer;
    using System.Collections.Generic;

    [TestClass]
    public class GamePlayerAPIMorphTest
    {
        public GamePlayerAPIMorph morph;
        public GamePlayer player;

        [TestInitialize]
        public void Initialize()
        {
            morph = new GamePlayerAPIMorph();

            player = new GamePlayer
            {
                Coins = 99,
                Coupons = 66,
                Experience = 1000,
                Level = 4,
                StateChanged = false,
                WorkItems = new List<WorkItem>
                {
                    new WorkItem 
                    {
                         ItemCode = BuildableItemEnum.White_Flour,
                         FinishTime = DateTime.UtcNow
                    }
                },
                BuildableItems = new Dictionary<BuildableItemEnum, int>
                {
                    { BuildableItemEnum.White_Flour, 1 }, 
                    { BuildableItemEnum.Dry_Goods_Delivery_Truck_L1, 1 }
                },
                TutorialStage = 10
            };
        }

        [TestMethod]
        public void TestToAPIFormat()
        {
            GamePlayerAPI playerAPI = (GamePlayerAPI)morph.ToAPIFormat(player);

            Assert.AreEqual(99, playerAPI.Coins);
            Assert.AreEqual(66, playerAPI.Coupons);
            Assert.AreEqual(1000, playerAPI.Experience);
            Assert.AreEqual(4, playerAPI.Level);
            Assert.AreEqual(1, playerAPI.WorkItems.Count);
            Assert.AreEqual(BuildableItemEnum.White_Flour, playerAPI.WorkItems[0].ItemCode);
            Assert.AreEqual("1:1:35:1", playerAPI.BuildableItems);
            Assert.AreEqual(10, playerAPI.TutorialStage);
        }

        [TestMethod]
        public void TestFromAPIFormat()
        {
            GamePlayerAPI playerAPI = (GamePlayerAPI)morph.ToAPIFormat(player);
            GamePlayer flip = (GamePlayer)morph.ToBusinessFormat(playerAPI);

            Assert.AreEqual(99, flip.Coins);
            Assert.AreEqual(66, flip.Coupons);
            Assert.AreEqual(1000, flip.Experience);
            Assert.AreEqual(4, flip.Level);
            Assert.AreEqual(1, flip.WorkItems.Count);
            Assert.AreEqual(BuildableItemEnum.White_Flour, flip.WorkItems[0].ItemCode);
            Assert.AreEqual(2, flip.BuildableItems.Count);
            Assert.AreEqual(1, flip.BuildableItems[BuildableItemEnum.White_Flour]);
            Assert.AreEqual(1, flip.BuildableItems[BuildableItemEnum.Dry_Goods_Delivery_Truck_L1]);
            Assert.AreEqual(10, flip.TutorialStage);
        }
    }
}
