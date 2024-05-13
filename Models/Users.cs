using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using UBB_SE_2024_923_1.Enums;

namespace UBB_SE_2024_923_1.Models
{
    [PrimaryKey(nameof(UserId))]
    public class Users
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public int Role { get; set; }
    }
}
