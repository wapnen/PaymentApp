using Microsoft.VisualStudio.TestTools.UnitTesting;
using Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;

namespace Web.Controllers.Tests
{
    [TestClass()]
    public class HomeControllerTests
    {
        [TestMethod()]
        public void IndexTest()
        {
            //Arrange
            HomeController controller = new HomeController();

            //Act 
            var result = controller.Index() as RedirectToRouteResult;

            //Assert
            result.RouteValues["Action"].Equals("Index");
            result.RouteValues["controller"].Equals("Pay");
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Pay", result.RouteValues["controller"]);
        }
        
    }
}