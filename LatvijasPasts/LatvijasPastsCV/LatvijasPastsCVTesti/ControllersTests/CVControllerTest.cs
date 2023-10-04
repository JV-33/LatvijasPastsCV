using Microsoft.EntityFrameworkCore;
using Moq;
using Pasts.Controllers;
using LatvijasPastsCV.DBData;
using LatvijasPastsCV.Models;
using Microsoft.AspNetCore.Mvc;

namespace Pasts.Tests.Controllers
{
    [TestClass]
    public class CVControllerTests
    {
        private Mock<CVDbContext> _mockContext;
        private CVController _controller;
        private CVDbContext _context;

        [TestInitialize]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<CVDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new CVDbContext(options);
            _controller = new CVController(_context);
        }

        [TestMethod]
        public void Add_Item_ShouldIncreaseCountByOne()
        {
            var options = new DbContextOptionsBuilder<CVDbContext>()
                .UseInMemoryDatabase(databaseName: "Add_Item_ShouldIncreaseCountByOne")
                .Options;

            using (var context = new CVDbContext(options))
            {
                context.CV.Add(new CV());
                context.SaveChanges();
            }

            using (var context = new CVDbContext(options))
            {
                Assert.AreEqual(1, context.CV.Count());
            }
        }

        [TestMethod]
        public void CVDetails_WithInvalidId_ReturnsNotFound()
        {
            int invalidId = 999;

            var result = _controller.CVDetails(invalidId);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}