namespace KS.PizzaEmpire.Unity
{
	using Common.ObjectPool;
	
	public class GUIItemFactory
	{
		private static volatile GUIItemFactory instance;
		private static object syncRoot = new object();
		
		private GUIItemFactory() 
		{
			Pool = new ObjectPoolWithReset<GUIItem>(20);
		}
				
		/// <summary>
		/// 
		/// </summary>
		public ObjectPoolWithReset<GUIItem> Pool { get; protected set; }
		
		/// <summary>
		/// Provides the Singleton instance of the GUIItemFactory
		/// </summary>
		public static GUIItemFactory Instance
		{
			get
			{
				if (instance == null)
				{
					lock (syncRoot)
					{
						if (instance == null)
						{
							instance = new GUIItemFactory();
						}
					}
				}
				return instance;
			}
		}		
	}
}