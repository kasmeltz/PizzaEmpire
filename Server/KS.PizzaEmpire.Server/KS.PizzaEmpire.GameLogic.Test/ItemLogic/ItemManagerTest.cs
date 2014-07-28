namespace KS.PizzaEmpire.GameLogic.Test.ItemLogic
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using GameLogic.ItemLogic;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using KS.PizzaEmpire.Common.BusinessObjects;

    [TestClass]
    public class ItemManagerTest
    {
        [TestMethod]
        public void TestInstance()
        {
            Task.WaitAll(ItemManager.Instance.Initialize());

            Dictionary<BuildableItemEnum, BuildableItem> bitems = ItemManager.Instance.BuildableItems;

            Assert.AreEqual(9, bitems.Count);
        }
    }
}
