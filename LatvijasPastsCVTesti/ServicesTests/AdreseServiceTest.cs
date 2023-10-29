using Moq;
using LatvijasPastsCV.Services;
using LatvijasPastsCV.Models;
using Microsoft.EntityFrameworkCore;

namespace Pasts.Tests.Controllers
{
    [TestClass]
    public class AdreseServiceTests
    {
        private Mock<ICVDbContext> _contextMock;
        private AdreseService _service;

        [TestInitialize]
        public void Setup()
        {
            _contextMock = new Mock<ICVDbContext>();
            _service = new AdreseService(_contextMock.Object);
        }

        [TestMethod]
        public void GetAll_ShouldReturnAllAdreses()
        {
            var adreses = new List<Adrese>
        {
        new Adrese(),
        new Adrese(),
        };

            var queryableList = adreses.AsQueryable();

            var adresesDbSetMock = new Mock<DbSet<Adrese>>();
            adresesDbSetMock.As<IQueryable<Adrese>>().Setup(m => m.Provider).Returns(queryableList.Provider);
            adresesDbSetMock.As<IQueryable<Adrese>>().Setup(m => m.Expression).Returns(queryableList.Expression);
            adresesDbSetMock.As<IQueryable<Adrese>>().Setup(m => m.ElementType).Returns(queryableList.ElementType);
            adresesDbSetMock.As<IQueryable<Adrese>>().Setup(m => m.GetEnumerator()).Returns(queryableList.GetEnumerator());

            _contextMock.Setup(c => c.Adreses).Returns(adresesDbSetMock.Object);

            var result = _service.GetAll();

            Assert.AreEqual(adreses.Count, result.Count());
        }

        [TestMethod]
        public void GetById_ValidId_ReturnsAdrese()
        {
            var adrese = new Adrese { ID = 1 };

            var adresesDbSetMock = new Mock<DbSet<Adrese>>();
            adresesDbSetMock.Setup(m => m.Find(It.IsAny<int>())).Returns(adrese);

            _contextMock.Setup(c => c.Adreses).Returns(adresesDbSetMock.Object);

            var result = _service.GetById(1);

            Assert.AreEqual(adrese, result);
        }

        [TestMethod]
        public void GetById_InvalidId_ReturnsNull()
        {
            int invalidId = 999;

            var adresesDbSetMock = new Mock<DbSet<Adrese>>();
            adresesDbSetMock.Setup(m => m.Find(invalidId)).Returns((Adrese)null);

            _contextMock.Setup(c => c.Adreses).Returns(adresesDbSetMock.Object);

            var result = _service.GetById(invalidId);

            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Add_NullAdrese_ThrowsArgumentNullException()
        {
            Adrese adrese = null;

            _service.Add(adrese);
        }


        [TestMethod]
        public void Add_ValidAdrese_AddsAdreseAndSavesChanges()
        {
            var adrese = new Adrese();

            var adresesDbSetMock = new Mock<DbSet<Adrese>>();
            adresesDbSetMock.Setup(m => m.Add(It.IsAny<Adrese>())).Verifiable();

            _contextMock.Setup(c => c.Adreses).Returns(adresesDbSetMock.Object);
            _contextMock.Setup(c => c.SaveChanges()).Verifiable();

            _service.Add(adrese);

            adresesDbSetMock.Verify(m => m.Add(It.IsAny<Adrese>()), Times.Once);
            _contextMock.Verify(m => m.SaveChanges(), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Update_NullAdrese_ThrowsArgumentNullException()
        {
            Adrese adrese = null;

            _service.Update(adrese);
        }


        [TestMethod]
        public void Update_ValidAdrese_UpdatesAdreseAndSavesChanges()
        {
            var adrese = new Adrese();

            var adresesDbSetMock = new Mock<DbSet<Adrese>>();
            adresesDbSetMock.Setup(m => m.Update(It.IsAny<Adrese>())).Verifiable();

            _contextMock.Setup(c => c.Adreses).Returns(adresesDbSetMock.Object);
            _contextMock.Setup(c => c.SaveChanges()).Verifiable();

            _service.Update(adrese);

            adresesDbSetMock.Verify(m => m.Update(It.IsAny<Adrese>()), Times.Once);
            _contextMock.Verify(m => m.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void Delete_ValidId_DeletesAdreseAndSavesChanges()
        {
            var adrese = new Adrese { ID = 1 };

            var adresesDbSetMock = new Mock<DbSet<Adrese>>();
            adresesDbSetMock.Setup(m => m.Find(It.IsAny<int>())).Returns(adrese);
            adresesDbSetMock.Setup(m => m.Remove(It.IsAny<Adrese>())).Verifiable();

            _contextMock.Setup(c => c.Adreses).Returns(adresesDbSetMock.Object);
            _contextMock.Setup(c => c.SaveChanges()).Verifiable();

            _service.Delete(1);

            adresesDbSetMock.Verify(m => m.Remove(It.IsAny<Adrese>()), Times.Once);
            _contextMock.Verify(m => m.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void Delete_InvalidId_NoChangesInDb()
        {
            int invalidId = 999;

            var adresesDbSetMock = new Mock<DbSet<Adrese>>();
            adresesDbSetMock.Setup(m => m.Find(invalidId)).Returns((Adrese)null);

            _contextMock.Setup(c => c.Adreses).Returns(adresesDbSetMock.Object);
            _contextMock.Setup(c => c.SaveChanges()).Verifiable();

            _service.Delete(invalidId);

            _contextMock.Verify(m => m.SaveChanges(), Times.Never);
        }
    }
}