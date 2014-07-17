using KS.PizzaEmpire.Business.Logic;

namespace KS.PizzaEmpire.Business.Conversion
{
    /// <summary>
    /// Defines an item that can be converted into a logic entity.
    /// </summary>
    public interface IToLogiEntity
    {
        /// <summary>
        /// Returns a new instance of the appropriate ILogicEntity with cloned data.
        /// </summary>
        /// <returns></returns>
        ILogicEntity ToLogicEntity();
    }
}
