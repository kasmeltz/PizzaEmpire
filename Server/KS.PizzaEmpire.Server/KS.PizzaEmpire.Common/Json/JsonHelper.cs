using KS.PizzaEmpire.Common.APITransfer;
using KS.PizzaEmpire.Common.BusinessObjects;
using System.Collections.Generic;
namespace KS.PizzaEmpire.Common.Utility
{
    public class JsonHelper
    {
        private static volatile JsonHelper instance;
        private static object syncRoot = new object();

        private JsonHelper() { }

        /// <summary>
        /// Provides the Singleton instance of the GamePlayerLogic
        /// </summary>
        public static JsonHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new JsonHelper();
                        }
                    }
                }
                return instance;
            }
        }

        protected IJsonConverter Converter;

        public void Initialize(IJsonConverter converter)
        {
            Converter = converter;
        }

        /// <summary>
        /// Returns a dictionary of buildable items as represented by the supplied json string.
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public Dictionary<BuildableItemEnum, BuildableItem> ItemsFromJSON(string json)
        {
            Dictionary<BuildableItemEnum, BuildableItem> itemDictionary = new Dictionary<BuildableItemEnum, BuildableItem>();
            BuildableItemAPIMorph morph = new BuildableItemAPIMorph();
            List<BuildableItemAPI> itemList = Converter.Deserialize<List<BuildableItemAPI>>(json);
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
        public string ItemsToJSON(Dictionary<BuildableItemEnum, BuildableItem> items)
        {
            BuildableItemAPIMorph morph = new BuildableItemAPIMorph();
            List<BuildableItemAPI> itemList = new List<BuildableItemAPI>();
            foreach (BuildableItem item in items.Values)
            {
                itemList.Add((BuildableItemAPI)morph.ToAPIFormat(item));
            }
            return Converter.Serlialize<List<BuildableItemAPI>>(itemList);
        }

        /// <summary>
        /// Returns a dictionary of experience levels as represented by the supplied json string.
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public Dictionary<int, ExperienceLevel> LevelsFromJSON(string json)
        {
            Dictionary<int, ExperienceLevel> itemDictionary = new Dictionary<int, ExperienceLevel>();
            ExperienceLevelAPIMorph morph = new ExperienceLevelAPIMorph();
            List<ExperienceLevelAPI> itemList = Converter.Deserialize<List<ExperienceLevelAPI>>(json);
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
        public string LevelsToJSON(Dictionary<int, ExperienceLevel> items)
        {
            ExperienceLevelAPIMorph morph = new ExperienceLevelAPIMorph();
            List<ExperienceLevelAPI> itemList = new List<ExperienceLevelAPI>();
            foreach (ExperienceLevel item in items.Values)
            {
                itemList.Add((ExperienceLevelAPI)morph.ToAPIFormat(item));
            }
            return Converter.Serlialize<List<ExperienceLevelAPI>>(itemList);
        }

        /// <summary>
        /// Returns a GamePlayer as represented by the supplied json string.
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public GamePlayer PlayerFromJSON(string json)
        {
            GamePlayerAPIMorph morph = new GamePlayerAPIMorph();
            return (GamePlayer)morph.ToBusinessFormat(
                Converter.Deserialize<GamePlayerAPI>(json));
        }

        /// <summary>
        /// Returns a string that represents the GamePlayer instance.
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public string PlayerToJSON(GamePlayer item)
        {
            GamePlayerAPIMorph morph = new GamePlayerAPIMorph();
            return Converter.Serlialize<GamePlayerAPI>((GamePlayerAPI)morph.ToAPIFormat(item));
        }
    }
}
