using Azure.Sendgrid;
using Azure.Storage.TravelTableService;
using Microsoft.Extensions.DependencyInjection;
using Predica.CommonServices.ConfiguratorManager;
using Predica.Services.AzureStorageService;

namespace Predica.Services
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection ServiceExtension(this IServiceCollection services)
        {
            services.AddTransient<ITravelTableService, TravelTableService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<ITravelService, TravelService>();
            services.AddTransient<IConfigurationManagerHelper, ConfigurationManagerHelper>();

            return services;
        }
    }
}
