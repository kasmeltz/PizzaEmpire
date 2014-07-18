using KS.PizzaEmpire.Business.TableStorage;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KS.PizzaEmpire.Services.Storage
{
    /// <summary>
    /// A simple wrapper class for using Azure Table Storage.
    /// Batch operations are limited to items with the same partition key.
    /// </summary>
    public class AzureTableStorage
    {
        private CloudTable Table { get; set; }
        private CloudStorageAccount StorageAccount { get; set; }
        CloudTableClient TableClient { get; set; }

        /// <summary>
        /// The number of times to retry a failed cache operation before giving up and throwing an Exception.
        /// Defaults to 3.
        /// </summary>
        public static int MaxRetryAttempts { get; set; }

        /// <summary>
        /// The number of milliseconds to wait before retrying a failed cache operation.
        /// Defaults to 0.
        /// </summary>
        public static int RetryMillis { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureTableStorage"/> class.
        /// </summary>
        /// <param name="TableName">Name of the table.</param>
        public AzureTableStorage(string connectionString)
        {
            StorageAccount = CloudStorageAccount.Parse(connectionString);
            TableClient = StorageAccount.CreateCloudTableClient();
        }

        /// <summary>
        /// Sets the current table.
        /// </summary>
        /// <param name="TableName">The name of the table to use.</param>
        /// <returns></returns>
        public async Task SetTable(string tableName)
        {
            Table = TableClient.GetTableReference(tableName);
            await Table.CreateIfNotExistsAsync();
        }

        /// <summary>
        /// Gets all of the items in one partition.
        /// </summary>
        /// <typeparam name="T">DTO that inherits from TableEntity</typeparam>
        /// <param name="PartitionKey">The partition key.</param>
        /// <returns>An IEnumerable of items in the partition.</returns>
        public async Task<IEnumerable<T>> GetAll<T>(string partitionKey) where T : TableEntity, ITableStorageEntity, new()
        {
            return await ServiceHelper.RetryAsync<IEnumerable<T>>(async () =>
            {
                TableQuery<T> query = new TableQuery<T>().Where(
                TableQuery.GenerateFilterCondition(
                    "PartitionKey", QueryComparisons.Equal, partitionKey));
                return await Table.ExecuteQueryAsync<T>(query);
            }, AzureTableStorage.RetryMillis, AzureTableStorage.MaxRetryAttempts);
        }

        /// <summary>
        /// Gets a single item from the table.
        /// </summary>
        /// <typeparam name="T">DTO that inherits from TableEntity</typeparam>
        /// <param name="partitionKey">The partition key.</param>
        /// <param name="rowKey">The row key.</param>
        /// <returns></returns>
        public async Task<T> Get<T>(string partitionKey, string rowKey) where T : TableEntity, ITableStorageEntity
        {
            return await ServiceHelper.RetryAsync<T>(async () =>
            {
                TableOperation operation = TableOperation.Retrieve<T>(partitionKey, rowKey);
                TableResult results = await Table.ExecuteAsync(operation);
                return results.Result as T;
            }, AzureTableStorage.RetryMillis, AzureTableStorage.MaxRetryAttempts);
        }

        /// <summary>
        /// Inserts the specified item into the current table.
        /// </summary>
        /// <typeparam name="T">DTO that inherits from TableEntity</typeparam>
        /// <param name="item">The item to insert.</param>
        public async Task Insert<T>(T item) where T : TableEntity, ITableStorageEntity
        {
            await ServiceHelper.RetryAsync<int>(async () =>
            {
                TableOperation operation = TableOperation.Insert(item);
                await Table.ExecuteAsync(operation);
                return 1;
            }, AzureTableStorage.RetryMillis, AzureTableStorage.MaxRetryAttempts);
        }

        /// <summary>
        /// Inserts a list of items into the current table.
        /// Batch operations are limited to items with the same partition key.
        /// </summary>
        /// <typeparam name="T">DTO that inherits from TableEntity</typeparam>
        /// <param name="items">The items to insert.</param>
        public async Task Insert<T>(IEnumerable<T> items) where T : TableEntity, ITableStorageEntity
        {
            await ServiceHelper.RetryAsync<int>(async () =>
            {
                TableBatchOperation operation = new TableBatchOperation();
                operation.Clear();
                foreach (T item in items)
                {
                    operation.Insert(item);
                }
                await Table.ExecuteBatchAsync(operation);
                return 1;
            }, AzureTableStorage.RetryMillis, AzureTableStorage.MaxRetryAttempts);
        }

        /// <summary>
        /// Replaces the specified item into the current table.
        /// </summary>
        /// <typeparam name="T">DTO that inherits from TableEntity</typeparam>
        /// <param name="item">The item to replace.</param>
        public async Task Replace<T>(T item) where T : TableEntity, ITableStorageEntity
        {
            await ServiceHelper.RetryAsync<int>(async () =>
            {
                item.ETag = "*";
                TableOperation operation = TableOperation.Replace(item);
                await Table.ExecuteAsync(operation);
                return 1;
            }, AzureTableStorage.RetryMillis, AzureTableStorage.MaxRetryAttempts);
        }

        /// <summary>
        /// Replaces a list of items into the current table.
        /// Batch operations are limited to items with the same partition key.
        /// </summary>
        /// <typeparam name="T">DTO that inherits from TableEntity</typeparam>
        /// <param name="items">The items to replace.</param>
        public async Task Replace<T>(IEnumerable<T> items) where T : TableEntity, ITableStorageEntity
        {
            await ServiceHelper.RetryAsync<int>(async () =>
            {
                TableBatchOperation operation = new TableBatchOperation();
                foreach (T item in items)
                {
                    item.ETag = "*";
                    operation.Replace(item);
                }
                await Table.ExecuteBatchAsync(operation);
                return 1;
            }, AzureTableStorage.RetryMillis, AzureTableStorage.MaxRetryAttempts);
        }

        /// <summary>
        /// Merges the specified item into the current table.
        /// </summary>
        /// <typeparam name="T">DTO that inherits from TableEntity</typeparam>
        /// <param name="item">The item to merge.</param>
        public async Task Merge<T>(T item) where T : TableEntity, ITableStorageEntity
        {
            await ServiceHelper.RetryAsync<int>(async () =>
            {
                item.ETag = "*";
                TableOperation operation = TableOperation.Merge(item);
                await Table.ExecuteAsync(operation);
                return 1;
            }, AzureTableStorage.RetryMillis, AzureTableStorage.MaxRetryAttempts);
        }

        /// <summary>
        /// Merges a list of items into the current table.
        /// Batch operations are limited to items with the same partition key.
        /// </summary>
        /// <typeparam name="T">DTO that inherits from TableEntity</typeparam>
        /// <param name="item">The items to merge.</param>
        public async Task Merge<T>(IEnumerable<T> items) where T : TableEntity, ITableStorageEntity
        {
            await ServiceHelper.RetryAsync<int>(async () =>
            {
                TableBatchOperation operation = new TableBatchOperation();
                foreach (T item in items)
                {
                    item.ETag = "*";
                    operation.Merge(item);
                }

                await Table.ExecuteBatchAsync(operation);
                return 1;
            }, AzureTableStorage.RetryMillis, AzureTableStorage.MaxRetryAttempts);
        }

        /// <summary>
        /// Inserts or Replaces the specified item into the current table.
        /// </summary>
        /// <typeparam name="T">DTO that inherits from TableEntity</typeparam>
        /// <param name="item">The item to insert or replace.</param>
        public async Task InsertOrReplace<T>(T item) where T : TableEntity, ITableStorageEntity
        {
            await ServiceHelper.RetryAsync<int>(async () =>
            {
                TableOperation operation = TableOperation.InsertOrReplace(item);
                await Table.ExecuteAsync(operation);
                return 1;
            }, AzureTableStorage.RetryMillis, AzureTableStorage.MaxRetryAttempts);
        }

        /// <summary>
        /// Inserts or Replaces a list of items into the current table.
        /// Batch operations are limited to items with the same partition key.
        /// </summary>
        /// <typeparam name="T">DTO that inherits from TableEntity</typeparam>
        /// <param name="items">The items to insert or replace.</param>
        public async Task InsertOrReplace<T>(IEnumerable<T> items) where T : TableEntity, ITableStorageEntity
        {
            await ServiceHelper.RetryAsync<int>(async () =>
            {
                TableBatchOperation operation = new TableBatchOperation();
                foreach (T item in items)
                {
                    operation.InsertOrReplace(item);
                }

                await Table.ExecuteBatchAsync(operation);
                return 1;
            }, AzureTableStorage.RetryMillis, AzureTableStorage.MaxRetryAttempts);
        }

        /// <summary>
        /// Inserts or Merges the specified item into the current table.
        /// </summary>
        /// <typeparam name="T">DTO that inherits from TableEntity</typeparam>
        /// <param name="item">The item to insert or merge.</param>
        public async Task InsertOrMerge<T>(T item) where T : TableEntity, ITableStorageEntity
        {
            await ServiceHelper.RetryAsync<int>(async () =>
            {
                TableOperation operation = TableOperation.InsertOrMerge(item);
                await Table.ExecuteAsync(operation);
                return 1;
            }, AzureTableStorage.RetryMillis, AzureTableStorage.MaxRetryAttempts);
        }

        /// <summary>
        /// Inserts or Merges a list of items into the current table.
        /// Batch operations are limited to items with the same partition key.
        /// </summary>
        /// <typeparam name="T">DTO that inherits from TableEntity</typeparam>
        /// <param name="items">The items to insert or merge.</param>
        public async Task InsertOrMerge<T>(IEnumerable<T> items) where T : TableEntity, ITableStorageEntity
        {
            await ServiceHelper.RetryAsync<int>(async () =>
            {
                TableBatchOperation operation = new TableBatchOperation();
                foreach (T item in items)
                {
                    operation.InsertOrMerge(item);
                }
                await Table.ExecuteBatchAsync(operation);
                return 1;
            }, AzureTableStorage.RetryMillis, AzureTableStorage.MaxRetryAttempts);
        }

        /// <summary>
        /// Deletes the specified item from the current table.
        /// </summary>
        /// <typeparam name="T">DTO that inherits from TableEntity</typeparam>
        /// <param name="item">The item to delete.</param>
        public async Task Delete<T>(T item) where T : TableEntity, ITableStorageEntity
        {
            item.ETag = "*";
            TableOperation operation = TableOperation.Delete(item);
            await Table.ExecuteAsync(operation);
        }

        /// <summary>
        /// Deletes a list of items from the current table.
        /// Batch operations are limited to items with the same partition key.
        /// </summary>
        /// <typeparam name="T">DTO that inherits from TableEntity</typeparam>
        /// <param name="item">The items to delete.</param>
        public async Task Delete<T>(IEnumerable<T> items) where T : TableEntity, ITableStorageEntity
        {
            await ServiceHelper.RetryAsync<int>(async () =>
            {
                TableBatchOperation operation = new TableBatchOperation();
                foreach (TableEntity item in items)
                {
                    item.ETag = "*";
                    operation.Delete(item);
                }
                await Table.ExecuteBatchAsync(operation);
                return 1;
            }, AzureTableStorage.RetryMillis, AzureTableStorage.MaxRetryAttempts);
        }

        /// <summary>
        /// Deletes the current table.
        /// </summary>
        public async Task DeleteTable()
        {
            await Table.DeleteIfExistsAsync();
        }
    }
}
