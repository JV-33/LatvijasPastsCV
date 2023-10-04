using Microsoft.EntityFrameworkCore;
using Moq;
using LatvijasPastsCV.Models;
using LatvijasPastsCV.Services;

namespace Pasts.Tests.Controllers
{
    [TestClass]
    public class CVDataServiceTests
    {
        private Mock<ICVDbContext> _mockContext;
        private CVDataService _service;

        [TestInitialize]
        public void SetUp()
        {
            _mockContext = new Mock<ICVDbContext>();

            var mockPamatdatiSet = new Mock<DbSet<Pamatdati>>();
            var mockAdreseSet = new Mock<DbSet<Adrese>>();

            _mockContext.Setup(m => m.Pamatdati).Returns(mockPamatdatiSet.Object);
            _mockContext.Setup(m => m.Adreses).Returns(mockAdreseSet.Object);

            _service = new CVDataService(_mockContext.Object);
        }



        [TestMethod]
        public void GetCVById_ShouldReturnCV()
        {
            var cvId = 1;
            var data = new List<Pamatdati>
        {
            new Pamatdati { ID = cvId },
            new Pamatdati { ID = 2 },
      
        }.AsQueryable();

            var mockSet = new Mock<DbSet<Pamatdati>>();
            mockSet.As<IQueryable<Pamatdati>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Pamatdati>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Pamatdati>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Pamatdati>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            _mockContext.Setup(c => c.Pamatdati).Returns(mockSet.Object);

            var result = _service.GetCVById(cvId);

            Assert.IsNotNull(result);
            Assert.AreEqual(cvId, result.Pamatdati.ID);
        }

        [TestMethod]
        public void AddCV_WithEmptyCV_ShouldNotThrowException()
        {
            var cv = new CV();
            Exception ex = null;
            try
            {
                _service.AddCV(cv);
            }
            catch (Exception e)
            {
                ex = e;
            }
            Assert.IsNull(ex);
        }

        [TestMethod]
        public void AddCV_WithOnlyPamatdati_ShouldNotAccessOtherFields()
        {
            var cv = new CV
            {
                Pamatdati = new Pamatdati()
            };

            _service.AddCV(cv);

            _mockContext.Verify(m => m.Adreses, Times.Never);
            _mockContext.Verify(m => m.Izglitiba, Times.Never);
            _mockContext.Verify(m => m.DarbaPieredze, Times.Never);
            _mockContext.Verify(m => m.Prasmes, Times.Never);
        }

        [TestMethod]
        public void AddCV_ShouldAddCV()
        {
            var cv = new CV
            {
                Pamatdati = new Pamatdati(),
                Adrese = new Adrese(),
                Izglitiba = new List<Izglitiba>(),
                DarbaPieredzes = new List<DarbaPieredze>(),
                Prasmes = new List<Prasmes>()
            };

            _service.AddCV(cv);

            _mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void AddCV_WithoutAdrese_ShouldNotAccessAdrese()
        {
            var cv = new CV
            {
                Pamatdati = new Pamatdati(),
                Izglitiba = new List<Izglitiba>(),
                DarbaPieredzes = new List<DarbaPieredze>(),
                Prasmes = new List<Prasmes>()
            };

            _service.AddCV(cv);

            _mockContext.Verify(m => m.Adreses, Times.Never);
        }
    }
}