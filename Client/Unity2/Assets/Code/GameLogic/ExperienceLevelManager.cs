namespace KS.PizzaEmpire.Unity
{	
	using System.Collections.Generic;
	using Common.BusinessObjects;
	using Common.Utility;
    using System;
	
	/// <summary>
	/// Represents an item that handles the game logic for experience levels
	/// </summary>
	public class ExperienceLevelManager
	{
		private static volatile ExperienceLevelManager instance;
		private static object syncRoot = new object();
		
		/// <summary>
		/// 
		/// </summary>
		public Dictionary<int, ExperienceLevel> ExperienceLevels;
		
		private ExperienceLevelManager() { }
		
		/// <summary>
		/// Provides the Singleton instance of the ExperienceManager
		/// </summary>
		public static ExperienceLevelManager Instance
		{
			get
			{
				if (instance == null)
				{
					lock (syncRoot)
					{
						if (instance == null)
						{
							instance = new ExperienceLevelManager();
						}
					}
				}
				return instance;
			}
		}
		
		/// <summary>
		/// Initializes the experience level manager.
		/// </summary>
		/// <returns></returns>
		public void Initialize(string json)
		{
			LoadExperienceLevelDefinitions(json);

            json = null;

            GC.Collect();
		}
		
		/// <summary>
		/// Load the experience level definitions 
		/// </summary>
		public void LoadExperienceLevelDefinitions(string json)
		{			
			ExperienceLevels = JsonHelper.Instance.LevelsFromJSON(json);
		}
	}
}
