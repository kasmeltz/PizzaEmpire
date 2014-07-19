﻿using KS.PizzaEmpire.Business.Cache;
using KS.PizzaEmpire.Business.Conversion;
using KS.PizzaEmpire.Business.Logic;
using Microsoft.WindowsAzure.Storage.Table;

namespace KS.PizzaEmpire.Business.TableStorage
{
    /// <summary>
    /// Represents the state for a player of the game as stored in table storage.
    /// </summary>
    public class GamePlayerTableStorage : TableEntity, ITableStorageEntity, IToLogicEntity
    {
        /// <summary>
        /// Creates a new instance of the GamePlayerTableStorage class.
        /// </summary>
        public GamePlayerTableStorage()
        { }

        public int Coins { get; set; }
        public int Coupons { get; set; }

        #region IToLogicEntity
       
        /// <summary>
        /// Returns a new instance of the appropriate ILogicEntity with cloned data.
        /// </summary>
        /// <returns></returns>
        public ILogicEntity ToLogicEntity()
        {
            return GamePlayer.From(this);
        }

        #endregion

        #region Cloners
       
        /// <summary>
        /// Generates a new GamePlayerTableStorage instance from a GamePlayer instance.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static GamePlayerTableStorage From(GamePlayer item)
        {
            GamePlayerTableStorage clone = new GamePlayerTableStorage();
            clone.PartitionKey = item.StorageInformation.PartitionKey;
            clone.RowKey = item.StorageInformation.RowKey;
            clone.Coins = item.Coins;
            clone.Coupons = item.Coupons;

            return clone;
        }

        #endregion
    }
}
