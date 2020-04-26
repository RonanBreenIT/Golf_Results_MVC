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
//    public class TestCompResultController
//    {
//        [TestMethod]
//        public void PostCompResult_ShouldReturnSameCompResult()
//        {
//            var controller = new CompResultController(new TestGolfContext());

//            var item = GetDemoCompResult();

//            var result =
//                controller.PostComp_Result(item) as CreatedAtRouteNegotiatedContentResult<Comp_Result>;

//            Assert.IsNotNull(result);
//            Assert.AreEqual(result.RouteName, "AddCompResult");
//            Assert.AreEqual(result.RouteValues["ID"], result.Content.CompResultID);
//            Assert.AreEqual(result.Content.CompetitionID, item.CompetitionID);
//        }

//        [TestMethod]
//        public void PutCompResult_ShouldReturnStatusCode()
//        {
//            var controller = new CompResultController(new TestGolfContext());

//            var item = GetDemoCompResult();

//            var result = controller.PutComp_Result(item.CompResultID, item) as StatusCodeResult;
//            Assert.IsNotNull(result);
//            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
//            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
//        }

//        [TestMethod]
//        public void PutCompResult_ShouldFail_WhenDifferentID()
//        {
//            var controller = new CompResultController(new TestGolfContext());

//            var badresult = controller.PutComp_Result(999, GetDemoCompResult());
//            Assert.IsInstanceOfType(badresult, typeof(BadRequestErrorMessageResult));
//        }

//        [TestMethod]
//        public void GetCompResult_ShouldReturnCompResultWithSameID()
//        {
//            var context = new TestGolfContext();
//            context.Comp_Results.Add(GetDemoCompResult());

//            var controller = new CompResultController(context);
//            var result = controller.GetCompResultID(3) as OkNegotiatedContentResult<Comp_Result>;

//            Assert.IsNotNull(result);
//            Assert.AreEqual(3, result.Content.CompResultID);
//        }

//        // Need to update
//        //[TestMethod]
//        //public void GetCompResult_ShouldReturnAllCompsWithCompID()
//        //{
//        //    var context = new TestGolfContext();
//        //    context.Comp_Results.Add(new Comp_Result { CompResultID = 1, CompetitionID = 1, Season = 2020, StartDate = new DateTime(2020, 01, 01), EndDate = new DateTime(2020, 01, 04), GolferID = 1, Position = "1", GolferScore = "-7" });
//        //    context.Comp_Results.Add(new Comp_Result { CompResultID = 2, CompetitionID = 1, Season = 2020, StartDate = new DateTime(2020, 01, 01), EndDate = new DateTime(2020, 01, 04), GolferID = 2, Position = "2", GolferScore = "Evs" });
//        //    context.Comp_Results.Add(new Comp_Result { CompResultID = 3, CompetitionID = 1, Season = 2020, StartDate = new DateTime(2020, 01, 01), EndDate = new DateTime(2020, 01, 04), GolferID = 3, Position = "3", GolferScore = "+2" });

//        //    var controller = new CompResultController(context);
//        //    //var result = controller.GetCompID(1) as TestCompResultDbSet;

//        //    IHttpActionResult actionResult = controller.GetCompID(1);
//        //    //var contentResult = actionResult as OkNegotiatedContentResult<Comp_Result>;
//        //    var contentResult = actionResult as TestCompResultDbSet;


//        //    Assert.IsNotNull(contentResult);
//        //    //Assert.IsNotNull(contentResult.Content);
//        //    //Assert.AreEqual(42, contentResult.Content.CompetitionID);
//        //}

//        // Need to update
//        //[TestMethod]
//        //public void GetCompIDPerSeason_ShouldReturnResultWithSameCompIDGetPerSeason()
//        //{
//        //    var context = new TestGolfContext();
//        //    context.Comp_Results.Add(GetDemoCompResult());

//        //    var controller = new CompResultController(context);

            
//        //    var result = controller.GetCompResultPerSeason(1,2020) as OkNegotiatedContentResult<Comp_Result>;



//        //    Assert.IsNotNull(result);
//        //    Assert.AreEqual(1, result.Content.CompetitionID);
//        //    Assert.AreEqual(2020, result.Content.Season);
//        //}

//        [TestMethod]
//        public void GetCompResult_ShouldReturnAllComps()
//        {
//            var context = new TestGolfContext();
//            context.Comp_Results.Add(new Comp_Result { CompResultID = 1, CompetitionID = 1, Season = 2020, StartDate = new DateTime(2020, 01, 01), EndDate = new DateTime(2020, 01, 04), GolferID = 1, Position = "1", GolferScore = "-7" });
//            context.Comp_Results.Add(new Comp_Result { CompResultID = 2, CompetitionID = 1, Season = 2020, StartDate = new DateTime(2020, 01, 01), EndDate = new DateTime(2020, 01, 04), GolferID = 2, Position = "2", GolferScore = "Evs" });
//            context.Comp_Results.Add(new Comp_Result { CompResultID = 3, CompetitionID = 1, Season = 2020, StartDate = new DateTime(2020, 01, 01), EndDate = new DateTime(2020, 01, 04), GolferID = 3, Position = "3", GolferScore = "+2" });

//            var controller = new CompResultController(context);
//            var result = controller.GetComp_Results() as TestCompResultDbSet;

//            Assert.IsNotNull(result);
//            Assert.AreEqual(3, result.Local.Count);
//        }

//        [TestMethod]
//        public void DeleteCompResult_ShouldReturnOK()
//        {
//            var context = new TestGolfContext();
//            var item = GetDemoCompResult();
//            context.Comp_Results.Add(item);

//            var controller = new CompResultController(context);
//            var result = controller.DeleteComp_Result(3) as OkNegotiatedContentResult<Comp_Result>;

//            Assert.IsNotNull(result);
//            Assert.AreEqual(item.CompResultID, result.Content.CompResultID);
//        }

//        [TestMethod]
//        public void GetCompResult_OutOfRange()
//        {
//            var context = new TestGolfContext();
//            var controller = new CompResultController(context);

//            IHttpActionResult actionResult = controller.GetCompResultID(100);
//            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
//        }

//        Comp_Result GetDemoCompResult()
//        {
//            return new Comp_Result() { CompResultID = 3, CompetitionID = 1, Season = 2020, StartDate = new DateTime(2020,01,01), EndDate = new DateTime(2020, 01, 04), GolferID = 3, Position = "3", GolferScore = "+2" };
//        }
//    }
//}