using Moq;
using Pasts.Controllers;
using LatvijasPastsCV.Services;
using LatvijasPastsCV.Models;
using Microsoft.AspNetCore.Mvc;

namespace Pasts.Tests.Controllers
{
    [TestClass]
    public class PrasmesControllerTests
    {
        private Mock<IPrasmesService> _serviceMock;
        private PrasmesController _controller;

        [TestInitialize]
        public void Setup()
        {
            _serviceMock = new Mock<IPrasmesService>();
            _controller = new PrasmesController(_serviceMock.Object);
        }

        [TestMethod]
        public void Index_ReturnsViewResultWithListOfPrasmes()
        {
            _serviceMock.Setup(service => service.GetAll()).Returns(new List<Prasmes>());

            var result = _controller.Index();

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.IsInstanceOfType(viewResult.Model, typeof(List<Prasmes>));
        }

        [TestMethod]
        public void Edit_ValidIdAndPrasmes_ReturnsRedirectToIndex()
        {
            int validId = 1;
            var validPrasmes = new Prasmes() { ID = validId };

            var result = _controller.Edit(validId, validPrasmes);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            var redirectResult = result as RedirectToActionResult;
            Assert.AreEqual("Index", redirectResult.ActionName);
        }

        [TestMethod]
        public void Edit_ValidIdButPrasmesNotFound_ReturnsNotFound()
        {
            int validId = 1;
            _serviceMock.Setup(service => service.GetById(validId)).Returns((Prasmes)null);

            var result = _controller.Edit(validId, new Prasmes());

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }


        [TestMethod]
        public void Edit_InvalidPrasmes_ReturnsViewWithPrasmes()
        {
            int validId = 1;
            var invalidPrasmes = new Prasmes() { ID = validId };
            _controller.ModelState.AddModelError("Error", "Some error");

            var result = _controller.Edit(validId, invalidPrasmes);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual(invalidPrasmes, viewResult.Model);
        }


        [TestMethod]
        public void Edit_InvalidId_ReturnsNotFound()
        {
            int invalidId = 1;
            var validPrasmes = new Prasmes() { ID = 2 };

            var result = _controller.Edit(invalidId, validPrasmes);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void Details_InvalidId_ReturnsNotFound()
        {
            int? invalidId = null;

            var result = _controller.Details(invalidId);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void Details_ValidIdButPrasmesNotFound_ReturnsNotFound()
        {
            int validId = 1;
            _serviceMock.Setup(service => service.GetById(validId)).Returns((Prasmes)null);

            var result = _controller.Details(validId);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void Details_ValidIdAndPrasmesFound_ReturnsViewResultWithPrasmes()
        {
            int validId = 1;
            var prasmes = new Prasmes();
            _serviceMock.Setup(service => service.GetById(validId)).Returns(prasmes);

            var result = _controller.Details(validId);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual(prasmes, viewResult.Model);
        }

        [TestMethod]
        public void Create_ValidPrasmes_ReturnsRedirectToIndex()
        {
            var validPrasmes = new Prasmes();

            var result = _controller.Create(validPrasmes);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            var redirectResult = result as RedirectToActionResult;
            Assert.AreEqual("Index", redirectResult.ActionName);
        }

        [TestMethod]
        public void Create_InvalidPrasmes_ReturnsViewWithPrasmes()
        {
            var invalidPrasmes = new Prasmes();
            _controller.ModelState.AddModelError("Error", "Some error");

            var result = _controller.Create(invalidPrasmes);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual(invalidPrasmes, viewResult.Model);
        }

        [TestMethod]
        public void DeleteConfirmed_PrasmesExists_ReturnsRedirectToIndex()
        {
            int validId = 1;
            var prasmes = new Prasmes();
            _serviceMock.Setup(service => service.Prasmes.Find(validId)).Returns(prasmes);

            var result = _controller.DeleteConfirmed(validId);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            var redirectResult = result as RedirectToActionResult;
            Assert.AreEqual("Index", redirectResult.ActionName);
        }

        [TestMethod]
        public void DeleteConfirmed_PrasmesNotExists_ReturnsRedirectToIndex()
        {
            int invalidId = 1;
            _serviceMock.Setup(service => service.Prasmes.Find(invalidId)).Returns((Prasmes)null);

            var result = _controller.DeleteConfirmed(invalidId);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            var redirectResult = result as RedirectToActionResult;
            Assert.AreEqual("Index", redirectResult.ActionName);
        }
    }
}