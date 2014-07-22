namespace KS.PizzaEmpire.GameLogic.ItemLogic
{
    using Business.Common;
    using Business.Logic;
    using Business.StorageInformation;
    using Business.TableStorage;
    using Services.Storage;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

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
        public Dictionary<BuildableItemEnum, BuildableItem> BuildableItems;

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
            BuildableItems = new Dictionary<BuildableItemEnum, BuildableItem>();

            AzureTableStorage storage = new AzureTableStorage();
            await storage.SetTable("BuildableItem");

            IEnumerable<BuildableItemTableStorage> items = 
                await storage.GetAll<BuildableItemTableStorage>(
                    "Version" + Constants.APPLICATION_VERSION);

            foreach(BuildableItemTableStorage item in items)
            {
                BuildableItems[(BuildableItemEnum)item.ItemCode] = 
                    (BuildableItem)item.ToLogicEntity();
            }
        }
      
        /// <summary>
        /// Load the item definitions 
        /// </summary>
        public async Task LoadItemDefinitions()
        {
            await LoadBuildableItems();            
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
                    ItemCode = en,
                    RequiredLevel = 1,
                    CoinCost = 1,
                    Capacity = 1,
                    BaseProduction = 1,
                    ProductionMultiplier = 1,
                    Experience = 1,
                    BuildSeconds = 1,
                    CouponCost = 1,
                    SpeedUpCoupons = 1,
                    SpeedUpSeconds = 1,
                    RequiredItems = new List<ItemQuantity>()
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

        /*

        /// <summary>
        /// A way to get the items into the table storage for now.
        /// @ TO DO Figure out how we actually want to do this
        /// </summary>
        public async Task StoreRecipes()
        {
            List<RecipeTableStorage> recs = new List<RecipeTableStorage>();
            RequiredItem re;

            re = new RequiredItem
            {
                ItemCode = (int)BuildableItemEnum.Basil,
                EquipmentCode = (int)EquipmentEnum.Delivery_Truck
            };
            re.StorageInformation = new RecipeStorageInformation(re.ItemCode.ToString());
            recs.Add((RecipeTableStorage)re.ToTableStorageEntity());

            re = new RequiredItem
            {
                ItemCode = (int)BuildableItemEnum.Citrus_Syrup,
                EquipmentCode = (int)EquipmentEnum.Delivery_Truck
            };
            re.StorageInformation = new RecipeStorageInformation(re.ItemCode.ToString());
            recs.Add((RecipeTableStorage)re.ToTableStorageEntity());

            re = new RequiredItem
            {
                ItemCode = (int)BuildableItemEnum.Cola_Syrup,
                EquipmentCode = (int)EquipmentEnum.Delivery_Truck
            };
            re.StorageInformation = new RecipeStorageInformation(re.ItemCode.ToString());
            recs.Add((RecipeTableStorage)re.ToTableStorageEntity());

            re = new RequiredItem
            {
                ItemCode = (int)BuildableItemEnum.Ham,
                EquipmentCode = (int)EquipmentEnum.Delivery_Truck
            };
            re.StorageInformation = new RecipeStorageInformation(re.ItemCode.ToString());
            recs.Add((RecipeTableStorage)re.ToTableStorageEntity());

            re = new RequiredItem
            {
                ItemCode = (int)BuildableItemEnum.Olive_Oil,
                EquipmentCode = (int)EquipmentEnum.Delivery_Truck
            };
            re.StorageInformation = new RecipeStorageInformation(re.ItemCode.ToString());
            recs.Add((RecipeTableStorage)re.ToTableStorageEntity());

            re = new RequiredItem
            {
                ItemCode = (int)BuildableItemEnum.Pepper,
                EquipmentCode = (int)EquipmentEnum.Delivery_Truck
            };
            re.StorageInformation = new RecipeStorageInformation(re.ItemCode.ToString());
            recs.Add((RecipeTableStorage)re.ToTableStorageEntity());

            re = new RequiredItem
            {
                ItemCode = (int)BuildableItemEnum.Pepperoni,
                EquipmentCode = (int)EquipmentEnum.Delivery_Truck
            };
            re.StorageInformation = new RecipeStorageInformation(re.ItemCode.ToString());
            recs.Add((RecipeTableStorage)re.ToTableStorageEntity());

            re = new RequiredItem
            {
                ItemCode = (int)BuildableItemEnum.Pineapple,
                EquipmentCode = (int)EquipmentEnum.Delivery_Truck
            };
            re.StorageInformation = new RecipeStorageInformation(re.ItemCode.ToString());
            recs.Add((RecipeTableStorage)re.ToTableStorageEntity());

            re = new RequiredItem
            {
                ItemCode = (int)BuildableItemEnum.Salt,
                EquipmentCode = (int)EquipmentEnum.Delivery_Truck
            };
            re.StorageInformation = new RecipeStorageInformation(re.ItemCode.ToString());
            recs.Add((RecipeTableStorage)re.ToTableStorageEntity());

            re = new RequiredItem
            {
                ItemCode = (int)BuildableItemEnum.Tomatoes,
                EquipmentCode = (int)EquipmentEnum.Delivery_Truck
            };
            re.StorageInformation = new RecipeStorageInformation(re.ItemCode.ToString());
            recs.Add((RecipeTableStorage)re.ToTableStorageEntity());
          
            re = new RequiredItem
            {
                ItemCode = (int)BuildableItemEnum.White_Flour,
                EquipmentCode = (int)EquipmentEnum.Delivery_Truck
            };
            re.StorageInformation = new RecipeStorageInformation(re.ItemCode.ToString());
            recs.Add((RecipeTableStorage)re.ToTableStorageEntity());

            re = new RequiredItem
            {
                ItemCode = (int)BuildableItemEnum.Yeast,
                EquipmentCode = (int)EquipmentEnum.Delivery_Truck
            };
            re.StorageInformation = new RecipeStorageInformation(re.ItemCode.ToString());
            recs.Add((RecipeTableStorage)re.ToTableStorageEntity());

            re = new RequiredItem
            {                
                ItemCode = (int)BuildableItemEnum.Grated_Mozzarella_Cheese,
                EquipmentCode = (int)EquipmentEnum.Cheese_Grater,  
                Ingredients = new List<ItemQuantity>
                {
                    new ItemQuantity
                    {
                        ItemCode = (int)BuildableItemEnum.Mozzarella_Cheese,
                        Quantity = 1
                    }
                }
            };
            re.StorageInformation = new RecipeStorageInformation(re.ItemCode.ToString());
            recs.Add((RecipeTableStorage)re.ToTableStorageEntity());

            re = new RequiredItem
            {
                ItemCode = (int)BuildableItemEnum.White_Pizza_Dough,
                EquipmentCode = (int)EquipmentEnum.Dough_Mixer,
                Ingredients = new List<ItemQuantity>
                {
                    new ItemQuantity
                    {
                        ItemCode = (int)BuildableItemEnum.White_Flour,
                        Quantity = 1
                    },
                    new ItemQuantity
                    {
                        ItemCode = (int)BuildableItemEnum.Salt,
                        Quantity = 1
                    },
                    new ItemQuantity
                    {
                        ItemCode = (int)BuildableItemEnum.Olive_Oil,
                        Quantity = 1
                    },
                    new ItemQuantity
                    {
                        ItemCode = (int)BuildableItemEnum.Yeast,
                        Quantity = 1
                    }
                }
            };
            re.StorageInformation = new RecipeStorageInformation(re.ItemCode.ToString());
            recs.Add((RecipeTableStorage)re.ToTableStorageEntity());

            re = new RequiredItem
            {
                ItemCode = (int)BuildableItemEnum.Cola_Soda,
                EquipmentCode = (int)EquipmentEnum.Soda_Machine,
                Ingredients = new List<ItemQuantity>
                {
                    new ItemQuantity
                    {
                        ItemCode = (int)BuildableItemEnum.Cola_Syrup,
                        Quantity = 1
                    }
                }
            };
            re.StorageInformation = new RecipeStorageInformation(re.ItemCode.ToString());
            recs.Add((RecipeTableStorage)re.ToTableStorageEntity());

            re = new RequiredItem
            {
                ItemCode = (int)BuildableItemEnum.Citrus_Soda,
                EquipmentCode = (int)EquipmentEnum.Soda_Machine,
                Ingredients = new List<ItemQuantity>
                {
                    new ItemQuantity
                    {
                        ItemCode = (int)BuildableItemEnum.Citrus_Syrup,
                        Quantity = 1
                    }
                }
            };
            re.StorageInformation = new RecipeStorageInformation(re.ItemCode.ToString());
            recs.Add((RecipeTableStorage)re.ToTableStorageEntity());

            re = new RequiredItem
            {
                ItemCode = (int)BuildableItemEnum.Sliced_Pepperoni,
                EquipmentCode = (int)EquipmentEnum.Meat_Slicer,
                Ingredients = new List<ItemQuantity>
                {
                    new ItemQuantity
                    {
                        ItemCode = (int)BuildableItemEnum.Pepperoni,
                        Quantity = 1
                    }
                }
            };
            re.StorageInformation = new RecipeStorageInformation(re.ItemCode.ToString());
            recs.Add((RecipeTableStorage)re.ToTableStorageEntity());

            re = new RequiredItem
            {
                ItemCode = (int)BuildableItemEnum.Sliced_Ham,
                EquipmentCode = (int)EquipmentEnum.Meat_Slicer,
                Ingredients = new List<ItemQuantity>
                {
                    new ItemQuantity
                    {
                        ItemCode = (int)BuildableItemEnum.Ham,
                        Quantity = 1
                    }
                }
            };
            re.StorageInformation = new RecipeStorageInformation(re.ItemCode.ToString());
            recs.Add((RecipeTableStorage)re.ToTableStorageEntity());

            re = new RequiredItem
            {
                ItemCode = (int)BuildableItemEnum.Basic_Pizza_Sauce,
                EquipmentCode = (int)EquipmentEnum.Cooking_Range,
                Ingredients = new List<ItemQuantity>
                {
                    new ItemQuantity
                    {
                        ItemCode = (int)BuildableItemEnum.Tomatoes,
                        Quantity = 1
                    },
                    new ItemQuantity
                    {
                        ItemCode = (int)BuildableItemEnum.Basil,
                        Quantity = 1
                    },
                    new ItemQuantity
                    {
                        ItemCode = (int)BuildableItemEnum.Pepper,
                        Quantity = 1
                    }
                }
            };
            re.StorageInformation = new RecipeStorageInformation(re.ItemCode.ToString());
            recs.Add((RecipeTableStorage)re.ToTableStorageEntity());

            re = new RequiredItem
            {
                ItemCode = (int)BuildableItemEnum.Sliced_Pineapple,
                EquipmentCode = (int)EquipmentEnum.Vegetable_Slicer,
                Ingredients = new List<ItemQuantity>
                {
                    new ItemQuantity
                    {
                        ItemCode = (int)BuildableItemEnum.Pineapple,
                        Quantity = 1
                    }
                }
            };
            re.StorageInformation = new RecipeStorageInformation(re.ItemCode.ToString());
            recs.Add((RecipeTableStorage)re.ToTableStorageEntity());

            re = new RequiredItem
            {
                ItemCode = (int)BuildableItemEnum.Cheese_Pizza_S,
                EquipmentCode = (int)EquipmentEnum.Pizza_Oven,
                Ingredients = new List<ItemQuantity>
                {
                    new ItemQuantity
                    {
                        ItemCode = (int)BuildableItemEnum.White_Pizza_Dough,
                        Quantity = 1
                    },
                    new ItemQuantity
                    {
                        ItemCode = (int)BuildableItemEnum.Basic_Pizza_Sauce,
                        Quantity = 1
                    },
                    new ItemQuantity
                    {
                        ItemCode = (int)BuildableItemEnum.Grated_Mozzarella_Cheese,
                        Quantity = 1
                    },
                    new ItemQuantity
                    {
                        ItemCode = (int)BuildableItemEnum.Pizza_Box_S,
                        Quantity = 1
                    }
                }
            };
            re.StorageInformation = new RecipeStorageInformation(re.ItemCode.ToString());
            recs.Add((RecipeTableStorage)re.ToTableStorageEntity());

            re = new RequiredItem
            {
                ItemCode = (int)BuildableItemEnum.Cheese_Pizza_M,
                EquipmentCode = (int)EquipmentEnum.Pizza_Oven,
                Ingredients = new List<ItemQuantity>
                {
                    new ItemQuantity
                    {
                        ItemCode = (int)BuildableItemEnum.White_Pizza_Dough,
                        Quantity = 1
                    },
                    new ItemQuantity
                    {
                        ItemCode = (int)BuildableItemEnum.Basic_Pizza_Sauce,
                        Quantity = 1
                    },
                    new ItemQuantity
                    {
                        ItemCode = (int)BuildableItemEnum.Grated_Mozzarella_Cheese,
                        Quantity = 1
                    },
                    new ItemQuantity
                    {
                        ItemCode = (int)BuildableItemEnum.Pizza_Box_M,
                        Quantity = 1
                    }
                }
            };
            re.StorageInformation = new RecipeStorageInformation(re.ItemCode.ToString());
            recs.Add((RecipeTableStorage)re.ToTableStorageEntity());

            re = new RequiredItem
            {
                ItemCode = (int)BuildableItemEnum.Cheese_Pizza_L,
                EquipmentCode = (int)EquipmentEnum.Pizza_Oven,
                Ingredients = new List<ItemQuantity>
                {
                    new ItemQuantity
                    {
                        ItemCode = (int)BuildableItemEnum.White_Pizza_Dough,
                        Quantity = 1
                    },
                    new ItemQuantity
                    {
                        ItemCode = (int)BuildableItemEnum.Basic_Pizza_Sauce,
                        Quantity = 1
                    },
                    new ItemQuantity
                    {
                        ItemCode = (int)BuildableItemEnum.Grated_Mozzarella_Cheese,
                        Quantity = 1
                    },
                    new ItemQuantity
                    {
                        ItemCode = (int)BuildableItemEnum.Pizza_Box_L,
                        Quantity = 1
                    }
                }
            };
            re.StorageInformation = new RecipeStorageInformation(re.ItemCode.ToString());
            recs.Add((RecipeTableStorage)re.ToTableStorageEntity());

            re = new RequiredItem
            {
                ItemCode = (int)BuildableItemEnum.Pepperoni_Pizza_S,
                EquipmentCode = (int)EquipmentEnum.Pizza_Oven,
                Ingredients = new List<ItemQuantity>
                {
                    new ItemQuantity
                    {
                        ItemCode = (int)BuildableItemEnum.White_Pizza_Dough,
                        Quantity = 1
                    },
                    new ItemQuantity
                    {
                        ItemCode = (int)BuildableItemEnum.Basic_Pizza_Sauce,
                        Quantity = 1
                    },
                    new ItemQuantity
                    {
                        ItemCode = (int)BuildableItemEnum.Grated_Mozzarella_Cheese,
                        Quantity = 1
                    },
                    new ItemQuantity
                    {
                        ItemCode = (int)BuildableItemEnum.Sliced_Pepperoni,
                        Quantity = 1
                    },
                    new ItemQuantity
                    {
                        ItemCode = (int)BuildableItemEnum.Pizza_Box_S,
                        Quantity = 1
                    }
                }
            };
            re.StorageInformation = new RecipeStorageInformation(re.ItemCode.ToString());
            recs.Add((RecipeTableStorage)re.ToTableStorageEntity());

            re = new RequiredItem
            {
                ItemCode = (int)BuildableItemEnum.Pepperoni_Pizza_M,
                EquipmentCode = (int)EquipmentEnum.Pizza_Oven,
                Ingredients = new List<ItemQuantity>
                {
                    new ItemQuantity
                    {
                        ItemCode = (int)BuildableItemEnum.White_Pizza_Dough,
                        Quantity = 1
                    },
                    new ItemQuantity
                    {
                        ItemCode = (int)BuildableItemEnum.Basic_Pizza_Sauce,
                        Quantity = 1
                    },
                    new ItemQuantity
                    {
                        ItemCode = (int)BuildableItemEnum.Grated_Mozzarella_Cheese,
                        Quantity = 1
                    },
                    new ItemQuantity
                    {
                        ItemCode = (int)BuildableItemEnum.Sliced_Pepperoni,
                        Quantity = 1
                    },
                    new ItemQuantity
                    {
                        ItemCode = (int)BuildableItemEnum.Pizza_Box_M,
                        Quantity = 1
                    }
                }
            };
            re.StorageInformation = new RecipeStorageInformation(re.ItemCode.ToString());
            recs.Add((RecipeTableStorage)re.ToTableStorageEntity());

            re = new RequiredItem
            {
                ItemCode = (int)BuildableItemEnum.Pepperoni_Pizza_L,
                EquipmentCode = (int)EquipmentEnum.Pizza_Oven,
                Ingredients = new List<ItemQuantity>
                {
                    new ItemQuantity
                    {
                        ItemCode = (int)BuildableItemEnum.White_Pizza_Dough,
                        Quantity = 1
                    },
                    new ItemQuantity
                    {
                        ItemCode = (int)BuildableItemEnum.Basic_Pizza_Sauce,
                        Quantity = 1
                    },
                    new ItemQuantity
                    {
                        ItemCode = (int)BuildableItemEnum.Grated_Mozzarella_Cheese,
                        Quantity = 1
                    },
                    new ItemQuantity
                    {
                        ItemCode = (int)BuildableItemEnum.Sliced_Pepperoni,
                        Quantity = 1
                    },
                    new ItemQuantity
                    {
                        ItemCode = (int)BuildableItemEnum.Pizza_Box_L,
                        Quantity = 1
                    }
                }
            };
            re.StorageInformation = new RecipeStorageInformation(re.ItemCode.ToString());
            recs.Add((RecipeTableStorage)re.ToTableStorageEntity());

            re = new RequiredItem
            {
                ItemCode = (int)BuildableItemEnum.Hawaiin_Pizza_S,
                EquipmentCode = (int)EquipmentEnum.Pizza_Oven,
                Ingredients = new List<ItemQuantity>
                {
                    new ItemQuantity
                    {
                        ItemCode = (int)BuildableItemEnum.White_Pizza_Dough,
                        Quantity = 1
                    },
                    new ItemQuantity
                    {
                        ItemCode = (int)BuildableItemEnum.Basic_Pizza_Sauce,
                        Quantity = 1
                    },
                    new ItemQuantity
                    {
                        ItemCode = (int)BuildableItemEnum.Grated_Mozzarella_Cheese,
                        Quantity = 1
                    },
                    new ItemQuantity
                    {
                        ItemCode = (int)BuildableItemEnum.Sliced_Ham,
                        Quantity = 1
                    },
                    new ItemQuantity
                    {
                        ItemCode = (int)BuildableItemEnum.Sliced_Pineapple,
                        Quantity = 1
                    },
                    new ItemQuantity
                    {
                        ItemCode = (int)BuildableItemEnum.Pizza_Box_S,
                        Quantity = 1
                    }
                }
            };
            re.StorageInformation = new RecipeStorageInformation(re.ItemCode.ToString());
            recs.Add((RecipeTableStorage)re.ToTableStorageEntity());

            re = new RequiredItem
            {
                ItemCode = (int)BuildableItemEnum.Hawaiin_Pizza_M,
                EquipmentCode = (int)EquipmentEnum.Pizza_Oven,
                Ingredients = new List<ItemQuantity>
                {
                    new ItemQuantity
                    {
                        ItemCode = (int)BuildableItemEnum.White_Pizza_Dough,
                        Quantity = 1
                    },
                    new ItemQuantity
                    {
                        ItemCode = (int)BuildableItemEnum.Basic_Pizza_Sauce,
                        Quantity = 1
                    },
                    new ItemQuantity
                    {
                        ItemCode = (int)BuildableItemEnum.Grated_Mozzarella_Cheese,
                        Quantity = 1
                    },
                    new ItemQuantity
                    {
                        ItemCode = (int)BuildableItemEnum.Sliced_Ham,
                        Quantity = 1
                    },
                    new ItemQuantity
                    {
                        ItemCode = (int)BuildableItemEnum.Sliced_Pineapple,
                        Quantity = 1
                    },
                    new ItemQuantity
                    {
                        ItemCode = (int)BuildableItemEnum.Pizza_Box_M,
                        Quantity = 1
                    }
                }
            };
            re.StorageInformation = new RecipeStorageInformation(re.ItemCode.ToString());
            recs.Add((RecipeTableStorage)re.ToTableStorageEntity());

            re = new RequiredItem
            {
                ItemCode = (int)BuildableItemEnum.Hawaiin_Pizza_L,
                EquipmentCode = (int)EquipmentEnum.Pizza_Oven,
                Ingredients = new List<ItemQuantity>
                {
                    new ItemQuantity
                    {
                        ItemCode = (int)BuildableItemEnum.White_Pizza_Dough,
                        Quantity = 1
                    },
                    new ItemQuantity
                    {
                        ItemCode = (int)BuildableItemEnum.Basic_Pizza_Sauce,
                        Quantity = 1
                    },
                    new ItemQuantity
                    {
                        ItemCode = (int)BuildableItemEnum.Grated_Mozzarella_Cheese,
                        Quantity = 1
                    },
                    new ItemQuantity
                    {
                        ItemCode = (int)BuildableItemEnum.Sliced_Ham,
                        Quantity = 1
                    },
                    new ItemQuantity
                    {
                        ItemCode = (int)BuildableItemEnum.Sliced_Pineapple,
                        Quantity = 1
                    },
                    new ItemQuantity
                    {
                        ItemCode = (int)BuildableItemEnum.Pizza_Box_L,
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
         * */

        /// <summary>
        /// A way to get the items into the table storage for now.
        /// @ TO DO Figure out how we actually want to do this
        /// </summary>
        public async Task StoreItemDefintiions()
        {
            await StoreBuildableItems();    
        }
    }
}
