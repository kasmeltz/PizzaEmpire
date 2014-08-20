namespace KS.PizzaEmpire.Common.BusinessObjects
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents one of the business locations for a game player
    /// e.g. a Restaurant or Head office
    /// 
    /// </summary>
    public class BusinessLocation
    {
        /// <summary>
        /// Creates a new instance of the BusinessLocation class
        /// </summary>
        public BusinessLocation() { }

        /// <summary>
        /// The items at this location
        /// </summary>
        public LocationStorage Storage { get; set; }
    }
}
