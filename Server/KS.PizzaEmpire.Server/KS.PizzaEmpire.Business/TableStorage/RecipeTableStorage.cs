using KS.PizzaEmpire.Business.Conversion;
using KS.PizzaEmpire.Business.Logic;
using Microsoft.WindowsAzure.Storage.Table;
using ProtoBuf;
using System.Collections.Generic;
using System.IO;

namespace KS.PizzaEmpire.Business.TableStorage
{
    /// <summary>
    /// Represents a Recipe that determines what is required to build an item
    /// as stored in table storage.
    /// </summary>
    public class RecipeTableStorage : TableEntity, ITableStorageEntity, IToLogicEntity
    {
        /// <summary>
        /// Creates a new instance of the Recipe class.
        /// </summary>
        public RecipeTableStorage() { }

        public int ItemCode { get; set; }
        public int EquipmentCode { get; set; }
        public byte[] IngredientsSerialized { get; set; }
        
        //public List<ItemQuantityTableStorage> Ingredients { get; set; }

        #region IToLogicEntity
       
        /// <summary>
        /// Returns a new instance of the appropriate ILogicEntity with cloned data.
        /// </summary>
        /// <returns></returns>
        public ILogicEntity ToLogicEntity()
        {
            return Recipe.From(this);
        }

        #endregion

        #region Cloners
       
        /// <summary>
        /// Generates a new RecipeTableStorage instance from a Recipe instance.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static RecipeTableStorage From(Recipe item)
        {
            RecipeTableStorage clone = new RecipeTableStorage();

            clone.PartitionKey = item.StorageInformation.PartitionKey;
            clone.RowKey = item.StorageInformation.RowKey;

            clone.ItemCode = item.ItemCode;
            clone.EquipmentCode = item.EquipmentCode;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                Serializer.Serialize(memoryStream, item.Ingredients);
                clone.IngredientsSerialized = memoryStream.ToArray();
            }

            return clone;
        }

        #endregion
    }
}
