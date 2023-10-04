using Moq;
using Pasts.Controllers;
using LatvijasPastsCV.Services;
using LatvijasPastsCV.Models;
using Microsoft.AspNetCore.Mvc;

namespace Pasts.Tests.Controllers
{
    [TestClass]
    public class PamatdatiControllerTests
    {
        private Mock<IPamatdatiService> _serviceMock;
        private PamatdatiController _controller;

        [TestInitialize]
        public void Setup()
        {
            _serviceMock = new Mock<IPamatdatiService>();
            _controller = new PamatdatiController(_serviceMock.Object);
        }

        [TestMethod]
        public void Index_ReturnsAViewResult_WithAListOfPamatdati()
        {
            _serviceMock.Setup(service => service.GetAll()).Returns(new List<Pamatdati>());

            var result = _controller.Index();

            Assert.IsInstanceOfType(result, typeof(ViewResult), "Index method does not return a ViewResult");
            var viewResult = result as ViewResult;
            Assert.IsInstanceOfType(viewResult.Model, typeof(List<Pamatdati>), "Model is not a List of Pamatdati");
        }

        [TestMethod]
        public void Details_InvalidId_ReturnsNotFoundResult()
        {
            int? id = null; 

            var result = _controller.Details(id);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult), "Details method with invalid id does not return NotFoundResult");
        }

        [TestMethod]
        public void Details_ValidId_ReturnsAViewResult_WithPamatdati()
        {
            int id = 1;
            var expectedPamatdati = new Pamatdati(); 
            _serviceMock.Setup(service => service.GetById(id)).Returns(expectedPamatdati);

            var result = _controller.Details(id);

            Assert.IsInstanceOfType(result, typeof(ViewResult), "Details method does not return a ViewResult");
            var viewResult = result as ViewResult;
            Assert.AreEqual(expectedPamatdati, viewResult.Model, "Model does not match the expected Pamatdati");
        }

        [TestMethod]
        public void Create_ServiceAddFails_ReturnsAViewResult_WithPamatdati()
        {
            var newPamatdati = new Pamatdati();
            _serviceMock.Setup(service => service.Add(newPamatdati)).Throws(new Exception("Service failed"));

            var result = _controller.Create(newPamatdati);

            Assert.IsInstanceOfType(result, typeof(ViewResult), "Create method does not return a ViewResult when service Add fails");
            var viewResult = result as ViewResult;
            Assert.AreEqual(newPamatdati, viewResult.Model, "Model does not match the provided Pamatdati when service Add fails");
        }


        [TestMethod]
        public void Create_ValidObject_ReturnsRedirectToActionResult()
        {
            var validPamatdati = new Pamatdati();

            var result = _controller.Create(validPamatdati);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult), "Create method does not return a RedirectToActionResult");
        }

        [TestMethod]
        public void Create_InvalidObject_ReturnsAViewResult_WithPamatdati()
        {

            var invalidPamatdati = new Pamatdati();
            _controller.ModelState.AddModelError("Error", "Model is invalid"); 

            var result = _controller.Create(invalidPamatdati);

            Assert.IsInstanceOfType(result, typeof(ViewResult), "Create method does not return a ViewResult");
            var viewResult = result as ViewResult;
            Assert.AreEqual(invalidPamatdati, viewResult.Model, "Model does not match the invalid Pamatdati");
        }

        [TestMethod]
        public void Edit_ValidObject_ReturnsRedirectToActionResult()
        {
            var validPamatdati = new Pamatdati { ID = 1 };

            var result = _controller.Edit(1, validPamatdati);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult), "Edit method does not return a RedirectToActionResult");
        }

        [TestMethod]
        public void Edit_InvalidObject_ReturnsAViewResult_WithPamatdati()
        {
            var invalidPamatdati = new Pamatdati { ID = 1 };
            _controller.ModelState.AddModelError("Error", "Model is invalid");

            var result = _controller.Edit(1, invalidPamatdati);

            Assert.IsInstanceOfType(result, typeof(ViewResult), "Edit method does not return a ViewResult");
            var viewResult = result as ViewResult;
            Assert.AreEqual(invalidPamatdati, viewResult.Model, "Model does not match the invalid Pamatdati");
        }

        [TestMethod]
        public void Edit_IDMismatch_ReturnsNotFound()
        {
            var pamatdati = new Pamatdati { ID = 2 };

            var result = _controller.Edit(1, pamatdati);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult), "Edit method with ID mismatch should return NotFound");
        }

        [TestMethod]
        public void DeleteConfirmed_InvalidId_ReturnsNotFound()
        {
            int id = 1;
            _serviceMock.Setup(service => service.GetById(id)).Returns((Pamatdati)null);

            var result = _controller.DeleteConfirmed(id);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult), "DeleteConfirmed method with an invalid id should return NotFound");
        }

        [TestMethod]
        public void Delete_ValidId_ReturnsAViewResult_WithPamatdati()
        {
            int id = 1;
            var expectedPamatdati = new Pamatdati();
            _serviceMock.Setup(service => service.GetById(id)).Returns(expectedPamatdati);

            var result = _controller.Delete(id);

            Assert.IsInstanceOfType(result, typeof(ViewResult), "Delete method does not return a ViewResult");
            var viewResult = result as ViewResult;
            Assert.AreEqual(expectedPamatdati, viewResult.Model, "Model does not match the expected Pamatdati");
        }

        [TestMethod]
        public void Delete_InvalidId_ReturnsNotFoundResult()
        {
            int? id = null;

            var result = _controller.Delete(id);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult), "Delete method with invalid id does not return NotFoundResult");
        }

        [TestMethod]
        public void DeleteConfirmed_ValidId_ReturnsRedirectToActionResult()
        {
            int id = 1;
            _serviceMock.Setup(service => service.GetById(id)).Returns(new Pamatdati());
            _serviceMock.Setup(service => service.Delete(id));

            var result = _controller.DeleteConfirmed(id);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult), "DeleteConfirmed method does not return a RedirectToActionResult");
        }

        [TestMethod]
        public void DeleteConfirmed_ValidId_ReturnsNotFoundResult()
        {
            int id = 1;
            _serviceMock.Setup(service => service.Delete(id));

            var result = _controller.DeleteConfirmed(id);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult), "DeleteConfirmed method does not return a NotFoundResult");
        }
    }
}