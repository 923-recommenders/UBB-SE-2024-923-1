using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using UBB_SE_2024_923_1.Models;
using Microsoft.EntityFrameworkCore;
using UBB_SE_2024_923_1.Data;

namespace UBB_SE_2024_923_1.Repositories
{
    /// <summary>
    /// Represents a repository for managing user playback behavior data,
    /// including operations for retrieving playback behavior records.
    /// </summary>
    public class UserPlaybackBehaviourRepository : Repository<UserPlaybackBehaviour>, IUserPlaybackBehaviourRepository
    {
        public UserPlaybackBehaviourRepository(DataContext context) : base(context)
        {
        }

        /// <summary>
        /// Retrieves a specific user's playback behavior record
        /// based on the provided criteria.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="songId">The optional ID of the song.</param>
        /// <param name="timestamp">The optional timestamp.</param>
        /// <returns>The user's playback behavior record matching
        /// the specified criteria, or null if no match is found.</returns>
        public async Task<UserPlaybackBehaviour> GetUserPlaybackBehaviour(int userId, int? songId = null, DateTime? timestamp = null)
        {
            IQueryable<UserPlaybackBehaviour> query = _context.UserPlaybackBehaviour.Where(upb => upb.UserId == userId);
            if (songId.HasValue)
            {
                query = query.Where(upb => upb.SongId == songId.Value);
            }
            if (timestamp.HasValue)
            {
                query = query.Where(upb => upb.Timestamp == timestamp.Value);
            }
            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Retrieves all playback behavior records for a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A list of playback behavior records for the specified user.</returns>
        public async Task<List<UserPlaybackBehaviour>> GetListOfUserPlaybackBehaviourEntities(int userId)
        {
            return await _context.UserPlaybackBehaviour.Where(upb => upb.UserId == userId).ToListAsync();
        }
    }
}
