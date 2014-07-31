namespace KS.PizzaEmpire.Unity
{
	using KS.PizzaEmpire.Common;
	using System;
	using System.Collections.Generic;
	using UnityEngine;
	
	/// <summary>
	/// Manages the resources used in the game.
	/// Doesn't use referene counting and is suitable for items that 
	/// are very lightweight and will live in memory for the
	/// lifetime of the applicaton.
	/// </summary>
	public class LightweightResourceManager<T>
	{
		private Dictionary<ResourceEnum, T> ResourceObjects { get; set; }
		
		private static volatile LightweightResourceManager<T> instance;
		private static object syncRoot = new object();
		
		private LightweightResourceManager() { }
		
		/// <summary>
		/// Provides the Singleton instance of the LightweightResourceManager
		/// </summary>
		public static LightweightResourceManager<T> Instance
		{
			get
			{
				if (instance == null)
				{
					lock (syncRoot)
					{
						if (instance == null)
						{
							instance = new LightweightResourceManager<T>();
							instance.Initialize();
						}
					}
				}
				return instance;
			}
		}
		
		/// <summary>
		/// Initializes the lightweight resource manager.
		/// </summary>
		/// <returns></returns>
		public void Initialize()
		{
			ResourceObjects = new Dictionary<ResourceEnum,T>();					
		}
				
		/// <summary>
		/// Sets the resource associated with the provided resource enum.
		/// </summary>	
		/// <typeparam name="T"></typeparam>
		/// <param name="resource"></param>
		/// <returns></returns>
		public void Set(ResourceEnum resource, T item)
		{
			ResourceObjects[resource] = item;
			
			Debug.Log("Setting " + typeof(T).ToString() + " Item: " + resource + ", " + ResourceObjects[resource]);
		}
		
		/// <summary>
		/// Gets the resource associated with the provided resource enum.
		/// </summary>		
		/// <typeparam name="T"></typeparam>
		/// <param name="resource"></param>
		/// <returns></returns>
		public T Get(ResourceEnum resource)
		{
			Debug.Log("Getting " + typeof(T).ToString() + " Item: " + resource + ", " + ResourceObjects[resource]);
			
			return ResourceObjects[resource];
		}			
	}
}
