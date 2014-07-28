namespace KS.PizzaEmpire.Business.TableStorage
{
    using Microsoft.WindowsAzure.Storage.Table;
    using ProtoBuf;
    using System.IO;

    /// <summary>
    /// Represents the state for a player of the game as stored in table storage.
    /// </summary>
    public class ExperienceLevelTableStorage  : TableEntity, ITableStorageEntity
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
    }
}
