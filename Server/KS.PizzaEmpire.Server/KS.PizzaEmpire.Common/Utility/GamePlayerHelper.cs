namespace KS.PizzaEmpire.Common.Utility
{
    using BusinessObjects;
    using KS.PizzaEmpire.Common.APITransfer;
    using Newtonsoft.Json;

    public class GamePlayerHelper
    {
        /// <summary>
        /// Returns a GamePlayer as represented by the supplied json string.
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static GamePlayer FromJSON(string json)
        {
            GamePlayerAPIMorph morph = new GamePlayerAPIMorph();
            return (GamePlayer)morph.ToBusinessFormat(
                JsonConvert.DeserializeObject<GamePlayerAPI>(json));
        }

        /// <summary>
        /// Returns a string that represents the GamePlayer instance.
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static string ToJSON(GamePlayer item)
        {
            GamePlayerAPIMorph morph = new GamePlayerAPIMorph();
            return JsonConvert.SerializeObject(morph.ToAPIFormat(item));
        }
    }
}
