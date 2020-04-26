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
    public class AccountControllerTests
    {
        [TestMethod()]
        public void LoginTestView()
        {
            var controller = new AccountController();
            var result = controller.Login("http://localhost:55215/Account/Login") as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod()]
        public void RegisterTestView()
        {
            var controller = new AccountController();
            var result = controller.Register() as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod()]
        public void ForgotPasswordTestView()
        {
            var controller = new AccountController();
            var result = controller.ForgotPassword() as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod()]
        public void ForgotPasswordConfirmationTestView()
        {
            var controller = new AccountController();
            var result = controller.ForgotPasswordConfirmation() as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod()]
        public void ResetPasswordConfirmationTestView()
        {
            var controller = new AccountController();
            var result = controller.ResetPasswordConfirmation() as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod()]
        public void ExternalLoginTestView()
        {
            var controller = new AccountController();
            var result = controller.ExternalLoginFailure() as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("", result.ViewName);
        }
    }
}