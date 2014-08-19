namespace KS.PizzaEmpire.Common.GameLogic.GamePlayerState
{
    using BusinessObjects;

    /// <summary>
    /// Represents a rule that compares the player's coins to some value
    /// </summary>
    public class CoinCompareRule : ComparisonRule
    {
        /// <summary>
        /// The number of coins to compare
        /// </summary>
        public int Coins { get; set; }

        public override bool IsValid(GamePlayer player)
        {
            switch (ComparisonType)
            {
                case ComparisonEnum.Equal:
                    return player.Coins == Coins;
                case ComparisonEnum.GreaterThan:
                    return player.Coins > Coins;
                case ComparisonEnum.GreaterThanOrEqual:
                    return player.Coins >= Coins;
                case ComparisonEnum.LessThan:
                    return player.Coins < Coins;
                case ComparisonEnum.LessThanOrEqual:
                    return player.Coins <= Coins;
                case ComparisonEnum.NotEqual:
                    return player.Coins != Coins;
            }

            return false;
        }
    }
}