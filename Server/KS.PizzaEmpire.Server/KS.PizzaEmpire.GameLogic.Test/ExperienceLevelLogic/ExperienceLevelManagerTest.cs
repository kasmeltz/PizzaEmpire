namespace KS.PizzaEmpire.GameLogic.Test.ExperienceLevelLogic
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using GameLogic.ExperienceLevelLogic;
    using System.Threading.Tasks;
    using Common.BusinessObjects;
    using System.Collections.Generic;

    [TestClass]
    public class ExperienceLevelManagerTest
    {
        [TestMethod]
        public void TestInstance()
        {
            Task.WaitAll(ExperienceLevelManager.Instance.Initialize());

            Dictionary<int, ExperienceLevel> elevels = ExperienceLevelManager.Instance.ExperienceLevels;

            Assert.AreEqual(4, elevels.Count);
            Assert.AreEqual(1, elevels[1].Level);
            Assert.AreEqual(0, elevels[1].ExperienceRequired);
            Assert.AreEqual(2, elevels[2].Level);
            Assert.AreEqual(100, elevels[2].ExperienceRequired);
            Assert.AreEqual(3, elevels[3].Level);
            Assert.AreEqual(300, elevels[3].ExperienceRequired);
            Assert.AreEqual(4, elevels[4].Level);
            Assert.AreEqual(700, elevels[4].ExperienceRequired);
        }
    }
}

