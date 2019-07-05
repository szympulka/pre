using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Azure.Sendgrid;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Predica.CommonServices.ConfiguratorManager;
using Predica.Services.AzureStorageService;
using Predica.Web.Models;

namespace Predica.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ITravelService _travelService;
        private readonly IEmailService _emailService;
        private readonly IConfigurationManagerHelper _configurationManagerHelper;
        public HomeController(ITravelService travelService, IConfigurationManagerHelper configurationManagerHelper, IEmailService emailService)
        {
            _travelService = travelService;
            _emailService = emailService;
            _configurationManagerHelper = configurationManagerHelper;
        }
        public IActionResult Index()
        {
            if (User.IsInRole(_configurationManagerHelper.Role_Admin))
            {
                return View(_travelService.GetTravelList());
            }
            var model = _travelService.GetTravelUserList(User.Identity.Name);
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> SendMail(string mail)
        {
           await  _emailService.SendEmailasync(mail,"Witam w systemie");
            return Ok("Wysłano!");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
