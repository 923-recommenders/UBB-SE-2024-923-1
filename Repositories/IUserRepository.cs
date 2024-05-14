using UBB_SE_2024_923_1.Models;

namespace UBB_SE_2024_923_1.Repositories
{
    public interface IUserRepository
    {
        Task BcryptPassword(Users user);

        bool VerifyPassword(string inputPassword, string hashedPassword);

        Task<Users> GetUserByUsername(string username);

        Task EnableOrDisableArtist(Users user);

        Task Add(Users entity);

        Task<Users> GetById(int id);
    }
}