namespace KS.PizzaEmpire.Unity
{	
	/// <summary>
	/// Represents an item that returns true or false
	/// depending if the state of a passed in GamePlayer
	/// instance matches the expected state
	/// </summary>
	public class GamePlayerStateCheck
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="KS.PizzaEmpire.Unity.GamePlayerStateCheck"/> class.
		/// </summary>
		public GamePlayerStateCheck() { }
	
		public ItemQuantity[] ItemsGreaterThan;
		
		/// <summary>
		/// Returns true if the given GamePlayer stats matches the criteria contained
		/// in the instance of this object.
		/// </summary>
		/// <returns><c>true</c>, if state matches, <c>false</c> otherwise.</returns>
		/// <param name="player">The persistent player data.</param>
		public bool IsTrue(GamePlayer player)
		{
			bool shouldAdvance = false;
			
			
			return shouldAdvance;
		}
	}
}