namespace KS.PizzaEmpire.Common.Utility
{
    using BusinessObjects;
    using APITransfer;
    using LitJson;
    using System.Collections.Generic;

    /// <summary>
    /// A class that does some work with experience levels in the game
    /// </summary>
    public class ExperienceLevelHelper
    {
        /// <summary>
        /// Returns a dictionary of experience levels as represented by the supplied json string.
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static Dictionary<int, ExperienceLevel> FromJSON(string json)
        {
            Dictionary<int, ExperienceLevel> itemDictionary = new Dictionary<int, ExperienceLevel>();
            ExperienceLevelAPIMorph morph = new ExperienceLevelAPIMorph();
            List<ExperienceLevelAPI> itemList = JsonMapper.ToObject<List<ExperienceLevelAPI>>(json);
            foreach (ExperienceLevelAPI item in itemList)
            {
                itemDictionary[item.Level] = (ExperienceLevel)morph.ToBusinessFormat(item);
            }

            return itemDictionary;
        }

        /// <summary>
        /// Returns a string that represents the dictionary of experience levels in json format.
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static string ToJSON(Dictionary<int, ExperienceLevel> items)
        {
            ExperienceLevelAPIMorph morph = new ExperienceLevelAPIMorph();
            List<ExperienceLevelAPI> itemList = new List<ExperienceLevelAPI>();
            foreach (ExperienceLevel item in items.Values)
            {
                itemList.Add((ExperienceLevelAPI)morph.ToAPIFormat(item));
            }
            return JsonMapper.ToJson(itemList);
        }
    }
}
