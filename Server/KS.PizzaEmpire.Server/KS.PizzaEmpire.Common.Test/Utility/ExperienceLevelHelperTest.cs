namespace KS.PizzaEmpire.Common.Test.Utility
{
    using BusinessObjects;
    using Common.Utility;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;

    [TestClass]
    public class ExperienceLevelHelperTest
    {
        [TestMethod]
        public void TestToJSONEmptyDict()
        {
            // Arrange
            Dictionary<int, ExperienceLevel> dict = new Dictionary<int, ExperienceLevel>();

            // Act
            string json = ExperienceLevelHelper.ToJSON(dict);

            // Assert
            Assert.AreEqual("[]", json);
        }

        [TestMethod]
        public void TestToJSON()
        {
            // Arrange
            Dictionary<int, ExperienceLevel> dict = new Dictionary<int, ExperienceLevel>();
            dict[1] = new ExperienceLevel { Level = 1, ExperienceRequired = 100 };
            dict[2] = new ExperienceLevel { Level = 2, ExperienceRequired = 300 };
            string expectedJson = @"[{""Level"":1,""ExperienceRequired"":100},{""Level"":2,""ExperienceRequired"":300}]";
            
            // Act
            string json = ExperienceLevelHelper.ToJSON(dict);

            // Assert
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void TestFromJSON()
        {
            // Arrange
            string expectedJson = @"[{""Level"":1,""ExperienceRequired"":100},{""Level"":2,""ExperienceRequired"":300}]";
            
            // Act
            Dictionary<int, ExperienceLevel> dict = ExperienceLevelHelper.FromJSON(expectedJson);

            // Assert
            Assert.AreEqual(2, dict.Values.Count);
            Assert.AreEqual(1, dict[1].Level);
            Assert.AreEqual(100, dict[1].ExperienceRequired);
            Assert.AreEqual(2, dict[2].Level);
            Assert.AreEqual(300, dict[2].ExperienceRequired);
        }
    }
}
