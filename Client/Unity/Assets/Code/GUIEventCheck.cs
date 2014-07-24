namespace KS.PizzaEmpire.Unity
{	
	/// <summary>
	/// Represents an item that returns true or false
	/// depending if the GUI event passed in 
	/// match the expected GUI event
	/// </summary>
	public class GUIEventCheck
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="KS.PizzaEmpire.Unity.GUIEventCheck"/> class.
		/// </summary>
		public GUIEventCheck(){}
	
		/// <summary>
		/// Determines whether the passed in GUI Event matches the 
		/// details in the instance.
		/// </summary>
		public bool IsSame(GUIEvent guiEvent)
		{
			bool isSame = true;
			
			return isSame;
		}
	}
}