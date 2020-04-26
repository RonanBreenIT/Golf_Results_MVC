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
    public class ManageControllerTests
    {
        [TestMethod()]
        public void AddPhoneNumberViewTest()
        {
            var controller = new ManageController();
            var result = controller.AddPhoneNumber() as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod()]
        public void ChangePasswordTestView()
        {
            var controller = new ManageController();
            var result = controller.ChangePassword() as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod()]
        public void SetPasswordTestView()
        {
            var controller = new ManageController();
            var result = controller.SetPassword() as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("", result.ViewName);
        }

    }
}