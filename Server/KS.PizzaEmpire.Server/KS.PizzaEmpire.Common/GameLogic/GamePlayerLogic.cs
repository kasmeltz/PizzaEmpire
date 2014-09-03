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

        #region Events

        /// <summary>
        /// An event that is fired when the player's level changes
        /// </summary>
        public event EventHandler<GamePlayerLogicEventArgs> LevelChanged;
        protected virtual void OnLevelChanged(GamePlayerLogicEventArgs e)
        {
            try
            {
                EventHandler<GamePlayerLogicEventArgs> handler = LevelChanged;
                if (handler != null)
                {
                    handler(this, e);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError("Exception in LevelChanged: " + ex.Message);
            }
        }

        /// <summary>
        /// An event that is fired when the player's experience changes
        /// </summary>
        public event EventHandler<GamePlayerLogicEventArgs> ExperienceChanged;
        protected virtual void OnExperienceChanged(GamePlayerLogicEventArgs e)
        {
            try
            {
                EventHandler<GamePlayerLogicEventArgs> handler = ExperienceChanged;
                if (handler != null)
                {
                    handler(this, e);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError("Exception in ExperienceChanged: " + ex.Message);
            }
        }

        /// <summary>
        /// An event that is fired when the player's coins change
        /// </summary>
        public event EventHandler<GamePlayerLogicEventArgs> CoinsChanged;
        protected virtual void OnCoinsChanged(GamePlayerLogicEventArgs e)
        {
            try
            {
                EventHandler<GamePlayerLogicEventArgs> handler = CoinsChanged;
                if (handler != null)
                {
                    handler(this, e);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError("Exception in CoinsChanged: " + ex.Message);
            }
        }

        /// <summary>
        /// An event that is fired when the player's coupons change
        /// </summary>
        public event EventHandler<GamePlayerLogicEventArgs> CouponsChanged;
        protected virtual void OnCouponsChanged(GamePlayerLogicEventArgs e)
        {
            try
            {
                EventHandler<GamePlayerLogicEventArgs> handler = CouponsChanged;
                if (handler != null)
                {
                    handler(this, e);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError("Exception in CouponsChanged: " + ex.Message);
            }
        }

        /// <summary>
        /// An event that is fired when an item is subtracted
        /// </summary>
        public event EventHandler<GamePlayerLogicEventArgs> ItemSubtracted;
        protected virtual void OnItemSubtracted(GamePlayerLogicEventArgs e)
        {
            try
            {
                EventHandler<GamePlayerLogicEventArgs> handler = ItemSubtracted;
                if (handler != null)
                {
                    handler(this, e);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError("Exception in ItemSubtracted: " + ex.Message);
            }
        }

        /// <summary>
        /// An event that is fired when an item is added to the player
        /// </summary>
        public event EventHandler<GamePlayerLogicEventArgs> ItemAdded;
        protected virtual void OnItemAdded(GamePlayerLogicEventArgs e)
        {
            try
            {
                EventHandler<GamePlayerLogicEventArgs> handler = ItemAdded;
                if (handler != null)
                {
                    handler(this, e);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError("Exception in ItemAdded: " + ex.Message);
            }
        }

        /// <summary>
        /// An event that is fired when the player's work is started
        /// </summary>
        public event EventHandler<GamePlayerLogicEventArgs> WorkStarted;
        protected virtual void OnWorkStarted(GamePlayerLogicEventArgs e)
        {
            try
            {
                EventHandler<GamePlayerLogicEventArgs> handler = WorkStarted;
                if (handler != null)
                {
                    handler(this, e);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError("Exception in WorkStarted: " + ex.Message);
            }
        }

        /// <summary>
        /// An event that is fired when the player's work is finished
        /// </summary>
        public event EventHandler<GamePlayerLogicEventArgs> WorkFinished;
        protected virtual void OnWorkFinished(GamePlayerLogicEventArgs e)
        {
            try
            {
                EventHandler<GamePlayerLogicEventArgs> handler = WorkFinished;
                if (handler != null)
                {
                    handler(this, e);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError("Exception in WorkFinished: " + ex.Message);
            }
        }

        #endregion

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

            player.Locations = new List<BusinessLocation>();

            AddNewLocation(player);

            player.WorkInProgress = new List<WorkInProgress>();
            SetLevel(player, 1);

            return player;
        }

        /// <summary>
        /// Adds a new location to a player
        /// </summary>
        /// <param name="player"></param>
        public void AddNewLocation(GamePlayer player)
        {
            BusinessLocation location = new BusinessLocation();
            LocationStorage storage = new LocationStorage();
            storage.Items = new Dictionary<BuildableItemEnum, ItemQuantity>();
            location.Storage = storage;
            player.Locations.Add(location);

            AddItem(player,
                0, new ItemQuantity 
                { 
                    ItemCode = BuildableItemEnum.Dry_Goods_Delivery_Truck, 
                    UnStoredQuantity = 1, 
                    StoredQuantity = 0,
                    Level = 0
                });

            AddItem(player,
                0, new ItemQuantity
                {
                    ItemCode = BuildableItemEnum.Vegetable_Farm_Delivery_Truck,
                    UnStoredQuantity = 1,
                    StoredQuantity = 0,
                    Level = 0
                });

            AddItem(player,
                0, new ItemQuantity
                {
                    ItemCode = BuildableItemEnum.Restaurant_Storage,
                    UnStoredQuantity = 1,
                    StoredQuantity = 0,
                    Level = 0
                });

            AddItem(player,
                0, new ItemQuantity
                {
                    ItemCode = BuildableItemEnum.Dirty_Table,
                    UnStoredQuantity = 1,
                    StoredQuantity = 0,
                    Level = 0
                });

            AddItem(player,
                0, new ItemQuantity
                {
                    ItemCode = BuildableItemEnum.Dirty_Dishes,
                    UnStoredQuantity = 1,
                    StoredQuantity = 0,
                    Level = 0
                });

            AddItem(player,
                0, new ItemQuantity
                {
                    ItemCode = BuildableItemEnum.Dirty_Floor,
                    UnStoredQuantity = 1,
                    StoredQuantity = 0,
                    Level = 0
                });
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

            if (LevelChanged != null)
            {
                OnLevelChanged(new GamePlayerLogicEventArgs(player, level));
            }

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

            if (ExperienceChanged != null)
            {
                OnExperienceChanged(new GamePlayerLogicEventArgs(player, experience));
            }

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
        public bool DoesPlayerHaveProductionCapacity(GamePlayer player, int location, int level, BuildableItemEnum itemCode)
        {
            ConsumableItem consumable = BuildableItems[itemCode] as ConsumableItem;
            if (consumable == null)
            {
                return true;
            }
            
            BusinessLocation businessLocation = player.Locations[location];
            LocationStorage storage = businessLocation.Storage;

            if (!storage.Items.ContainsKey(consumable.ProducedWith) ||
                storage.Items[consumable.ProducedWith].UnStoredQuantity == 0)
            {
                return false;
            }

            int inUse = 0;
            foreach (WorkInProgress wi in player.WorkInProgress)
            {
                ConsumableItem cip = BuildableItems[wi.Quantity.ItemCode] as ConsumableItem;
                if (cip == null)
                {
                    continue;
                }

                if (cip.ProducedWith == consumable.ProducedWith)
                {
                    inUse++;
                }
            }

            ProductionItem productionItem = BuildableItems[consumable.ProducedWith] as ProductionItem;
            int productionItemLevel = storage.Items[productionItem.ItemCode].Level;
            ProductionItemStat productionStat = productionItem.ProductionStats[productionItemLevel];

            return inUse < productionStat.Capacity;
        }
        
        /// <summary>
        /// Returns true if the player has storage capacity for the item, false otherwise
        /// </summary>
        /// <param name="player"></param>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public bool DoesPlayeraHaveStorageCapacity(GamePlayer player, int location, int level, BuildableItemEnum itemCode)
        {
            ConsumableItem consumable = BuildableItems[itemCode] as ConsumableItem;
            if (consumable == null)
            {
                return true;
            }           

            BusinessLocation businessLocation = player.Locations[location];
            LocationStorage storage = businessLocation.Storage;

            if (!storage.Items.ContainsKey(consumable.StoredIn))
            {
                return false;
            }

            int itemsInStorage = 0;
            foreach (ItemQuantity iq in storage.Items.Values)
            {
                ConsumableItem csi = BuildableItems[iq.ItemCode] as ConsumableItem;
                if (csi == null)
                {
                    continue;
                }                
                if (csi.StoredIn == consumable.StoredIn)
                {
                    itemsInStorage += iq.StoredQuantity;
                }                
            }

            StorageItem storageItem = BuildableItems[consumable.StoredIn] as StorageItem;
            int storageItemLevel = storage.Items[storageItem.ItemCode].Level;
            StorageItemStat storageStat = storageItem.StorageStats[storageItemLevel];

            return itemsInStorage +
                GetProductionQuantityForItem(player, location, level, itemCode) <
                    storageStat.Capacity;
        }

        /// <summary>
        /// Returns ErrorCode.OK if the player can build the item,
        /// an error code otherwise
        /// </summary>
        /// <param name="player"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public ErrorCode CanBuildItem(GamePlayer player, int location, int level, BuildableItemEnum itemCode)
        {
            if (!BuildableItems.ContainsKey(itemCode))
            {
                return ErrorCode.START_WORK_INVALID_ITEM;
            }

            BuildableItem buildabelItem = BuildableItems[itemCode];

            if (level < 0 || level > buildabelItem.Stats.Count)
            {
                return ErrorCode.START_WORK_INVALID_LEVEL;
            }

            BuildableItemStat stat = buildabelItem.Stats[level];

            if (player.Level < stat.RequiredLevel)
            {
                return ErrorCode.START_WORK_ITEM_NOT_AVAILABLE;
            }

            if (stat.CoinCost > player.Coins)
            {
                return ErrorCode.START_WORK_INSUFFICIENT_COINS;
            }

            if (stat.CouponCost > player.Coupons)
            {
                return ErrorCode.START_WORK_INSUFFICIENT_COUPONS;
            }

            if (location < 0 || location > player.Locations.Count)
            {
                return ErrorCode.START_WORK_INVALID_LOCATION;
            }

            BusinessLocation businessLocation = player.Locations[location];
            LocationStorage storage = businessLocation.Storage;

            if (stat.RequiredItems != null)
            {
                foreach (ItemQuantity itemQuantity in stat.RequiredItems)
                {
                    if (!storage.Items.ContainsKey(itemQuantity.ItemCode))
                    {
                        return ErrorCode.START_WORK_INVALID_INGREDIENTS;
                    }

                    if (storage.Items[itemQuantity.ItemCode].StoredQuantity 
                        < itemQuantity.StoredQuantity)
                    {
                        return ErrorCode.START_WORK_INSUFFICIENT_INGREDIENTS;
                    }
                }
            }

            if (!DoesPlayerHaveProductionCapacity(player, location, level, itemCode))
            {
                return ErrorCode.START_WORK_NO_PRODUCTION_CAPACITY;
            }

            if (!DoesPlayeraHaveStorageCapacity(player, location, level, itemCode))
            {
                return ErrorCode.START_WORK_NO_STORAGE_CAPACITY;
            }

            WorkItem workItem = buildabelItem as WorkItem;
            if (workItem != null)
            {
                if (!storage.Items.ContainsKey(itemCode))
                {
                    return ErrorCode.START_WORK_INSUFFICIENT_NEGATIVE;
                }
                
                if (storage.Items[itemCode].UnStoredQuantity < 
                    GetProductionQuantityForItem(player, location, level, itemCode))
                {
                    return ErrorCode.START_WORK_INSUFFICIENT_NEGATIVE;
                }
            }                 
            else
            {
                ConsumableItem consumable = buildabelItem as ConsumableItem;
                if (consumable == null)
                {
                    ItemQuantity inStorage = storage.Items[itemCode];
                    if (inStorage.UnStoredQuantity > 0 && inStorage.Level >= level)
                    {
                        return ErrorCode.START_WORK_MULTIPLE_NON_CONSUMABLE;
                    }
                }
            }

            return ErrorCode.ERROR_OK;
        }

        /// <summary>
        /// Returns true if the player can build the item, throws an exception otherwise
        /// </summary>
        /// <returns></returns>
        public bool TryBuildItem(GamePlayer player, int location, int level, BuildableItemEnum itemCode)
        {
            ErrorCode error = CanBuildItem(player, location, level, itemCode);
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
        public void DeductResources(GamePlayer player, int location, int level, BuildableItemEnum itemCode)
        {
            BuildableItem buildableItem = BuildableItems[itemCode];
            BuildableItemStat stat = buildableItem.Stats[level];

            if (stat.RequiredItems != null)
            {
                foreach (ItemQuantity requiredItem in stat.RequiredItems)
                {
                    RemoveItem(player, location, requiredItem);
                }
            }

            ModifyCoins(player, -stat.CoinCost);
            ModifyCoupons(player, -stat.CouponCost);

            player.StateChanged = true;
        }
         
        /// <summary>
        /// Modifies the player's coins
        /// </summary>
        /// <param name="player"></param>
        /// <param name="amount"></param>
        public void ModifyCoins(GamePlayer player, int amount)
        {
            player.Coins += amount;

            if (amount != 0 && CoinsChanged != null)
            {
                OnCoinsChanged(new GamePlayerLogicEventArgs(player, amount));
            }
        }

        /// <summary>
        /// Modifies the player's coupons
        /// </summary>
        /// <param name="player"></param>
        /// <param name="amount"></param>
        public void ModifyCoupons(GamePlayer player, int amount)
        {
            player.Coupons += amount;

            if (amount != 0 && CouponsChanged != null)
            {
                OnCouponsChanged(new GamePlayerLogicEventArgs(player, amount));
            }
        }

        /// <summary>
        /// Returns the production amount for an item
        /// </summary>
        /// <param name="player"></param>
        /// <param name="location"></param>
        /// <param name="level"></param>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public int GetProductionQuantityForItem(GamePlayer player, int location, int level, BuildableItemEnum itemCode)
        {
            BuildableItem buildableItem = BuildableItems[itemCode];    
            ConsumableItem consumable = buildableItem as ConsumableItem;
            if (consumable != null)
            {
                ConsumableItemStat stat = consumable.ConsumableStats[level];
                if (stat != null)
                {
                    return stat.ProductionQuantity;
                }
            }

            return 1;
        }
            
        /// <summary>
        /// Starts a player doing delayed work if all conditions pass.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="code"></param>
        public WorkInProgress StartWork(GamePlayer player, int location, int level, BuildableItemEnum itemCode)
        {
            bool canDoWork = TryBuildItem(player, location, level, itemCode);

            if (!canDoWork)
            {
                return null;
            }

            DeductResources(player, location, level, itemCode);

            BuildableItem buildableItem = BuildableItems[itemCode];
            BuildableItemStat stat = buildableItem.Stats[level];

            WorkInProgress wip = new WorkInProgress
            {
                Quantity = new ItemQuantity
                {
                    ItemCode = itemCode,
                    UnStoredQuantity = GetProductionQuantityForItem(player, location, level, itemCode)
                },
                FinishTime = DateTime.UtcNow.AddSeconds(stat.BuildSeconds)
            };

            player.WorkInProgress.Add(wip);

            if (WorkStarted != null)
            {
                OnWorkStarted(new GamePlayerLogicEventArgs(player, wip));
            }

            WorkItem workItem = buildableItem as WorkItem;
            if (workItem != null)
            {
                FinishWork(player, DateTime.UtcNow);
            }

            player.StateChanged = true;

            return wip;
        }

        /// <summary>
        /// Adds an item to the player's inventory
        /// </summary>
        /// <param name="player"></param>
        /// <param name="quantity"></param>
        public void AddItem(GamePlayer player, int businessLocation, ItemQuantity itemQuantity)
        {
            BusinessLocation location = player.Locations[businessLocation];
            LocationStorage storage = location.Storage;
            storage.AddItem(itemQuantity);

            if (ItemAdded != null)
            {
                OnItemAdded(new GamePlayerLogicEventArgs(player, itemQuantity));
            }
        }

        /// <summary>
        /// Removes an item from the player's inventory
        /// </summary>
        /// <param name="player"></param>
        /// <param name="quantity"></param>
        public void RemoveItem(GamePlayer player, int businessLocation, ItemQuantity itemQuantity)
        {
            BusinessLocation location = player.Locations[businessLocation];
            LocationStorage storage = location.Storage;
            storage.RemoveItem(itemQuantity);

            if (ItemSubtracted != null)
            {
                OnItemSubtracted(new GamePlayerLogicEventArgs(player, itemQuantity));
            }
        }        

        /// <summary>
        /// Adds or removes an item to or from a player when work is completed
        /// </summary>
        /// <param name="player"></param>
        /// <param name="item"></param>
        public void AdjustInventory(GamePlayer player, WorkInProgress item)
        {
            WorkItem workItem = BuildableItems[item.Quantity.ItemCode] as WorkItem;
            if (workItem != null)
            {
                RemoveItem(player, item.Location, item.Quantity);
            }
            else
            {
                AddItem(player, item.Location, item.Quantity);
            }            
        }

        /// <summary>
        /// Checks if a player is finished doing some work.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="itemCode"></param>
        public List<WorkInProgress> FinishWork(GamePlayer player, DateTime checkBefore)
        {
            List<WorkInProgress> finishedItems = new List<WorkInProgress>();

            if (player.WorkInProgress == null ||
                player.WorkInProgress.Count == 0)
            {
                Trace.TraceError("Attempt to finish work but no work started!");
                throw new ArgumentException();
            }

            foreach (WorkInProgress item in player.WorkInProgress)
            {
                if (item.FinishTime < checkBefore)
                {
                    finishedItems.Add(item);
                }
            }

            foreach (WorkInProgress item in finishedItems)
            {
                AdjustInventory(player, item);
                BuildableItem buildableItem = BuildableItems[item.Quantity.ItemCode];
                BuildableItemStat stat = buildableItem.Stats[item.Quantity.Level];
                AddExperience(player, stat.Experience);

                player.WorkInProgress.Remove(item);

                if (WorkFinished != null)
                {
                    OnWorkFinished(new GamePlayerLogicEventArgs(player, item));                        
                }
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
        public List<WorkInProgress> GetCurrentWorkItemsForProductionItem(GamePlayer player, 
            BuildableItemEnum productionItem)
        {
            List<WorkInProgress> workItems = new List<WorkInProgress>();

            foreach (WorkInProgress workInProgress in player.WorkInProgress)
            {
                ConsumableItem item = BuildableItems[workInProgress.Quantity.ItemCode] as ConsumableItem;
                if (item != null)
                {
                    if (item.ProducedWith == productionItem)
                    {
                        workItems.Add(workInProgress);
                    }
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
        public double GetPercentageCompleteForWorkItem(WorkInProgress workItem)
        {
            BuildableItem buildableItem = BuildableItems[workItem.Quantity.ItemCode];
            BuildableItemStat stat = buildableItem.Stats[workItem.Quantity.Level];
            double secondsToGo = workItem.FinishTime.Subtract(DateTime.UtcNow).TotalSeconds;
            double ratio = 1 - (secondsToGo / (double)stat.BuildSeconds);
            return Math.Min(1, Math.Max(ratio, 0));
        }
    }
}
