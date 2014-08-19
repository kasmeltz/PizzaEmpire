namespace KS.PizzaEmpire.Common.GameLogic.GamePlayerState
{
    using BusinessObjects;

    /// <summary>
    /// Represents one rule about the state of a game player
    /// that is ultimately either true or false
    /// </summary>
    public interface IGamePlayerStateRule
    {
        /// <summary>
        /// Returns true if the current state of the provided GamePlayer
        /// matches the conditions in the rule, false otherwise.
        /// </summary>
        /// <param name="player">The GamePlayer whose state is to be tested.</param>
        bool IsValid(GamePlayer player);
    }
}