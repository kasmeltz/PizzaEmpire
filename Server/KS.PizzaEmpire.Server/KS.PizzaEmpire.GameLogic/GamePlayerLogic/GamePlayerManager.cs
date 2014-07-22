namespace KS.PizzaEmpire.GameLogic.GamePlayerLogic
{
    using GameLogic.ExperienceLevelLogic;
    using GameLogic.ItemLogic;
    using Business.Common;
    using Business.Logic;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    /// <summary>
    /// Represents the game logic that has to do with Game Players
    /// </summary>
    public class GamePlayerManager
    {
        /// <summary>
        /// Creates a brand new game player - e.g. a Game Player who is logging in for
        /// the first time with no persistant state
        /// </summary>
        /// <returns></returns>
        public static GamePlayer CreateNewGamePlayer()
        {
            GamePlayer player = new GamePlayer();
            player.Coins = 1000;
            player.Coupons = 5;
            player.Experience = 0;

            player.BuildableItems = new Dictionary<BuildableItemEnum, int>();
            player.BuildableItems[BuildableItemEnum.Delivery_Truck_L1] = 1;

            player.WorkItems = new List<WorkItem>();
            SetLevel(player, 1);

            return player;
        }

        /// <summary>
        /// Sets a new level for the player
        /// </summary>
        /// <param name="player"></param>
        /// <param name="level"></param>
        public static void SetLevel(GamePlayer player, int level)
        {
            if (!ExperienceLevelManager.Instance.ExperienceLevels.ContainsKey(level))
            {
                Trace.TraceError("Attempt to set a non existent player level: " + level);
                throw new ArgumentException();
            }

            player.Level = level;
        }

        /// <summary>
        /// Adds experience to a player
        /// </summary>
        /// <param name="player"></param>
        /// <param name="experience"></param>
        public static void AddExperience(GamePlayer player, int experience)
        {
            player.Experience += experience;

            bool levelUp;
            do
            {
                levelUp = false;
                if (ExperienceLevelManager.Instance
                    .ExperienceLevels
                    .ContainsKey(player.Level + 1))
                {
                    ExperienceLevel exl = ExperienceLevelManager.Instance
                        .ExperienceLevels[player.Level + 1];
                    if (player.Experience >= exl.ExperienceRequired)
                    {
                        SetLevel(player, player.Level + 1);
                        levelUp = true;
                    }
                }
            } while (levelUp);
        }
        
        /// <summary>
        /// Returns true if the player has the capacity to start hhe item.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public static bool DoesPlayerHaveCapacity(GamePlayer player, BuildableItemEnum itemCode)
        {
            BuildableItem capacityItem = null;

            BuildableItem bi = ItemManager.Instance.BuildableItems[itemCode];
            foreach (ItemQuantity iq in bi.RequiredItems)
            {
                BuildableItem ri = ItemManager.Instance.BuildableItems[iq.ItemCode];
                if (ri.Capacity > 0)
                {
                    capacityItem = ri;
                    break;
                }
            }

            if (capacityItem == null)
            {
                return true;
            }

            int inUse = 0;
            foreach (WorkItem wi in player.WorkItems)
            {
                BuildableItem wbi = ItemManager.Instance.BuildableItems[wi.ItemCode];
                foreach(ItemQuantity iq in wbi.RequiredItems)
                {
                    if (iq.ItemCode == capacityItem.ItemCode)
                    {
                        inUse++;
                    }
                }
            }

            return inUse < capacityItem.Capacity;
        }

        /// <summary>
        /// Returns true if the player can build the specific item.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool CanBuildItem(GamePlayer player, BuildableItemEnum itemCode)
        {            
            if (!ItemManager.Instance.BuildableItems.ContainsKey(itemCode))
            {
                Trace.TraceError("Attempt to start work for a non existant item");
                throw new ArgumentException();
            }

            BuildableItem bi = ItemManager.Instance.BuildableItems[itemCode];

            if (player.Level < bi.RequiredLevel)
            {
                Trace.TraceError("Attempt to start work for an item not availble to a player");
                throw new ArgumentException();
            }

            if (bi.CoinCost > player.Coins)
            {
                Trace.TraceError("Attempt to start work for an item with insufficient coins");
                throw new ArgumentException();
            }

            if (bi.CouponCost > player.Coupons)
            {
                Trace.TraceError("Attempt to start work for an item with insufficient coupons");
                throw new ArgumentException();
            }
           
            if (bi.RequiredItems != null)
            {                               
                foreach (ItemQuantity itemQuantity in bi.RequiredItems)
                {
                    if (!player.BuildableItems.ContainsKey(itemQuantity.ItemCode))
                    {
                        Trace.TraceError("Attempt to start work for an item without proper ingredients");
                        throw new ArgumentException();
                    }

                    if (player.BuildableItems[itemQuantity.ItemCode] < itemQuantity.Quantity)
                    {
                        Trace.TraceError("Attempt to start work for an item with insufficient ingredients");
                        throw new ArgumentException();
                    }
                }

                if (!DoesPlayerHaveCapacity(player, itemCode))
                {
                    Trace.TraceError("Attempt to start work but the equipment is full");
                    throw new ArgumentException();
                }
            }

            if (player.BuildableItems.ContainsKey(itemCode))
            {
                if (player.BuildableItems[itemCode] >= bi.MaxQuantity)
                {
                    Trace.TraceError("Attempt to start work for an item that the player can't hold more of.");
                    throw new ArgumentException();
                }
            }

            return true;
        }

        /// <summary>
        ///  Deducts the resources required to build an item from the player
        /// </summary>
        /// <param name="player"></param>
        /// <param name="itemCode"></param>
        public static void DeductResources(GamePlayer player, BuildableItemEnum itemCode)
        {
            BuildableItem bi = ItemManager.Instance.BuildableItems[itemCode];

            if (bi.RequiredItems != null)
            {
                foreach (ItemQuantity requiredItem in bi.RequiredItems)
                {
                    BuildableItem ri = ItemManager.Instance.BuildableItems[requiredItem.ItemCode];
                    if (ri.IsConsumable)
                    {
                        player.BuildableItems[requiredItem.ItemCode] -= requiredItem.Quantity;                        
                    }
                }
            }

            player.Coins -= bi.CoinCost;
            player.Coupons -= bi.CouponCost;
        }

        /// <summary>
        /// Starts a player doing delayed work if all conditions pass.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="code"></param>
        public static WorkItem StartWork(GamePlayer player, BuildableItemEnum itemCode)
        {
            bool canDoWork = CanBuildItem(player, itemCode);
            
            if (!canDoWork)
            {
                return null;
            }

            DeductResources(player, itemCode);

            BuildableItem bi = ItemManager.Instance.BuildableItems[itemCode];

            WorkItem workItem = new WorkItem
            {   
                ItemCode = itemCode,
                FinishTime = DateTime.UtcNow.AddSeconds(bi.BuildSeconds)
            };

            player.WorkItems.Add(workItem);

            return workItem;
        }       

        /// <summary>
        /// Adds an item to a player
        /// </summary>
        /// <param name="player"></param>
        /// <param name="item"></param>
        public static void AddItem(GamePlayer player, WorkItem item)
        {
            BuildableItem bi = ItemManager.Instance.BuildableItems[item.ItemCode];

            if (!player.BuildableItems.ContainsKey(item.ItemCode))
            {
                player.BuildableItems[item.ItemCode] = 0;
            }
            
            double production = bi.BaseProduction;
            if (bi.RequiredItems != null)
            {
                foreach (ItemQuantity iq in bi.RequiredItems)
                {
                    BuildableItem pi = ItemManager.Instance.BuildableItems[iq.ItemCode];
                    production = production * pi.ProductionMultiplier;
                }
            }

            int count = player.BuildableItems[item.ItemCode];
            count += (int)production;
            if (count > bi.MaxQuantity)
            {
                count = bi.MaxQuantity;
            }
            player.BuildableItems[item.ItemCode] = count;

            AddExperience(player, bi.Experience);
        }
       
        /// <summary>
        /// Checks if a player is finished doing some work.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="itemCode"></param>
        public static List<WorkItem> FinishWork(GamePlayer player)
        {
            List<WorkItem> finishedItems = new List<WorkItem>();

            if (player.WorkItems == null ||
                player.WorkItems.Count == 0)
            {
                Trace.TraceError("Attempt to finish work but no work started!");
                throw new ArgumentException();
            }

            DateTime now = DateTime.UtcNow;

            foreach (WorkItem item in player.WorkItems)
            {
                if (item.FinishTime < now)
                {
                    finishedItems.Add(item);
                    AddItem(player, item);
                }                
            }                      
                
            return finishedItems;
        }
    }
}