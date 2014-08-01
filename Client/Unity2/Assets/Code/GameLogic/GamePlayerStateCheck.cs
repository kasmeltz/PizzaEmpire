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
			ItemsGreaterThan = new List<ItemQuantity>();
			WorkItemsInProgress = new List<ItemQuantity>();
		}

        public List<ItemQuantity> ItemsGreaterThan { get; set; }
        public List<ItemQuantity> WorkItemsInProgress { get; set; }
        public int? RequiredLevel { get; set; }
        public BuildableItemEnum CanBuildItem { get; set; }

		/// <summary>
		/// Checks the work items in progress.
		/// </summary>
		/// <param name="player">Player.</param>
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

            if (WorkItemsInProgress != null)
            {
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

            return true;
		}
		
		/// <summary>
		/// Copies the state from another instance
		/// </summary>
		/// <param name="other">The GamePlayerStateCheck to copy from</param>
		public void CopyFrom(GamePlayerStateCheck other)
		{
			//public List<ItemQuantity> ItemsGreaterThan { get; set; }
			//public List<ItemQuantity> WorkItemsInProgress { get; set; }
			ItemsGreaterThan.Clear();
			for(int i = 0;i < other.ItemsGreaterThan.Count;i++)
			{
				ItemsGreaterThan.Add(other.ItemsGreaterThan[i]);
			}
			
			WorkItemsInProgress.Clear();			
			for(int i = 0;i < other.WorkItemsInProgress.Count;i++)
			{
				WorkItemsInProgress.Add(other.WorkItemsInProgress[i]);
			}
			
			RequiredLevel = other.RequiredLevel;
			CanBuildItem = other.CanBuildItem;
		}
		
		#region IResetable
		
		public void Reset()
		{
			ItemsGreaterThan.Clear();
			WorkItemsInProgress.Clear();
			RequiredLevel = null;
			CanBuildItem = BuildableItemEnum.None;
		}
				
		#endregion
	}
}