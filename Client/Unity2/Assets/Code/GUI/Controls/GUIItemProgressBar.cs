namespace KS.PizzaEmpire.Unity
{
	using UnityEngine;
	
	/// <summary>
	/// Represents a progress bar 
	/// </summary>
	public class GUIItemProgressBar : GUIItem
	{	
		/// <summary>
		/// Creates a new instace of the GUIItemProgressBar class
		/// </summary>
		public GUIItemProgressBar()
			: base ()
		{
			TexCoords = new Rect(0, 0, 0, 0);
		}			
				
		/// <summary>
		/// The texture that will be used for the inner bar.
		/// </summary>
		/// <value>The bar inner.</value>
		public Texture2D BarInner { get; set; }
		
		/// <summary>
		/// The minimum progress value
		/// </summary>
		public float MinValue { get; set; }
		
		/// <summary>
		/// The maximum progress value
		/// </summary>
		public float MaxValue { get; set; }
		
		/// <summary>
		/// The Value of the bar
		/// </summary>
		private float _value;
		public float Value 
		{ 
			get
			{
				return _value;
		 	} 
			set 
			{
				_value = value;
				SetWidth();
			} 
		}			
		
		/// <summary>
		/// The texture coordinates for the inner bar
		/// </summary>
		public Rect TexCoords;
		
		/// <summary>
		/// The size of the inner bar
		/// </summary>		
		public Rect InnerRect;
		
		/// <summary>
		/// Sets the width of the bar
		/// </summary>
		public void	SetWidth()
		{
			float ratio = (_value - MinValue) / (MaxValue - MinValue);
			Rect rectangle = TexCoords;
			rectangle.x = 0;
			rectangle.y = 0;
			rectangle.width = 1 * ratio;
			rectangle.height = 1;
			TexCoords = rectangle;
			rectangle = InnerRect;
			rectangle.x = Rectangle.x;
			rectangle.y = Rectangle.y;
			rectangle.width = Rectangle.width * ratio;
			rectangle.height = Rectangle.height;
			InnerRect = rectangle;
		}
		
		#region GUIItem
		
		public override void Render ()
		{			
			GUI.DrawTexture(Rectangle, Content.image);
			GUI.DrawTextureWithTexCoords(InnerRect, BarInner, TexCoords);
		}
		
		public override GUIItem Clone ()
		{
			GUIItemProgressBar item = GUIItemFactory<GUIItemProgressBar>.Instance.Pool.New();
			item.CopyFrom(this);
			return item;
		}	
		
		public override void Destroy()
		{
			GUIItemFactory<GUIItemProgressBar>.Instance.Pool.Store(this);
		}
		
		/// <summary>
		/// Copies the state from another instance
		/// </summary>
		/// <param name="other">The GUIItem to copy from</param>
		public override void CopyFrom (GUIItem other)
		{
			base.CopyFrom (other);
			
			GUIItemProgressBar bar = other as GUIItemProgressBar;
			if (bar == null)
			{
				return;
			}
			
			BarInner = bar.BarInner;			
			MinValue = bar.MinValue;			
			MaxValue = bar.MaxValue;			
			Value = bar.Value;	
			Rect rectangle = TexCoords;
			rectangle.x = bar.TexCoords.x;
			rectangle.y = bar.TexCoords.y;
			rectangle.width = bar.TexCoords.width;
			rectangle.height = bar.TexCoords.height;
			TexCoords = rectangle;
		}
		
		public override void Reset ()
		{
			base.Reset ();
			
			BarInner = null;			
			_value = 0;
			MinValue = 0;
			MaxValue = 0;	
			Rect rectangle = TexCoords;
			rectangle.x = 0;
			rectangle.y = 0;
			rectangle.width = 0;
			rectangle.height = 0;
			TexCoords = rectangle;
		}		
		
		#endregion
	}	
}