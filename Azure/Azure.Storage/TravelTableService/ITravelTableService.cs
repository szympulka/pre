using Predica.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Azure.Storage.TravelTableService
{
    public interface ITravelTableService
    {
        IEnumerable<TravelModel> GetAllTravelList();
        IEnumerable<TravelModel> GetTravelUserList(string name);
        Task<string> CreateAsync(CreateTravelViewModel model);
        Task<TravelModel> GetTravelDetailsAsync(string rowKey, string partitionKey);
        Task EditAsync(EditTravelViewModel model);
    }
}
