using Azure.Storage.TravelTableService;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Predica.CommonServices.ConfiguratorManager;
using Predica.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
namespace Azure.Storage.TravelTableService
{
    public class TravelTableService : ITravelTableService
    {
        private readonly IConfigurationManagerHelper _configurationManagerHelper;
        private AzureStorageT<TravelModel> _storageTableClient;
        public TravelTableService(IConfigurationManagerHelper configurationManagerHelper)
        {
            _configurationManagerHelper = configurationManagerHelper;

            var _storageAccount = new CloudStorageAccount(
                             new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials(configurationManagerHelper.AzureStorage_Name, configurationManagerHelper.AzureStorage_Key), true);
            var _tableClient = _storageAccount.CreateCloudTableClient();
            var _travelTable = _tableClient.GetTableReference(_configurationManagerHelper.AzureStorage_TravelTableName);
            _storageTableClient = new AzureStorageT<TravelModel>(_storageAccount, _travelTable);
        }

        public async Task<string> CreateAsync(CreateTravelViewModel model)
        {
            var id = Guid.NewGuid();
            TravelModel tvModel = new TravelModel(model.UserName, id.ToString())
            {
                Title = model.Title,
                Content = model.Content,
            };
            await _storageTableClient.CreateAsync<TravelModel>(tvModel);
            return id.ToString();
        }

        public async Task EditAsync(EditTravelViewModel model)
        {
            var query = TableOperation.Retrieve<TravelModel>(model.PartitionKey, model.RowKey);


            TravelModel newModel = new TravelModel(model.PartitionKey, model.RowKey)
            {
                Title = model.Title,
                Content = model.Content,
            };
            await _storageTableClient.EditAsync<TravelModel>(newModel);
        }

        public async Task<TravelModel> GetTravelDetailsAsync(string rowKey, string partitionKey)
        {
            return await _storageTableClient.GetDetailsAsync<TravelModel>(rowKey, partitionKey);
        }

        public IEnumerable<TravelModel> GetAllTravelList()
        {
            var query = new TableQuery<TravelModel>();
            return _storageTableClient.GetList<TravelModel>(query);
        }

        public IEnumerable<TravelModel> GetTravelUserList(string userName)
        {
            var query = new TableQuery<TravelModel>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, userName));

            return _storageTableClient.GetList<TravelModel>(query);
        }

    }
}
