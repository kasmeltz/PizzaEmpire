namespace KS.PizzaEmpire.Unity
{		
	using LitJson;	
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
		public Dictionary<string, int> BuildableItems { get; set; }
		
		/// <summary>
		/// The work in progress for the player
		/// </summary>
		public List<WorkItem> WorkItems { get; set; }
	}
}