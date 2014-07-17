using Microsoft.WindowsAzure.Storage.Table;
using ProtoBuf;
using System;

namespace KS.PizzaEmpire.Business
{
    /// <summary>
    /// Represents the state for a player of the game.
    /// </summary>
    [ProtoContract]
    [Serializable]
    public class GamePlayer : TableEntity
    {
        /// <summary>
        /// Returns a parition key for table storage from the provided key
        /// </summary>
        /// <returns></returns>
        public static string AutoPartitionKey(string key)
        {
            return key.Substring(0, 2);
        }
        
        /// <summary>
        /// A unique identifier for this Game Player across the application
        /// </summary>
        private string _uniqueKey;
        public string UniqueKey
        {
            get
            {
                return _uniqueKey;
            }
            set
            {
                _uniqueKey = value;
                PartitionKey = AutoPartitionKey(_uniqueKey);
                RowKey = _uniqueKey;
            }
        }
    }
}
