namespace KS.PizzaEmpire.Services.Test.Serialization
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Services.Serialization;
    using System;

    /// <summary>
    /// Simple data class that will be used to test the
    /// BinaryFormatSerializer class.
    /// </summary>
    [Serializable]
    public class BinaryFormatSerializerTestEntity
    {
        public string Name { get; set; }
        public int? Number { get; set; }
    }

    /// <summary>
    /// Unit tests for the BinaryFormatSerializer class
    /// </summary>
    [TestClass]
    public class BinaryFormatSerializerTest
    {
        [TestMethod]
        public void TestBinaryFormatSerializer()
        {
            // Arrange
            BinaryFormatSerializerTestEntity entity = new BinaryFormatSerializerTestEntity
            {
                Name = "Kevin",
                Number = 5
            };
            BinaryFormatSerializer serializer = new BinaryFormatSerializer();

            // Act
            byte[] b = serializer.Serialize(entity);
            BinaryFormatSerializerTestEntity otherEntity = serializer.Deserialize<BinaryFormatSerializerTestEntity>(b);

            // Assert
            Assert.AreEqual(274, b.Length);
            Assert.AreEqual(entity.Name, otherEntity.Name);
            Assert.AreEqual(entity.Number, otherEntity.Number);
        }
    }
}
