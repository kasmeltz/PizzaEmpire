namespace KS.PizzaEmpire.Common.BusinessObjects
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents the storage information for one type of item in one storage
    /// </summary>
    public class StorageItem
    {
        /// <summary>
        /// Creates a new instance of the StorageItem class
        /// </summary>
        public StorageItem() { }

        /// <summary>
        /// The items in this storage
        /// </summary>
        public Dictionary<int, int> Quantity { get; set; }

        protected int highestLevel = 0;

        /// <summary>
        /// Gets the quantity of the items of the requested level
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public int GetQuantity(int level)
        {
            if (!Quantity.ContainsKey(level))
            {
                return 0;
            }

            return Quantity[level];
        }

        /// <summary>
        /// Set the quantity of items for a level
        /// </summary>
        /// <param name="level"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public void SetQuantity(int level, int quantity)
        {
            Quantity[level] = quantity;

            if (quantity > 0)
            {
                if (level > highestLevel)
                {
                    highestLevel = level;
                }
            } 
            else
            {
                highestLevel = 0;
                foreach(int i in Quantity.Keys)
                {
                    if (i > highestLevel && Quantity[i] > 0)
                    {
                        highestLevel = i;
                    }
                }                
            }
        }

        /// <summary>
        /// Gets the highest level of the items
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public int GetHighestLevel()
        {
            return highestLevel;
        }
    }
}
