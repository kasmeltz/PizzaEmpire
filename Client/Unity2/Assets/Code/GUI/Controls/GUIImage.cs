namespace KS.PizzaEmpire.Unity
{
	using UnityEngine;
	using System;
	
	/// <summary>
	/// Represents an image
	/// </summary>
	public class GUIItemImage : GUIItem
	{	
		/// <summary>
		/// Creates a new instace of the GUIItemImage class
		/// </summary>
		public GUIItemImage()
			: base ()
		{
			
		}		
		
		#region GUIItem
				
		public override void Render ()
		{
			GUI.DrawTexture(Rectangle, Content.image, Scale);
		}
		
		public override GUIItem Clone ()
		{
			GUIItemImage item = GUIItemFactory<GUIItemImage>.Instance.Pool.New();
			item.CopyFrom(this);
			return item;
		}	
		
		public override void Destroy()
		{
			GUIItemFactory<GUIItemImage>.Instance.Pool.Store(this);
		}		
				
		#endregion		
	}	
}