namespace KS.PizzaEmpire.Business.Logic
{
    using Cache;
    using Conversion;
    using ProtoBuf;
    using StorageInformation;
    using System.Collections.Generic;
    using System.IO;
    using TableStorage;

    /// <summary>
    /// Represents an experience level as used in the game logic.
    /// </summary>
    public class ExperienceLevel : ILogicEntity, IToTableStorageEntity
    {
        /// <summary>
        /// Creates a new instance of the GamePlayer class.
        /// </summary>
        public ExperienceLevel() { }

        /// <summary>
        /// The information fow how this entity should be stored in different types of storage
        /// </summary>
        public IStorageInformation StorageInformation { get; set; }

        /// <summary>
        /// The level represnted by this item
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// The experience required to achieve the level
        /// </summary>
        public int ExperienceRequired { get; set; }

        #region IToTableStorageEntity

        /// <summary>
        /// Returns a new instance of the appropriate ITableStorageEntity with cloned data.
        /// </summary>
        /// <returns></returns>
        public ITableStorageEntity ToTableStorageEntity()
        {
            return ExperienceLevelTableStorage.From(this);
        }

        #endregion

        #region Cloners

        /// <summary>
        /// Generates a new ExperienceLevel instance from a ExperienceLevelTableStorage instance.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static ExperienceLevel From(ExperienceLevelTableStorage other)
        {
            ExperienceLevel clone = new ExperienceLevel();
            clone.Level = other.Level;
            clone.ExperienceRequired = other.ExperienceRequired;

            return clone;
        }
       
        #endregion
    }
}
