using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using UBB_SE_2024_923_1.Data;
using UBB_SE_2024_923_1.DTO;
using UBB_SE_2024_923_1.Enums;
using UBB_SE_2024_923_1.Models;

namespace UBB_SE_2024_923_1.Repositories
{
    public class SongBasicDetailsRepository(DataContext context)
        : Repository<SongDataBaseModel>(context), ISongBasicDetailsRepository
    {
        public async Task<SongBasicInformation> TransformSongBasicDetailsToSongBasicInfo(SongDataBaseModel song)
        {
            int artistId = song.ArtistId;
            var artistName = await _context.ArtistDetails
                .Where(artist => artist.ArtistId == artistId)
                .Select(artist => artist.Name)
                .FirstOrDefaultAsync();
            return new SongBasicInformation
            {
                SongId = song.SongId,
                Name = song.Name,
                Genre = song.Genre,
                Subgenre = song.Subgenre,
                Artist = artistName,
                Language = song.Language,
                Country = song.Country,
                Album = song.Album,
                Image = song.Image
            };
        }

        public async Task<SongDataBaseModel> GetSongBasicDetails(int songId)
        {
            return await _context.SongDataBaseModel
                .Where(song => song.SongId == songId)
                .FirstOrDefaultAsync();
        }

        public async Task<List<SongDataBaseModel>> GetTop5MostListenedSongs(int userId)
        {
            var top5SongIds = await _context.UserPlaybackBehaviour
                .Where(ub => ub.UserId == userId && ub.EventType == PlaybackEventType.StartSongPlayback)
                .GroupBy(ub => ub.SongId)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .Take(5)
                .ToListAsync();

            return await _context.SongDataBaseModel
                .Where(song => top5SongIds.Contains(song.SongId))
                .ToListAsync();
        }

        public async Task<Tuple<SongDataBaseModel, decimal>> GetMostPlayedSongPercentile(int userId)
        {
            var mostPlayedSong = await GetMostPlayedSong(userId);
            var totalSongs = await GetTotalSongsPlayedByUser(userId);
            var mostListenedSongCount = await GetMostListenedSongCount(userId);
            return new Tuple<SongDataBaseModel, decimal>(mostPlayedSong, (decimal)mostListenedSongCount / totalSongs);
        }

        private async Task<SongDataBaseModel> GetMostPlayedSong(int userId)
        {
            var mostPlayedSongId = await _context.UserPlaybackBehaviour
                .Where(ub => ub.UserId == userId && ub.EventType == PlaybackEventType.StartSongPlayback && ub.Timestamp.Year == DateTime.UtcNow.Year)
                .GroupBy(ub => ub.SongId)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefaultAsync();

            return await _context.SongDataBaseModel
                .Where(song => song.SongId == mostPlayedSongId)
                .FirstOrDefaultAsync();
        }

        private async Task<int> GetTotalSongsPlayedByUser(int userId)
        {
            return await _context.UserPlaybackBehaviour
                .Where(ub => ub.UserId == userId && ub.EventType == PlaybackEventType.StartSongPlayback && ub.Timestamp.Year == DateTime.UtcNow.Year)
                .CountAsync();
        }

        private async Task<int> GetMostListenedSongCount(int userId)
        {
            var mostListenedSongId = await _context.UserPlaybackBehaviour
                .Where(ub => ub.UserId == userId && ub.EventType == PlaybackEventType.StartSongPlayback && ub.Timestamp.Year == DateTime.UtcNow.Year)
                .GroupBy(ub => ub.SongId)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefaultAsync();

            return await _context.UserPlaybackBehaviour
                .Where(ub => ub.UserId == userId && ub.EventType == PlaybackEventType.StartSongPlayback && ub.Timestamp.Year == DateTime.UtcNow.Year && ub.SongId == mostListenedSongId)
                .CountAsync();
        }

        public Tuple<string, decimal> GetMostPlayedArtistPercentile(int userId)
        {
            throw new NotImplementedException();
        }

        public List<string> GetTop5Genres(int userId)
        {
            throw new NotImplementedException();
        }

        public List<string> GetAllNewGenresDiscovered(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
