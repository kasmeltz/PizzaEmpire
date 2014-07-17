using KS.PizzaEmpire.Services.Caching;
using KS.PizzaEmpire.Services.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WindowsAzure.Storage.Table;
using ProtoBuf;
using System;
using System.Threading.Tasks;

namespace KS.PizzaEmpire.Services.Test.Caching
{/// <summary>
    /// Simple data class that will be used to test the
    /// RedisCache class.
    /// </summary>
    [Serializable]
    [ProtoContract]
    public class RedisCacheTestEntity : TableEntity
    {
        [ProtoMember(1)]
        public string Name { get; set; }
        [ProtoMember(2)]
        public int? Number { get; set; }
    }
   
    [TestClass]
    public class RedisCacheTest
    {
        [TestMethod]
        public async Task TestRedisCache()
        {
            // Arrange
            RedisCacheTestEntity entity = new RedisCacheTestEntity
            {
                Name = "Kevin",
                Number = 5
            };
            RedisCache.Instance.ConnectionString = "localhost:6379";
            RedisCache.Instance.CacheSerializer = new ProtoBufSerializer();

            // Act
            await RedisCache.Instance.Set("key1", entity, TimeSpan.FromMinutes(5));
            RedisCacheTestEntity otherEntity = await RedisCache.Instance.Get<RedisCacheTestEntity>("key1");

            // Assert
            Assert.AreEqual(entity.Name, otherEntity.Name);
            Assert.AreEqual(entity.Number, otherEntity.Number);

            await RedisCache.Instance.Delete("key1");
            otherEntity = await RedisCache.Instance.Get<RedisCacheTestEntity>("key1");
            
            Assert.IsNull(otherEntity);
        }
    }
}
