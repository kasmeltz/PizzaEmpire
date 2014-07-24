namespace KS.PizzaEmpire.Unity
{
	/// <summary>
	/// Represents an item quanity as used by the game logic
	/// </summary>
	public class ItemQuantity
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="KS.PizzaEmpire.Unity.ItemQuantity"/> class.
		/// </summary>
		public ItemQuantity() { }
		
		public BuildableItemEnum ItemCode { get; set; }
		public int Quantity { get; set; }
	}
}