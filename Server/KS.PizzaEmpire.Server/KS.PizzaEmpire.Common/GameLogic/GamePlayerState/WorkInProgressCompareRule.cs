namespace KS.PizzaEmpire.Common.GameLogic.GamePlayerState
{
    using BusinessObjects;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a rule that compares the player's work in progress
    /// </summary>
    public class WorkInProgressCompareRule : ComparisonRule
    {
        /// <summary>
        /// The item to compare
        /// </summary>
        public ItemQuantity Item { get; set; }

        public override bool IsValid(GamePlayer player)
        {
            int actual = 0;

            for (int i = 0; i < player.WorkInProgress.Count; i++)
            {
                WorkInProgress wip = player.WorkInProgress[i];
                if (wip.Quantity.ItemCode == Item.ItemCode)
                {
                    actual += wip.Quantity.UnStoredQuantity;
                }
            }

            switch (ComparisonType)
            {
                case ComparisonEnum.Equal:
                    return actual == Item.UnStoredQuantity;
                case ComparisonEnum.GreaterThan:
                    return actual > Item.UnStoredQuantity;
                case ComparisonEnum.GreaterThanOrEqual:
                    return actual >= Item.UnStoredQuantity;
                case ComparisonEnum.LessThan:
                    return actual < Item.UnStoredQuantity;
                case ComparisonEnum.LessThanOrEqual:
                    return actual <= Item.UnStoredQuantity;
                case ComparisonEnum.NotEqual:
                    return actual != Item.UnStoredQuantity;
            }

            return false;
        }
    }
}