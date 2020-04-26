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
    public class ImportControllerTests
    {
        [TestMethod()]
        public void UploadGolfersTestView()
        {
            var controller = new ImportController();
            var result = controller.UploadGolfers() as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod()]
        public void UploadCompsTestView()
        {
            var controller = new ImportController();
            var result = controller.UploadComps() as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod()]
        public void UploadResultsTestView()
        {
            var controller = new ImportController();
            var result = controller.UploadCompResults() as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("", result.ViewName);
        }
    }
}