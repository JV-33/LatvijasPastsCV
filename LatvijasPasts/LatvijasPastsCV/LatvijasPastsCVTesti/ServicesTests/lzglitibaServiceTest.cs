using Moq;
using LatvijasPastsCV.Models;
using LatvijasPastsCV.Services;

namespace Pasts.Tests.Controllers
{
    [TestClass]
    public class IzglitibaServiceTests
    {
        private Mock<IIzglitibaService> _serviceMock;
        private IIzglitibaService _service;

        [TestInitialize]
        public void Setup()
        {
            _serviceMock = new Mock<IIzglitibaService>();
            _service = _serviceMock.Object;
        }

        [TestMethod]
        public void GetAll_ReturnsIEnumerableIzglitiba()
        {
            var expectedIzglitiba = new List<Izglitiba> { new Izglitiba() };
            _serviceMock.Setup(service => service.GetAll()).Returns(expectedIzglitiba);

            var result = _service.GetAll();

            Assert.AreEqual(expectedIzglitiba, result);
        }

        [TestMethod]
        public void GetById_ValidId_ReturnsIzglitiba()
        {
            int id = 1;
            var expectedIzglitiba = new Izglitiba();
            _serviceMock.Setup(service => service.GetById(id)).Returns(expectedIzglitiba);

            var result = _service.GetById(id);

            Assert.AreEqual(expectedIzglitiba, result);
        }

        [TestMethod]
        public void Add_ValidIzglitiba_AddsIzglitiba()
        {
            var izglitiba = new Izglitiba();

            _service.Add(izglitiba);

            _serviceMock.Verify(service => service.Add(izglitiba), Times.Once);
        }

        [TestMethod]
        public void Update_ValidIzglitiba_UpdatesIzglitiba()
        {
            var izglitiba = new Izglitiba();

            _service.Update(izglitiba);

            _serviceMock.Verify(service => service.Update(izglitiba), Times.Once);
        }

        [TestMethod]
        public void Delete_ValidId_DeletesIzglitiba()
        {
            int id = 1;

            _service.Delete(id);

            _serviceMock.Verify(service => service.Delete(id), Times.Once);
        }
    }
}