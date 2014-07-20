using KS.PizzaEmpire.Business.Conversion;
using KS.PizzaEmpire.Business.StorageInformation;
using KS.PizzaEmpire.Business.TableStorage;
using System.Collections.Generic;
using System.IO;
using ProtoBuf;

namespace KS.PizzaEmpire.Business.Logic
{
    /// <summary>
    /// Represents a Recipe that determines what is required to build an item
    /// in the game logic.
    /// </summary>
    public class Recipe : ILogicEntity, IToTableStorageEntity
    {
        /// <summary>
        /// Creates a new instance of the Recipe class.
        /// </summary>
        public Recipe() { }

        /// <summary>
        /// The information fow how this entity should be stored in different types of storage
        /// </summary>
        public IStorageInformation StorageInformation { get; set; }
       
        public int ItemCode { get; set; }
        public int EquipmentCode { get; set; }
        public List<ItemQuantity> Ingredients { get; set; }
        
        #region IToTableStorageEntity

        /// <summary>
        /// Returns a new instance of the appropriate ITableStorageEntity with cloned data.
        /// </summary>
        /// <returns></returns>
        public ITableStorageEntity ToTableStorageEntity()
        {
            return RecipeTableStorage.From(this);
        }

        #endregion

        /// <summary>
        /// Generates a new Recipe instance from a RecipeTableStorage instance.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static Recipe From(RecipeTableStorage item)
        {
            Recipe clone = new Recipe();

            clone.ItemCode = item.ItemCode;
            clone.EquipmentCode = item.EquipmentCode;

            using (MemoryStream memoryStream = new MemoryStream(item.IngredientsSerialized))
            {
                clone.Ingredients = Serializer.Deserialize<List<ItemQuantity>>(memoryStream);
            }
            return clone;
        }
    }
}
