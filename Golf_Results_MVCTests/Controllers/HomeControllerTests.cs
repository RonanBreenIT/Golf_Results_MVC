using Microsoft.VisualStudio.TestTools.UnitTesting;
using Golf_Results_MVC.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Golf_Results_MVC.Controllers.Tests
{
    [TestClass()]
    public class HomeControllerTests
    {

        [TestMethod()]
        public void SampleTest() // Just checking HomeController = HomeController
        {
            Assert.AreEqual("HomeController", "HomeController");
        }

        [TestMethod()]
        public void TypeOfIndex() // Just chekcing Index is not null and is a type of view
        {
            var controller = new HomeController();
            var result = controller.Index();
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod()]
        public void IndexTest()
        {
            HomeController controllerUnderTest = new HomeController();
            var result = controllerUnderTest.Index() as ViewResult;
            Assert.AreEqual("Index", result.ViewName); // Just checking the view returned is Index
            //Assert.Fail();
        }

        // just a reference if have a viewbag in controller which we don't
        //[TestMethod()]
        //public void IndexTestViewBag()
        //{
        //    HomeController controllerUnderTest = new HomeController();
        //    var result = controllerUnderTest.Index() as ViewResult;
        //    Assert.AreEqual("Home Page", result.ViewData["Title"]);
        //}



        //[TestMethod()] // Not using About at present
        //public void AboutTest()
        //{
        //    HomeController controllerUnderTest = new HomeController();
        //    var result = controllerUnderTest.Index() as ViewResult;
        //    //Assert.AreEqual("Index", result.ViewName); // Just checking the view returned is About
        //   // Assert.Fail();
        //}

        //[TestMethod()] // Not using About at present
        //public void ContactTest()
        //{
        //    HomeController controllerUnderTest = new HomeController();
        //    var result = controllerUnderTest.Index() as ViewResult;
        //    //Assert.AreEqual("Contact", result.ViewName); // Just checking the view returned is Contact
        //    //Assert.Fail();
        //}
    }
}