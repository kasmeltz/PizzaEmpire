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
        /// An event that is fired when an item is consumed
        /// </summary>
        public event EventHandler<GamePlayerLogicEventArgs> ItemConsumed;
        protected virtual void OnItemConsumed(GamePlayerLogicEventArgs e)
        {
            try
            {
                EventHandler<GamePlayerLogicEventArgs> handler = ItemConsumed;
                if (handler != null)
                {
                    handler(this, e);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError("Exception in ItemsConsumed: " + ex.Message);
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

            player.WorkItems = new List<WorkInProgress>();
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
                    Level = 1 
                });

            AddItem(player,
                0, new ItemQuantity
                {
                    ItemCode = BuildableItemEnum.Restaurant_Storage,
                    UnStoredQuantity = 1,
                    StoredQuantity = 0,
                    Level = 1
                });

            AddItem(player,
                0, new ItemQuantity
                {
                    ItemCode = BuildableItemEnum.Dirty_Table,
                    UnStoredQuantity = 1,
                    StoredQuantity = 0,
                    Level = 1
                });

            AddItem(player,
                0, new ItemQuantity
                {
                    ItemCode = BuildableItemEnum.Dirty_Dishes,
                    UnStoredQuantity = 1,
                    StoredQuantity = 0,
                    Level = 1
                });

            AddItem(player,
                0, new ItemQuantity
                {
                    ItemCode = BuildableItemEnum.Dirty_Floor,
                    UnStoredQuantity = 1,
                    StoredQuantity = 0,
                    Level = 1
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

        /*
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
            foreach (WorkInProgress wi in player.WorkItems)
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

                        if (ItemConsumed != null)
                        {
                            OnItemConsumed(new GamePlayerLogicEventArgs(player, requiredItem));
                        }
                    }
                }
            }

            ModifyCoins(player, -bi.CoinCost);
            ModifyCoupons(player, -bi.CouponCost);

            player.StateChanged = true;
        }
        */
         
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

        /*
        /// <summary>
        /// Starts a player doing delayed work if all conditions pass.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="code"></param>
        public WorkInProgress StartWork(GamePlayer player, BuildableItemEnum itemCode)
        {
            bool canDoWork = TryBuildItem(player, itemCode);

            if (!canDoWork)
            {
                return null;
            }

            BuildableItem bi = BuildableItems[itemCode];

            DeductResources(player, itemCode);

            WorkInProgress workItem = new WorkInProgress
            {
                ItemCode = itemCode,
                FinishTime = DateTime.UtcNow.AddSeconds(bi.BuildSeconds)
            };

            player.WorkItems.Add(workItem);

            if (WorkStarted != null)
            {
                OnWorkStarted(new GamePlayerLogicEventArgs(player, workItem));
            }

            if (bi.IsImmediate)
            {
                FinishWork(player, DateTime.UtcNow);
            }

            player.StateChanged = true;

            return workItem;
        }
         * */

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

        /*
        /// <summary>
        /// Adds an item to a player when work is completed
        /// </summary>
        /// <param name="player"></param>
        /// <param name="item"></param>
        public void AddItem(GamePlayer player, WorkInProgress item)
        {
            BuildableItem bi = BuildableItems[item.ItemCode];

            if (!player.BuildableItems.ContainsKey(item.ItemCode))
            {
                player.BuildableItems[item.ItemCode] = 0;
            }

            player.BuildableItems[item.ItemCode] += bi.BaseProduction;

            if (ItemAdded != null)
            {
                OnItemAdded(new GamePlayerLogicEventArgs(player,
                    new ItemQuantity { ItemCode = item.ItemCode, Quantity = bi.BaseProduction } ));
            }

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
        public void SubtractItem(GamePlayer player, WorkInProgress item)
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

            if (ItemSubtracted != null)
            {
                OnItemSubtracted(new GamePlayerLogicEventArgs(player,
                    new ItemQuantity { ItemCode = item.ItemCode, Quantity = bi.BaseProduction }));
            }
        }


        /// <summary>
        /// Adds or removes an item to or from a player when work is completed
        /// </summary>
        /// <param name="player"></param>
        /// <param name="item"></param>
        public void AdjustInventory(GamePlayer player, WorkInProgress item)
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
        public List<WorkInProgress> FinishWork(GamePlayer player, DateTime checkBefore)
        {
            List<WorkInProgress> finishedItems = new List<WorkInProgress>();

            if (player.WorkItems == null ||
                player.WorkItems.Count == 0)
            {
                Trace.TraceError("Attempt to finish work but no work started!");
                throw new ArgumentException();
            }

            foreach (WorkInProgress item in player.WorkItems)
            {
                if (item.FinishTime < checkBefore)
                {
                    finishedItems.Add(item);
                }
            }

            foreach (WorkInProgress item in finishedItems)
            {
                AdjustInventory(player, item);
                player.WorkItems.Remove(item);

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
        public List<WorkInProgress> GetCurrentWorkItemsForProductionItem(GamePlayer player, BuildableItemEnum productionItem)
        {
            List<WorkInProgress> workItems = new List<WorkInProgress>();

            foreach (WorkInProgress item in player.WorkItems)
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
        public double GetPercentageCompleteForWorkItem(WorkInProgress workItem)
        {
            BuildableItem bi = BuildableItems[workItem.ItemCode];
            double secondsToGo = workItem.FinishTime.Subtract(DateTime.UtcNow).TotalSeconds;
            double ratio = 1 - (secondsToGo / (double)bi.BuildSeconds);
            return Math.Min(1, Math.Max(ratio, 0));
        }
         * */
    }
}
