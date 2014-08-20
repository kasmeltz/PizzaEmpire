namespace KS.PizzaEmpire.Common.BusinessObjects
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents the per level stats associated with a BuildableItem
    /// </summary>
    public class BuildableItemStat
    {
        /// <summary>
        /// Creates a new instance of the BuildableItemStat class.
        /// </summary>
        public BuildableItemStat() { }

        /// <summary>
        /// The BuildableItem this stat is associated with
        /// </summary>
        public BuildableItem Parent { get; set; }

        /// <summary>
        /// The level of this stat
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// The level required to build this item
        /// </summary>
        public int RequiredLevel { get; set; }

        /// <summary>
        /// The cost in coins to build this item
        /// </summary>
        public int CoinCost { get; set; }
               
        /// <summary>
        /// The experience gained when this item is built
        /// </summary>
        public int Experience { get; set; }

        /// <summary>
        /// The number of seconds required to build this item
        /// </summary>
        public int BuildSeconds { get; set; }

        /// <summary>
        /// The number of coupons required to build this item
        /// </summary>
        public int CouponCost { get; set; }

        /// <summary>
        /// The number of coupons required to speed up this item
        /// </summary>
        public int SpeedUpCoupons { get; set; }

        /// <summary>
        /// The number of seconds this item will be sped up by specnding coupons
        /// </summary>
        public int SpeedUpSeconds { get; set; }

        /// <summary>
        /// The required items to build this item
        /// </summary>
        public List<ItemQuantity> RequiredItems { get; set; } 
    }
}