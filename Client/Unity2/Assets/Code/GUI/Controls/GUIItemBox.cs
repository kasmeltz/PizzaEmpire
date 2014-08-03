namespace KS.PizzaEmpire.Unity
{
	using UnityEngine;
	
	/// <summary>
	/// Represents a box with some text
	/// </summary>
	public class GUIItemBox : GUIItem
	{	
		/// <summary>
		/// Creates a new instace of the GUIItemBox class
		/// </summary>
		public GUIItemBox()
			: base ()
		{
		}
		
		/// <summary>
		/// Creates a new instance of the GUIItemBox class
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="w">The width.</param>
		/// <param name="h">The height.</param>
		public GUIItemBox(float x, float y, float w, float h) 
			: base(x, y, w, h)
		{
		}
				
		public override void Render ()
		{
			GUI.Box(Rectangle, Text, Style);
		}
	}	
}