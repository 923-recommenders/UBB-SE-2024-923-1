using System.ComponentModel.DataAnnotations;

namespace UBB_SE_2024_923_1.Models
{
    /// <summary>
    /// Represents the details of an artist, including their unique identifier
    /// and name.
    /// </summary>
    public class ArtistDetails
    {
        [Key] public int ArtistId { get; set; } = 0;
        public string Name { get; set; } = string.Empty;

    }
}
