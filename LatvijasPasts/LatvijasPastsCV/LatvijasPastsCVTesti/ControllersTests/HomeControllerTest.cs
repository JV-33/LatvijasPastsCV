using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using LatvijasPastsCV.Controllers;
using LatvijasPastsCV.Models;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace Pasts.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTests
    {
        private Mock<ILogger<HomeController>> _loggerMock;
        private HomeController _controller;

        [TestInitialize]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<HomeController>>();
            _controller = new HomeController(_loggerMock.Object);
        }

        [TestMethod]
        public void Index_ReturnsAViewResult()
        {
            var result = _controller.Index();

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Error_ReturnsAViewResult_With_ErrorViewModel()
        {
            var result = _controller.Error();

            Assert.IsNotNull(result, "Result is null"); 
            Assert.IsInstanceOfType(result, typeof(ViewResult), "Result is not of type ViewResult");

            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult.Model, "Model is null"); 
            Assert.IsInstanceOfType(viewResult.Model, typeof(ErrorViewModel), "Model is not of type ErrorViewModel");
        }

        [TestMethod]
        public void Error_WhenHttpContextTraceIdentifierNotNullButActivityCurrentIsNull_ReturnsCorrectRequestId()
        {
            // Set up Activity.Current to null and set HttpContext.TraceIdentifier
            var testId = "TestId";
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { TraceIdentifier = testId }
            };

            var result = _controller.Error() as ViewResult;
            var model = result.Model as ErrorViewModel;

            Assert.AreEqual(testId, model.RequestId);
        }

        [TestMethod]
        public void Error_WhenBothActivityCurrentAndHttpContextTraceIdentifierAreNull_ReturnsNA()
        {
            // Mock the HttpContext and TraceIdentifier to return null.
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.SetupGet(a => a.TraceIdentifier).Returns((string)null);

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = mockHttpContext.Object
            };

            var result = _controller.Error() as ViewResult;
            var model = result.Model as ErrorViewModel;

            Assert.AreEqual("N/A", model.RequestId);
        }

        [TestMethod]
        public void Error_WhenActivityCurrentNotNull_ReturnsCorrectRequestId()
        {
            // Set up Activity.Current
            var activityName = "TestActivity";
            var activity = new Activity(activityName).Start();

            var result = _controller.Error() as ViewResult;
            var model = result.Model as ErrorViewModel;

            Assert.AreEqual(activity.Id, model.RequestId);  // Use the Id property of the activity

            activity.Stop();
        }




    }
}