namespace KS.PizzaEmpire.Common.BusinessObjects
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the state for a player of the game
    /// </summary>
    public class GamePlayer : IBusinessObjectEntity
    {
        /// <summary>
        /// Creates a new instance of the GamePlayer class.
        /// </summary>
        public GamePlayer() { }

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
        /// The player's current tutorial stage
        /// </summary>
        public int TutorialStage { get; set; }

        /// <summary>
        /// Whether the state has changed
        /// </summary>
        public bool StateChanged { get; set; }

        /// <summary>
        /// The locations (restuarants or head office) associated with the player
        /// </summary>
        public List<BusinessLocation> Locations { get; set; }

        /// <summary>
        /// The work in progress for the player
        /// </summary>
        public List<WorkInProgress> WorkInProgress { get; set; }
    }
}