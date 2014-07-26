namespace KS.PizzaEmpire.Unity
{
	using System;
	
	/// <summary>
	/// Represents ongoing work that will produce some finished item(s) after some length of time
	/// as used by the game logic.
	/// </summary>
	public class WorkItem
	{
		/// <summary>
		/// Creates a new instance of the WorkItem class.
		/// </summary>
		public WorkItem() { }
		
		public BuildableItemEnum ItemCode { get; set; }
		public DateTime FinishTime { get; set; }
	}
}