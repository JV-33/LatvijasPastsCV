using Moq;
using LatvijasPastsCV.Models;
using LatvijasPastsCV.Services;
using Microsoft.EntityFrameworkCore;

namespace Pasts.Tests.Controllers
{
    [TestClass]
    public class DarbaPieredzeServiceTests
    {
        private Mock<ICVDbContext> _mockContext;
        private IDarbaPieredzeService _service;
        private Mock<DbSet<DarbaPieredze>> _mockSet;
        private List<DarbaPieredze> _data;

        [TestInitialize]
        public void Setup()
        {
            _mockContext = new Mock<ICVDbContext>();

            _data = new List<DarbaPieredze>
    {
        new DarbaPieredze { ID = 1 },
        new DarbaPieredze { ID = 2 }
    };

            _mockSet = new Mock<DbSet<DarbaPieredze>>();
            _mockSet.As<IQueryable<DarbaPieredze>>().Setup(m => m.Provider).Returns(_data.AsQueryable().Provider);
            _mockSet.As<IQueryable<DarbaPieredze>>().Setup(m => m.Expression).Returns(_data.AsQueryable().Expression);
            _mockSet.As<IQueryable<DarbaPieredze>>().Setup(m => m.ElementType).Returns(_data.AsQueryable().ElementType);
            _mockSet.As<IQueryable<DarbaPieredze>>().Setup(m => m.GetEnumerator()).Returns(_data.AsQueryable().GetEnumerator());

            _mockContext.Setup(c => c.DarbaPieredze).Returns(_mockSet.Object); 
            _mockSet.Setup(m => m.Find(1)).Returns(new DarbaPieredze { ID = 1 });
            _mockSet.Setup(m => m.Remove(It.IsAny<DarbaPieredze>())).Verifiable();

            _service = new DarbaPieredzeService(_mockContext.Object);
        }

        [TestMethod]
        public void GetAll_ShouldReturnAllItems()
        {
            var results = _service.GetAll();

            Assert.AreEqual(2, results.Count());
        }

        [TestMethod]
        public void GetById_ShouldReturnCorrectItem()
        {
            var result = _service.GetById(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.ID);
        }

        [TestMethod]
        public void GetById_InvalidId_ShouldReturnNull()
        {
            var result = _service.GetById(999);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void Add_ShouldAddNewItem()
        {
            var newItem = new DarbaPieredze { ID = 3 };

            _service.Add(newItem);

            _mockSet.Verify(m => m.Add(It.IsAny<DarbaPieredze>()), Times.Once());
            _mockContext.Verify(c => c.SaveChanges(), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Add_NullItem_ShouldThrowException()
        {
            _service.Add(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Update_NullItem_ShouldThrowException()
        {
            _service.Update(null);
        }


        [TestMethod]
        public void Update_ShouldUpdateExistingItem()
        {
            var updatedItem = new DarbaPieredze { ID = 1 };

            _service.Update(updatedItem);

            _mockContext.Verify(c => c.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void Delete_ShouldCallRemoveAndSaveChanges()
        {
            _service.Delete(1);

            _mockSet.Verify(m => m.Remove(It.IsAny<DarbaPieredze>()), Times.Once());

            _mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void Delete_NonExistentItem_ShouldNotThrow()
        {
            _service.Delete(999); 

            _mockSet.Verify(m => m.Remove(It.IsAny<DarbaPieredze>()), Times.Never()); 
            _mockContext.Verify(m => m.SaveChanges(), Times.Never()); 
        }
    }
}