using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using UBB_SE_2024_923_1.Data;
using UBB_SE_2024_923_1.Models;
using UBB_SE_2024_923_1.Repositories;
using Xunit;


namespace UBB_SE_2024_923_1_TEST.Repositories
{
    public class RepositoryTest
    {
        [Fact]
        public async Task Add_Entity_AddsToContext()
        {
            // Arrange
            var mockSet = new Mock<DbSet<TestEntityOneKey>>();
            var mockContext = new Mock<DataContext>(MockBehavior.Default, new DbContextOptions<DataContext>());
            mockContext.Setup(m => m.Set<TestEntityOneKey>()).Returns(mockSet.Object);
            var repository = new Repository<TestEntityOneKey>(mockContext.Object);
            var entity = new TestEntityOneKey();

            // Act
            await repository.Add(entity);

            // Assert
            mockSet.Verify(m => m.Add(entity), Times.Once);
            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }

    public class TestEntityOneKey
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = "DefaultName";
    }

    public class TestEntityTwoKeys
    {
        public int FirstId { get; set; } = 0;
        public int SecondId { get; set; } = 0;
        public string Name { get; set; } = "DefaultName";
    }

    public class TestEntityThreeKeys
    {
        public int FirstId { get; set; } = 0;
        public int SecondId { get; set; } = 0;
        public int ThirdId { get; set; } = 0;
        public string Name { get; set; } = "DefaultName";
    }

    public class TestEntityFourKeys
    {
        public int FirstId { get; set; } = 0;
        public int SecondId { get; set; } = 0;
        public int ThirdId { get; set; } = 0;
        public int FourthId { get; set; } = 0;
        public string Name { get; set; } = "DefaultName";
    }
}
