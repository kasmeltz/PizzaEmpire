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
				
		#region GUIItem
				
		public override void Render ()
		{
			GUI.Box(Rectangle, Content, Style);
		}
		
		public override GUIItem Clone ()
		{
			GUIItemBox item = GUIItemFactory<GUIItemBox>.Instance.Pool.New();
			item.CopyFrom(this);
			return item;
		}	
		
		public override void Destroy()
		{
			GUIItemFactory<GUIItemBox>.Instance.Pool.Store(this);
		}		
		
		#endregion
	}	
}