namespace KS.PizzaEmpire.Unity
{
    using KS.PizzaEmpire.Common;
    using System;
	using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    
    /// <summary>
    /// Manages the resources used in the game.
    /// This class is used to manage resources that will need to be 
    /// loaded into and unloaded out of memory in order to avoid
    /// memory problems especially on mobile devices.
    /// 
    /// Every call to Load must be paired with a call to Unload.    
    /// </summary>
    public class ResourceManager<T> where T : UnityEngine.Object
    {
        private Dictionary<ResourceEnum, string> ResourceLocations {  get; set; }
        private Dictionary<ResourceEnum, int> ResourceCounts { get; set; }
        private Dictionary<ResourceEnum, T> ResourceObjects { get; set; }
        
        public int numAsyncLists;
        
        /// <summary>
        /// Returns true if the manager is currently loading 
        /// resources asynchronously, false otherwise.
        /// </summary>
        public bool LoadingAsync 
        { 
	        get 
	        {
	        	return numAsyncLists == 0;
	        }
        }

        private static volatile ResourceManager<T> instance;
		private static object syncRoot = new object();

		private ResourceManager() { numAsyncLists = 0; }
		
		/// <summary>
		/// Provides the Singleton instance of the ResourceManager
		/// </summary>
        public static ResourceManager<T> Instance
		{
			get
			{
				if (instance == null)
				{
					lock (syncRoot)
					{
						if (instance == null)
						{
                            instance = new ResourceManager<T>();
						}
					}
				}
				return instance;
			}
		}
		
		/// <summary>
		/// Initializes the resource manager.
		/// </summary>
		/// <returns></returns>
		public void Initialize(string resourceList)
		{
            ResourceLocations = new Dictionary<ResourceEnum, string>();
            ResourceCounts = new Dictionary<ResourceEnum,int>();
            ResourceObjects = new Dictionary<ResourceEnum,T>();

            string[] lines = resourceList.Split('\n');
            for (int i = 0; i < lines.Length; i++)
            {
                string[] tokens = lines[i].Split(',');

                ResourceEnum re = (ResourceEnum)Enum.Parse(typeof(ResourceEnum), tokens[0]);
                
                Debug.Log("Initializing " + typeof(T).ToString() + " asset: " + re + ", " + tokens[1]);
                
				ResourceLocations[re] = tokens[1].Trim();
                ResourceCounts[re] = 0;
            }

            GC.Collect();
		}

        /// <summary>
        /// Returns the resource with the specified path
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        protected T Load(string path)
        {
			Debug.Log ("Loading from Resources.Load: " + path);		
			return Resources.Load<T>(path);
        }        
               
        /// <summary>
        /// Loads the resource associated with the provided resource enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resource"></param>
        /// <returns></returns>
        public T Load(ResourceEnum resource)
        {
			if (ResourceCounts[resource] == 0)
            {
				Debug.Log("Loading for first time!: " + resource);
                ResourceObjects[resource] = Load(ResourceLocations[resource]);
                ResourceCounts[resource] = 1;
            }   
            else
            {
				Debug.Log("Resource already loaded!: " + resource);
                ResourceCounts[resource]++;
            }
            
			Debug.Log("Loading " + typeof(T).ToString() + " Item: " + resource + ", " + ResourceObjects[resource]);
			Debug.Log("Reference count: " + ResourceCounts[resource]);
			
            return ResourceObjects[resource];
        }
        
        /// <summary>
        /// Loads the list of resources, yielding between each one.
        /// </summary>
        /// <returns>The list.</returns>
        /// <param name="resources">Resources.</param>
        public IEnumerator LoadList(List<ResourceEnum> resources)
        {
			WaitForEndOfFrame eof = new WaitForEndOfFrame();
			
			lock (syncRoot)
			{
				numAsyncLists++;
			}
			
        	for (int i = 0;i < resources.Count;i++)
        	{
				yield return Load(ResourceLocations[resources[i]]);
				yield return eof;
        	}
        	
			lock (syncRoot)
			{
				numAsyncLists--;
			}
        }

        /// <summary>
        /// Unloads the resource associated with the provided resource enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resource"></param>
        /// <returns></returns>
        public ErrorCode UnLoad(ResourceEnum resource)
        {
			if (ResourceCounts[resource] == 1)
            {     
				UnityEngine.Object.Destroy(ResourceObjects[resource]);
                ResourceObjects[resource] = null;
                Resources.UnloadAsset(ResourceObjects[resource]);
            }

            ResourceCounts[resource]--;
            
			Debug.Log("UnLoading " + typeof(T).ToString() + " Item: " + resource + ", " + ResourceObjects[resource]);
			Debug.Log("Reference count: " + ResourceCounts[resource]);

            if (ResourceCounts[resource] < 0)
            {
                return ErrorCode.RESOURCE_CALLS_NOT_PAIRED;
            }

            return ErrorCode.ERROR_OK;
        }
    }
}
