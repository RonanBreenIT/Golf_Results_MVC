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
    public class GolferControllerTests
    {
        [TestMethod()]
        public void IndexTestView()
        {
            var controller = new GolferController();
            var result = controller.Index("1", "1", "1", 50) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod()]
        public void CreateTestView()
        {
            var controller = new GolferController();
            var result = controller.Create() as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("", result.ViewName);
        }

    }
}