namespace KS.PizzaEmpire.WebAPI.Utility
{
    using KS.PizzaEmpire.DataAccess.DataProvider;
    using KS.PizzaEmpire.Services;
    using KS.PizzaEmpire.Services.Caching;

    public class WebAPIUtility
    {

        /// <summary>
        /// Updates the services to match the current configuration parameters
        /// </summary>
        /// <returns></returns>
        public static void UpdateServiceConfig()
        {
            ConfigurableDataProvider.Instance.UseCache = true;
            RedisCache.Instance.MaxRetryAttempts = ServiceHelper.IntValueFromConfig("RedisCacheMaxRetryAttempts");
            RedisCache.Instance.RetryMillis = ServiceHelper.IntValueFromConfig("RedisCacheRetryMillis");
            RedisCache.Instance.ThrottleMillis = ServiceHelper.IntValueFromConfig("RedisCacheThrottleMillis");
        }
    }
}