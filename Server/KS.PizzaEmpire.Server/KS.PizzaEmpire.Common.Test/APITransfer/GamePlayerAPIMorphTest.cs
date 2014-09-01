namespace KS.PizzaEmpire.Common.Test.APITransfer
{
    using Common.APITransfer;
    using Common.BusinessObjects;
    using Common.Utility;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Services.Json;
    using System;
    using System.Collections.Generic;

    [TestClass]
    public class GamePlayerAPIMorphTest
    {
        public GamePlayerAPIMorph Morph;
        public GamePlayer Player;
        protected IJsonConverter converter;

        [TestInitialize]
        public void Initialize()
        {
            converter = new NewtonsoftJsonConverter();

            // Arrange
            Player = new GamePlayer
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
            
            Morph = new GamePlayerAPIMorph();
        }

        [TestMethod]
        public void TestToAPIFormat()
        {
            // Act
            GamePlayerAPI playerAPI = (GamePlayerAPI)Morph.ToAPIFormat(Player);
            // Assert
            Assert.AreEqual(99, playerAPI.Coins);
            Assert.AreEqual(66, playerAPI.Coupons);
            Assert.AreEqual(1000, playerAPI.Experience);
            Assert.AreEqual(4, playerAPI.Level);
            Assert.AreEqual(1, playerAPI.WorkInProgress.Count);
            Assert.AreEqual(0, playerAPI.WorkInProgress[0].Location);
            Assert.AreEqual(BuildableItemEnum.Dough_Mixer, playerAPI.WorkInProgress[0].Quantity.ItemCode);
            Assert.AreEqual(1, playerAPI.WorkInProgress[0].Quantity.Level);
            Assert.AreEqual(0, playerAPI.WorkInProgress[0].Quantity.StoredQuantity);
            Assert.AreEqual(2, playerAPI.WorkInProgress[0].Quantity.UnStoredQuantity);
            Assert.AreEqual(1, playerAPI.Locations.Count);
            Assert.AreEqual(2, playerAPI.Locations[0].Storage.Items.Count);
            Assert.AreEqual(BuildableItemEnum.White_Flour, playerAPI.Locations[0].Storage.Items[BuildableItemEnum.White_Flour].ItemCode);
            Assert.AreEqual(0, playerAPI.Locations[0].Storage.Items[BuildableItemEnum.White_Flour].Level);
            Assert.AreEqual(2, playerAPI.Locations[0].Storage.Items[BuildableItemEnum.White_Flour].StoredQuantity);
            Assert.AreEqual(1, playerAPI.Locations[0].Storage.Items[BuildableItemEnum.White_Flour].UnStoredQuantity);
            Assert.AreEqual(10, playerAPI.TutorialStage);
        }

        [TestMethod]
        public void TestFromAPIFormat()
        {
            // Act
            GamePlayerAPI playerAPI = (GamePlayerAPI)Morph.ToAPIFormat(Player);
            GamePlayer flip = (GamePlayer)Morph.ToBusinessFormat(playerAPI);
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

        [TestMethod]
        public void TestRoundTripJSON()
        {
            // Act            
            GamePlayerAPI playerAPI = (GamePlayerAPI)Morph.ToAPIFormat(Player);
            string json = converter.Serlialize<GamePlayerAPI>(playerAPI);
            GamePlayerAPI unJson = converter.Deserialize<GamePlayerAPI>(json);
            GamePlayer flip = (GamePlayer)Morph.ToBusinessFormat(unJson);
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
