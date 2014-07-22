namespace KS.PizzaEmpire.Business.Conversion
{
    using Logic;

    /// <summary>
    /// Defines an item that can be converted into a logic entity.
    /// </summary>
    public interface IToLogicEntity
    {
        /// <summary>
        /// Returns a new instance of the appropriate ILogicEntity with cloned data.
        /// </summary>
        /// <returns></returns>
        ILogicEntity ToLogicEntity();
    }
}
