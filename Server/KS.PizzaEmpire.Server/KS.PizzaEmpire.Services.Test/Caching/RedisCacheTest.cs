﻿namespace KS.PizzaEmpire.Services.Test.Caching
{
    using Business.Cache;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ProtoBuf;
    using Services.Caching;
    using Services.Serialization;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Simple data class that will be used to test the
    /// RedisCache class.
    /// </summary>
    [Serializable]
    [ProtoContract]
    public class RedisCacheTestEntity : ICacheEntity
    {
        [ProtoMember(1)]
        public string Name { get; set; }
        [ProtoMember(2)]
        public int? Number { get; set; }
    }

    /// <summary>
    /// Simple data class that will be used to test the
    /// RedisCache class.
    /// </summary>
    [Serializable]
    [ProtoContract]
    public class RedisCacheTestEntityInts : ICacheEntity
    {
        [ProtoMember(1)]
        public int Coins { get; set; }
        [ProtoMember(2)]
        public int Coupons { get; set; }
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
            RedisCache.Instance.CacheSerializer = new ProtoBufSerializer();
            RedisCache.Instance.RetryMillis = 5000;

            // Act
            await RedisCache.Instance.Set<RedisCacheTestEntity>("key1", entity, TimeSpan.FromMinutes(5));
            RedisCacheTestEntity otherEntity = await RedisCache.Instance.Get<RedisCacheTestEntity>("key1");

            // Assert
            Assert.AreEqual(entity.Name, otherEntity.Name);
            Assert.AreEqual(entity.Number, otherEntity.Number);

            await RedisCache.Instance.Delete<RedisCacheTestEntity>("key1");
            otherEntity = await RedisCache.Instance.Get<RedisCacheTestEntity>("key1");

            Assert.IsNull(otherEntity);
        }


        [TestMethod]
        public async Task TestRedisCacheInts()
        {
            // Arrange
            RedisCacheTestEntityInts entity = new RedisCacheTestEntityInts
            {
                Coins = 0,
                Coupons = 0
            };
            RedisCache.Instance.CacheSerializer = new ProtoBufSerializer();
            RedisCache.Instance.RetryMillis = 5000;

            // Act
            await RedisCache.Instance.Set<RedisCacheTestEntityInts>("key1", entity, TimeSpan.FromMinutes(5));
            RedisCacheTestEntityInts otherEntity = await RedisCache.Instance.Get<RedisCacheTestEntityInts>("key1");

            // Assert
            Assert.AreEqual(entity.Coins, otherEntity.Coins);
            Assert.AreEqual(entity.Coupons, otherEntity.Coupons);

            await RedisCache.Instance.Delete<RedisCacheTestEntityInts>("key1");
            otherEntity = await RedisCache.Instance.Get<RedisCacheTestEntityInts>("key1");

            Assert.IsNull(otherEntity);
        }
    }
}
