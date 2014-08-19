namespace KS.PizzaEmpire.Common.BusinessObjects
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents one of the business locations for a game player
    /// e.g. a Restaurant or Head office
    /// 
    /// </summary>
    public class BusinessLocation
    {
        /// <summary>
        /// Creates a new instance of the BusinessLocation class
        /// </summary>
        public BusinessLocation() { }

        /// <summary>
        /// The inventories at this location
        /// </summary>
        public Dictionary<InventoryStorageEnum, LocationStorage> Storage { get; set; }

        /// <summary>
        /// Returns the storage for the given storage type
        /// </summary>
        /// <param name="storage">The storage type to retrieve</param>
        public LocationStorage GetStorage(InventoryStorageEnum storageType)
        {
            return Storage[storageType];
        }
    }
}
