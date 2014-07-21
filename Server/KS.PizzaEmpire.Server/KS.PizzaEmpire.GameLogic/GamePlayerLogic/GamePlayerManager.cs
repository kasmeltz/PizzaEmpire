using GameLogic.ExperienceLevelLogic;
using GameLogic.ItemLogic;
using KS.PizzaEmpire.Business.Common;
using KS.PizzaEmpire.Business.Logic;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GameLogic.GamePlayerLogic
{
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

            player.BuildableItems = new Dictionary<int, int>();
            player.Equipment = new Dictionary<int, int>();
            player.DelayedItems = new List<DelayedItem>();

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

            ExperienceLevel expLevel = ExperienceLevelManager.Instance.ExperienceLevels[level];

            foreach (int bi in expLevel.NewBuildableItems)
            {
                if (!player.BuildableItems.ContainsKey(bi))
                {
                    player.BuildableItems[bi] = -1;
                }
            }

            foreach(int ne in expLevel.NewEquipment)
            {
                if (!player.Equipment.ContainsKey(ne))
                {
                    player.Equipment[ne] = -1;
                }
            }
        }

        /// <summary>
        /// Starts a player doing delayed work if all conditions pass.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="itemCode"></param>
        public static DelayedItem StartWork(GamePlayer player, int itemCode)
        {
            // check for improper requests for starting work
            if (!ItemManager.Instance.BuildableItems.ContainsKey(itemCode))
            {
                Trace.TraceError("Attempt to start work for a non existant item");
                throw new ArgumentException();
            }

            if (!player.BuildableItems.ContainsKey(itemCode))
            {
                Trace.TraceError("Attempt to start work for an item not availble to a player");
                throw new ArgumentException();
            }

            BuildableItem bi = ItemManager.Instance.BuildableItems[itemCode];
            
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

            if (bi.Recipe != null)
            {
                if (!player.Equipment.ContainsKey(bi.Recipe.EquipmentCode))
                {
                    Trace.TraceError("Attempt to start work for an item without proper equipment");
                    throw new ArgumentException();
                }
                
                foreach(ItemQuantity ingred in bi.Recipe.Ingredients)
                {
                    if (!player.BuildableItems.ContainsKey(ingred.ItemCode))
                    {
                        Trace.TraceError("Attempt to start work for an item without proper ingredients");
                        throw new ArgumentException();
                    }

                    if (player.BuildableItems[ingred.ItemCode] < ingred.Quantity)
                    {
                        Trace.TraceError("Attempt to start work for an item with insufficient ingredients");
                        throw new ArgumentException();
                    }
                }
            }

            // If we get here then the work can actually be started!
            if (bi.Recipe != null)
            {
                foreach (ItemQuantity ingred in bi.Recipe.Ingredients)
                {
                    int count = player.BuildableItems[ingred.ItemCode];
                    count -= ingred.Quantity;
                    player.BuildableItems[ingred.ItemCode] = count;
                }
            }

            player.Coins -= bi.CoinCost;
            player.Coupons -= bi.CouponCost;

            DelayedItem item = new DelayedItem
            {
                ItemCode = itemCode,
                FinishTime = DateTime.UtcNow.AddSeconds(bi.BuildSeconds)
            };

            player.DelayedItems.Add(item);

            return item;
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
                    if (player.Experience > exl.ExperienceRequired)
                    {
                        SetLevel(player, player.Level + 1);
                        levelUp = true;
                    }                    
                }
            } while (levelUp);
            
        }

        /// <summary>
        /// Applies a finished item to a player
        /// </summary>
        /// <param name="player"></param>
        /// <param name="item"></param>
        public static void ApplyFinishedItem(GamePlayer player, DelayedItem item)
        {
            BuildableItem bi = ItemManager.Instance.BuildableItems[item.ItemCode];

            if (player.BuildableItems[item.ItemCode] == -1)
            {
                player.BuildableItems[item.ItemCode] = 0;
            }

            player.BuildableItems[item.ItemCode] += bi.Quantity;

            AddExperience(player, bi.Experience);
        }
        
        /// <summary>
        /// Checks if a player is finished doing some work.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="itemCode"></param>
        public static List<DelayedItem> FinishWork(GamePlayer player)
        {
            List<DelayedItem> finishedItems = new List<DelayedItem>();

            if (player.DelayedItems == null || 
                player.DelayedItems.Count == 0)
            {
                Trace.TraceError("Attempt to finish work but no work started!");
                throw new ArgumentException();
            }

            DateTime now = DateTime.UtcNow;

            foreach (DelayedItem item in player.DelayedItems)
            {
                if (item.FinishTime < now)
                {
                    finishedItems.Add(item);
                }
            }

            foreach (DelayedItem item in finishedItems)
            {
                ApplyFinishedItem(player, item);
                player.DelayedItems.Remove(item);
            }

            return finishedItems;
        }
    }
}
