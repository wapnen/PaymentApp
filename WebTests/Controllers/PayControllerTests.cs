using Microsoft.VisualStudio.TestTools.UnitTesting;
using Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Web.Controllers.Tests
{
    [TestClass()]
    public class PayControllerTests
    {
        [TestMethod()]
        public void IndexTest()
        {
            //Arrange
            PayController payController = new PayController();

            //Act
            ViewResult viewResult = payController.Index() as ViewResult;

            //Assert
            Assert.IsNotNull(viewResult);
        }

        [TestMethod()]
        public void ConfirmAsyncTest()
        {
            //Arrange 
            PayController payController = new PayController();
            String token = "dafadfsdfd";
            var actionResultTask = payController.ConfirmAsync(token);

            //Act
            actionResultTask.Wait();
            var result = actionResultTask.Result as RedirectToRouteResult;

            //Assert
            result.RouteValues["Action"].Equals("Index");
            result.RouteValues["controller"].Equals("Pay");
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsNotNull(result);
           
        }
    }
}