namespace KS.PizzaEmpire.Unity
{
	using UnityEngine;
	using Common.BusinessObjects;
	using Common.GameLogic;
	
	/// <summary>
	/// Represents a progress bar 
	/// </summary>
	public class WorkItemProgressBar : GUIProgressBar
	{	
		/// <summary>
		/// Creates a new instace of the WorkItemProgressBar class
		/// </summary>
		public WorkItemProgressBar()
			: base ()
		{
			Animated = true;
			MinValue = 0;
			MaxValue = 1;
		}			
		
		/// <summary>
		/// The WorkItem that determines the length of the progess bar
		/// </summary>
		/// <value>The work item.</value>
		public WorkInProgress WorkInProgress { get; set; }
		
		public override void Animate (float dt)
		{
			Value = (float)GamePlayerLogic.Instance
				.GetPercentageCompleteForWorkItem(WorkInProgress);
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
			
			WorkInProgress = bar.WorkInProgress; 
		}
		
		public override void Reset ()
		{
			base.Reset ();
			
			Animated = true;
			MinValue = 0;
			MaxValue = 1;
			WorkInProgress = null;
		}		
		
		#endregion
	}	
}