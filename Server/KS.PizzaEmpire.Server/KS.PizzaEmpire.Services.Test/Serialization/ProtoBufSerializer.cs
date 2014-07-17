using KS.PizzaEmpire.Services.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProtoBuf;

namespace KS.PizzaEmpire.Services.Test.Serialization
{
    /// <summary>
    /// Simple data class that will be used to test the
    /// ProtoBufSerializer class.
    /// </summary>
    [ProtoContract]
    public class ProtoBufSerializerTestEntity
    {
        [ProtoMember(1)]
        public string Name { get; set; }
        [ProtoMember(2)]
        public int? Number { get; set; }
    }

    /// <summary>
    /// Unit tests for the ProtoBufSerializer class
    /// </summary>
    [TestClass]
    public class ProtoBufSerializerTest
    {
        [TestMethod]
        public void TestProtoBufSerializerSerialize()
        {
            // Arrange
            ProtoBufSerializerTestEntity entity = new ProtoBufSerializerTestEntity{
                Name = "Kevin",
                Number = 5
            };
            ProtoBufSerializer serializer = new ProtoBufSerializer();

            // Act
            byte[] b = serializer.Serialize(entity);

            // Assert
            Assert.AreEqual(9, b.Length);
            Assert.AreEqual(10, b[0]);
            Assert.AreEqual(5, b[1]);
            Assert.AreEqual(75, b[2]);
            Assert.AreEqual(101, b[3]);
            Assert.AreEqual(118, b[4]);
            Assert.AreEqual(105, b[5]);
            Assert.AreEqual(110, b[6]);
            Assert.AreEqual(16, b[7]);
            Assert.AreEqual(5, b[8]);        
        }

        [TestMethod]
        public void TestProtoBufSerializerDeserialize()
        {
            // Arrange
            byte[] b = { 10, 5, 75, 101, 118, 105, 110, 16, 5 };
            ProtoBufSerializer serializer = new ProtoBufSerializer();

            // Act
            ProtoBufSerializerTestEntity entity = serializer.Deserialize<ProtoBufSerializerTestEntity>(b);

            // Assert
            Assert.AreEqual("Kevin", entity.Name);
            Assert.AreEqual(5, entity.Number);
        }
    }
}
