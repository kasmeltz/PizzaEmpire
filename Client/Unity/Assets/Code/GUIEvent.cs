namespace KS.PizzaEmpire.Unity
{
	/// Reprents a GUI event
	public class GUIEvent
	{
		public GUIElementEnum Element { get; set; }
		public GUIEventEnum GEvent { get; set; }		
		public static GUIEvent Empty = new GUIEvent { Element = GUIElementEnum.None, GEvent = GUIEventEnum.None };	
				
		/// <summary>
		/// Determines whether the passed in GUI Event matches the 
		/// details in the instance.
		/// </summary>
		public bool IsSame(GUIEvent other)
		{
			return 	other.Element == Element &&
					other.GEvent == GEvent;
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="KS.PizzaEmpire.Unity.GUIEvent"/> class.
		/// </summary>
		public GUIEvent()
		{
		}
	}
}