namespace KS.PizzaEmpire.Unity
{	
	using System.Collections.Generic;
	
	/// <summary>
	/// Represents the state for a player of the game as used in the game logic.
	/// </summary>
	public class GamePlayer
	{
		/// <summary>
		/// Creates a new instance of the GamePlayer class.
		/// </summary>
		public GamePlayer() { }
		
		/// <summary>
		/// The information fow how this entity should be stored in different types of storage
		/// </summary>
		public object StorageInformation { get; set; }
		
		/// <summary>
		/// The number of coins owned by the player
		/// </summary>
		public int Coins { get; set; }
		
		/// <summary>
		/// The number of coupons owned by the player
		/// </summary>
		public int Coupons { get; set; }
		
		/// <summary>
		/// The current experience of the player
		/// </summary>
		public int Experience { get; set; }
		
		/// <summary>
		/// The players current level
		/// </summary>
		public int Level { get; set; }
		
		/// <summary>
		/// The players inventory of items
		/// </summary>
		public Dictionary<BuildableItemEnum, int> BuildableItems { get; set; }
		
		/// <summary>
		/// The work in progress for the player
		/// </summary>
		public List<int> WorkItems { get; set; }		
	}
}