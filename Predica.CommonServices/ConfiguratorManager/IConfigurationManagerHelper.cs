using System;
using System.Collections.Generic;
using System.Text;

namespace Predica.CommonServices.ConfiguratorManager
{
    public interface IConfigurationManagerHelper
    {
        //STORAGE
        string AzureStorage_Name { get; }
        string AzureStorage_DefaultConnection { get; }
        string AzureStorage_Key { get; }
        string AzureStorage_TravelTableName { get; }
        //ROLE
        string Role_Admin{ get; }
        string Role_User{ get; }
        //EMAIL
        string Sendgrid_Key { get;  }
        string Sendgrid_AdminMail { get;  }
    }
}
