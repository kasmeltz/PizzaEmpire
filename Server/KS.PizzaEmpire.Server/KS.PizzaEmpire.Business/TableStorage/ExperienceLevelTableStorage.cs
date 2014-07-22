namespace KS.PizzaEmpire.Business.TableStorage
{
    using Conversion;
    using Logic;
    using Microsoft.WindowsAzure.Storage.Table;
    using ProtoBuf;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Represents a ExperienceLevel that determines how players level up
    /// as stored in table storage.
    /// </summary>
    public class ExperienceLevelTableStorage : TableEntity, ITableStorageEntity, IToLogicEntity
    {
        /// <summary>
        /// Creates a new instance of the ExperienceLevelTableStorage class.
        /// </summary>
        public ExperienceLevelTableStorage() { }

        public int Level { get; set; }
        public int ExperienceRequired { get; set; }
        public byte[] NewEquipmentSerialized { get; set; }
        public byte[] NewBuildableItemsSerialized { get; set; }
        
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
        /// Generates a new RecipeTableStorage instance from a Recipe instance.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static ExperienceLevelTableStorage From(ExperienceLevel item)
        {
            ExperienceLevelTableStorage clone = new ExperienceLevelTableStorage();

            clone.PartitionKey = item.StorageInformation.PartitionKey;
            clone.RowKey = item.StorageInformation.RowKey;

            clone.Level = item.Level;
            clone.ExperienceRequired = item.ExperienceRequired;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                Serializer.Serialize(memoryStream, item.NewEquipment);
                clone.NewEquipmentSerialized = memoryStream.ToArray();
            }

            using (MemoryStream memoryStream = new MemoryStream())
            {
                Serializer.Serialize(memoryStream, item.NewBuildableItems);
                clone.NewBuildableItemsSerialized = memoryStream.ToArray();
            }

            return clone;
        }

        #endregion
    }
}
