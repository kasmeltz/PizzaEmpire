using KS.PizzaEmpire.Business.Logic;
using KS.PizzaEmpire.Business.Result;
using KS.PizzaEmpire.Business.TableStorage;
using KS.PizzaEmpire.Services.Storage;
using System;
using System.Collections.Generic;

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
                            instance.LoadItemDefinitions();
                        }
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// Load the BuildableItems
        /// </summary>
        public async void LoadBuildableItems()
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
        public void LoadItemDefinitions()
        {
            LoadBuildableItems();            
        }
    }
}
