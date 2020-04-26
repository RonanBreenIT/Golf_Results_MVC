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

//namespace Golf_Results_MVCTests.Controllers
//{
//    [TestClass]
//    public class TestCompetitionController
//    {
//        [TestMethod]
//        public void PostComp_ShouldReturnSameComp()
//        {
//            var controller = new CompetitionController(new TestGolfContext());

//            var item = GetDemoComp();

//            var result =
//                controller.PostComp(item) as CreatedAtRouteNegotiatedContentResult<Competition>;

//            Assert.IsNotNull(result);
//            Assert.AreEqual(result.RouteName, "AddCompetition");
//            Assert.AreEqual(result.RouteValues["ID"], result.Content.ID);
//            Assert.AreEqual(result.Content.Name, item.Name);
//        }

//        [TestMethod]
//        public void PostComp_CantAddSameName()
//        {
//            var controller = new CompetitionController(new TestGolfContext());

//            var item = GetDemoComp();

//            var result = controller.PostComp(item) as CreatedAtRouteNegotiatedContentResult<Competition>;
//            var SameName = controller.PostComp(item) as CreatedAtRouteNegotiatedContentResult<Competition>;
//            Assert.IsNotNull(result); // Check that first post is not null.
//            Assert.IsNull(SameName); // Check that wasn't SameName wasn't added by confirming it is null.
//        }

//        [TestMethod]
//        public void PutComp_ShouldReturnStatusCode()
//        {
//            var controller = new CompetitionController(new TestGolfContext());

//            var item = GetDemoComp();

//            var result = controller.PutComp(item.ID, item) as StatusCodeResult;
//            Assert.IsNotNull(result);
//            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
//            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
//        }

//        [TestMethod]
//        public void PutComp_ShouldFail_WhenDifferentID()
//        {
//            var controller = new CompetitionController(new TestGolfContext());

//            var badresult = controller.PutComp(999, GetDemoComp());
//            Assert.IsInstanceOfType(badresult, typeof(BadRequestErrorMessageResult));
//        }

//        [TestMethod]
//        public void PutComp_UpdatedName()
//        {
//            var controller = new CompetitionController(new TestGolfContext());

//            var item = GetDemoComp();
//            var put1 = controller.PutComp(item.ID, item);
//            Assert.AreEqual("Test", item.Name);

//            Competition updatedComp = new Competition() { ID = 3, Name = "New Comp" };
//            var put2 = controller.PutComp(item.ID, updatedComp);
//            Assert.AreEqual("New Comp", updatedComp.Name);
//        }

//        [TestMethod]
//        public void GetComp_ShouldReturnCompWithSameID()
//        {
//            var context = new TestGolfContext();
//            context.Competitions.Add(GetDemoComp());

//            var controller = new CompetitionController(context);
//            var result = controller.GetComp(3) as OkNegotiatedContentResult<Competition>;

//            Assert.IsNotNull(result);
//            Assert.AreEqual(3, result.Content.ID);
//        }

//        [TestMethod]
//        public void GetComp_ShouldReturnAllComps()
//        {
//            var context = new TestGolfContext();
//            context.Competitions.Add(new Competition { ID = 1, Name = "Test1" });
//            context.Competitions.Add(new Competition { ID = 2, Name = "Test2" });
//            context.Competitions.Add(new Competition { ID = 3, Name = "Test3" });

//            var controller = new CompetitionController(context);
//            var result = controller.GetCompetitions() as TestCompDbSet;

//            Assert.IsNotNull(result);
//            Assert.AreEqual(3, result.Local.Count);
//        }

//        [TestMethod]
//        public void DeleteComp_ShouldReturnOK()
//        {
//            var context = new TestGolfContext();
//            var item = GetDemoComp();
//            context.Competitions.Add(item);

//            var controller = new CompetitionController(context);
//            var result = controller.DeleteComp(3) as OkNegotiatedContentResult<Competition>;

//            Assert.IsNotNull(result);
//            Assert.AreEqual(item.ID, result.Content.ID);
//        }

//        [TestMethod]
//        public void GetComp_OutOfRange()
//        {
//            var context = new TestGolfContext();
//            var controller = new CompetitionController(context);

//            IHttpActionResult actionResult = controller.GetComp(100);
//            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
//        }


//        Competition GetDemoComp()
//        {
//            return new Competition() { ID = 3, Name = "Test" };
//        }
//    }
//}
