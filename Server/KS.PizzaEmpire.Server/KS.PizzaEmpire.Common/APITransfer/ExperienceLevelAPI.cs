namespace KS.PizzaEmpire.Common.APITransfer
{
    using BusinessObjects;
    using System.Collections.Generic;

    public class ExperienceLevelAPI : IAPIEntity
    {
        /// <summary>
        /// Creates a new instance of the ExperienceLevelAPI class.
        /// </summary>
        public ExperienceLevelAPI() { }

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
