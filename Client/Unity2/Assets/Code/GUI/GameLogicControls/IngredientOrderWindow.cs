namespace KS.PizzaEmpire.Unity
{
	using UnityEngine;
	using Common.BusinessObjects;
	using Common.GameLogic;
	using System.Collections.Generic;
	
	/// <summary>
	/// Represents a progress bar 
	/// </summary>
	public class IngredientOrderWindow : GUIWindow
	{	
		/// <summary>
		/// Creates a new instace of the WorkItemProgressBar class
		/// </summary>
		public IngredientOrderWindow(GamePlayer player)
			: base ()
		{
			CurrentOrder = new List<ItemQuantity>();
			Player = player;
		}			
					
		public GamePlayer Player { get; set; }
		public List<ItemQuantity> CurrentOrder;	
	
		public override void Open ()
		{	
			CurrentOrder.Clear();	
				
			base.Open();	
		}
	
		public override void Close()
		{
			base.Close();
		}
	}	
}