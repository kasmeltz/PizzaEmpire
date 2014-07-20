using KS.PizzaEmpire.Business.Logic;
using KS.PizzaEmpire.Business.Result;
using KS.PizzaEmpire.Business.StorageInformation;
using KS.PizzaEmpire.Business.TableStorage;
using KS.PizzaEmpire.GameLogic.ItemLogic;
using KS.PizzaEmpire.Services.Storage;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

                            // @TODO need to decide on how to get item definitions
                            // into table storage in the first place
                            instance.BuildItemDefintiions().Wait();
                            instance.LoadItemDefinitions().Wait();
                        }
                    }
                }
                return instance;
            }
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
        public async Task BuildItemDefintiions()
        {
            List<BuildableItemTableStorage> buildableItems = new List<BuildableItemTableStorage>();

            BuildableItem bi = new BuildableItem
            {
                ItemCode = (int)BuildableItemEnum.White_Flour,
                BuildSeconds = 120,
                CoinCost = 50,
                CouponCost = 0,
                SpeedUpCoupons = 1,
                Name = "White Flour",
            };
            bi.StorageInformation = new BuildableItemStorageInformation(bi.ItemCode.ToString());

            buildableItems.Add((BuildableItemTableStorage)bi.ToTableStorageEntity());

            AzureTableStorage storage = new AzureTableStorage();            
            await storage.SetTable("BuildableItem");

            /*
            await storage.DeleteTable();
            await storage.SetTable("BuildableItem");
            await storage.InsertOrReplace<BuildableItemTableStorage>(buildableItems);         
            */
        }
    }
}
