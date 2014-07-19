using KS.PizzaEmpire.Business.StorageInformation;

namespace KS.PizzaEmpire.Business.Logic
{
    /// <summary>
    /// Interface to mark classes that are used for business logic.
    /// </summary>
    public interface ILogicEntity
    {
        IStorageInformation StorageInformation { get; set; }
    }
}
