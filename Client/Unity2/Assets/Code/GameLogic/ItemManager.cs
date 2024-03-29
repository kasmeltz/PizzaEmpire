﻿namespace KS.PizzaEmpire.Unity
{
	using System;
	using System.Collections.Generic;
	using Common.BusinessObjects;
	using Common.Utility;
	
	/// <summary>
	/// Represents an item that handles the game logic for items
	/// </summary>
	public class ItemManager
	{
		private static volatile ItemManager instance;
        private static object syncRoot = new object();
		
		/// <summary>
		/// 
		/// </summary>
		public Dictionary<BuildableItemEnum, BuildableItem> BuildableItems;
		
		private ItemManager() { }
		
		/// <summary>
		/// Provides the Singleton instance of the ItemManager
		/// </summary>
		public static ItemManager Instance
		{
			get
			{
				if (instance == null)
				{
					lock (syncRoot)
					{
						if (instance == null)
						{
							instance = new ItemManager();
						}
					}
				}
				return instance;
			}
		}
		
		/// <summary>
		/// Initializes the item manager.
		/// </summary>
		/// <returns></returns>
		public void Initialize(string json)
		{
			LoadItemDefinitions(json);

            json = null;

            GC.Collect();
		}
		
		/// <summary>
		/// Load the BuildableItems
		/// </summary>
		public void LoadItemDefinitions(string json)
		{
			BuildableItems = JsonHelper.Instance.ItemsFromJSON(json);
		}
	}
}
