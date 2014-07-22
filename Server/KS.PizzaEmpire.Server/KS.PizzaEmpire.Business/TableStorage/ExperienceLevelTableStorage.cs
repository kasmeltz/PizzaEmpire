namespace KS.PizzaEmpire.Business.TableStorage
{
    using Conversion;
    using Logic;
    using Microsoft.WindowsAzure.Storage.Table;
    using ProtoBuf;
    using System.IO;

    /// <summary>
    /// Represents the state for a player of the game as stored in table storage.
    /// </summary>
    public class ExperienceLevelTableStorage  : TableEntity, ITableStorageEntity, IToLogicEntity
    {
        /// <summary>
        /// Creates a new instance of the GamePlayerTableStorage class.
        /// </summary>
        public ExperienceLevelTableStorage() { }

        /// <summary>
        /// The level represnted by this item
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// The experience required to achieve the level
        /// </summary>
        public int ExperienceRequired { get; set; }

        #region IToLogicEntity
       
        /// <summary>
        /// Returns a new instance of the appropriate ILogicEntity with cloned data.
        /// </summary>
        /// <returns></returns>
        public ILogicEntity ToLogicEntity()
        {
            return ExperienceLevel.From(this);
        }

        #endregion

        #region Cloners
       
        /// <summary>
        /// Generates a new GamePlayerTableStorage instance from a GamePlayer instance.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static ExperienceLevelTableStorage From(ExperienceLevel other)
        {
            ExperienceLevelTableStorage clone = new ExperienceLevelTableStorage();
            clone.PartitionKey = other.StorageInformation.PartitionKey;
            clone.RowKey = other.StorageInformation.RowKey;

            clone.Level = other.Level;
            clone.ExperienceRequired = other.ExperienceRequired;

            return clone;
        }

        #endregion
    }
}
