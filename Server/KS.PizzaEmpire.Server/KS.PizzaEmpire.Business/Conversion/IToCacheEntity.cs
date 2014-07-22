namespace KS.PizzaEmpire.Business.Conversion
{
    using KS.PizzaEmpire.Business.Cache;

    /// <summary>
    /// Defines an item that can be converted into a cache entity.
    /// </summary>
    public interface IToCacheEntity
    {
        /// <summary>
        /// Returns a new instance of the appropriate ICacheEntity with cloned data.
        /// </summary>
        /// <returns></returns>
        ICacheEntity ToCacheEntity();
    }
}
