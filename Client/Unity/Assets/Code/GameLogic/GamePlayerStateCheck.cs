namespace KS.PizzaEmpire.Unity
{	
	using System;
	using System.Collections.Generic;
	using UnityEngine;
	using Common.BusinessObjects;
	
	/// <summary>
	/// Represents an item that returns true or false
	/// depending if the state of a passed in GamePlayer
	/// instance matches the expected state
	/// </summary>
	public class GamePlayerStateCheck
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="KS.PizzaEmpire.Unity.GamePlayerStateCheck"/> class.
		/// </summary>
		public GamePlayerStateCheck() { }
	
		public ItemQuantity[] ItemsGreaterThan;
		public ItemQuantity[] WorkItemsInProgress;		   
		
		/// <summary>
		/// Checks the work items in progress.
		/// </summary>
		/// <param name="player">Player.</param>
		public bool CheckWorkItemsInProgress(GamePlayer player)
		{
			Dictionary<BuildableItemEnum, int> playerWis = new Dictionary<BuildableItemEnum, int>();
			foreach(WorkItem playerWi in player.WorkItems)
			{
				if (!playerWis.ContainsKey(playerWi.ItemCode))
				{
					playerWis[playerWi.ItemCode] = 1;
				}
				else
				{
					playerWis[playerWi.ItemCode]++;
				}
			}
			
			if (WorkItemsInProgress != null)
			{
				foreach(ItemQuantity checkWi in WorkItemsInProgress)
				{
					if (!playerWis.ContainsKey(checkWi.ItemCode))
					{
						return false;
					}
					if (playerWis[checkWi.ItemCode] < checkWi.Quantity)
					{
						return false;
					}
				}					
			}
			
			return true;
		}
		
		/// <summary>
		/// Returns true if the given GamePlayer stats matches the criteria contained
		/// in the instance of this object.
		/// </summary>
		/// <returns><c>true</c>, if state matches, <c>false</c> otherwise.</returns>
		/// <param name="player">The persistent player data.</param>
		public bool CheckAll(GamePlayer player)
		{
			bool shouldAdvance = true;
			
			shouldAdvance = CheckWorkItemsInProgress(player);			
			
			return shouldAdvance;
		}
	}
}