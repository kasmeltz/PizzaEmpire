namespace KS.PizzaEmpire.Unity
{
	/// <summary>
	/// A list of the actions that we can perform on the server
	/// </summary>
	public enum ServerActionEnum
	{
		None = 0,
		CreatePlayer,
		GetPlayer,
		StartWork,
		FinishWork
	}
}