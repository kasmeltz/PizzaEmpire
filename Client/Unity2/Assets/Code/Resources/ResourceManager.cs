using System.Collections.Generic;
namespace KS.PizzaEmpire.Unity
{
    /// <summary>
    /// Manages the resources used in the game
    /// </summary>
    public class ResourceManager
    {
        private Dictionary<ResourceEnum, string> ResourceLocations {  get; set; }



        /// <summary>
        /// Returns the resource associated with the provided resource enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resource"></param>
        /// <returns></returns>
        public T Get<T>(ResourceEnum resource)
        {
            return default(T);
        }
    }
}
