using Microsoft.VisualStudio.TestTools.UnitTesting;
using Golf_Results_MVC.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Golf_Results_MVC.Models;

namespace Golf_Results_MVC.Controllers.Tests
{
    [TestClass()]
    public class CompResultControllerTests
    {

        [TestMethod()]
        public void CreateTestView()
        {
            var controller = new CompResultController();
            var result = controller.Create() as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("", result.ViewName);
        }

    }
}