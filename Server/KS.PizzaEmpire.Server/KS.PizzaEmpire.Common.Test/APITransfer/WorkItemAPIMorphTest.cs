using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KS.PizzaEmpire.Common.Test.APITransfer
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Common.BusinessObjects;
    using Common.APITransfer;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    [TestClass]
    public class WorkItemAPIMorphTest
    {
        public BuildableItemAPIMorph Morph;
        public WorkItem Item;

        [TestInitialize]
        public void Initialize()
        {
            Morph = new BuildableItemAPIMorph();

            Item = new WorkItem
            {
                ItemCode = BuildableItemEnum.White_Pizza_Dough,
                Stats = new List<BuildableItemStat>
                {
                    new BuildableItemStat
                    {
                        RequiredLevel = 2,
                        CoinCost = 50,
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
                                StoredQuantity = 1
                            },
                            new ItemQuantity
                            {
                                ItemCode = BuildableItemEnum.Salt,
                                StoredQuantity = 1
                            },
                            new ItemQuantity
                            {
                                ItemCode = BuildableItemEnum.Yeast,
                                StoredQuantity = 1                    
                            }
                        }
                    }
                },
                WorkStats = new List<WorkItemStat>
                {
                    new WorkItemStat
                    {
                    }
                }
            };
        }

        [TestMethod]
        public void TestToAPIFormat()
        {
            BuildableItemAPI itemAPI = (BuildableItemAPI)Morph.ToAPIFormat(Item);

            Assert.AreEqual(Item, itemAPI.WorkItem);
        }

        [TestMethod]
        public void TestFromAPIFormat()
        {
            BuildableItemAPI itemAPI = (BuildableItemAPI)Morph.ToAPIFormat(Item);
            WorkItem flip = (WorkItem)Morph.ToBusinessFormat(itemAPI);

            Assert.AreEqual(BuildableItemEnum.White_Pizza_Dough, flip.ItemCode);
            Assert.AreEqual(1, flip.Stats.Count);
            Assert.AreEqual(2, flip.Stats[0].RequiredLevel);
            Assert.AreEqual(50, flip.Stats[0].CoinCost);
            Assert.AreEqual(100, flip.Stats[0].Experience);
            Assert.AreEqual(60, flip.Stats[0].BuildSeconds);
            Assert.AreEqual(0, flip.Stats[0].CouponCost);
            Assert.AreEqual(1, flip.Stats[0].SpeedUpCoupons);
            Assert.AreEqual(60, flip.Stats[0].SpeedUpSeconds);
            Assert.AreEqual(3, flip.Stats[0].RequiredItems.Count);
            Assert.AreEqual(BuildableItemEnum.White_Flour, flip.Stats[0].RequiredItems[0].ItemCode);
            Assert.AreEqual(1, flip.Stats[0].RequiredItems[0].StoredQuantity);
            Assert.AreEqual(BuildableItemEnum.Salt, flip.Stats[0].RequiredItems[1].ItemCode);
            Assert.AreEqual(1, flip.Stats[0].RequiredItems[1].StoredQuantity);
            Assert.AreEqual(BuildableItemEnum.Yeast, flip.Stats[0].RequiredItems[2].ItemCode);
            Assert.AreEqual(1, flip.Stats[0].RequiredItems[2].StoredQuantity);
            Assert.AreEqual(1, flip.WorkStats.Count);
        }

        [TestMethod]
        public void TestRoundTripJSON()
        {
            // Act            
            BuildableItemAPI itemAPI = (BuildableItemAPI)Morph.ToAPIFormat(Item);
            string json = JsonConvert.SerializeObject(itemAPI);
            BuildableItemAPI unJson = JsonConvert.DeserializeObject<BuildableItemAPI>(json);
            WorkItem flip = (WorkItem)Morph.ToBusinessFormat(unJson);

            // Assert
            Assert.AreEqual(BuildableItemEnum.White_Pizza_Dough, flip.ItemCode);
            Assert.AreEqual(1, flip.Stats.Count);
            Assert.AreEqual(2, flip.Stats[0].RequiredLevel);
            Assert.AreEqual(50, flip.Stats[0].CoinCost);
            Assert.AreEqual(100, flip.Stats[0].Experience);
            Assert.AreEqual(60, flip.Stats[0].BuildSeconds);
            Assert.AreEqual(0, flip.Stats[0].CouponCost);
            Assert.AreEqual(1, flip.Stats[0].SpeedUpCoupons);
            Assert.AreEqual(60, flip.Stats[0].SpeedUpSeconds);
            Assert.AreEqual(3, flip.Stats[0].RequiredItems.Count);
            Assert.AreEqual(BuildableItemEnum.White_Flour, flip.Stats[0].RequiredItems[0].ItemCode);
            Assert.AreEqual(1, flip.Stats[0].RequiredItems[0].StoredQuantity);
            Assert.AreEqual(BuildableItemEnum.Salt, flip.Stats[0].RequiredItems[1].ItemCode);
            Assert.AreEqual(1, flip.Stats[0].RequiredItems[1].StoredQuantity);
            Assert.AreEqual(BuildableItemEnum.Yeast, flip.Stats[0].RequiredItems[2].ItemCode);
            Assert.AreEqual(1, flip.Stats[0].RequiredItems[2].StoredQuantity);
            Assert.AreEqual(1, flip.WorkStats.Count);
        }
    }
}
