namespace KS.PizzaEmpire.Unity
{	
	using Common.Utility;
	using Newtonsoft.Json;
	
	/// <summary>
	/// Represents an item that can communicate with the server 
	/// </summary>
	public class UnityJsonConverter	: IJsonConverter
	{
		#region IJsonConverter
		
		public string Serlialize<T>(T item)
		{
			return JsonConvert.SerializeObject(item);
		}
		
		public T Deserialize<T>(string json)
		{
			return JsonConvert.DeserializeObject<T>(json);
		}
		
		#endregion
	}
}