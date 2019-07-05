using System;
using System.Collections.Generic;
using System.Text;

namespace Predica.ViewModel
{
    public class GetDetailsTravelViewModel
    {
        public string Title { get; set; }
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public string TimeCreated { get; set; }
        public string Content { get; set; }
    }
}
