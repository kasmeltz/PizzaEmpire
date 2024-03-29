﻿namespace KS.PizzaEmpire.Services.Storage
{
    using CodeSuperior.Lucifure;
    using Microsoft.WindowsAzure.Storage.Table;
    using System.Threading.Tasks;

    /// <summary>
    /// A simple wrapper class for using Azure Table Storage.
    /// Batch operations are limited to items with the same partition key.
    /// </summary>
    public class LucifureTableStorage
    {     
        /// <summary>
        /// The number of times to retry a failed cache operation before giving up and throwing an Exception.
        /// </summary>
        public int MaxRetryAttempts { get; set; }

        /// <summary>
        /// The number of milliseconds to wait before retrying a failed cache operation.
        /// </summary>
        public int RetryMillis { get; set; }

        /// <summary>
        /// The number of milliseconds to wait before performing a cache operation.
        /// </summary>
        public int ThrottleMillis { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureTableStorage"/> class.
        /// </summary>
        /// <param name="TableName">Name of the table.</param>
        public LucifureTableStorage()
        {
            MaxRetryAttempts = ServiceHelper.IntValueFromConfig("AzureTableStorageMaxRetryAttempts");
            RetryMillis = ServiceHelper.IntValueFromConfig("AzureTableStorageRetryMillis");
            ThrottleMillis = ServiceHelper.IntValueFromConfig("AzureTableStorageThrottleMillis");
            string connectionString = Microsoft.WindowsAzure.CloudConfigurationManager.GetSetting("AzureTableStorageConnectionString");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        private async Task InsertAsync<T>(T item)
        {
            await Task.Factory.StartNew(() =>
            {
                StashClient<T> client = new StashClient<T>();
                client.CreateTableIfNotExist();
                client.InsertOrUpdate(item);
            }); 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task Insert<T>(T item)
        {
            await ServiceHelper.RetryAsync<int>(async () =>
            {
                await InsertAsync<T>(item);
                return 1;
            }, RetryMillis, MaxRetryAttempts, ThrottleMillis);               	
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paritionKey"></param>
        /// <param name="rowKey"></param>
        /// <returns></returns>
        private async Task<T> GetAsync<T>(string paritionKey, string rowKey)
            where T :TableEntity
        {
            return await Task<T>.Factory.StartNew(() =>
            {
                StashClient<T> client = new StashClient<T>();
                client.CreateTableIfNotExist();
                return client.Get(paritionKey, rowKey);
            }); 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="paritionKey"></param>
        /// <param name="rowKey"></param>
        /// <returns></returns>
        public async Task<T> Get<T>(string paritionKey, string rowKey)
            where T :TableEntity
        {
            return await ServiceHelper.RetryAsync<T>(async () =>
            {
                return await GetAsync<T>(paritionKey, rowKey);
            }, RetryMillis, MaxRetryAttempts, ThrottleMillis);               	
        }                    

        /*

        /// <summary>
        /// Sets the current table.
        /// </summary>
        /// <param name="TableName">The name of the table to use.</param>
        /// <returns></returns>
        public async Task SetTable(string tableName)
        {
            await ServiceHelper.RetryAsync<int>(async () =>
            {
                Table = TableClient.GetTableReference(tableName);
                await Table.CreateIfNotExistsAsync();
                return 1;
            }, RetryMillis, MaxRetryAttempts, ThrottleMillis);
        }
        
        /// <summary>
        /// Gets all of the items in one partition.
        /// </summary>
        /// <typeparam name="T">DTO that inherits from TableEntity</typeparam>
        /// <param name="PartitionKey">The partition key.</param>
        /// <returns>An IEnumerable of items in the partition.</returns>
        public async Task<IEnumerable<T>> GetAll<T>(string partitionKey) 
            where T : TableEntity, ITableStorageEntity, new()
        {
            return await ServiceHelper.RetryAsync<IEnumerable<T>>(async () =>
            {
                TableQuery<T> query = new TableQuery<T>().Where(
                TableQuery.GenerateFilterCondition(
                    "PartitionKey", QueryComparisons.Equal, partitionKey));
                return await Table.ExecuteQueryAsync<T>(query);
            }, RetryMillis, MaxRetryAttempts, ThrottleMillis);
        }

        /// <summary>
        /// Gets a single item from the table.
        /// </summary>
        /// <typeparam name="T">DTO that inherits from TableEntity</typeparam>
        /// <param name="partitionKey">The partition key.</param>
        /// <param name="rowKey">The row key.</param>
        /// <returns></returns>
        public async Task<T> Get<T>(string partitionKey, string rowKey) 
            where T : TableEntity, ITableStorageEntity
        {
            return await ServiceHelper.RetryAsync<T>(async () =>
            {
                TableOperation operation = TableOperation.Retrieve<T>(partitionKey, rowKey);
                TableResult results = await Table.ExecuteAsync(operation);
                return results.Result as T;
            }, RetryMillis, MaxRetryAttempts, ThrottleMillis);
        }

        /// <summary>
        /// Inserts the specified item into the current table.
        /// </summary>
        /// <typeparam name="T">DTO that inherits from TableEntity</typeparam>
        /// <param name="item">The item to insert.</param>
        public async Task Insert<T>(T item) 
            where T : TableEntity, ITableStorageEntity
        {
            await ServiceHelper.RetryAsync<int>(async () =>
            {
                TableOperation operation = TableOperation.Insert(item);
                await Table.ExecuteAsync(operation);
                return 1;
            }, RetryMillis, MaxRetryAttempts, ThrottleMillis);
        }

        /// <summary>
        /// Inserts a list of items into the current table.
        /// Batch operations are limited to items with the same partition key.
        /// </summary>
        /// <typeparam name="T">DTO that inherits from TableEntity</typeparam>
        /// <param name="items">The items to insert.</param>
        public async Task Insert<T>(IEnumerable<T> items) 
            where T : TableEntity, ITableStorageEntity
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
            }, RetryMillis, MaxRetryAttempts, ThrottleMillis);
        }

        /// <summary>
        /// Replaces the specified item into the current table.
        /// </summary>
        /// <typeparam name="T">DTO that inherits from TableEntity</typeparam>
        /// <param name="item">The item to replace.</param>
        public async Task Replace<T>(T item) 
            where T : TableEntity, ITableStorageEntity
        {
            await ServiceHelper.RetryAsync<int>(async () =>
            {
                item.ETag = "*";
                TableOperation operation = TableOperation.Replace(item);
                await Table.ExecuteAsync(operation);
                return 1;
            }, RetryMillis, MaxRetryAttempts, ThrottleMillis);
        }

        /// <summary>
        /// Replaces a list of items into the current table.
        /// Batch operations are limited to items with the same partition key.
        /// </summary>
        /// <typeparam name="T">DTO that inherits from TableEntity</typeparam>
        /// <param name="items">The items to replace.</param>
        public async Task Replace<T>(IEnumerable<T> items) 
            where T : TableEntity, ITableStorageEntity
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
            }, RetryMillis, MaxRetryAttempts, ThrottleMillis);
        }

        /// <summary>
        /// Merges the specified item into the current table.
        /// </summary>
        /// <typeparam name="T">DTO that inherits from TableEntity</typeparam>
        /// <param name="item">The item to merge.</param>
        public async Task Merge<T>(T item) 
            where T : TableEntity, ITableStorageEntity
        {
            await ServiceHelper.RetryAsync<int>(async () =>
            {
                item.ETag = "*";
                TableOperation operation = TableOperation.Merge(item);
                await Table.ExecuteAsync(operation);
                return 1;
            }, RetryMillis, MaxRetryAttempts, ThrottleMillis);
        }

        /// <summary>
        /// Merges a list of items into the current table.
        /// Batch operations are limited to items with the same partition key.
        /// </summary>
        /// <typeparam name="T">DTO that inherits from TableEntity</typeparam>
        /// <param name="item">The items to merge.</param>
        public async Task Merge<T>(IEnumerable<T> items) 
            where T : TableEntity, ITableStorageEntity
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
            }, RetryMillis, MaxRetryAttempts, ThrottleMillis);
        }

        /// <summary>
        /// Inserts or Replaces the specified item into the current table.
        /// </summary>
        /// <typeparam name="T">DTO that inherits from TableEntity</typeparam>
        /// <param name="item">The item to insert or replace.</param>
        public async Task InsertOrReplace<T>(T item) 
            where T : TableEntity, ITableStorageEntity
        {
            await ServiceHelper.RetryAsync<int>(async () =>
            {
                TableOperation operation = TableOperation.InsertOrReplace(item);
                await Table.ExecuteAsync(operation);
                return 1;
            }, RetryMillis, MaxRetryAttempts, ThrottleMillis);
        }

        /// <summary>
        /// Inserts or Replaces a list of items into the current table.
        /// Batch operations are limited to items with the same partition key.
        /// </summary>
        /// <typeparam name="T">DTO that inherits from TableEntity</typeparam>
        /// <param name="items">The items to insert or replace.</param>
        public async Task InsertOrReplace<T>(IEnumerable<T> items) 
            where T : TableEntity, ITableStorageEntity
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
            }, RetryMillis, MaxRetryAttempts, ThrottleMillis);
        }

        /// <summary>
        /// Inserts or Merges the specified item into the current table.
        /// </summary>
        /// <typeparam name="T">DTO that inherits from TableEntity</typeparam>
        /// <param name="item">The item to insert or merge.</param>
        public async Task InsertOrMerge<T>(T item) 
            where T : TableEntity, ITableStorageEntity
        {
            await ServiceHelper.RetryAsync<int>(async () =>
            {
                TableOperation operation = TableOperation.InsertOrMerge(item);
                await Table.ExecuteAsync(operation);
                return 1;
            }, RetryMillis, MaxRetryAttempts, ThrottleMillis);
        }

        /// <summary>
        /// Inserts or Merges a list of items into the current table.
        /// Batch operations are limited to items with the same partition key.
        /// </summary>
        /// <typeparam name="T">DTO that inherits from TableEntity</typeparam>
        /// <param name="items">The items to insert or merge.</param>
        public async Task InsertOrMerge<T>(IEnumerable<T> items) 
            where T : TableEntity, ITableStorageEntity
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
            }, RetryMillis, MaxRetryAttempts, ThrottleMillis);
        }

        /// <summary>
        /// Deletes the specified item from the current table.
        /// </summary>
        /// <typeparam name="T">DTO that inherits from TableEntity</typeparam>
        /// <param name="item">The item to delete.</param>
        public async Task Delete<T>(T item) 
            where T : TableEntity, ITableStorageEntity
        {
            await ServiceHelper.RetryAsync<int>(async () =>
            {
                item.ETag = "*";
                TableOperation operation = TableOperation.Delete(item);
                await Table.ExecuteAsync(operation);
                return 1;
            }, RetryMillis, MaxRetryAttempts, ThrottleMillis);
        }

        /// <summary>
        /// Deletes a list of items from the current table.
        /// Batch operations are limited to items with the same partition key.
        /// </summary>
        /// <typeparam name="T">DTO that inherits from TableEntity</typeparam>
        /// <param name="item">The items to delete.</param>
        public async Task Delete<T>(IEnumerable<T> items) 
            where T : TableEntity, ITableStorageEntity
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
            }, RetryMillis, MaxRetryAttempts, ThrottleMillis);
        }

        /// <summary>
        /// Deletes the current table.
        /// </summary>
        public async Task DeleteTable()
        {
            await Table.DeleteIfExistsAsync();
        }
         */
    }
}
