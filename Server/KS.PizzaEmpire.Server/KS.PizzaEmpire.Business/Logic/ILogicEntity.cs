
namespace KS.PizzaEmpire.Business.Logic
{
    /// <summary>
    /// Interface to mark classes that are used for business logic.
    /// </summary>
    public interface ILogicEntity
    {
        /// <summary>
        /// The CacheKey for this item
        /// </summary>
        string CacheKey { get; }
    }
}
