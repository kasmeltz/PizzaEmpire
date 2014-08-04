namespace KS.PizzaEmpire.Unity
{
	using UnityEngine;
	using Common.BusinessObjects;
	using Common.GameLogic;
	
	/// <summary>
	/// Represents a progress bar 
	/// </summary>
	public class WorkItemProgressBar : GUIItemProgressBar
	{	
		/// <summary>
		/// Creates a new instace of the WorkItemProgressBar class
		/// </summary>
		public WorkItemProgressBar()
			: base ()
		{
			MinValue = 0;
			MaxValue = 1;
			Animated = true;
		}			
		
		/// <summary>
		/// Creates a new instance of the WorkItemProgressBar class
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="w">The width.</param>
		/// <param name="h">The height.</param>
		public WorkItemProgressBar(float x, float y, float w, float h) 
			: base(x, y, w, h)
		{
			MinValue = 0;
			MaxValue = 1;
			Animated = true;		
		}				
		
		/// <summary>
		/// The WorkItem that determines the length of the progess bar
		/// </summary>
		/// <value>The work item.</value>
		public WorkItem WorkItem { get; set; }
		
		public override void Animate (float dt)
		{
			Value = (float)GamePlayerLogic.Instance
				.GetPercentageCompleteForWorkItem(WorkItem);
		}
						
		#region GUIItem
				
		public override GUIItem Clone ()
		{
			WorkItemProgressBar item = GUIItemFactory<WorkItemProgressBar>.Instance.Pool.New();
			item.CopyFrom(this);
			return item;
		}	
		
		public override void Destroy()
		{
			GUIItemFactory<WorkItemProgressBar>.Instance.Pool.Store(this);
		}
		
		/// <summary>
		/// Copies the state from another instance
		/// </summary>
		/// <param name="other">The GUIItem to copy from</param>
		public override void CopyFrom (GUIItem other)
		{
			base.CopyFrom (other);
			
			WorkItemProgressBar bar = other as WorkItemProgressBar;
			if (bar == null)
			{
				return;
			}
			
			WorkItem = bar.WorkItem; 
		}
		
		public override void Reset ()
		{
			base.Reset ();
			
			WorkItem = null;
		}		
		
		#endregion
	}	
}