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

namespace GameLogic.ExperienceLevelLogic
{
    /// <summary>
    /// Represents an item that handles the game logic for items
    /// </summary>
    public class ExperienceLevelManager
    {
        private static volatile ExperienceLevelManager instance;
        private static object syncRoot = new Object();

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<int, ExperienceLevel> ExperienceLevels;

        private ExperienceLevelManager() { }

        /// <summary>
        /// Provides the Singleton instance of the RedisCache
        /// </summary>
        public static ExperienceLevelManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new ExperienceLevelManager();
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
            await StoreExperienceLevelDefintiions();
            await LoadExperienceLevelDefinitions();
        }

        /// <summary>
        /// Load the item definitions 
        /// </summary>
        public async Task LoadExperienceLevelDefinitions()
        {
            ExperienceLevels = new Dictionary<int, ExperienceLevel>();

            AzureTableStorage storage = new AzureTableStorage();
            await storage.SetTable("ExperienceLevel");

            IEnumerable<ExperienceLevelTableStorage> items =
                await storage.GetAll<ExperienceLevelTableStorage>(
                    "Version" + Constants.APPLICATION_VERSION);

            foreach (ExperienceLevelTableStorage item in items)
            {
                ExperienceLevels[item.Level] =
                    (ExperienceLevel)item.ToLogicEntity();
            }
        }

        /// <summary>
        /// A way to get the items into the table storage for now.
        /// @ TO DO Figure out how we actually want to do this
        /// </summary>
        public async Task StoreExperienceLevelDefintiions()
        {
            List<ExperienceLevelTableStorage> exls = new List<ExperienceLevelTableStorage>();
            ExperienceLevel exl;

            exl = new ExperienceLevel
            {
                Level = 1,
                ExperienceRequired = 0,
                NewBuildableItems = new List<int>
                {
                    (int)BuildableItemEnum.White_Flour, 
                    (int)BuildableItemEnum.Salt, 
                    (int)BuildableItemEnum.Yeast, 
                    (int)BuildableItemEnum.Olive_Oil
                },
                NewEquipment = new List<int>
                {
                    (int)EquipmentEnum.Fridge,
                    (int)EquipmentEnum.Phone,
                    (int)EquipmentEnum.Delivery_Truck,
                }
            };
            exl.StorageInformation = new ExperienceLevelStorageInformation(exl.Level.ToString());
            exls.Add((ExperienceLevelTableStorage)exl.ToTableStorageEntity());

            AzureTableStorage storage = new AzureTableStorage();
            await storage.SetTable("ExperienceLevel");
            await storage.DeleteTable();
            await storage.SetTable("ExperienceLevel");
            await storage.InsertOrReplace<ExperienceLevelTableStorage>(exls);
        }
    }
}
