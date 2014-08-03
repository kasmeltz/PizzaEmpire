namespace KS.PizzaEmpire.Unity
{
	using UnityEngine;
	
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
		
		/// <summary>
		/// Creates a new instance of the GUIItemImage class
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="w">The width.</param>
		/// <param name="h">The height.</param>
		public GUIItemImage(float x, float y, float w, float h) 
			: base(x, y, w, h)
		{
		}
		
		#region GUIItem
				
		public override void Render ()
		{
			GUI.DrawTexture(Rectangle, Texture);
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