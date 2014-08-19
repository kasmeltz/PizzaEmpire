namespace KS.PizzaEmpire.Common.GameLogic.GamePlayerState
{
    using BusinessObjects;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a rule that compares the player's work items
    /// </summary>
    public class WorkItemCompareRule : ComparisonRule
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
                    return actual == iq.Quantity;
                case ComparisonEnum.GreaterThan:
                    return actual > iq.Quantity;
                case ComparisonEnum.GreaterThanOrEqual:
                    return actual >= iq.Quantity;
                case ComparisonEnum.LessThan:
                    return actual < iq.Quantity;
                case ComparisonEnum.LessThanOrEqual:
                    return actual <= iq.Quantity;
                case ComparisonEnum.NotEqual:
                    return actual != iq.Quantity;
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