namespace KS.PizzaEmpire.Common.GameLogic.GamePlayerState
{
    using BusinessObjects;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a rule that compares the player's storage items
    /// </summary>
    public abstract class StorageItemLevelCompareRule : StorageItemCompareRule
    {                
        public override bool IsValid(GamePlayer player)
        {
            BusinessLocation bl = player.Locations[Location];
            LocationStorage ls = bl.Storage;
            ItemQuantity iq = ls.GetItem(Item);
            int l = iq.Level;

            switch (ComparisonType)
            {
                case ComparisonEnum.Equal:
                    return l == Level;
                case ComparisonEnum.GreaterThan:
                    return l > Level;
                case ComparisonEnum.GreaterThanOrEqual:
                    return l >= Level;
                case ComparisonEnum.LessThan:
                    return l < Level;
                case ComparisonEnum.LessThanOrEqual:
                    return l <= Level;
                case ComparisonEnum.NotEqual:
                    return l != Level;
            }

            return false;            
        }
    }
}