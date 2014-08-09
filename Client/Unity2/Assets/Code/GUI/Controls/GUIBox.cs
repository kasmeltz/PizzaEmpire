namespace KS.PizzaEmpire.Unity
{
	using UnityEngine;
	
	/// <summary>
	/// Represents a box with some text
	/// </summary>
	public class GUIBox : GUIItem
	{	
		/// <summary>
		/// Creates a new instace of the GUIItemBox class
		/// </summary>
		public GUIBox()
			: base ()
		{
		}
				
		#region GUIItem
				
		public override void Render ()
		{
			GUI.Box(Rectangle, Content, Style);
		}
		
		public override GUIItem Clone ()
		{
			GUIBox item = GUIItemFactory<GUIBox>.Instance.Pool.New();
			item.CopyFrom(this);
			return item;
		}	
		
		public override void Destroy()
		{
			GUIItemFactory<GUIBox>.Instance.Pool.Store(this);
		}		
		
		#endregion
	}	
}