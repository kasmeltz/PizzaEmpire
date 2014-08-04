namespace KS.PizzaEmpire.Unity
{
	using System;
	using UnityEngine;
	
	/// <summary>
	/// Represents a button that performs some action
	/// when click
	/// </summary>
	public class GUIItemButton : GUIItem
	{	
		/// <summary>
        /// Creates a new instace of the GUIItemButton class
		/// </summary>
		public GUIItemButton()
			: base ()
		{
		}
		
		/// <summary>
		/// Creates a new instance of the GUIItemButton class
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="w">The width.</param>
		/// <param name="h">The height.</param>
		public GUIItemButton(float x, float y, float w, float h) 
			: base(x, y, w, h)
		{
		}
		
		/// <summary>
		/// The action to be performed when this button is clicked
		/// </summary>
		/// <value>The on click.</value>
		public Action<GUIItem> OnClick { get; set; }
		
		#region GUIItem
				
		public override void Render ()
		{
			if (GUI.Button(Rectangle, Texture, Style))
			{
				OnClick(this);
			}
		}
		
		public override GUIItem Clone ()
		{
			GUIItemButton item = GUIItemFactory<GUIItemButton>.Instance.Pool.New();
			item.CopyFrom(this);
			return item;
		}	
		
		public override void Destroy()
		{
			GUIItemFactory<GUIItemButton>.Instance.Pool.Store(this);
		}		
		
		/// <summary>
		/// Copies the state from another instance
		/// </summary>
		/// <param name="other">The GUIItem to copy from</param>
		public override void CopyFrom (GUIItem other)
		{
			base.CopyFrom (other);
			
			GUIItemButton button = other as GUIItemButton;
			if (button == null)
			{
				return;
			}
						
			OnClick = button.OnClick;			
		}
		
		public override void Reset ()
		{
			base.Reset ();
			
			OnClick = null;
		}
		
		#endregion		
	}	
}