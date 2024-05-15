using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBB_SE_2024_923_1.Models;
using UBB_SE_2024_923_1.Repositories;

namespace UBB_SE_2024_923_1_TEST.Services.Stubs
{
    internal class TestUserRepository : IUserRepository
    {
        private List<Users> _users;

        public TestUserRepository()
        {
            _users = new List<Users>();
        }

        public Task Add(Users entity)
        {
            Users user = new Users
            {
                UserId = 0,
                UserName = entity.UserName,
                Password = entity.Password,
                Age = entity.Age,
                Email = entity.Email,
                Country = entity.Country
            };
            if (_users.Count == 0)
            {
                user.UserId = 1;
            }
            else
            {
                user.UserId = _users[_users.Count - 1].UserId + 1;
            }

            _users.Add(user);
            return Task.CompletedTask;
        }

        public Task BcryptPassword(Users user)
        {
            return Task.CompletedTask;
        }

        public Task EnableOrDisableArtist(Users user)
        {
            return Task.CompletedTask;
        }

        public async Task<Users> GetById(int id)
        {
            return await Task.FromResult(_users.FirstOrDefault(u => u.UserId == id));
        }

        public async Task<Users> GetUserByUsername(string username)
        {
            return await Task.FromResult(_users.FirstOrDefault(u => u.UserName == username));
        }

        public bool VerifyPassword(string inputPassword, string hashedPassword)
        {
            return inputPassword == hashedPassword;
        }
    }
}