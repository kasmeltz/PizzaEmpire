﻿namespace KS.PizzaEmpire.Common.GameLogic.GamePlayerState
{
    using BusinessObjects;

    /// <summary>
    /// Represents a rule that compares whether the player can build an item
    /// </summary>
    public class CanBuildItemRule : IGamePlayerStateRule
    {
        public CanBuildItemRule() {}

        public BuildableItemEnum Item { get; set; }
        public int Level { get; set; }
        public int Location { get; set; }

        #region IGamePlayerStateRule

        public bool IsValid(GamePlayer player)
        {
            if (GamePlayerLogic.Instance
                .CanBuildItem(player, Location, Level, Item) != 
                    ErrorCode.ERROR_OK)
            {
                return false;
            }

            return true;
        }
        
        #endregion          
    }
}
