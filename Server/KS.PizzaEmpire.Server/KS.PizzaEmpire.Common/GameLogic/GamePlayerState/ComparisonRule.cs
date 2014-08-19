namespace KS.PizzaEmpire.Common.GameLogic.GamePlayerState
{
    using BusinessObjects;

    /// <summary>
    /// Represents a rule that performs a comparison between values
    /// </summary>
    public abstract class ComparisonRule : IGamePlayerStateRule
    {
        /// <summary>
        /// The type of comparison to perform
        /// </summary>
        public ComparisonEnum ComparisonType { get; set; }

        #region IGamePlayerStateRule

        public abstract bool IsValid(GamePlayer player);
        
        #endregion 
    }
}
