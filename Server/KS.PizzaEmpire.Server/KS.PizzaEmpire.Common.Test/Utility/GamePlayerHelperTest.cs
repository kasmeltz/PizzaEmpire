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
