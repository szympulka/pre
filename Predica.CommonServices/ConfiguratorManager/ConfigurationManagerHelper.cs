using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Predica.CommonServices.ConfiguratorManager
{
    public class ConfigurationManagerHelper : IConfigurationManagerHelper
    {
        /// <summary>
        /// Class ConfigurationManagerHelper.
        /// </summary>
        /// <seealso cref="IConfigurationManagerHelper" />
        private readonly IConfiguration _configuration;
        public ConfigurationManagerHelper(IConfiguration config)
        {
            _configuration = config;
        }

        public string AzureStorage_Name => _configuration["AzureStorage:Name"];

        public string AzureStorage_DefaultConnection => _configuration["AzureStorage:DefaultConnection"];

        public string AzureStorage_Key => _configuration["AzureStorage:Key"];

        public string AzureStorage_TravelTableName => _configuration["AzureStorage:TravelTableName"];

        public string Role_Admin => _configuration["Role:Admin"];

        public string Role_User => _configuration["Role:User"];

        public string Sendgrid_Key => _configuration["Sendgrid:Key"];
        public string Sendgrid_AdminMail => _configuration["Sendgrid:AdminMail"];
    }
}
