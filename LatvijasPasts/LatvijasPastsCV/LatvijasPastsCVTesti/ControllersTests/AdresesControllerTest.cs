using Microsoft.AspNetCore.Mvc;
using Moq;
using Pasts.Controllers;
using LatvijasPastsCV.Models;
using LatvijasPastsCV.Services;
using Microsoft.EntityFrameworkCore;

namespace Pasts.Tests.Controllers
{
    [TestClass]
    public class AdreseControllerTests
    {
        private Mock<IAdreseService> _mockAdreseService;
        private AdreseController _controller;

        [TestInitialize]
        public void SetUp()
        {
            _mockAdreseService = new Mock<IAdreseService>();
            _controller = new AdreseController(_mockAdreseService.Object);
        }

        [TestMethod]
        public void Index_ReturnsViewResult_WithListOfAdrese()
        {
            var adreses = new List<Adrese> { new Adrese(), new Adrese() };
            _mockAdreseService.Setup(s => s.GetAll()).Returns(adreses);

            var result = _controller.Index();

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual(adreses, viewResult.Model);
        }

        [TestMethod]
        public void Details_NullId_ReturnsNotFoundResult()
        {
            var result = _controller.Details(null);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void Details_ValidId_ReturnsViewResultWithAdrese()
        {
            var expectedAdrese = new Adrese { ID = 1 };
            _mockAdreseService.Setup(s => s.GetById(1)).Returns(expectedAdrese);

            var result = _controller.Details(1);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual(expectedAdrese, viewResult.Model);
        }

        [TestMethod]
        public void Details_InvalidId_ReturnsNotFoundResult()
        {
            _mockAdreseService.Setup(s => s.GetById(It.IsAny<int>())).Returns((Adrese)null);

            var result = _controller.Details(1);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void Create_InvalidModelState_ReturnsSameViewWithAdrese()
        {
            var adrese = new Adrese();
            _controller.ModelState.AddModelError("Error", "Sample error");

            var result = _controller.Create(adrese);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual(adrese, viewResult.Model);
        }

        [TestMethod]
        public void Edit_ValidIdInvalidModelState_ReturnsSameViewWithAdrese()
        {
            var adrese = new Adrese { ID = 1 };
            _controller.ModelState.AddModelError("Error", "Sample error");

            var result = _controller.Edit(1, adrese);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual(adrese, viewResult.Model);
        }

        [TestMethod]
        public void Delete_InvalidId_ReturnsNotFoundResult()
        {
            _mockAdreseService.Setup(s => s.GetById(It.IsAny<int>())).Returns((Adrese)null);

            var result = _controller.Delete(1);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void DeleteConfirmed_ValidId_RedirectsToIndex()
        {
            var result = _controller.DeleteConfirmed(1);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            var redirectResult = result as RedirectToActionResult;
            Assert.AreEqual("Index", redirectResult.ActionName);
        }

        [TestMethod]
        public void Create_ValidModelState_RedirectsToIndex()
        {
            var adrese = new Adrese();

            var result = _controller.Create(adrese);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            var redirectResult = result as RedirectToActionResult;
            Assert.AreEqual("Index", redirectResult.ActionName);
        }

        [TestMethod]
        public void Edit_MismatchedId_ReturnsNotFoundResult()
        {
            var adrese = new Adrese { ID = 2 };

            var result = _controller.Edit(1, adrese);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void Edit_DbUpdateConcurrencyExceptionAndAdreseNotExists_ReturnsNotFoundResult()
        {
            var adrese = new Adrese { ID = 1 };
            _mockAdreseService.Setup(s => s.Update(adrese)).Throws(new DbUpdateConcurrencyException());
            _mockAdreseService.Setup(s => s.GetAll()).Returns(new List<Adrese>());

            var result = _controller.Edit(1, adrese);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        [ExpectedException(typeof(DbUpdateConcurrencyException))]
        public void Edit_DbUpdateConcurrencyExceptionAndAdreseExists_ThrowsException()
        {
            var adrese = new Adrese { ID = 1 };
            _mockAdreseService.Setup(s => s.Update(adrese)).Throws(new DbUpdateConcurrencyException());
            _mockAdreseService.Setup(s => s.GetAll()).Returns(new List<Adrese> { adrese });

            _controller.Edit(1, adrese);
        }

        [TestMethod]
        public void Delete_ValidId_ReturnsViewWithAdrese()
        {
            var expectedAdrese = new Adrese { ID = 1 };
            _mockAdreseService.Setup(s => s.GetById(1)).Returns(expectedAdrese);

            var result = _controller.Delete(1);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual(expectedAdrese, viewResult.Model);
        }
    }
}