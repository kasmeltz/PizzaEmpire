namespace KS.PizzaEmpire.Business.TableStorage
{
    using Microsoft.WindowsAzure.Storage.Table;

    /// <summary>
    /// Represents the state for a player of the game as stored in table storage.
    /// </summary>
    public class GamePlayerTableStorage : TableEntity, ITableStorageEntity
    {
        /// <summary>
        /// Creates a new instance of the GamePlayerTableStorage class.
        /// </summary>
        public GamePlayerTableStorage() { }

        /// <summary>
        /// The number of coins owned by the player
        /// </summary>
        public int Coins { get; set; }

        /// <summary>
        /// The number of coupons owned by the player
        /// </summary>
        public int Coupons { get; set; }

        /// <summary>
        /// The current experience of the player
        /// </summary>
        public int Experience { get; set; }

        /// <summary>
        /// The players current level
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// The players inventory of items
        /// </summary>
        public byte[] BuildableItemsSerialized { get; set; }

        /// <summary>
        /// The work in progress for the player
        /// </summary>
        public byte[] WorkItemsSerialized { get; set; }
    }
}
