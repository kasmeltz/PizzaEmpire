namespace KS.PizzaEmpire.Common.GameLogic.GamePlayerState
{
    using BusinessObjects;
    using ObjectPool;
    using System.Collections.Generic;

    /// <summary>
    /// Represents an item that returns true or false
    /// depending if the state of a passed in GamePlayer
    /// instance matches the expected state
    /// </summary>
    public class GamePlayerStateCheck : IResetable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KS.PizzaEmpire.Unity.GamePlayerStateCheck"/> class.
        /// </summary>
        public GamePlayerStateCheck()
        {
            Rules = new List<IGamePlayerStateRule>();
        }
        
        public List<IGamePlayerStateRule> Rules;

        /// <summary>
        /// Returns true if the given GamePlayer stats matches the current list of 
        /// GamePlayerStateRules
        /// </summary>
        /// <returns><c>true</c>, if state matches, <c>false</c> otherwise.</returns>
        /// <param name="player">The persistent player data.</param>
        public bool IsValid(GamePlayer player)
        {
            for (int i = 0; i < Rules.Count; i++)
            {
                if (!Rules[i].IsValid(player))
                {
                    return false;
                }
            }           

            return true;
        }

        /// <summary>
        /// Copies the state from another instance
        /// </summary>
        /// <param name="other">The GamePlayerStateCheck to copy from</param>
        public void CopyFrom(GamePlayerStateCheck other)
        {
            Rules.Clear();
            for (int i = 0; i < other.Rules.Count; i++)
            {
                Rules.Add(other.Rules[i]);                    
            }
        }

        #region IResetable

        public void Reset()
        {
            Rules.Clear();
        }

        #endregion
    }
}