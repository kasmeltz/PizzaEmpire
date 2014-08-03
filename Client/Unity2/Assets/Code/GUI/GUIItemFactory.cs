namespace KS.PizzaEmpire.Unity
{
	using Common.ObjectPool;
	using System.Collections;
	using System;
	
	public class GUIItemFactory<T> where T: class, IResetable, new()
	{
		private static volatile GUIItemFactory<T> instance;
		private static object syncRoot = new object();
		public ObjectPoolWithReset<T> Pool { get; set; }
		
		private GUIItemFactory()
		{
			Pool = new ObjectPoolWithReset<T>(20);	
		}	
		
		/// <summary>
		/// Provides the Singleton instance of the GUIItemFactory
		/// </summary>
		public static GUIItemFactory<T> Instance
		{
			get
			{
				if (instance == null)
				{
					lock (syncRoot)
					{
						if (instance == null)
						{
							instance = new GUIItemFactory<T>();
						}
					}
				}
				return instance;
			}
		}		
	}
}