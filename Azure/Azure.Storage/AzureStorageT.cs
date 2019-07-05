using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Predica.CommonServices.ConfiguratorManager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Azure.Storage
{
    internal class AzureStorageT<T>
    {
        private CloudStorageAccount _storageAccount;
        private CloudTableClient _tableClient;
        private CloudTable _cloudTable;
        internal AzureStorageT(CloudStorageAccount storageAccount, CloudTable cloudTable )
        {
            _storageAccount = storageAccount;
            _tableClient = _storageAccount.CreateCloudTableClient();
            _cloudTable = cloudTable;
        }
        internal IEnumerable<T> GetList<T>(TableQuery<T> query) where T : ITableEntity, new()
        {
            Task.Run(() => _cloudTable.CreateIfNotExistsAsync()).GetAwaiter().GetResult();
            TableContinuationToken token = null;
            do
            {
                var queryResult = Task.Run(() => _cloudTable.ExecuteQuerySegmentedAsync(query, token)).GetAwaiter().GetResult();
                foreach (var item in queryResult.Results)
                {
                    yield return item;
                }
                token = queryResult.ContinuationToken;
            } while (token != null);
        }

        internal async Task<T> GetDetailsAsync<T>(string rowKey, string partitionKey) where T : ITableEntity, new()
        {
            // Create a retrieve operation that takes a customer entity.
            TableOperation retrieveOperation = TableOperation.Retrieve<T>(partitionKey, rowKey);

            // Execute the retrieve operation.
            TableResult retrievedResult = await _cloudTable.ExecuteAsync(retrieveOperation);
            return (T)retrievedResult.Result;
        }
        internal async Task CreateAsync<T>(T model) where T : ITableEntity, new()
        {
            TableOperation insertOperation = TableOperation.Insert(model);
            await _cloudTable.ExecuteAsync(insertOperation);
        }

        internal async Task EditAsync<T>(T model) where T : ITableEntity, new()
        {
            TableOperation InsertOrReplace = TableOperation.InsertOrReplace(model);
            await _cloudTable.ExecuteAsync(InsertOrReplace);
        }
    }
}
