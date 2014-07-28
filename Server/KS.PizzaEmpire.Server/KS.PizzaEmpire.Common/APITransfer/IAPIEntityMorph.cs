using KS.PizzaEmpire.Common.BusinessObjects;

namespace KS.PizzaEmpire.Common.APITransfer
{
    /// <summary>
    /// Defines an interface for morphing business objects to API dto objects
    /// </summary>
    public interface IAPIEntityMorph
    {
        /// <summary>
        /// Converts a business object to an API dto object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        IAPIEntity ToAPIFormat(IBusinessObjectEntity entity);

        /// <summary>
        /// Converts an API dto objec to a business object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        IBusinessObjectEntity ToBusinessFormat(IAPIEntity entity);            
    }
}
