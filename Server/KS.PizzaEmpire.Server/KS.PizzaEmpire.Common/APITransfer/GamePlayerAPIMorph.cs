namespace KS.PizzaEmpire.Common.APITransfer
{
    using BusinessObjects;
    using System.Collections.Generic;

    /// <summary>
    /// Class that can morph GamePlayer objects to and from a dto format
    /// </summary>
    public class GamePlayerAPIMorph : IAPIEntityMorph
    {
        /// <summary>
        /// Converts a business object to an API dto object
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public IAPIEntity ToAPIFormat(IBusinessObjectEntity entity)
        {
            GamePlayer item = entity as GamePlayer;
            GamePlayerAPI clone = new GamePlayerAPI();

            clone.Coins = item.Coins;
            clone.Coupons = item.Coupons;
            clone.Experience = item.Experience;
            clone.Level = item.Level;

            clone.BuildableItems = "";

            clone.WorkItems = item.WorkItems;

            return clone;
        }

        /// <summary>
        /// Converts an API dto objec to a business object
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public IBusinessObjectEntity ToBusinessFormat(IAPIEntity entity)
        {
            GamePlayerAPI item = entity as GamePlayerAPI;
            GamePlayer clone = new GamePlayer();

            clone.Coins = item.Coins;
            clone.Coupons = item.Coupons;
            clone.Experience = item.Experience;
            clone.Level = item.Level;

            clone.BuildableItems = new Dictionary<BuildableItemEnum, int>();

            clone.WorkItems = item.WorkItems;

            return clone;
        }
    }
}
