using KS.PizzaEmpire.Business.TableStorage;

namespace KS.PizzaEmpire.Business.Conversion
{
    /// <summary>
    /// Defines an item that can be converted into a table storage entity.
    /// </summary>
    public interface IToTableStorageEntity
    {
        /// <summary>
        /// Returns a new instance of the appropriate ITableStorageEntity with cloned data.
        /// </summary>
        /// <returns></returns>
        ITableStorageEntity ToTableStorageEntity();
    }
}
