///
/// Code audo generated by a tool other than you.
///
namespace KS.PizzaEmpire.Common.APITransfer
{
	using BusinessObjects;
	using System;
	using System.Collections.Generic;

	/// <summary>
	/// Represents an item that can be built in the game
	/// </summary>
	public class BuildableItemAPI : IAPIEntity
	{
		/// <summary>
		/// Creates a new instance of the BuildableItemAPI class.
		/// </summary>
		public BuildableItemAPI() { }

		/// <summary>
		/// Identifies the type of the item
		/// </summary>
		public BuildableItemEnum ItemCode { get; set; }

		/// <summary>
		/// The level required to build the item
		/// </summary>
		public int RequiredLevel { get; set; }

		/// <summary>
		/// The cost in coins to build the item
		/// </summary>
		public int CoinCost { get; set; }

		/// <summary>
		/// The item that is required to produce this item
		/// </summary>
		public BuildableItemEnum ProductionItem { get; set; }

		/// <summary>
		/// The production capacity of the item
		/// </summary>
		public int ProductionCapacity { get; set; }

		/// <summary>
		/// The base amount of items that are produced when work is completed
		/// </summary>
		public int BaseProduction { get; set; }

		/// <summary>
		/// The maximum number of items this item can store
		/// </summary>
		public int StorageCapacity { get; set; }

		/// <summary>
		/// The item this item should be stored in
		/// </summary>
		public BuildableItemEnum StorageItem { get; set; }

		/// <summary>
		/// Whether the item is used for storage
		/// </summary>
		public bool IsStorage { get; set; }

		/// <summary>
		/// Whether the item is consumed if required for other items
		/// </summary>
		public bool IsConsumable { get; set; }

		/// <summary>
		/// Whether work on this item finishes immediately
		/// </summary>
		public bool IsImmediate { get; set; }

		/// <summary>
		/// Whether doing work subtracts from the quantity of this item
		/// </summary>
		public bool IsWorkSubtracted { get; set; }

		/// <summary>
		/// The experience gained when this item is built
		/// </summary>
		public int Experience { get; set; }

		/// <summary>
		/// The number of seconds required to build this item
		/// </summary>
		public int BuildSeconds { get; set; }

		/// <summary>
		/// The number of coupons required to build this item
		/// </summary>
		public int CouponCost { get; set; }

		/// <summary>
		/// The number of coupons required to speed up this item
		/// </summary>
		public int SpeedUpCoupons { get; set; }

		/// <summary>
		/// The number of seconds this item will be sped up by specnding coupons
		/// </summary>
		public int SpeedUpSeconds { get; set; }

		/// <summary>
		/// The items required to build this item
		/// </summary>
		public List<ItemQuantityAPI> RequiredItems { get; set; }
	}
}