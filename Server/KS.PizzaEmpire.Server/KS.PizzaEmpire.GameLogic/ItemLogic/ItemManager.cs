using KS.PizzaEmpire.Business.Common;
using KS.PizzaEmpire.Business.Logic;
using KS.PizzaEmpire.Business.StorageInformation;
using KS.PizzaEmpire.Business.TableStorage;
using KS.PizzaEmpire.GameLogic.ItemLogic;
using KS.PizzaEmpire.Services.Storage;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace GameLogic.Items
{
    /// <summary>
    /// Represents an item that handles the game logic for items
    /// </summary>
    public class ItemManager
    {
        private static volatile ItemManager instance;
        private static object syncRoot = new Object();

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<int, BuildableItem> BuildableItems;

        private ItemManager() { }

        /// <summary>
        /// Provides the Singleton instance of the RedisCache
        /// </summary>
        public static ItemManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new ItemManager();
                        }
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// Initializes the item manager.
        /// </summary>
        /// <returns></returns>
        public async Task Initialize()
        {
            // @TODO need to decide on how to get item definitions
            // into table storage in the first place
            await StoreItemDefintiions();
            await LoadItemDefinitions();
        }

        /// <summary>
        /// Load the BuildableItems
        /// </summary>
        public async Task LoadBuildableItems()
        {
            BuildableItems = new Dictionary<int, BuildableItem>();

            AzureTableStorage storage = new AzureTableStorage();
            await storage.SetTable("BuildableItem");

            IEnumerable<BuildableItemTableStorage> items = 
                await storage.GetAll<BuildableItemTableStorage>(
                    "Version" + Constants.APPLICATION_VERSION);

            foreach(BuildableItemTableStorage item in items)
            {
                BuildableItems[item.ItemCode] = 
                    (BuildableItem)item.ToLogicEntity();
            }

            await storage.SetTable("Recipe");

            IEnumerable<RecipeTableStorage> recipes =
                await storage.GetAll<RecipeTableStorage>(
                    "Version" + Constants.APPLICATION_VERSION);

            foreach (RecipeTableStorage recipe in recipes)
            {
                if (BuildableItems.ContainsKey(recipe.ItemCode))
                {
                    BuildableItem bi = BuildableItems[recipe.ItemCode];
                    bi.Recipe = (Recipe)recipe.ToLogicEntity();
                }
            }
        }

        /// <summary>
        /// Load the item definitions 
        /// </summary>
        public async Task LoadItemDefinitions()
        {
            await LoadBuildableItems();
            //LoadRecipes();
        }

        /// <summary>
        /// A way to get the items into the table storage for now.
        /// @ TO DO Figure out how we actually want to do this
        /// </summary>
        public async Task StoreBuildableItems()
        {
            List<BuildableItemTableStorage> buildableItems = new List<BuildableItemTableStorage>();
            BuildableItem bi;

            foreach(var en in Enum.GetValues(typeof(BuildableItemEnum)).Cast<BuildableItemEnum>())
            {
                bi = new BuildableItem
                {
                    ItemCode = (int)en,
                    BuildSeconds = 120,
                    CoinCost = 50,
                    CouponCost = 0,
                    SpeedUpCoupons = 1,
                    Name = en.ToString().Replace("_", " ")
                };
                bi.StorageInformation = new BuildableItemStorageInformation(bi.ItemCode.ToString());
                buildableItems.Add((BuildableItemTableStorage)bi.ToTableStorageEntity());          
            }

            AzureTableStorage storage = new AzureTableStorage();
            await storage.SetTable("BuildableItem");
            await storage.DeleteTable();
            await storage.SetTable("BuildableItem");
            await storage.InsertOrReplace<BuildableItemTableStorage>(buildableItems);
        }

        /// <summary>
        /// A way to get the items into the table storage for now.
        /// @ TO DO Figure out how we actually want to do this
        /// </summary>
        public async Task StoreRecipes()
        {
            List<RecipeTableStorage> recs = new List<RecipeTableStorage>();
            Recipe re;
            ItemQuantity iq;

            re = new Recipe
            {                
                ItemCode = (int)BuildableItemEnum.White_Pizza_Dough,
                EquipmentCode = (int)EquipmentEnum.Dough_Mixer,  
                Ingredients = new List<ItemQuantity>
                {
                    new ItemQuantity
                    {
                        ItemCode = (int)BuildableItemEnum.White_Flour,
                        Quantity = 1
                    }
                }
            };
            re.StorageInformation = new RecipeStorageInformation(re.ItemCode.ToString());
            recs.Add((RecipeTableStorage)re.ToTableStorageEntity());

            AzureTableStorage storage = new AzureTableStorage();
            await storage.SetTable("Recipe");
            await storage.DeleteTable();
            await storage.SetTable("Recipe");
            await storage.InsertOrReplace<RecipeTableStorage>(recs);
        }

        /// <summary>
        /// A way to get the items into the table storage for now.
        /// @ TO DO Figure out how we actually want to do this
        /// </summary>
        public async Task StoreItemDefintiions()
        {
            await StoreBuildableItems();
            await StoreRecipes();      
        }
    }
}
