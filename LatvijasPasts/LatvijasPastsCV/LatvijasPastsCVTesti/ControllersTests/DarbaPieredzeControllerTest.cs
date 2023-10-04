using Microsoft.AspNetCore.Mvc;
using Moq;
using Pasts.Controllers;
using LatvijasPastsCV.Models;
using LatvijasPastsCV.Services;

namespace Pasts.Tests.Controllers
{
    [TestClass]
    public class DarbaPieredzeControllerTests
    {
        private Mock<IDarbaPieredzeService> _mockService;
        private DarbaPieredzeController _controller;

        [TestInitialize]
        public void SetUp()
        {
            _mockService = new Mock<IDarbaPieredzeService>();
            _controller = new DarbaPieredzeController(_mockService.Object);
        }

        [TestMethod]
        public void Index_ReturnsAViewResult_WithAListOfDarbaPieredze()
        {
            var list = new List<DarbaPieredze> { new DarbaPieredze() };
            _mockService.Setup(service => service.GetAll()).Returns(list);

            var result = _controller.Index();

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.IsInstanceOfType(viewResult.Model, typeof(List<DarbaPieredze>));
            Assert.AreEqual(list.Count, ((List<DarbaPieredze>)viewResult.Model).Count);
        }

        [TestMethod]
        public void Details_NullId_ReturnsNotFoundResult()
        {
            var result = _controller.Details(null);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void Details_ValidId_ReturnsViewResult_WithDarbaPieredze()
        {

            var id = 1;
            var darbaPieredze = new DarbaPieredze { ID = id };
            _mockService.Setup(service => service.GetById(id)).Returns(darbaPieredze);

            var result = _controller.Details(id);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.IsInstanceOfType(viewResult.Model, typeof(DarbaPieredze));
            Assert.AreEqual(darbaPieredze, viewResult.Model);
        }

        [TestMethod]
        public void Details_InvalidId_ReturnsNotFoundResult()
        {
            var id = 1;
            _mockService.Setup(service => service.GetById(id)).Returns((DarbaPieredze)null);

            var result = _controller.Details(id);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void Create_InvalidModel_ReturnsViewWithModel()
        {
            var darbaPieredze = new DarbaPieredze();
            _controller.ModelState.AddModelError("Error", "Some error");

            var result = _controller.Create(darbaPieredze);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual(darbaPieredze, viewResult.Model);
        }

        [TestMethod]
        public void Edit_GetValidId_ReturnsViewResult_WithDarbaPieredze()
        {
            var id = 1;
            var darbaPieredze = new DarbaPieredze { ID = id };
            _mockService.Setup(service => service.GetById(id)).Returns(darbaPieredze);

            var result = _controller.Edit(id);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual(darbaPieredze, viewResult.Model);
        }

        [TestMethod]
        public void Delete_ValidId_ReturnsViewResult_WithDarbaPieredze()
        {
            var id = 1;
            var darbaPieredze = new DarbaPieredze { ID = id };
            _mockService.Setup(service => service.GetById(id)).Returns(darbaPieredze);

            var result = _controller.Delete(id);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual(darbaPieredze, viewResult.Model);
        }
    }
}