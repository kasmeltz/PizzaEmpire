namespace KS.PizzaEmpire.Common.GameLogic.GamePlayerState
{
    using BusinessObjects;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a rule that compares the player's storage items
    /// </summary>
    public class StorageItemStoredQuantityCompareRule : StorageItemCompareRule
    {        
        /// <summary>
        /// The quantity to compare
        /// </summary>
        public int Quantity { get; set; }

        public override bool IsValid(GamePlayer player)
        {
            BusinessLocation bl = player.Locations[Location];
            LocationStorage ls = bl.Storage;            
            ItemQuantity iq = ls.GetItem(Item);
            int q;
            if (iq == null)
            {
                q = 0;
            }
            else
            {
                q = iq.StoredQuantity;
            }       

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