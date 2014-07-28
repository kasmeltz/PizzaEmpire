namespace KS.PizzaEmpire.Common.BusinessObjects
{
    using System.IO;

    /// <summary>
    /// Represents an experience level as used in the game logic.
    /// </summary>
    public class ExperienceLevel : IBusinessObjectEntity
    {
        /// <summary>
        /// Creates a new instance of the GamePlayer class.
        /// </summary>
        public ExperienceLevel() { }  

        /// <summary>
        /// The level represnted by this item
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// The experience required to achieve the level
        /// </summary>
        public int ExperienceRequired { get; set; }
    }
}
