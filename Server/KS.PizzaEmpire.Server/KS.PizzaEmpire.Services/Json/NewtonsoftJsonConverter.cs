namespace KS.PizzaEmpire.Services.Json
{
    using Newtonsoft.Json;
    using Common.Utility;

    /// <summary>
    /// Represents an item that can convert to / from Json using the Newtonsoft library
    /// </summary>
    public class NewtonsoftJsonConverter : IJsonConverter
    {
        #region IJsonConverter

        public string Serlialize<T>(T item)
        {
            return JsonConvert.SerializeObject(item);
        }

        public T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        #endregion
    }
}
