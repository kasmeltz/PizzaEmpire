namespace KS.PizzaEmpire.Unity
{
	using System;
	using System.Collections.Generic;
	using Common.BusinessObjects;
	using Common.Utility;
	
	/// <summary>
	/// Represents an item that manages the game player objects
	/// for the game
	/// </summary>
	public class GamePlayerManager
	{
		private static volatile GamePlayerManager instance;
		private static object syncRoot = new object();
		
		/// <summary>
		/// 
		/// </summary>
		public GamePlayer LoggedInPlayer { get; set; }
		
		private GamePlayerManager() { }
		
		/// <summary>
		/// Provides the Singleton instance of the GamePlayerManager
		/// </summary>
		public static GamePlayerManager Instance
		{
			get
			{
				if (instance == null)
				{
					lock (syncRoot)
					{
						if (instance == null)
						{
							instance = new GamePlayerManager();
						}
					}
				}
				return instance;
			}
		}	
	}
}
