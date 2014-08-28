namespace KS.PizzaEmpire.Common.APITransfer
{
    using BusinessObjects;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Text;

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
            GamePlayerAPI clone = new GamePlayerAPI();
            GamePlayer other = entity as GamePlayer;

            clone.Coins = other.Coins;
            clone.Coupons = other.Coupons;
            clone.Experience = other.Experience;
            clone.Level = other.Level;
            clone.TutorialStage = other.TutorialStage;
            clone.StateChanged = other.StateChanged;
            clone.Locations = other.Locations;
            clone.WorkInProgress = other.WorkInProgress;

            return clone;
        }

        /// <summary>
        /// Converts an API dto objec to a business object
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public IBusinessObjectEntity ToBusinessFormat(IAPIEntity entity)
        {
            GamePlayer clone = new GamePlayer();
            GamePlayerAPI other = entity as GamePlayerAPI;

            clone.Coins = other.Coins;
            clone.Coupons = other.Coupons;
            clone.Experience = other.Experience;
            clone.Level = other.Level;
            clone.TutorialStage = other.TutorialStage;
            clone.StateChanged = other.StateChanged;
            clone.Locations = other.Locations;
            clone.WorkInProgress = other.WorkInProgress;

            return clone;
        }
    }
}
