using Microsoft.WindowsAzure;
using System;
using System.Threading.Tasks;

namespace KS.PizzaEmpire.Services
{
    public class ServiceHelper
    {
        /// <summary>
        /// Performs an async operation with retries, ultimatetly propogating the Exception after maxAttempts.
        /// </summary>        
        /// <typeparam name="T">The return type of the operation,</typeparam>
        /// <param name="fn">The operation to perform.</param>
        /// <param name="retryMillis">The number of milliseconds to pause before attempting a retry.</param>
        /// <param name="maxAttempts">The maximum number of attempts before throwing an exception.</param>
        /// <param name="throttleMillis">The number of milliseconds to pause before performing any operation.</param>
        /// <param name="currentAttempt">The current attempt number.</param>
        /// <param name="currentException">The last exception that was encountered.</param>
        /// <returns></returns>
        public static async Task<T> RetryAsync<T>(Func<Task<T>> fn, int retryMillis, int maxAttempts,
            int throttleMillis = 0, int currentAttempt = 0, Exception currentException = null)
        {
            await Task.Delay(throttleMillis);

            if (currentAttempt > maxAttempts)
            {
                // @TODO Log fact that max attempts failed
                throw currentException;
            }

            try
            {
                return await fn();
            }
            catch (Exception e)
            {
                // @TODO Log Exception
                currentException = e;
            }

            await Task.Delay(retryMillis);
            return await RetryAsync<T>(fn, retryMillis, maxAttempts, throttleMillis, ++currentAttempt, currentException);
        }
        
        /// <summary>
        /// Returns an integer value from an Azure Configuration string
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int IntValueFromConfig(string key)
        {
            int i = 0;
            Int32.TryParse(CloudConfigurationManager.GetSetting(key), out i);
            return i;
        }
    }
}
