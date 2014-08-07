///
/// Code audo generated by a tool other than you.
///
namespace KS.PizzaEmpire.Common.BusinessObjects
{
	using System;
	using System.Collections.Generic;

	/// <summary>
	/// Represents a quantity of some item
	/// </summary>
	public class ItemQuantity : IBusinessObjectEntity
	{
		/// <summary>
		/// Creates a new instance of the ItemQuantity class.
		/// </summary>
		public ItemQuantity() { }

		/// <summary>
		/// The type of item this quanity is for
		/// </summary>
		public BuildableItemEnum ItemCode { get; set; }

		/// <summary>
		/// The quantity of the item
		/// </summary>
		public int Quantity { get; set; }
	}
}