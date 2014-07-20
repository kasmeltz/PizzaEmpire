using GameLogic.ExperienceLevelLogic;
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
            player.Level = level;

            if (!ExperienceLevelManager.Instance.ExperienceLevels.ContainsKey(level))
            {
                Trace.TraceError("Attempt to set a non existent player level: " + level);
                throw new ArgumentException();
            }

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
    }
}
