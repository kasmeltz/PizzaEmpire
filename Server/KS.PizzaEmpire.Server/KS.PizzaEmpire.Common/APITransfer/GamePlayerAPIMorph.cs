namespace KS.PizzaEmpire.Common.APITransfer
{
    using BusinessObjects;
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

            GamePlayer item = entity as GamePlayer;
            GamePlayerAPI clone = new GamePlayerAPI();

            /*
            clone.Coins = item.Coins;
            clone.Coupons = item.Coupons;
            clone.Experience = item.Experience;
            clone.Level = item.Level;

            StringBuilder sb = new StringBuilder();
            foreach(KeyValuePair<BuildableItemEnum, int> kvp in item.BuildableItems)
            {
                sb.Append((int)kvp.Key);
                sb.Append(":");
                sb.Append(kvp.Value);
                sb.Append(":");
            }
            sb.Remove(sb.Length - 1, 1);
            clone.BuildableItems = sb.ToString();

            clone.WorkItems = item.WorkItems;

            clone.TutorialStage = item.TutorialStage;
             * */

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

            /*
            clone.Coins = item.Coins;
            clone.Coupons = item.Coupons;
            clone.Experience = item.Experience;
            clone.Level = item.Level;

            clone.BuildableItems = new Dictionary<BuildableItemEnum, int>();
            string[] items = item.BuildableItems.Split(':');
            for (int i = 0; i < items.Length; i += 2)
            {
                BuildableItemEnum bie = (BuildableItemEnum)Int32.Parse(items[i]);
                clone.BuildableItems[bie] = Int32.Parse(items[i + 1]);
            }

            clone.WorkItems = item.WorkItems;

            clone.TutorialStage = item.TutorialStage;
            */

            return clone;
        }
    }
}
