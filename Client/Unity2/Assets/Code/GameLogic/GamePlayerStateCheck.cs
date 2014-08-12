namespace KS.PizzaEmpire.Unity
{	
	using System;
	using System.Collections.Generic;
	using UnityEngine;
	using Common;
	using Common.GameLogic;
	using Common.BusinessObjects;
	using Common.ObjectPool;
	
	
	/// <summary>
	/// Represents an item that returns true or false
	/// depending if the state of a passed in GamePlayer
	/// instance matches the expected state
	/// </summary>
	public class GamePlayerStateCheck : IResetable
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="KS.PizzaEmpire.Unity.GamePlayerStateCheck"/> class.
		/// </summary>
		public GamePlayerStateCheck() 
		{ 
			WorkItemsInProgress = new List<ItemQuantity>();
		}

		public List<ItemQuantity> ItemQuantityLessThan { get; set; }
        public List<ItemQuantity> WorkItemsInProgress { get; set; }
        public int? RequiredLevel { get; set; }
        public BuildableItemEnum CanBuildItem { get; set; }
        public int? Coins { get; set; }
        public int? TutorialStage { get; set; }

		/// <summary>
		/// Checks whether the player has less than the
		/// indicated number of items
		/// </summary>
		/// <param name="player">The player to check.</param>
		public bool CheckItemsLessThan(GamePlayer player)
		{
			if (ItemQuantityLessThan == null || 
			    ItemQuantityLessThan.Count == 0)
			{
				return true;
			}

			for (int i = 0; i < ItemQuantityLessThan.Count; i++)
			{
				ItemQuantity checkIQ = ItemQuantityLessThan[i];
				if (player.BuildableItems.ContainsKey(checkIQ.ItemCode) && 
				    player.BuildableItems[checkIQ.ItemCode] >= checkIQ.Quantity)
				{
					return false;
				}
			}

			return true;
		}

		/// <summary>
		/// Checks the work items in progress.
		/// </summary>
		/// <param name="player">The player to check.</param>
		public bool CheckWorkItemsInProgress(GamePlayer player)
		{
            if (WorkItemsInProgress == null || 
                WorkItemsInProgress.Count == 0)
            {
                return true;
            }

			Dictionary<BuildableItemEnum, int> playerWis = new Dictionary<BuildableItemEnum, int>();
            for (int i = 0; i < player.WorkItems.Count; i++)
	        {
	            WorkItem playerWi = player.WorkItems[i];
	            if (!playerWis.ContainsKey(playerWi.ItemCode))
	            {
	                playerWis[playerWi.ItemCode] = 1;
	            }
	            else
	            {
	                playerWis[playerWi.ItemCode]++;
	            }
	        }

	        for (int i = 0; i < WorkItemsInProgress.Count; i++)
	        {
	            ItemQuantity checkWi = WorkItemsInProgress[i];
	            if (!playerWis.ContainsKey(checkWi.ItemCode))
	            {
	                return false;
	            }
	            if (playerWis[checkWi.ItemCode] < checkWi.Quantity)
	            {
	                return false;
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
            if (!CheckWorkItemsInProgress(player))
            {
                return false;
            }

			if (!CheckItemsLessThan(player)) 
			{
				return false;			
			}

            if (RequiredLevel.HasValue && 
                player.Level < RequiredLevel)
            {
                return false;
            }
            
			if (CanBuildItem != BuildableItemEnum.None)
			{
				if (GamePlayerLogic.Instance.CanBuildItem(player, CanBuildItem) != ErrorCode.ERROR_OK)
				{
					return false;
				}
				
			}
			
			if (Coins.HasValue)
			{
				if (player.Coins < Coins)
				{
					return false;
				}
			}
			
			if (TutorialStage.HasValue)
			{
				if (player.TutorialStage < TutorialStage)
				{
					return false;
				}
			}

            return true;
		}
		
		/// <summary>
		/// Copies the state from another instance
		/// </summary>
		/// <param name="other">The GamePlayerStateCheck to copy from</param>
		public void CopyFrom(GamePlayerStateCheck other)
		{
			WorkItemsInProgress.Clear();			
			for(int i = 0;i < other.WorkItemsInProgress.Count;i++)
			{
				WorkItemsInProgress.Add(other.WorkItemsInProgress[i]);
			}
			
			RequiredLevel = other.RequiredLevel;
			CanBuildItem = other.CanBuildItem;
			Coins = other.Coins;
			TutorialStage = other.TutorialStage;
		}
		
		#region IResetable
		
		public void Reset()
		{
			WorkItemsInProgress.Clear();
			RequiredLevel = null;
			CanBuildItem = BuildableItemEnum.None;
			TutorialStage = null;
		}
				
		#endregion
	}
}