namespace KS.PizzaEmpire.Common.GameLogic.GamePlayerState
{
    using BusinessObjects;

    /// <summary>
    /// Represents a rule that compares the player's level to some value
    /// </summary>
    public class LevelCompareRule : ComparisonRule
    {
        /// <summary>
        /// The level to compare
        /// </summary>
        public int Level { get; set; }

        public override bool IsValid(GamePlayer player)
        {
            switch (ComparisonType)
            {
                case ComparisonEnum.Equal:
                    return player.Level == Level;
                case ComparisonEnum.GreaterThan:
                    return player.Level > Level;
                case ComparisonEnum.GreaterThanOrEqual:
                    return player.Level >= Level;
                case ComparisonEnum.LessThan:
                    return player.Level < Level;
                case ComparisonEnum.LessThanOrEqual:
                    return player.Level <= Level;
                case ComparisonEnum.NotEqual:
                    return player.Level != Level;
            }

            return false;
        }
    }
}