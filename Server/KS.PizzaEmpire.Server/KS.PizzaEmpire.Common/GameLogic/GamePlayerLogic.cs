namespace KS.PizzaEmpire.Common.GameLogic
{
    using Common.BusinessObjects;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    /// <summary>
    /// Methods that represent the game logic around the game player
    /// </summary>
    public class GamePlayerLogic
    {
        private static volatile GamePlayerLogic instance;
        private static object syncRoot = new object();

        private GamePlayerLogic() { }

        /// <summary>
        /// Provides the Singleton instance of the GamePlayerLogic
        /// </summary>
        public static GamePlayerLogic Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new GamePlayerLogic();
                        }
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// The experience levels in the game
        /// </summary>
        public Dictionary<int, ExperienceLevel> ExperienceLevels { get; set; }

        /// <summary>
        /// The buildable items in the game
        /// </summary>
        public Dictionary<BuildableItemEnum, BuildableItem> BuildableItems { get; set; }

        /// <summary>
        /// Creates a brand new game player - e.g. a Game Player who is logging in for
        /// the first time with no persistant state
        /// </summary>
        /// <returns></returns>
        public GamePlayer CreateNewGamePlayer()
        {
            GamePlayer player = new GamePlayer();
            player.Coins = 1000;
            player.Coupons = 5;
            player.Experience = 0;

            player.BuildableItems = new Dictionary<BuildableItemEnum, int>();
            player.BuildableItems[BuildableItemEnum.Dry_Goods_Delivery_Truck_L1] = 1;
            player.BuildableItems[BuildableItemEnum.Restaurant_Storage] = 1;
            player.BuildableItems[BuildableItemEnum.Dirty_Dishes] = 1;
            player.BuildableItems[BuildableItemEnum.Dirty_Table] = 1;
            player.BuildableItems[BuildableItemEnum.Dirty_Floor] = 1;

            player.WorkItems = new List<WorkItem>();
            SetLevel(player, 1);

            return player;
        }

        /// <summary>
        /// Sets a new level for the player
        /// </summary>
        /// <param name="player"></param>
        /// <param name="level"></param>
        public void SetLevel(GamePlayer player, int level)
        {
            if (!ExperienceLevels.ContainsKey(level))
            {
                Trace.TraceError("Attempt to set a non existent player level: " + level);
                throw new ArgumentException();
            }

            player.Level = level;
            player.StateChanged = true;
        }

        /// <summary>
        /// Adds experience to a player
        /// </summary>
        /// <param name="player"></param>
        /// <param name="experience"></param>
        public void AddExperience(GamePlayer player, int experience)
        {
            player.Experience += experience;

            bool levelUp;
            do
            {
                levelUp = false;
                if (ExperienceLevels.ContainsKey(player.Level + 1))
                {
                    ExperienceLevel exl = ExperienceLevels[player.Level + 1];
                    if (player.Experience >= exl.ExperienceRequired)
                    {
                        SetLevel(player, player.Level + 1);
                        levelUp = true;
                    }
                }
            } while (levelUp);

            player.StateChanged = true;
        }

        /// <summary>
        /// Returns true if the player has the capacity to start hhe item.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public bool DoesPlayerHaveProductionCapacity(GamePlayer player, BuildableItemEnum itemCode)
        {
            BuildableItem bi = BuildableItems[itemCode];

            if (bi.ProductionItem == BuildableItemEnum.None)
            {
                return true;
            }

            BuildableItem capacityItem = BuildableItems[bi.ProductionItem];

            if (!player.BuildableItems.ContainsKey(capacityItem.ItemCode) || 
                player.BuildableItems[capacityItem.ItemCode] == 0)
            {
                return false;
            }

            int inUse = 0;
            foreach (WorkItem wi in player.WorkItems)
            {
                BuildableItem wbi = BuildableItems[wi.ItemCode];
                if (wbi.ProductionItem == bi.ProductionItem)
                {
                    inUse++;
                }
            }

            return inUse < capacityItem.ProductionCapacity;
        }
        
        /// <summary>
        /// Returns true if the player has storage capacity for the item, false otherwise
        /// </summary>
        /// <param name="player"></param>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public bool DoesPlayeraHaveStorageCapacity(GamePlayer player, BuildableItemEnum itemCode)
        {
            BuildableItem bi = BuildableItems[itemCode];

            if (bi.StorageItem == BuildableItemEnum.None)
            {
                return true;
            }

            BuildableItem storageItem = BuildableItems[bi.StorageItem];

            int itemsInInventory = 0;
            foreach (KeyValuePair<BuildableItemEnum, int> kvp in player.BuildableItems)
            {
                BuildableItem si = BuildableItems[kvp.Key];
                if (si.StorageItem == bi.StorageItem)
                {
                    itemsInInventory += kvp.Value;
                }
            }

            return itemsInInventory + bi.BaseProduction < storageItem.StorageCapacity;
        }
        
        /// <summary>
        /// Returns ErrorCode.OK if the player can build the item,
        /// an error code otherwise
        /// </summary>
        /// <param name="player"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public ErrorCode CanBuildItem(GamePlayer player, BuildableItemEnum itemCode)
        {
            if (!BuildableItems.ContainsKey(itemCode))
            {
                return ErrorCode.START_WORK_INVALID_ITEM;
            }

            BuildableItem bi = BuildableItems[itemCode];

            if (player.Level < bi.RequiredLevel)
            {
                return ErrorCode.START_WORK_ITEM_NOT_AVAILABLE;
            }

            if (bi.CoinCost > player.Coins)
            {
                return ErrorCode.START_WORK_INSUFFICIENT_COINS;
            }

            if (bi.CouponCost > player.Coupons)
            {
                return ErrorCode.START_WORK_INSUFFICIENT_COUPONS;
            }

            if (bi.RequiredItems != null)
            {
                foreach (ItemQuantity itemQuantity in bi.RequiredItems)
                {
                    if (!player.BuildableItems.ContainsKey(itemQuantity.ItemCode))
                    {
                        return ErrorCode.START_WORK_INVALID_INGREDIENTS;
                    }

                    if (player.BuildableItems[itemQuantity.ItemCode] < itemQuantity.Quantity)
                    {
                        return ErrorCode.START_WORK_INSUFFICIENT_INGREDIENTS;
                    }
                }
            }

            if (!DoesPlayerHaveProductionCapacity(player, itemCode))
            {
                return ErrorCode.START_WORK_NO_PRODUCTION_CAPACITY;
            }

            if (!DoesPlayeraHaveStorageCapacity(player, itemCode))
            {
                return ErrorCode.START_WORK_NO_STORAGE_CAPACITY;
            }

            if (bi.IsWorkSubtracted)
            {
                if (!player.BuildableItems.ContainsKey(itemCode) ||
                    player.BuildableItems[itemCode] < bi.BaseProduction)
                {
                    return ErrorCode.START_WORK_INSUFFICIENT_NEGATIVE;
                }
            }

            if (!bi.IsConsumable)
            {
                if (player.BuildableItems.ContainsKey(itemCode))
                {
                    return ErrorCode.START_WORK_MULTIPLE_NON_CONSUMABLE;
                }
            }

            return ErrorCode.ERROR_OK;
        }

        /// <summary>
        /// Returns true if the player can build the item, throws an exception otherwise
        /// </summary>
        /// <returns></returns>
        public bool TryBuildItem(GamePlayer player, BuildableItemEnum itemCode)
        {
            ErrorCode error = CanBuildItem(player, itemCode);
            if (error != ErrorCode.ERROR_OK)
            {
                Trace.TraceError(error.ToString());
                throw new ArgumentException(error.ToString());
            }

            return true;
        }

        /// <summary>
        ///  Deducts the resources required to build an item from the player
        /// </summary>
        /// <param name="player"></param>
        /// <param name="itemCode"></param>
        public void DeductResources(GamePlayer player, BuildableItemEnum itemCode)
        {
            BuildableItem bi = BuildableItems[itemCode];

            if (bi.RequiredItems != null)
            {
                foreach (ItemQuantity requiredItem in bi.RequiredItems)
                {
                    BuildableItem ri = BuildableItems[requiredItem.ItemCode];
                    if (ri.IsConsumable)
                    {
                        player.BuildableItems[requiredItem.ItemCode] -= requiredItem.Quantity;
                    }
                }
            }

            player.Coins -= bi.CoinCost;
            player.Coupons -= bi.CouponCost;

            player.StateChanged = true;
        }

        /// <summary>
        /// Starts a player doing delayed work if all conditions pass.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="code"></param>
        public WorkItem StartWork(GamePlayer player, BuildableItemEnum itemCode)
        {
            bool canDoWork = TryBuildItem(player, itemCode);

            if (!canDoWork)
            {
                return null;
            }

            BuildableItem bi = BuildableItems[itemCode];

            DeductResources(player, itemCode);

            WorkItem workItem = new WorkItem
            {
                ItemCode = itemCode,
                FinishTime = DateTime.UtcNow.AddSeconds(bi.BuildSeconds)
            };

            player.WorkItems.Add(workItem);

            if (bi.IsImmediate)
            {
                FinishWork(player, DateTime.UtcNow);
            }

            player.StateChanged = true;

            return workItem;
        }

        /// <summary>
        /// Adds an item to a player when work is completed
        /// </summary>
        /// <param name="player"></param>
        /// <param name="item"></param>
        public void AddItem(GamePlayer player, WorkItem item)
        {
            BuildableItem bi = BuildableItems[item.ItemCode];

            if (!player.BuildableItems.ContainsKey(item.ItemCode))
            {
                player.BuildableItems[item.ItemCode] = 0;
            }

            player.BuildableItems[item.ItemCode] += bi.BaseProduction;

            if (!bi.IsConsumable)
            {
                player.BuildableItems[item.ItemCode] = 1;
            }
        }

        /// <summary>
        /// Removes an item from a player when work is completed
        /// </summary>
        /// <param name="player"></param>
        /// <param name="item"></param>
        public void SubtractItem(GamePlayer player, WorkItem item)
        {
            BuildableItem bi = BuildableItems[item.ItemCode];

            if (!player.BuildableItems.ContainsKey(item.ItemCode))
            {
                Trace.WriteLine("Attempt to finished subtracted work for an item the player doesn't own");
                return;
            }

            if (player.BuildableItems[item.ItemCode] < bi.BaseProduction)
            {
                Trace.WriteLine("Attempt to finished subtracted work for an item the player doesn't have enough of");
                return;
            }

            player.BuildableItems[item.ItemCode] -= bi.BaseProduction;
        }


        /// <summary>
        /// Adds or removes an item to or from a player when work is completed
        /// </summary>
        /// <param name="player"></param>
        /// <param name="item"></param>
        public void AdjustInventory(GamePlayer player, WorkItem item)
        {
            BuildableItem bi = BuildableItems[item.ItemCode];

            if (bi.IsWorkSubtracted)
            {
                SubtractItem(player, item);
            }
            else
            {
                AddItem(player, item);
            }            

            AddExperience(player, bi.Experience);
        }

        /// <summary>
        /// Checks if a player is finished doing some work.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="itemCode"></param>
        public List<WorkItem> FinishWork(GamePlayer player, DateTime checkBefore)
        {
            List<WorkItem> finishedItems = new List<WorkItem>();

            if (player.WorkItems == null ||
                player.WorkItems.Count == 0)
            {
                Trace.TraceError("Attempt to finish work but no work started!");
                throw new ArgumentException();
            }

            foreach (WorkItem item in player.WorkItems)
            {
                if (item.FinishTime < checkBefore)
                {
                    finishedItems.Add(item);
                }
            }

            foreach (WorkItem item in finishedItems)
            {
                AdjustInventory(player, item);
                player.WorkItems.Remove(item);
            }

            player.StateChanged = true;
            return finishedItems;
        }

        /// <summary>
        /// Sets the tutorial stage for a player
        /// </summary>
        /// <param name="player"></param>
        /// <param name="stage"></param>
        public void SetTutorialStage(GamePlayer player, int stage)
        {
            // ignore out of order tutorial stage requests
            if (stage < player.TutorialStage && stage != 0)
            {
                return;
            }

            player.TutorialStage = stage;
            player.StateChanged = true;            
        }

        /// <summary>
        /// Gets the current work items being produced in 
        /// the supplied production item
        /// </summary>
        /// <param name="player"></param>
        /// <param name="stage"></param>
        public List<WorkItem> GetCurrentWorkItemsForProductionItem(GamePlayer player, BuildableItemEnum productionItem)
        {
            List<WorkItem> workItems = new List<WorkItem>();

            foreach (WorkItem item in player.WorkItems)
            {
                BuildableItem bi = BuildableItems[item.ItemCode];
                if (bi.ProductionItem == productionItem)
                {
                    workItems.Add(item);
                }
            }

            return workItems;
        }

        /// <summary>
        /// Gets the percentage of work complete for a given work item
        /// at the current time
        /// </summary>
        /// <param name="workItem"></param>
        /// <returns></returns>
        public double GetPercentageCompleteForWorkItem(WorkItem workItem)
        {
            BuildableItem bi = BuildableItems[workItem.ItemCode];
            double secondsToGo = workItem.FinishTime.Subtract(DateTime.UtcNow).TotalSeconds;
            double ratio = 1 - (secondsToGo / (double)bi.BuildSeconds);
            return Math.Min(1, Math.Max(ratio, 0));
        }
    }
}
