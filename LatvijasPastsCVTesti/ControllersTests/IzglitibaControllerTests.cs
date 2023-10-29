using Microsoft.AspNetCore.Mvc;
using Moq;
using Pasts.Controllers;
using LatvijasPastsCV.Services;
using LatvijasPastsCV.Models;

namespace Pasts.Tests.Controllers
{
    [TestClass]
    public class IzglitibaControllerTests
    {
        private Mock<IIzglitibaService> _izglitibaServiceMock;
        private IzglitibaController _controller;

        [TestInitialize]
        public void Setup()
        {
            _izglitibaServiceMock = new Mock<IIzglitibaService>();
            _controller = new IzglitibaController(_izglitibaServiceMock.Object);
        }

        [TestMethod]
        public void Index_ReturnsAViewResult_WithAListOfIzglitiba()
        {
            _izglitibaServiceMock.Setup(service => service.GetAll())
                .Returns(new List<Izglitiba> { new Izglitiba() });

            var result = _controller.Index();

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.IsInstanceOfType(viewResult.Model, typeof(List<Izglitiba>));
        }

        [TestMethod]
        public void Index_ReturnsViewWithEmptyList_WhenServiceReturnsNull()
        {
            _izglitibaServiceMock.Setup(service => service.GetAll()).Returns((List<Izglitiba>)null);

            var result = _controller.Index();

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.IsNull(viewResult.Model);
        }

        [TestMethod]
        public void Details_ReturnsNotFoundResult_WhenIdIsNull()
        {
            var result = _controller.Details(null);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void Details_ReturnsNotFoundResult_WhenIzglitibaIsNull()
        {
            _izglitibaServiceMock.Setup(service => service.GetById(It.IsAny<int>()))
                .Returns((Izglitiba)null);

            var result = _controller.Details(1);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void Details_ReturnsAViewResult_WithIzglitiba()
        {
            var expectedIzglitiba = new Izglitiba();
            _izglitibaServiceMock.Setup(service => service.GetById(It.IsAny<int>()))
                .Returns(expectedIzglitiba);

            var result = _controller.Details(1);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual(expectedIzglitiba, viewResult.Model);
        }

        [TestMethod]
        public void Create_ReturnsAViewResult_WhenModelStateIsInvalid()
        {
            _controller.ModelState.AddModelError("error", "some error");
            var result = _controller.Create(new Izglitiba());

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Create_RedirectsToIndex_WhenModelStateIsValid()
        {
            var result = _controller.Create(new Izglitiba());

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            var redirectResult = result as RedirectToActionResult;
            Assert.AreEqual("Index", redirectResult.ActionName);
        }

        [TestMethod]
        public void Edit_ReturnsNotFoundResult_WhenIdIsNull()
        {
            var result = _controller.Edit(null);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void Edit_ReturnsNotFoundResult_WhenIzglitibaIsNull()
        {
            _izglitibaServiceMock.Setup(service => service.GetById(It.IsAny<int>()))
                .Returns((Izglitiba)null);

            var result = _controller.Edit(1);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void Edit_ReturnsViewWithIzglitiba_WhenModelStateIsInvalid()
        {
            _controller.ModelState.AddModelError("error", "some error");
            var izglitiba = new Izglitiba { ID = 1 };
            _izglitibaServiceMock.Setup(service => service.GetById(1)).Returns(izglitiba);

            var result = _controller.Edit(1, izglitiba);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual(izglitiba, viewResult.Model);
        }


        [TestMethod]
        public void Edit_ReturnsAViewResult_WhenIdIsValid()
        {
            _izglitibaServiceMock.Setup(service => service.GetById(It.IsAny<int>()))
                .Returns(new Izglitiba());

            var result = _controller.Edit(1);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Edit_RedirectsToIndex_WhenModelStateIsValid()
        {
            var result = _controller.Edit(1, new Izglitiba { ID = 1 });

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            var redirectResult = result as RedirectToActionResult;
            Assert.AreEqual("Index", redirectResult.ActionName);
            _izglitibaServiceMock.Verify(service => service.Update(It.IsAny<Izglitiba>()), Times.Once);
        }

        [TestMethod]
        public void Edit_DoesNotUpdate_WhenIDMismatch()
        {
            var result = _controller.Edit(1, new Izglitiba { ID = 2 });

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            _izglitibaServiceMock.Verify(service => service.Update(It.IsAny<Izglitiba>()), Times.Never);
        }

        [TestMethod]
        public void Delete_ReturnsNotFoundResult_WhenIdIsNull()
        {
            var result = _controller.Delete(null);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void Delete_ReturnsNotFoundResult_WhenIzglitibaIsNull()
        {
            _izglitibaServiceMock.Setup(service => service.GetById(It.IsAny<int>()))
                .Returns((Izglitiba)null);

            var result = _controller.Delete(1);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void Delete_ReturnsAViewResult_WithIzglitiba()
        {
            var expectedIzglitiba = new Izglitiba();
            _izglitibaServiceMock.Setup(service => service.GetById(It.IsAny<int>()))
                .Returns(expectedIzglitiba);

            var result = _controller.Delete(1);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual(expectedIzglitiba, viewResult.Model);

        }

        [TestMethod]
        public void DeleteConfirmed_RedirectsToIndex_AfterDeletion()
        {
            var result = _controller.DeleteConfirmed(1);

            _izglitibaServiceMock.Verify(service => service.Delete(It.IsAny<int>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            var redirectResult = result as RedirectToActionResult;
            Assert.AreEqual("Index", redirectResult.ActionName);
        }
    }
}