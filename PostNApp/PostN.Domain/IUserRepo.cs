using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PostN.Domain
{
    public interface IUserRepo
    {
        Task<List<User>> GetUsers();
        Task<User> GetUserById(int id);
        Task<User> UpdateUser(int id, User user);
        Task<User> SearchUserById(int id);
        Task<User> AddAUser(User user);
        Task<bool> DeleteUserById(int id);
        User SearchUsersByName(string username);
        Task<List<Follower>> GetFollowers(int userId);
        Task<bool> AddAFollower(int userId, int friendId);
        Task<bool> DeleteFollower(int userId, int friendId);
        bool UniqueUsername(string username);
        bool UniqueEmail(string email);

        Task<User> UserLoginAsync(User user);

        Task<bool> CheckIfFriend(int userId, int friendId);

    }
}
