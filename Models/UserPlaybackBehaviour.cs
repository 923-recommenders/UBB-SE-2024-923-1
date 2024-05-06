using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using UBB_SE_2024_923_1.Enums;

namespace UBB_SE_2024_923_1.Models
{
    /// <summary>
    /// Represents detailed demographic information about a user stored in database,
    /// including their unique identifier, name, gender, date of birth,
    /// country, language, race, and whether they are a premium user.
    /// </summary>

    [PrimaryKey(nameof(UserId), nameof(SongId), nameof(Timestamp))]
    public class UserPlaybackBehaviour
    {
        public int UserId { get; set; }
        public int SongId { get; set; }
        public DateTime Timestamp { get; set; }


        public PlaybackEventType EventType { get; set; }

    }
}
