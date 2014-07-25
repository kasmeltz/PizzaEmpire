namespace KS.PizzaEmpire.Unity
{
	/// <summary>
	/// Represnts an item that captures information about the result of an operation
	/// </summary>    
	public class Result<T>
	{
		/// <summary>
		/// Creates an empty instance of the Result class
		/// </summary>
		public Result() { }
		
		/// <summary>
		/// The error code associated with the result
		/// </summary>		
		public int ErrorCode { get; set; }
		
		/// <summary>
		/// The item associated with the opertaion
		/// </summary>
		public T Item { get; set; }
	}
}