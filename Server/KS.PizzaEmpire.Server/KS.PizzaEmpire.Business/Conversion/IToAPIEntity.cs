using KS.PizzaEmpire.Business.API;

namespace KS.PizzaEmpire.Business.Conversion
{
    /// <summary>
    /// Defines an item that can be converted into an API entity.
    /// </summary>
    public interface IToAPIEntity
    {
        /// <summary>
        /// Returns a new instance of the appropriate IAPIEntity with cloned data.
        /// </summary>
        /// <returns></returns>
        IAPIEntity ToAPIEntity();
    }
}
