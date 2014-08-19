namespace KS.PizzaEmpire.Common.GameLogic.GamePlayerState
{
    using BusinessObjects;

    /// <summary>
    /// Represents a rule that compares the player's tutorial stage to some value
    /// </summary>
    public class TutorialStageCompareRule : ComparisonRule
    {
        /// <summary>
        /// The tutorial stage to compare
        /// </summary>
        public int TutorialStage { get; set; }

        public override bool IsValid(GamePlayer player)
        {
            switch (ComparisonType)
            {
                case ComparisonEnum.Equal:
                    return player.TutorialStage == TutorialStage;
                case ComparisonEnum.GreaterThan:
                    return player.TutorialStage > TutorialStage;
                case ComparisonEnum.GreaterThanOrEqual:
                    return player.TutorialStage >= TutorialStage;
                case ComparisonEnum.LessThan:
                    return player.TutorialStage < TutorialStage;
                case ComparisonEnum.LessThanOrEqual:
                    return player.TutorialStage <= TutorialStage;
                case ComparisonEnum.NotEqual:
                    return player.TutorialStage != TutorialStage;
            }

            return false;
        }
    }
}