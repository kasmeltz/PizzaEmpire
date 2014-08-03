namespace KS.PizzaEmpire.Common.APITransfer
{
    using BusinessObjects;
    using System.Collections.Generic;

    public class GamePlayerAPI : IAPIEntity
    {
        /// <summary>
        /// Creates a new instance of the GamePlayerAPI class.
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
        public string BuildableItems { get; set; }

        /// <summary>
        /// The work in progress for the player
        /// </summary>
        public List<WorkItem> WorkItems { get; set; }

        /// <summary>
        /// The player's current tutorial stage
        /// </summary>
        public int TutorialStage { get; set; }
    }
}
