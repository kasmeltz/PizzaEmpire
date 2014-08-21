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
        /// The items to compare
        /// </summary>
        public List<ItemQuantity> Items { get; set; }

        protected Dictionary<BuildableItemEnum, int> itemsFound 
            = new Dictionary<BuildableItemEnum, int>();

        protected bool CompareOneItem(ItemQuantity iq)
        {
            int actual = 0;

            if (itemsFound.ContainsKey(iq.ItemCode))
            {
                actual = itemsFound[iq.ItemCode];
            }

            switch (ComparisonType)
            {
                case ComparisonEnum.Equal:
                    return actual == iq.UnStoredQuantity;
                case ComparisonEnum.GreaterThan:
                    return actual > iq.UnStoredQuantity;
                case ComparisonEnum.GreaterThanOrEqual:
                    return actual >= iq.UnStoredQuantity;
                case ComparisonEnum.LessThan:
                    return actual < iq.UnStoredQuantity;
                case ComparisonEnum.LessThanOrEqual:
                    return actual <= iq.UnStoredQuantity;
                case ComparisonEnum.NotEqual:
                    return actual != iq.UnStoredQuantity;
            }

            return false;
        }

        public override bool IsValid(GamePlayer player)
        {
            for (int i = 0; i < player.WorkItems.Count; i++)
            {
                itemsFound[player.WorkItems[i].ItemCode]++;
            }

            for (int i = 0; i < Items.Count; i++) 
            {
                if (!CompareOneItem(Items[i]) )
                {
                    return false;
                }
            }

            return true;            
        }
    }
}