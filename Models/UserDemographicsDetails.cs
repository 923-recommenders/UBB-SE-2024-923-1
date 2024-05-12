using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
namespace UBB_SE_2024_923_1.Models
{
    /// <summary>
    /// Represents demographic information stored in the database about a user,
    /// including their unique identifier, name, gender, date of birth, country,
    /// language, race, and whether they are a premium user.
    /// </summary>
    [PrimaryKey(nameof(User_Id))]
    public class UserDemographicsDetails
    {
        public int User_Id { get; set; }
        public string Name { get; set; }
        public int Gender { get; set; }
        public DateTime Date_Of_fBirth { get; set; }
        public string Country { get; set; }
        public string Language { get; set; }
        public string Race { get; set; }
        public bool Premium_User { get; set; }
    }
}
