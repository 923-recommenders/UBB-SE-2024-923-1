using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBB_SE_2024_923_1.DTO;
using UBB_SE_2024_923_1.Models;
using UBB_SE_2024_923_1.Repositories;

namespace UBB_SE_2024_923_1_TEST.Services.Stubs
{
    internal class TestSongBasicDetailsRepository : ISongBasicDetailsRepository
    {
        public Task Add(SongDataBaseModel entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(SongDataBaseModel entity)
        {
            throw new NotImplementedException();
        }

        public Task<SongDataBaseModel> GetByFourIdentifiers(int id1, string id2, string id3, string id4)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SongDataBaseModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<SongDataBaseModel> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<SongDataBaseModel> GetByTwoIdentifiers(int id1, int id2)
        {
            throw new NotImplementedException();
        }

        public Task<SongDataBaseModel> GetByThreeIdentifiers(int id1, int id2, int id3)
        {
            throw new NotImplementedException();
        }

        public Task<SongDataBaseModel> GetByThreeIdentifiers(int id1, int id2, DateTime id3)
        {
            throw new NotImplementedException();
        }

        public async Task<Tuple<string, decimal>> GetMostPlayedArtistPercentile(int userId)
        {
            // Example implementation for testing purposes
            if (userId == 1)
            {
                return new Tuple<string, decimal>("Test", 10);
            }
            else
            {
                return null; // Adjust return type based on your actual implementation
            }
        }

        public async Task<Tuple<SongDataBaseModel, decimal>> GetMostPlayedSongPercentile(int userId)
        {
            // Example implementation for testing purposes
            if (userId == 1)
            {
                return new Tuple<SongDataBaseModel, decimal>(
                    new SongDataBaseModel() { Name = "Test" }, 10);
            }
            else
            {
                return null; // Adjust return type based on your actual implementation
            }
        }

        public Task<List<string>> GetTop5Genres(int userId)
        {
            // Example implementation for testing purposes
            return Task.FromResult(new List<string>()
        {
            "Test1",
            "Test2",
            "Test3",
            "Test4",
            "Test5"
        });
        }

        public Task<List<SongDataBaseModel?>> GetTop5MostListenedSongs(int userId)
        {
            // Example implementation for testing purposes
            return Task.FromResult(new List<SongDataBaseModel>()
        {
            new SongDataBaseModel(),
            new SongDataBaseModel(),
            new SongDataBaseModel(),
            new SongDataBaseModel(),
            new SongDataBaseModel()
        });
        }

        public async Task<List<string>> GetAllNewGenresDiscovered(int userId)
        {
            // Example implementation for testing purposes
            if (userId == 1)
            {
                return new List<string>()
            {
                "Test1",
                "Test2",
                "Test3",
                "Test4",
                "Test5"
            };
            }
            else
            {
                return new List<string>(); // Adjust return type based on your actual implementation
            }
        }

        public Task<SongDataBaseModel> GetSongBasicDetails(int songId)
        {
            throw new NotImplementedException();
        }

        public Task Update(SongDataBaseModel entity)
        {
            throw new NotImplementedException();
        }

        public Task<SongBasicInformation> TransformSongBasicDetailsToSongBasicInfo(SongDataBaseModel song)
        {
            // Example implementation for testing purposes
            return Task.FromResult(new SongBasicInformation() { Name = song.Name });
        }
    }

}
