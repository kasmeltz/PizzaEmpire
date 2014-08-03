namespace KS.PizzaEmpire.Unity
{
	/// <summary>	
	/// Reprents a GUI event
	/// </summary>	
	public struct GUIEvent	
	{
		public GUIElementEnum Element;
		public GUIEventEnum GEvent;
			
		public static GUIEvent Empty = new GUIEvent { Element = GUIElementEnum.None, GEvent = GUIEventEnum.None };	
				
		/// <summary>
		/// Initializes a new instance of the <see cref="KS.PizzaEmpire.Unity.GUIEvent"/> struct.
		/// </summary>
		/// <param name="el">The element enum to associate with this event</param>
		/// <param name="ge">The event enum that this event represents</param>
		public GUIEvent(GUIElementEnum el, GUIEventEnum ge)
		{
			this.Element = el;
			this.GEvent = ge;
		}
		
		/// <summary>
		/// Equalty operator
		/// </summary>
		public static bool operator ==(GUIEvent x, GUIEvent y) 
		{
			return 	x.Element == y.Element &&
				x.GEvent == y.GEvent;
		}	
		
		/// <summary>
		/// Inequality operator
		/// </summary>
		public static bool operator !=(GUIEvent x, GUIEvent y) 
		{
			return 	x.Element != y.Element ||
				x.GEvent != y.GEvent;
		}		
	}
}