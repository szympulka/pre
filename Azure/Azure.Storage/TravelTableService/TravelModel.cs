using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.WindowsAzure.Storage.Table;

namespace Azure.Storage.TravelTableService
{
    public class TravelModel : TableEntity
    {
        public TravelModel()
        {

        }
        public TravelModel(string userName, string id)
        {
            PartitionKey = userName;
            RowKey = id;
        }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
