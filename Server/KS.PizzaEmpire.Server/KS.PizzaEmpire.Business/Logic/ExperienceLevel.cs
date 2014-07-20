using KS.PizzaEmpire.Business.Conversion;
using KS.PizzaEmpire.Business.StorageInformation;
using KS.PizzaEmpire.Business.TableStorage;
using ProtoBuf;
using System.Collections.Generic;
using System.IO;

namespace KS.PizzaEmpire.Business.Logic
{
    /// <summary>
    /// Represents a ExperienceLevel that determines how players level up
    /// in the game logic.
    /// </summary>
    public class ExperienceLevel : ILogicEntity, IToTableStorageEntity
    {
        /// <summary>
        /// Creates a new instance of the ExperienceLevel class.
        /// </summary>
        public ExperienceLevel() { }

        /// <summary>
        /// The information fow how this entity should be stored in different types of storage
        /// </summary>
        public IStorageInformation StorageInformation { get; set; }

        public int Level { get; set; }
        public int ExperienceRequired { get; set; }
        public List<int> NewEquipment { get; set; }
        public List<int> NewBuildableItems { get; set; }

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

        /// <summary>
        /// Generates a new ExperienceLevel instance from a ExperienceLevelTableStorage instance.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static ExperienceLevel From(ExperienceLevelTableStorage item)
        {
            ExperienceLevel clone = new ExperienceLevel();

            clone.Level = item.Level;
            clone.ExperienceRequired = item.ExperienceRequired;

            using (MemoryStream memoryStream = new MemoryStream(item.NewEquipmentSerialized))
            {
                clone.NewEquipment = Serializer.Deserialize<List<int>>(memoryStream);
            }

            using (MemoryStream memoryStream = new MemoryStream(item.NewBuildableItemsSerialized))
            {
                clone.NewBuildableItems = Serializer.Deserialize<List<int>>(memoryStream);
            }

            return clone;
        }
    }
}
