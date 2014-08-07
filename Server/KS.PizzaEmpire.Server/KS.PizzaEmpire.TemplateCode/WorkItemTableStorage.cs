///
/// Code audo generated by a tool other than you.
///
namespace KS.PizzaEmpire.Business.TableStorage
{
	using Common.BusinessObjects;
	using Microsoft.WindowsAzure.Storage.Table;
	using System;

	/// <summary>
	/// Represents ongoing work that will produce some finished item(s) after some length of time
	/// </summary>
	public class WorkItemTableStorage : TableEntity, ITableStorageEntity
	{
		/// <summary>
		/// Creates a new instance of the WorkItemTableStorage class.
		/// </summary>
		public WorkItemTableStorage() { }

		/// <summary>
		/// The item code representinf the item being worked on
		/// </summary>
		public int ItemCode { get; set; }

		/// <summary>
		/// The UTC time when the work will be complete
		/// </summary>
		public DateTime FinishTime { get; set; }

	}
}