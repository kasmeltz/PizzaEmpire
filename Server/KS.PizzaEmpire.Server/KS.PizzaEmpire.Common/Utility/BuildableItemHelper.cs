namespace KS.PizzaEmpire.Common.Utility
{
    using BusinessObjects;
    using APITransfer;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// A class that does some work with buildable items in the game
    /// </summary>
    public class BuildableItemHelper
    {
        /// <summary>
        /// Returns a dictionary of buildable items as represented by the supplied json string.
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static Dictionary<BuildableItemEnum, BuildableItem> FromJSON(string json)
        {
            Dictionary<BuildableItemEnum, BuildableItem> itemDictionary = new Dictionary<BuildableItemEnum, BuildableItem>();
            BuildableItemAPIMorph morph = new BuildableItemAPIMorph();
            List<BuildableItemAPI> itemList = JsonConvert.DeserializeObject<List<BuildableItemAPI>>(json);
            foreach (BuildableItemAPI item in itemList)
            {
                itemDictionary[item.ItemCode] = (BuildableItem)morph.ToBusinessFormat(item);
            }

            return itemDictionary;
        }

        /// <summary>
        /// Returns a string that represents the dictionary of buildable items in json format.
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static string ToJSON(Dictionary<BuildableItemEnum, BuildableItem> items)
        {
            BuildableItemAPIMorph morph = new BuildableItemAPIMorph();
            List<BuildableItemAPI> itemList = new List<BuildableItemAPI>();
            foreach (BuildableItem item in items.Values)
            {
                itemList.Add((BuildableItemAPI)morph.ToAPIFormat(item));
            }
            return JsonConvert.SerializeObject(itemList); 
        }
    }
}
