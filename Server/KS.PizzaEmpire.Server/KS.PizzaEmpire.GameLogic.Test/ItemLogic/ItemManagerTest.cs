namespace KS.PizzaEmpire.GameLogic.Test.ItemLogic
{
    using AutoMapper;
    using Business.Automapper;
    using Common.BusinessObjects;
    using GameLogic.ItemLogic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [TestClass]
    public class ItemManagerTest
    {
        [ClassInitialize]
        public static void InitAllTests(TestContext testContext)
        {
            AutoMapperConfiguration.Configure();
            Mapper.AssertConfigurationIsValid();
        }

        [TestMethod]
        public void TestInstance()
        {            
            Task.WaitAll(ItemManager.Instance.Initialize());

            Dictionary<BuildableItemEnum, BuildableItem> bitems = ItemManager.Instance.BuildableItems;

            Assert.AreEqual(11, bitems.Count);
        }
    }
}
