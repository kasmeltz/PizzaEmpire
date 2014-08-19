namespace KS.PizzaEmpire.Common.GameLogic.GamePlayerState
{
    using BusinessObjects;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a rule that compares the player's storage items
    /// </summary>
    public abstract class StorageItemCompareRule : ComparisonRule
    {
        /// <summary>
        /// The location to compare
        /// </summary>
        public int Location { get; set; }

        /// <summary>
        /// The storage to check
        /// </summary>
        public InventoryStorageEnum Storage { get; set; }

        /// <summary>
        /// The item to check
        /// </summary>
        public BuildableItemEnum Item { get; set; }

        /// <summary>
        /// The level to compare
        /// </summary>
        public int Level { get; set; }
    }
}