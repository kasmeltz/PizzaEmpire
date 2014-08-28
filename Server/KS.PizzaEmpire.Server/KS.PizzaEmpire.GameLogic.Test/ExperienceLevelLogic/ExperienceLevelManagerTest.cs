namespace KS.PizzaEmpire.GameLogic.Test.ExperienceLevelLogic
{
    using AutoMapper;
    using Business.Automapper;
    using Common.BusinessObjects;
    using GameLogic.ExperienceLevelLogic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [TestClass]
    public class ExperienceLevelManagerTest
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

