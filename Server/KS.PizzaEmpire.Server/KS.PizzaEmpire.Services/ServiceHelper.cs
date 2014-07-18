using System;
using System.Threading.Tasks;

namespace KS.PizzaEmpire.Services
{
    public class ServiceHelper
    {
        /// <summary>
        /// Performs an async operation with retries, ultimatetly propogating the Exception after maxAttempts
        /// </summary>
        public static async Task<T> RetryAsync<T>(Func<Task<T>> fn, int retryMillis, int maxAttempts,
            int currentAttempt = 0, Exception currentException = null)
        {
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
            return await RetryAsync<T>(fn, retryMillis, maxAttempts, ++currentAttempt, currentException);
        }
    }
}
