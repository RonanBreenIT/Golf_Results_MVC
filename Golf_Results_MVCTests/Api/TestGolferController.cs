//using Golf_Results_MVC.Api;
//using Golf_Results_MVC.Models;
//using Golf_Results_MVCTests.Tests;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Text;
//using System.Threading.Tasks;
//using System.Web.Http;
//using System.Web.Http.Results;
//using System.Web.Mvc;

//namespace Golf_Results_MVCTests.Controllers
//{
//    [TestClass]
//    public class TestGolferController
//    {
//        [TestMethod]
//        public void PostGolfer_ShouldReturnSameGolfer()
//        {
//            var controller = new GolferController(new TestGolfContext());

//            var item = GetDemoGolfer();

//            var result =
//                controller.PostGolfer(item) as CreatedAtRouteNegotiatedContentResult<Golfer>;

//            Assert.IsNotNull(result);
//            Assert.AreEqual(result.RouteName, "AddGolfer");
//            Assert.AreEqual(result.RouteValues["ID"], result.Content.ID);
//            Assert.AreEqual(result.Content.Firstname, item.Firstname);
//        }

//        [TestMethod]
//        public void PostGolfer_CantAddSameName()
//        {
//            var controller = new GolferController(new TestGolfContext());

//            var item = GetDemoGolfer();

//            var result = controller.PostGolfer(item) as CreatedAtRouteNegotiatedContentResult<Golfer>;
//            var SameName = controller.PostGolfer(item) as CreatedAtRouteNegotiatedContentResult<Golfer>;
//            Assert.IsNotNull(result); // Check that first post is not null.
//            Assert.IsNull(SameName); // Check that wasn't SameName wasn't added by confirming it is null.
//        }

//        [TestMethod]
//        public void PutGolfer_ShouldReturnStatusCode()
//        {
//            var controller = new GolferController(new TestGolfContext());

//            var item = GetDemoGolfer();

//            var result = controller.PutGolfer(item.ID, item) as StatusCodeResult;
//            Assert.IsNotNull(result);
//            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
//            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
//        }

//        [TestMethod]
//        public void PutGolfer_ShouldFail_WhenDifferentID()
//        {
//            var controller = new GolferController(new TestGolfContext());

//            var badresult = controller.PutGolfer(999, GetDemoGolfer());
//            Assert.IsInstanceOfType(badresult, typeof(BadRequestErrorMessageResult));
//        }

//        [TestMethod]
//        public void PutGolfer_UpdatedName()
//        {
//            var controller = new GolferController(new TestGolfContext());

//            var item = GetDemoGolfer();
//            var put1 = controller.PutGolfer(item.ID, item);
//            Assert.AreEqual("Demo Test", item.FullName);

//            Golfer updatedGolfer = new Golfer() { ID = 3, Firstname = "Rich", Surname = "Beem" };
//            var put2 = controller.PutGolfer(item.ID, updatedGolfer);
//            Assert.AreEqual("Rich Beem", updatedGolfer.FullName);
//        }

//        [TestMethod]
//        public void GetGolfer_ShouldReturnGolferWithSameID()
//        {
//            var context = new TestGolfContext();
//            context.Golfers.Add(GetDemoGolfer());

//            var controller = new GolferController(context);
//            var result = controller.GetGolfer(3) as OkNegotiatedContentResult<Golfer>;

//            Assert.IsNotNull(result);
//            Assert.AreEqual(3, result.Content.ID);
//        }

//        [TestMethod]
//        public void GetGolfer_ShouldReturnAllGolfers()
//        {
//            var context = new TestGolfContext();
//            context.Golfers.Add(new Golfer { ID = 1, Firstname = "Demo1", Surname = "Test" });
//            context.Golfers.Add(new Golfer { ID = 2, Firstname = "Demo2", Surname = "Test" });
//            context.Golfers.Add(new Golfer { ID = 3, Firstname = "Demo3", Surname = "Test" });


//            var controller = new GolferController(context);
//            var result = controller.GetGolfers() as TestGolfDbSet;

//            Assert.IsNotNull(result);
//            Assert.AreEqual(3, result.Local.Count);
//        }

//        [TestMethod]
//        public void DeleteGolfer_ShouldReturnOK()
//        {
//            var context = new TestGolfContext();
//            var item = GetDemoGolfer();
//            context.Golfers.Add(item);

//            var controller = new GolferController(context);
//            var result = controller.DeleteGolfer(3) as OkNegotiatedContentResult<Golfer>;

//            Assert.IsNotNull(result);
//            Assert.AreEqual(item.ID, result.Content.ID);
//        }

//        [TestMethod]
//        public void GetGolfer_OutOfRange()
//        {
//            var context = new TestGolfContext();
//            var controller = new GolferController(context);

//            IHttpActionResult actionResult = controller.GetGolfer(100);
//            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
//        }

//        Golfer GetDemoGolfer()
//        {
//            return new Golfer() { ID = 3, Firstname = "Demo", Surname = "Test" };
//        }
//    }
//}
