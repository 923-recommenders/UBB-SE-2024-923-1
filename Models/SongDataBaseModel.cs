using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UBB_SE_2024_923_1.Models
{
    /// <summary>
    /// Represents basic details of a song stored in the database,
    /// including its ID, name, genre, subgenre, artist ID, language, country,
    /// album, and image.
    /// </summary>
    public class SongDataBaseModel
    {
        [Key] public int SongId { get; set; } = 0;

        public string Name { get; set; } = "DefaultName";

        public string Genre { get; set; } = "DefaultGenre";

        public string Subgenre { get; set; } = "DefaultSubgenre";

        public int ArtistId { get; set; } = 0;

        public string Language { get; set; } = "DefaultLanguage";

        public string Country { get; set; } = "DefaultCountry";

        public string Album { get; set; } = "DefaultAlbum";

        public string Image { get; set; } = "song_img_default.png";
    }
}
