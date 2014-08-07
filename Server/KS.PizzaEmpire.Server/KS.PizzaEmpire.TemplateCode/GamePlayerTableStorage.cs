///
/// Code audo generated by a tool other than you.
///
namespace KS.PizzaEmpire.Business.TableStorage
{
	using Common.BusinessObjects;
	using Microsoft.WindowsAzure.Storage.Table;
	using System;

	/// <summary>
	/// Represents the state for a player of the game
	/// </summary>
	public class GamePlayerTableStorage : TableEntity, ITableStorageEntity
	{
		/// <summary>
		/// Creates a new instance of the GamePlayerTableStorage class.
		/// </summary>
		public GamePlayerTableStorage() { }

		/// <summary>
		/// The number of coins owned by the player
		/// </summary>
		public int Coins { get; set; }

		/// <summary>
		/// The number of coupons owned by the player
		/// </summary>
		public int Coupons { get; set; }

		/// <summary>
		/// The current experience of the player
		/// </summary>
		public int Experience { get; set; }

		/// <summary>
		/// The players current level
		/// </summary>
		public int Level { get; set; }

		/// <summary>
		/// The players inventory of items
		/// </summary>
		public byte[] BuildableItems { get; set; }

		/// <summary>
		/// The work in progress for the player
		/// </summary>
		public byte[] WorkItems { get; set; }

		/// <summary>
		/// The player's current tutorial stage
		/// </summary>
		public int TutorialStage { get; set; }

		/// <summary>
		/// Whether the state has changed
		/// </summary>
		public bool StateChanged { get; set; }
	}
}