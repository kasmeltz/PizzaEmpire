namespace KS.PizzaEmpire.Unity
{	
	using System.Collections.Generic;
	using UnityEngine;
	
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
		/// Returns true if the player has the capacity to start hhe item.
		/// </summary>
		/// <param name="player"></param>
		/// <param name="itemCode"></param>
		/// <returns></returns>
		public bool DoesPlayerHaveCapacity(GamePlayer player, BuildableItemEnum itemCode)
		{
			BuildableItem capacityItem = null;
			
			BuildableItem bi = ItemManager.Instance.BuildableItems[itemCode];
			foreach (ItemQuantity iq in bi.RequiredItems)
			{
				BuildableItem ri = ItemManager.Instance.BuildableItems[iq.ItemCode];
				if (ri.Capacity > 0)
				{
					capacityItem = ri;
					break;
				}
			}
			
			if (capacityItem == null)
			{
				return true;
			}
			
			int inUse = 0;
			foreach (WorkItem wi in player.WorkItems)
			{
				BuildableItem wbi = ItemManager.Instance.BuildableItems[wi.ItemCode];
				foreach(ItemQuantity iq in wbi.RequiredItems)
				{
					if (iq.ItemCode == capacityItem.ItemCode)
					{
						inUse++;
					}
				}
			}
			
			return inUse < capacityItem.Capacity;
		}
						
		/// <summary>
		/// Returns true if the player can build the specific item.
		/// </summary>
		/// <param name="player"></param>
		/// <param name="item"></param>
		/// <returns></returns>
		public bool CanBuildItem(GamePlayer player, BuildableItemEnum itemCode)
		{            		
			if (!ItemManager.Instance.BuildableItems.ContainsKey(itemCode))
			{
				Debug.Log("Attempt to start work for a non existant item");
				return false;
			}
			
			BuildableItem bi = ItemManager.Instance.BuildableItems[itemCode];
			
			if (player.Level < bi.RequiredLevel)
			{
				Debug.Log("Attempt to start work for an item not availble to a player");
				return false;
			}
			
			if (bi.CoinCost > player.Coins)
			{
				Debug.Log("Attempt to start work for an item with insufficient coins");
				return false;
			}
			
			if (bi.CouponCost > player.Coupons)
			{
				Debug.Log("Attempt to start work for an item with insufficient coupons");
				return false;
			}
			
			if (bi.RequiredItems != null)
			{                               
				foreach (ItemQuantity itemQuantity in bi.RequiredItems)
				{
					string sic = ((int)itemQuantity.ItemCode).ToString();
			
					if (!player.BuildableItems.ContainsKey(sic))
					{
						Debug.Log("Attempt to start work for an item without proper ingredients");
						return false;
					}
					
					if (player.BuildableItems[sic] < itemQuantity.Quantity)
					{
						Debug.Log("Attempt to start work for an item with insufficient ingredients");
						return false;
					}
				}
				
				if (!DoesPlayerHaveCapacity(player, itemCode))
				{
					Debug.Log("Attempt to start work but the equipment is full");
					return false;
				}
			}
			
			string ic = ((int)itemCode).ToString();			
			if (player.BuildableItems.ContainsKey(ic))
			{
				if (player.BuildableItems[ic] >= bi.MaxQuantity)
				{
					Debug.Log("Attempt to start work for an item that the player can't hold more of.");
					return false;
				}
			}
			
			return true;
		}
		
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