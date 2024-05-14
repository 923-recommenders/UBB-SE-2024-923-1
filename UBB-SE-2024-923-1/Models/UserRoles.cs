using Microsoft.EntityFrameworkCore;

namespace UBB_SE_2024_923_1.Models
{
    [PrimaryKey(nameof(RoleId))]
    public class UserRoles
    {
        public int RoleId { get; set; }
        public string RoleType { get; set; }
    }
}
