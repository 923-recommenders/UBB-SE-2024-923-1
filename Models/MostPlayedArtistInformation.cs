using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
namespace UBB_SE_2024_923_1.Models
{
    /// <summary>
    /// Represents information about the most played artist, including their
    /// unique identifier and the number of start listen events.
    /// </summary>
    [PrimaryKey(nameof(Artist_Id))]
    public class MostPlayedArtistInformation
    {
        public int Artist_Id { get; set; }
        public int Start_Listen_Events { get; set; }
    }
}
