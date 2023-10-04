using Moq;
using LatvijasPastsCV.Models;
using LatvijasPastsCV.Services;

namespace Pasts.Tests.Controllers
{
    [TestClass]
    public class PamatdatiServiceTests
    {
        private Mock<IPamatdatiService> _serviceMock;
        private IPamatdatiService _service;

        [TestInitialize]
        public void Setup()
        {
            _serviceMock = new Mock<IPamatdatiService>();
            _service = _serviceMock.Object;
        }

        [TestMethod]
        public void GetAll_ReturnsIEnumerablePamatdati()
        {

            var expectedPamatdati = new List<Pamatdati> { new Pamatdati() };
            _serviceMock.Setup(service => service.GetAll()).Returns(expectedPamatdati);

            var result = _service.GetAll();

            Assert.AreEqual(expectedPamatdati, result);
        }

        [TestMethod]
        public void GetById_ValidId_ReturnsPamatdati()
        {
            int id = 1;
            var expectedPamatdati = new Pamatdati();
            _serviceMock.Setup(service => service.GetById(id)).Returns(expectedPamatdati);

            var result = _service.GetById(id);

            Assert.AreEqual(expectedPamatdati, result);
        }

        [TestMethod]
        public void Add_ValidPamatdati_AddsPamatdati()
        {
            var pamatdati = new Pamatdati();

            _service.Add(pamatdati);

            _serviceMock.Verify(service => service.Add(pamatdati), Times.Once);
        }

        [TestMethod]
        public void Update_ValidPamatdati_UpdatesPamatdati()
        {
            var pamatdati = new Pamatdati();

            _service.Update(pamatdati);

            _serviceMock.Verify(service => service.Update(pamatdati), Times.Once);
        }

        [TestMethod]
        public void Delete_ValidId_DeletesPamatdati()
        {
            int id = 1;

            _service.Delete(id);

            _serviceMock.Verify(service => service.Delete(id), Times.Once);
        }

        [TestMethod]
        public void Delete_NonExistentId_NoExceptionThrown()
        {
            int nonExistentId = 999;

            _service.Delete(nonExistentId);

            _serviceMock.Verify(service => service.Delete(nonExistentId), Times.Once);
        }

    }
}