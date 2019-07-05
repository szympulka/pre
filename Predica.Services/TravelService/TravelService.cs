using System;
using System.Collections.Generic;
using System.Text;
using Predica.ViewModel;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.TravelTableService;

namespace Predica.Services.AzureStorageService
{
    public class TravelService : ITravelService
    {
        private readonly ITravelTableService _azureStorageService;

        public TravelService(ITravelTableService azureStorageService)
        {
            _azureStorageService = azureStorageService;
        }

        public async Task<string> CreateAsync(CreateTravelViewModel model)
        {
            return await _azureStorageService.CreateAsync(model);
        }

        public async Task EditAsync(EditTravelViewModel model)
        {
            await _azureStorageService.EditAsync(model);
        }

        public async Task<GetDetailsTravelViewModel> GetTravelDetailsAsync(string rowKey, string partitionKey)
        {
            var result = await _azureStorageService.GetTravelDetailsAsync(rowKey, partitionKey);
            return new GetDetailsTravelViewModel()
            {
                Title = result.Title,
                PartitionKey = result.PartitionKey,
                RowKey = result.RowKey,
                Content = result.Content,
            };
        }

        public IEnumerable<GetTravelListViewModel> GetTravelList()
        {
            return _azureStorageService.GetAllTravelList().Select(x=> new GetTravelListViewModel()
            {
                Title = x.Title,
                PartitionKey = x.PartitionKey,
                RowKey = x.RowKey,
            });
        }

        public IEnumerable<GetTravelListViewModel> GetTravelUserList(string name)
        {
            return _azureStorageService.GetTravelUserList(name).Select(x => new GetTravelListViewModel()
            {
                Title = x.Title,
                PartitionKey = x.PartitionKey,
                RowKey = x.RowKey,
            });
        }
    }
}
