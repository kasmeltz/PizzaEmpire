using KS.PizzaEmpire.Services.Caching;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace KS.PizzaEmpire.Services.Test.Caching
{/// <summary>
    /// Simple data class that will be used to test the
    /// RedisCache class.
    /// </summary>
    [Serializable]
    public class RedisCacheTestEntity
    {
        public string Name { get; set; }
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
            RedisCache.Instance.ConnectionString = "localhost,6379";

            // Act
            await RedisCache.Instance.Set("key1", entity, TimeSpan.FromMinutes(5));
            RedisCacheTestEntity otherEntity = await RedisCache.Instance.Get<RedisCacheTestEntity>("key1");

            // Assert
            Assert.AreEqual(entity.Name, otherEntity.Name);
            Assert.AreEqual(entity.Number, otherEntity.Number);
        }
    }
}
