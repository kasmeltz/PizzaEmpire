namespace KS.PizzaEmpire.Common.APITransfer
{
    using BusinessObjects;
    using System.Collections.Generic;

    /// <summary>
    /// Class that can morph ExperienceLevel objects to and from a dto format
    /// </summary>
    public class ExperienceLevelAPIMorph : IAPIEntityMorph
    {
        /// <summary>
        /// Converts a business object to an API dto object
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public IAPIEntity ToAPIFormat(IBusinessObjectEntity entity)
        {
            ExperienceLevel other = entity as ExperienceLevel;
            ExperienceLevelAPI clone = new ExperienceLevelAPI();

            clone.Level = other.Level;
            clone.ExperienceRequired = other.ExperienceRequired;

            return clone;
        }

        /// <summary>
        /// Converts an API dto objec to a business object
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public IBusinessObjectEntity ToBusinessFormat(IAPIEntity entity)
        {
            ExperienceLevelAPI other = entity as ExperienceLevelAPI;
            ExperienceLevel clone = new ExperienceLevel();

            clone.Level = other.Level;
            clone.ExperienceRequired = other.ExperienceRequired;

            return clone;
        }
    }
}
