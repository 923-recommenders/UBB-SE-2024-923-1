using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using UBB_SE_2024_923_1.Data;
using UBB_SE_2024_923_1.DTO;
using UBB_SE_2024_923_1.Enums;
using UBB_SE_2024_923_1.Models;

namespace UBB_SE_2024_923_1.Repositories
{
    public class SongBasicDetailsRepository : Repository<SongDataBaseModel>, ISongBasicDetailsRepository
    {
        public SongBasicDetailsRepository(DataContext context) : base(context)
        {
        }

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
            /*SELECT * FROM SongBasicDetails WHERE song_id IN (SELECT TOP 5 song_id FROM UserPlaybackBehaviour WHERE user_id = @userId
             AND event_type = 2 GROUP BY song_id ORDER BY COUNT(song_id) DESC);*/

            /*var top5SongIds = await _context.UserPlaybackBehaviour
                .Where(ub => ub.UserId == userId && ub.EventType == PlaybackEventType.StartSongPlayback)
                .GroupBy(ub => ub.SongId)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .Take(5)
                .ToListAsync();

            return await _context.SongDataBaseModel
                .Where(song => top5SongIds.Contains(song.SongId))
                .ToListAsync();*/

            /*return await _context.UserPlaybackBehaviour
                .Where(upb => upb.UserId == userId && upb.EventType == PlaybackEventType.StartSongPlayback)
                .GroupBy(upb => upb.SongId)
                .Select(g => new { SongId = g.Key, Count = g.Count() })
                .OrderByDescending(g => g.Count)
                .Take(5)
                .Select(g => _context.SongDataBaseModel.Find(g.SongId))
                .ToListAsync();*/

            return await _context.SongDataBaseModel
                .Where(song => _context.UserPlaybackBehaviour
                    .Where(ub => ub.UserId == userId && ub.EventType == PlaybackEventType.StartSongPlayback)
                    .GroupBy(ub => ub.SongId)
                    .Select(g => g.Key)
                    .Take(5)
                    .Contains(song.SongId))
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

        public async Task<Tuple<string, decimal>> GetMostPlayedArtistPercentile(int userId)
        {
            var mostPlayedArtistInfo = await GetMostPlayedArtistInfoAsync(userId);
            var mostPlayedArtist = await GetMostPlayedArtist(userId, mostPlayedArtistInfo);
            var totalSongs = await GetTotalNumberOfSongs(userId);
            return new Tuple<string, decimal>(mostPlayedArtist, (decimal)mostPlayedArtistInfo.Start_Listen_Events / totalSongs);
        }

        private async Task<MostPlayedArtistInformation> GetMostPlayedArtistInfoAsync(int userId)
        {
            return await _context.UserPlaybackBehaviour
                .Where(ub =>
                    ub.UserId == userId && ub.EventType == PlaybackEventType.StartSongPlayback &&
                    ub.Timestamp.Year == DateTime.UtcNow.Year)
                .Join(
                    _context.SongDataBaseModel,
                    ub => ub.SongId,
                    sd => sd.SongId,
                    (ub, sd) => new { ArtistId = sd.ArtistId })
                .GroupBy(result => result.ArtistId)
                .Select(group => new MostPlayedArtistInformation
                {
                    Artist_Id = group.Key,
                    Start_Listen_Events = group.Count()
                })
                .OrderByDescending(info => info.Start_Listen_Events)
                .FirstOrDefaultAsync();
        }

        private async Task<string> GetMostPlayedArtist(int userId, MostPlayedArtistInformation mostPlayedArtistInfo)
        {
            return await _context.ArtistDetails
                .Where(ad => ad.ArtistId == mostPlayedArtistInfo.Artist_Id)
                .Select(ad => ad.Name)
                .FirstOrDefaultAsync();
        }

        private async Task<int> GetTotalNumberOfSongs(int userId)
        {
            return await _context.UserPlaybackBehaviour
                .Where(ub => ub.UserId == userId && ub.EventType == PlaybackEventType.StartSongPlayback && ub.Timestamp.Year == DateTime.UtcNow.Year)
                .CountAsync();
        }

        public async Task<List<string>> GetTop5Genres(int userId)
        {
            return await _context.UserPlaybackBehaviour
                .Where(ub => ub.UserId == userId && ub.EventType == PlaybackEventType.StartSongPlayback && ub.Timestamp.Year == DateTime.UtcNow.Year)
                .Join(
                    _context.SongDataBaseModel,
                    ub => ub.SongId,
                    sb => sb.SongId,
                    (ub, sb) => sb.Genre)
                .GroupBy(genre => genre)
                .OrderByDescending(group => group.Count())
                .Select(group => group.Key)
                .Take(5)
                .ToListAsync();
        }

        public async Task<List<string>> GetAllNewGenresDiscovered(int userId)
        {
            var currentYearGenres = await _context.SongDataBaseModel
                .Where(sb => _context.UserPlaybackBehaviour
                    .Where(ub => ub.UserId == userId && ub.EventType == PlaybackEventType.StartSongPlayback && ub.Timestamp.Year == DateTime.UtcNow.Year)
                    .Select(ub => ub.SongId)
                    .Contains(sb.SongId))
                .Select(sb => sb.Genre)
                .Distinct()
                .ToListAsync();

            var previousYearGenres = await _context.SongDataBaseModel
                .Where(sb => _context.UserPlaybackBehaviour
                    .Where(ub => ub.UserId == userId && ub.EventType == PlaybackEventType.StartSongPlayback && ub.Timestamp.Year == DateTime.UtcNow.Year - 1)
                    .Select(ub => ub.SongId)
                    .Contains(sb.SongId))
                .Select(sb => sb.Genre)
                .ToListAsync();

            return currentYearGenres.Except(previousYearGenres).ToList();
        }
    }
}