namespace KS.PizzaEmpire.Unity
{
	using System;
	using UnityEngine;
	
	/// <summary>
	/// Represents a button that performs some action
	/// when click
	/// </summary>
	public class GUIWindow : GUIItem
	{	
		/// <summary>
		/// Creates a new instace of the GUIItemButton class
		/// </summary>
		public GUIWindow()
			: base ()
		{
		}
		
		public void Toggle()
		{
			if (Visible)
			{
				Close();
			}
			else
			{
				Open();
			}
		}
		
		public virtual void Open() 
		{
			Visible = true;
		}
		
		public virtual void Close() 
		{ 
			Visible = false;
		}
				
		#region GUIItem
		
		public override void Render ()
		{
			GUI.DrawTexture(Rectangle, Content.image);
		}
		
		public override GUIItem Clone ()
		{
			GUIWindow item = GUIItemFactory<GUIWindow>.Instance.Pool.New();
			item.CopyFrom(this);
			return item;
		}	
		
		public override void Destroy()
		{
			GUIItemFactory<GUIWindow>.Instance.Pool.Store(this);
		}		
		
		#endregion		
	}	
}