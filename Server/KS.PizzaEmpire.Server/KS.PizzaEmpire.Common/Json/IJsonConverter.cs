
namespace KS.PizzaEmpire.Common.Utility
{
    /// <summary>
    /// Represents an item that can convert an object to Json and 
    /// from Json to an object
    /// </summary>
    public interface IJsonConverter
    {
        string Serlialize<T>(T item);
        T Deserialize<T>(string json);
    }
}
