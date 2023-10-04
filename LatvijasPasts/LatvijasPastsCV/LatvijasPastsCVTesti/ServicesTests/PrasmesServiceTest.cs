using Moq;
using LatvijasPastsCV.DBData;
using LatvijasPastsCV.Models;
using LatvijasPastsCV.Services;
using Microsoft.EntityFrameworkCore;

namespace Pasts.Tests.Controllers
{
    [TestClass]
    public class PrasmesServiceTests
    {
        private Mock<ICVDbContext> _contextMock;
        private IPrasmesService _service;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<CVDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
                .Options;
     
            ICVDbContext context = new CVDbContext(options); 
            _contextMock = new Mock<ICVDbContext>();
            _service = new PrasmesService(_contextMock.Object);

            _contextMock.Setup(c => c.Prasmes.Find(It.IsAny<int>())).Returns(new Prasmes { ID = 1 });
        }

        [TestMethod]
        public void GetAll_ReturnsIEnumerablePrasmes()
        {
            var expectedPrasmes = new List<Prasmes> { new Prasmes() };

            var options = new DbContextOptionsBuilder<CVDbContext>()
                .UseInMemoryDatabase(databaseName: "GetAll_ReturnsIEnumerablePrasmes")
                .Options;

            using (var context = new CVDbContext(options))
            {
                context.Prasmes.AddRange(expectedPrasmes);
                context.SaveChanges();

                var service = new PrasmesService(context);

                var result = service.GetAll().ToList();

                Assert.AreEqual(expectedPrasmes.Count, result.Count);
            }
        }

        [TestMethod]
        public void GetById_ExistingId_ReturnsPrasmes()
        {
            int id = 1;
            var expectedPrasme = new Prasmes { ID = 1 };
            _contextMock.Setup(c => c.Prasmes.Find(id)).Returns(expectedPrasme);

            var result = _service.GetById(id);

            Assert.AreEqual(expectedPrasme, result);
        }


        [TestMethod]
        public void Add_ValidPrasme_AddsPrasme()
        {
            var prasme = new Prasmes();
            var dbSetMock = new Mock<DbSet<Prasmes>>();
            _contextMock.Setup(c => c.Prasmes).Returns(dbSetMock.Object);

            _service.Add(prasme);

            dbSetMock.Verify(db => db.Add(prasme), Times.Once);
            _contextMock.Verify(c => c.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void Update_ValidPrasme_UpdatesPrasme()
        {
            var prasme = new Prasmes();
            _contextMock.Setup(c => c.Prasmes).Returns(new Mock<DbSet<Prasmes>>().Object);

            _service.Update(prasme);

            _contextMock.Verify(c => c.SaveChanges(), Times.Once);
        }

        private Mock<DbSet<Prasmes>> SetupDbSetMock(IList<Prasmes> data)
        {
            var queryable = data.AsQueryable();
            var dbSetMock = new Mock<DbSet<Prasmes>>();
            dbSetMock.As<IQueryable<Prasmes>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSetMock.As<IQueryable<Prasmes>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSetMock.As<IQueryable<Prasmes>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSetMock.As<IQueryable<Prasmes>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());
            return dbSetMock;
        }

        [TestMethod]
        public void Remove_ValidPrasme_RemovesPrasme()
        {
            var prasme = new Prasmes();
            var dbSetMock = new Mock<DbSet<Prasmes>>();
            _contextMock.Setup(c => c.Prasmes).Returns(dbSetMock.Object);

            _service.Remove(prasme);

            dbSetMock.Verify(db => db.Remove(prasme), Times.Once);
            _contextMock.Verify(c => c.SaveChanges(), Times.Never); 
        }

        [TestMethod]
        public void SaveChanges_CallsSaveChangesOnContext()
        {
            _service.SaveChanges();

            _contextMock.Verify(c => c.SaveChanges(), Times.Once);
        }
    }
}