using KS.PizzaEmpire.Business.Logic;
using System.Collections.Generic;

namespace KS.PizzaEmpire.Business.API
{
    /// <summary>
    /// Represents the state for a player of the game as used in the communication API.
    /// </summary>
    class GamePlayerAPI : IAPIEntity
    {
        /// <summary>
        /// Creates a new instance of the GamePlayer class.
        /// </summary>
        public GamePlayerAPI() { }

        /// <summary>
        /// The number of coins owned by the player
        /// </summary>
        public int Coins { get; set; }

        /// <summary>
        /// The number of coupons owned by the player
        /// </summary>
        public int Coupons { get; set; }

        /// <summary>
        /// The current experience of the player
        /// </summary>
        public int Experience { get; set; }

        /// <summary>
        /// The players current level
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// The players inventory of items
        /// </summary>
        public Dictionary<int, int> BuildableItems { get; set; }

        /// <summary>
        /// The work in progress for the player
        /// </summary>
        public List<WorkItem> WorkItems { get; set; }

        /// <summary>
        /// Generates a new GamePlayerAPI instance from a GamePlayer instance.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static GamePlayerAPI From(GamePlayer item)
        {
            GamePlayerAPI clone = new GamePlayerAPI();
            clone.Coins = item.Coins;
            clone.Coupons = item.Coupons;
            clone.Experience = item.Experience;
            clone.Level = item.Level;
            clone.BuildableItems = new Dictionary<int, int>();
            foreach (KeyValuePair<BuildableItemEnum, int> kvp in item.BuildableItems)
            {
                clone.BuildableItems[(int)kvp.Key] = kvp.Value;
            }                        
            clone.WorkItems = item.WorkItems;

            return clone;
        }
    }
}
