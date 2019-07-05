using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Predica.CommonServices.ConfiguratorManager;
using Predica.Services.AzureStorageService;
using Predica.ViewModel;

namespace Predica.Web.Controllers
{
    [Authorize]
    public class TravelController : Controller
    {
        private readonly ITravelService _travelService;
        private readonly IConfigurationManagerHelper _configurationManagerHelper;
        public TravelController(ITravelService travelService, IConfigurationManagerHelper configurationManagerHelper)
        {
            _travelService = travelService;
            _configurationManagerHelper = configurationManagerHelper;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTravelViewModel model)
        {
            model.UserName = User.Identity.Name;
            string id = await _travelService.CreateAsync(model);
            return RedirectToAction("Details", "Travel", new { rowKey = id, PartitionKey = User.Identity.Name });
        }

        public async Task<IActionResult> Details(string rowKey, string partitionKey)
        {
            if (!User.IsInRole(_configurationManagerHelper.Role_Admin) && partitionKey != User.Identity.Name)
            {
                return Unauthorized();
            }

            GetDetailsTravelViewModel model = await _travelService.GetTravelDetailsAsync(rowKey, partitionKey);
            return View(model);
        }

        public IActionResult Edit(EditTravelViewModel model)
        {
            if (!User.IsInRole(_configurationManagerHelper.Role_Admin) && model.PartitionKey != User.Identity.Name)
            {
                return Unauthorized();
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditTravel(EditTravelViewModel model)
        {
            if (!User.IsInRole(_configurationManagerHelper.Role_Admin) && model.PartitionKey != User.Identity.Name)
            {
                return Unauthorized();
            }
            await _travelService.EditAsync(model);
            return RedirectToAction("Details", "Travel", new { rowKey = model.RowKey, partitionKey = model.PartitionKey });
        }
    }
}