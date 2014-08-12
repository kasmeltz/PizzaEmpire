namespace KS.PizzaEmpire.Unity
{
	using UnityEngine;
	using System;
	
	/// <summary>
	/// Represents an image
	/// </summary>
	public class GUIImage : GUIItem
	{	
		/// <summary>
		/// Creates a new instace of the GUIItemImage class
		/// </summary>
		public GUIImage()
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
			GUIImage item = GUIItemFactory<GUIImage>.Instance.Pool.New();
			item.CopyFrom(this);
			return item;
		}	
		
		public override void Destroy()
		{
			GUIItemFactory<GUIImage>.Instance.Pool.Store(this);
		}		
				
		#endregion		
	}	
}