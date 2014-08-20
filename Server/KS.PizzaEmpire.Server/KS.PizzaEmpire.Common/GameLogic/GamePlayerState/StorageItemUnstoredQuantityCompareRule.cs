namespace KS.PizzaEmpire.Common.GameLogic.GamePlayerState
{
    using BusinessObjects;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a rule that compares the player's storage items
    /// </summary>
    public class StorageItemUnstoredQuantityCompareRule : StorageItemCompareRule
    {        
        /// <summary>
        /// The quantity to compare
        /// </summary>
        public int Quantity { get; set; }

        public override bool IsValid(GamePlayer player)
        {
            BusinessLocation bl = player.GetLocation(Location);
            LocationStorage ls = bl.Storage;            
            GamePlayerItem si = ls.GetItem(Item);
            int q = si.UnstoredQuantity;

            switch (ComparisonType)
            {
                case ComparisonEnum.Equal:
                    return q == Quantity;
                case ComparisonEnum.GreaterThan:
                    return q > Quantity;
                case ComparisonEnum.GreaterThanOrEqual:
                    return q >= Quantity;
                case ComparisonEnum.LessThan:
                    return q < Quantity;
                case ComparisonEnum.LessThanOrEqual:
                    return q <= Quantity;
                case ComparisonEnum.NotEqual:
                    return q != Quantity;
            }

            return false;
        }
    }
}