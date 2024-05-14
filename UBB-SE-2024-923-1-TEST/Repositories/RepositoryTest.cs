using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Mono.TextTemplating;
using Moq;
using UBB_SE_2024_923_1.Data;
using UBB_SE_2024_923_1.Models;
using UBB_SE_2024_923_1.Repositories;
using Xunit;
using System.IO;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBB_SE_2024_923_1.Data;
using UBB_SE_2024_923_1.Enums;
using UBB_SE_2024_923_1.Models;
using UBB_SE_2024_923_1.Repositories;
using Xunit;
using NuGet.Packaging.Signing;


namespace UBB_SE_2024_923_1_TEST.Repositories
{

    public class RepositoryTest
    {
        [Fact]
        public async Task GetAll_GetsAllExistingEntityData_ReturnsAllEntities()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var seededData = new List<ArtistDetails>
            {
                new ArtistDetails { ArtistId = 1, Name = "Test Artist1" },
                new ArtistDetails { ArtistId = 2, Name = "Test Artist2" },
                new ArtistDetails { ArtistId = 3, Name = "Test Artist3" }
            };

            using (var context = new DataContext(options))
            {
                context.Database.EnsureCreated();
                context.ArtistDetails.AddRangeAsync(seededData);
                await context.SaveChangesAsync();
            }

            using (var context = new DataContext(options))
            {
                var repository = new Repository<ArtistDetails>(context);
                var result = await repository.GetAll();

                Assert.NotNull(result);
                Assert.Equal(seededData.Count, result.Count());
                foreach (var expectedEntity in seededData)
                {
                    var actualEntity = result.FirstOrDefault(e => e.ArtistId == expectedEntity.ArtistId);
                    Assert.NotNull(actualEntity);
                    Assert.Equal(expectedEntity.ArtistId, actualEntity.ArtistId);
                    Assert.Equal(expectedEntity.Name, actualEntity.Name);
                }
                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task GetAll_GetNonExistingEntityData_ReturnsNull()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new DataContext(options))
            {
                context.Database.EnsureCreated();
                await context.SaveChangesAsync();
            }

            using (var context = new DataContext(options))
            {
                var repository = new Repository<ArtistDetails>(context);
                var result = await repository.GetAll();

                Assert.NotNull(result);
                Assert.Empty(result);
                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task GetEntityDetails_FindsEntityWithOneIdentifier_ReturnsCorrectEntityData()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
               .UseInMemoryDatabase(databaseName: "TestDatabase")
               .Options;

            var seededData = new ArtistDetails() { ArtistId = 1, Name = "Test User" };

            using (var context = new DataContext(options))
            {
                context.Database.EnsureCreated();
                context.ArtistDetails.Add(seededData);
                await context.SaveChangesAsync();
            }

            using (var context = new DataContext(options))
            {
                var repository = new Repository<ArtistDetails>(context);
                var result = await repository.GetById(seededData.ArtistId);
                Assert.NotNull(result);
                Assert.Equal(seededData.ArtistId, result.ArtistId);
                Assert.Equal(seededData.Name, result.Name);
                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task GetEntityDetails_LooksUpNonExistentEntityWithOneIdentifier_Returns()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var seededData = new ArtistDetails() { ArtistId = 1, Name = "Test User" };

            using (var context = new DataContext(options))
            {
                context.Database.EnsureCreated();
                context.ArtistDetails.Add(seededData);
                await context.SaveChangesAsync();
            }

            using (var context = new DataContext(options))
            {
                var repository = new Repository<ArtistDetails>(context);
                var result = await repository.GetById(100);
                Assert.Null(result);
                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task GetEntityDetails_FindsEntityWithTwoIdentifier_ReturnsCorrectEntityData()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var seededData = new SongFeatures() { ArtistId = 1, SongId = 1 };

            using (var context = new DataContext(options))
            {
                context.Database.EnsureCreated();
                context.SongFeatures.Add(seededData);
                await context.SaveChangesAsync();
            }

            using (var context = new DataContext(options))
            {
                var repository = new Repository<SongFeatures>(context);
                var result = await repository.GetByTwoIdentifiers(seededData.ArtistId, seededData.SongId);
                Assert.NotNull(result);
                Assert.Equal(seededData.ArtistId, result.ArtistId);
                Assert.Equal(seededData.SongId, result.SongId);
                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task GetEntityDetails_LooksUpNonExistentEntityWithTwoIdentifier_Returns()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var seededData = new SongFeatures() { ArtistId = 1, SongId = 1};

            using (var context = new DataContext(options))
            {
                context.Database.EnsureCreated();
                context.SongFeatures.Add(seededData);
                await context.SaveChangesAsync();
            }

            using (var context = new DataContext(options))
            {
                var repository = new Repository<SongFeatures>(context);
                var result = await repository.GetByTwoIdentifiers(100,100);
                Assert.Null(result);
                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task GetEntityDetails_FindsEntityWithThreeIdentifiers_ReturnsCorrectEntityData()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var seededData = new SongRecommendationDetails
            {
                SongId = 1,
                Month = 5,
                Year = 2023,
                Likes = 100,
                Dislikes = 50,
                MinutesListened = 120,
                NumberOfPlays = 10
            };

            using (var context = new DataContext(options))
            {
                context.Database.EnsureCreated();
                context.SongRecommendationDetails.Add(seededData);
                await context.SaveChangesAsync();
            }

            using (var context = new DataContext(options))
            {
                var repository = new Repository<SongRecommendationDetails>(context);
                var result = await repository.GetByThreeIdentifiers(seededData.SongId, seededData.Month, seededData.Year);
                Assert.NotNull(result);
                Assert.Equal(seededData.SongId, result.SongId);
                Assert.Equal(seededData.Month, result.Month);
                Assert.Equal(seededData.Year, result.Year);
                Assert.Equal(seededData.Likes, result.Likes);
                Assert.Equal(seededData.Dislikes, result.Dislikes);
                Assert.Equal(seededData.MinutesListened, result.MinutesListened);
                Assert.Equal(seededData.NumberOfPlays, result.NumberOfPlays);
                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task GetEntityDetails_LooksUpNonExistentEntityWithThreeIdentifiers_ReturnsNull()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new DataContext(options))
            {
                context.Database.EnsureCreated();
                await context.SaveChangesAsync();
            }

            using (var context = new DataContext(options))
            {
                var repository = new Repository<SongRecommendationDetails>(context);
                var result = await repository.GetByThreeIdentifiers(100, 100, 2023);
                Assert.Null(result);
                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task GetEntityDetails_FindsEntityWithThreeIdentifiersOneOfWhichDateTime_ReturnsCorrectEntityData()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var seededData = new UserPlaybackBehaviour
            {
                UserId = 1,
                SongId = 100,
                Timestamp = DateTime.UtcNow,
                EventType = PlaybackEventType.Like
            };

            using (var context = new DataContext(options))
            {
                context.Database.EnsureCreated();
                context.UserPlaybackBehaviour.Add(seededData);
                await context.SaveChangesAsync();
            }

            using (var context = new DataContext(options))
            {
                var repository = new Repository<UserPlaybackBehaviour>(context);
                var result = await repository.GetByThreeIdentifiers(seededData.UserId, seededData.SongId, seededData.Timestamp);
                Assert.NotNull(result);
                Assert.Equal(seededData.UserId, result.UserId);
                Assert.Equal(seededData.SongId, result.SongId);
                Assert.Equal(seededData.Timestamp, result.Timestamp);
                Assert.Equal(seededData.EventType, result.EventType);
                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task GetEntityDetails_LooksUpNonExistentEntityWithThreeIdentifiersOneOfWhichDateTime_ReturnsNull()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new DataContext(options))
            {
                context.Database.EnsureCreated();
                await context.SaveChangesAsync();
            }

            using (var context = new DataContext(options))
            {
                var repository = new Repository<UserPlaybackBehaviour>(context);
                var result = await repository.GetByThreeIdentifiers(100, 100, DateTime.UtcNow);
                Assert.Null(result);
                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task GetEntityDetails_FindsEntityWithFourIdentifiers_ReturnsCorrectEntityData()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var seededData = new Trends
            {
                SongId = 1,
                Genre = "Pop",
                Language = "English",
                Country = "USA"
            };

            using (var context = new DataContext(options))
            {
                context.Database.EnsureCreated();
                context.Trends.Add(seededData);
                await context.SaveChangesAsync();
            }

            using (var context = new DataContext(options))
            {
                var repository = new Repository<Trends>(context);
                var result = await repository.GetByFourIdentifiers(seededData.SongId, seededData.Genre, seededData.Language, seededData.Country);
                Assert.NotNull(result);
                Assert.Equal(seededData.SongId, result.SongId);
                Assert.Equal(seededData.Genre, result.Genre);
                Assert.Equal(seededData.Language, result.Language);
                Assert.Equal(seededData.Country, result.Country);
                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task GetEntityDetails_LooksUpNonExistentEntityWithFourIdentifiers_ReturnsNull()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new DataContext(options))
            {
                context.Database.EnsureCreated();
                await context.SaveChangesAsync();
            }

            using (var context = new DataContext(options))
            {
                var repository = new Repository<Trends>(context);
                var result = await repository.GetByFourIdentifiers(100, "Rock", "Spanish", "Mexico");
                Assert.Null(result);
                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task Add_AddNewEntity_SuccessfullyAddsNewEntity()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new DataContext(options))
            {
                context.Database.EnsureCreated();
                await context.SaveChangesAsync();
            }

            using (var context = new DataContext(options))
            {
                var repository = new Repository<ArtistDetails>(context);

                var newArtistDetails = new ArtistDetails()
                {
                    ArtistId = 1,
                    Name = "Test Artist1"
                };

                await repository.Add(newArtistDetails);

                var result = await repository.GetById(newArtistDetails.ArtistId);
                Assert.NotNull(result);
                Assert.Equal(newArtistDetails.ArtistId, result.ArtistId);
                Assert.Equal(newArtistDetails.Name, result.Name);
                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task Add_AddAlreadyExistingEntity_ThrowsException()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;


            var seededData = new List<ArtistDetails>
            {
                new ArtistDetails() { ArtistId = 1, Name = "Test Artist1" }
            };

            using (var context = new DataContext(options))
            {
                context.Database.EnsureCreated();
                context.ArtistDetails.AddRangeAsync(seededData);
                await context.SaveChangesAsync();
            }

            using (var context = new DataContext(options)){
                var repository = new Repository<ArtistDetails>(context);

                var newArtistDetails = new ArtistDetails()
                {
                    ArtistId = 1,
                    Name = "Test Artist1"
                };

                var exception = await Assert.ThrowsAsync<ArgumentException>(() => repository.Add(newArtistDetails));
                Assert.Contains("An item with the same key has already been added. Key: 1", exception.Message);
                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task Delete_DeleteExistingEntity_SuccessfullyDeletesEntity()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;


            var seededData = new List<ArtistDetails>
            {
                new ArtistDetails() { ArtistId = 1, Name = "Test Artist1" },
                new ArtistDetails() { ArtistId = 2, Name = "Test Artist2" }
            };

            using (var context = new DataContext(options))
            {
                context.Database.EnsureCreated();
                context.ArtistDetails.AddRangeAsync(seededData);
                await context.SaveChangesAsync();
            }

            using (var context = new DataContext(options)){
                var repository = new Repository<ArtistDetails>(context);

                var artistDetailsToDelete = new ArtistDetails()
                {
                    ArtistId = 1,
                    Name = "Test Artist1"
                };

                await repository.Delete(artistDetailsToDelete);

                var result = await repository.GetAll();
                Assert.NotNull(result);
                Assert.NotEqual(seededData.Count, result.Count());
                foreach (var expectedEntity in result)
                {
                    var actualEntity = new ArtistDetails {ArtistId = 2, Name = "Test Artist2"};
                    Assert.NotNull(actualEntity);
                    Assert.Equal(expectedEntity.ArtistId, actualEntity.ArtistId);
                    Assert.Equal(expectedEntity.Name, actualEntity.Name);
                }
                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task Delete_DeleteANonExistingEntity_ThrowsException()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;


            var seededData = new List<ArtistDetails>
            {
                new ArtistDetails() { ArtistId = 1, Name = "Test Artist1" }
            };

            using (var context = new DataContext(options))
            {
                context.Database.EnsureCreated();
                context.ArtistDetails.AddRangeAsync(seededData);
                await context.SaveChangesAsync();
            }

            using (var context = new DataContext(options))
            {
                var repository = new Repository<ArtistDetails>(context);

                var artistDetailsToDelete = new ArtistDetails()
                {
                    ArtistId = 2,
                    Name = "Test Artist2"
                };

                var exception = await Assert.ThrowsAsync<DbUpdateConcurrencyException> (() => repository.Delete(artistDetailsToDelete));
                Assert.Contains("Attempted to update or delete an entity that does not exist in the store.", exception.Message);
                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task Update_UpdateExistingEntity_SuccessfullyUpdatesExistingEntity()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var seededData = new ArtistDetails { ArtistId = 1, Name = "Test Artist1" };

            using (var context = new DataContext(options))
            {
                context.Database.EnsureCreated();
                context.ArtistDetails.Add(seededData);
                await context.SaveChangesAsync();
            }

            using (var context = new DataContext(options))
            {
                var repository = new Repository<ArtistDetails>(context);

                seededData.Name = "Test Artist2";
                await repository.Update(seededData);
                
                var result = await repository.GetById(seededData.ArtistId);
                Assert.NotNull(result);
                Assert.Equal(seededData.ArtistId, result.ArtistId);
                Assert.Equal("Test Artist2", result.Name);
                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task Update_UpdateNonExistingEntity_ThrowsException()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var seededData = new ArtistDetails { ArtistId = 1, Name = "Test Artist1" };

            using (var context = new DataContext(options))
            {
                context.Database.EnsureCreated();
                context.ArtistDetails.Add(seededData);
                await context.SaveChangesAsync();
            }

            using (var context = new DataContext(options))
            {
                var repository = new Repository<ArtistDetails>(context);

                var artistDetailsUpdated = new ArtistDetails { ArtistId = 2, Name = "Test Artist 2" };

                var exception = await Assert.ThrowsAsync<DbUpdateConcurrencyException>(() => repository.Update(artistDetailsUpdated));
                Assert.Contains("Attempted to update or delete an entity that does not exist in the store.", exception.Message);
                context.Database.EnsureDeleted();
            }
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
