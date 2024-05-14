using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using UBB_SE_2024_923_1.Enums;

namespace UBB_SE_2024_923_1.DTO
{
    public class UserPlayBackBehaviour
    {
        public int UserId { get; set; }

        public int SongId { get; set; }

        public PlaybackEventType EventType { get; set; }

        public DateTime Timestamp { get; set; }

        public UserPlayBackBehaviour()
        {
            UserId = 0;
            SongId = 0;
            EventType = PlaybackEventType.Like;
            Timestamp = DateTime.Now;
        }
    }
}
