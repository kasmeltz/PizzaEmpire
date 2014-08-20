namespace KS.PizzaEmpire.Common.BusinessObjects
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents one storage at one particular business location
    /// </summary>
    public class LocationStorage
    {
         /// <summary>
        /// Creates a new instance of the LocationStorage class
        /// </summary>
        public LocationStorage() { }

        /// <summary>
        /// The items in this storage
        /// </summary>
        public Dictionary<BuildableItemEnum, GamePlayerItem> Items { get; set; }

        /// <summary>
        /// Returns the GamePlayerItem for the given item type
        /// </summary>
        /// <param name="itemCode">The item to retrieve</param>
        public GamePlayerItem GetItem(BuildableItemEnum itemCode)
        {
            if (!Items.ContainsKey(itemCode))
            {
                return null;
            }

            return Items[itemCode];
        }
    }
}
