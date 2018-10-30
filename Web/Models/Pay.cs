using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class Pay : TableEntity
    {
        public string Token;

        public Pay(string token)
        {
            this.PartitionKey = token;
            this.RowKey = token;
        }

        public Pay() { }
    }
}