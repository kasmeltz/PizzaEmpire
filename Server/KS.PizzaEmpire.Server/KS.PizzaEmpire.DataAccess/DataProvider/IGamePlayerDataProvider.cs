using KS.PizzaEmpire.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.PizzaEmpire.DataAccess.DataProvider
{
    /// <summary>
    /// Defines an interface for storing and retrieving persitent data
    /// for game players
    /// </summary>
    public interface IGamePlayerDataProvider
    {
        /// <summary>
        /// Retrieves the persistent data for a game player from the data store.
        /// </summary>
        /// <param name="key">The unique key of the game player whose data is requested</param>
        /// <returns>A GamePlayer instance representing the persistent data associated with the 
        /// game player</returns>
        Task<GamePlayer> Get(string key);

        /// <summary>
        /// Saves the data for a game player to the persitent data store.
        /// </summary>
        /// <param name="player">The GamePlayer instance to save.</param>
        /// <returns>This is an async method</returns>
        Task Save(GamePlayer player);

        /// <summary>
        /// Sets the data for the provided game player to inactive
        /// </summary>
        /// <param name="player">The GamePlayer instance to set inactive</param>
        /// <returns>This is an async method</returns>
        Task SetInactive(GamePlayer player);
    }
}
