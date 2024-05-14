namespace UBB_SE_2024_923_1.Services
{
    public interface IUserService
    {
        Task<bool> RegisterUser(string username, string password, string country, string email, int age);
        Task<string> AuthenticateUser(string username, string password);
        Task<bool> EnableOrDisableArtist(int userId);
    }
}