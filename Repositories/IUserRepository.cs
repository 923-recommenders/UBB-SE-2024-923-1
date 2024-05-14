using UBB_SE_2024_923_1.Models;

namespace UBB_SE_2024_923_1.Repositories
{
    public interface IUserRepository
    {
        Task BcryptPassword(Users user);
    }
}