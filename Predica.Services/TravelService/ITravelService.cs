using Predica.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Predica.Services.AzureStorageService
{
    public interface ITravelService
    {
        IEnumerable<GetTravelListViewModel> GetTravelList();
        IEnumerable<GetTravelListViewModel> GetTravelUserList(string name);
        Task<string> CreateAsync(CreateTravelViewModel model);
        Task<GetDetailsTravelViewModel> GetTravelDetailsAsync(string rowKey, string partitionKey);
        Task EditAsync(EditTravelViewModel model);
    }
}
