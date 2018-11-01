using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Controllers
{
    public class PayController : Controller
    {
        // GET: Pay
        public ActionResult Index()
        {
            return View();
        }

        //store card token
        [HttpPost]
        public async Task<ActionResult> ConfirmAsync(string token)
        {
            var AzureStorageAccountName = System.Configuration.ConfigurationManager.AppSettings["azureStorageAccount"].ToString();
            var AzureStorageKey = System.Configuration.ConfigurationManager.AppSettings["azureStorageKey"].ToString();

            CloudStorageAccount storageAccount = new CloudStorageAccount(
            new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials(
            AzureStorageAccountName, AzureStorageKey), true);
            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable paymentTable = tableClient.GetTableReference("payments");
            //create new payment entity
            Pay pay = new Pay(token);
            //create the table operation that inserts the customer entity
            TableOperation insertOperation = TableOperation.Insert(pay);
            await paymentTable.ExecuteAsync(insertOperation);
            return RedirectToAction("Index");
        }
    }
}