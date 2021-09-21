using Microsoft.EntityFrameworkCore;
using PostN.DataAccess.Entities;
using PostN.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostN.DataAccess
{
    public class UserRepo : IUserRepo
    {
        private readonly CMKWDTP2Context _context;
        public UserRepo(CMKWDTP2Context context)
        {
            _context = context;
        }

        public Task<List<Domain.User>> GetUsers()
        {
            return /*Task.FromResult(_context.Users.Select(
                users => new Domain.User
                (
                    users.Id,
                    users.FirstName,
                    users.LastName,
                    users.Email,
                    users.Username,
                    users.AboutMe,
                    users.State,
                    users.Country,
                    users.Role,
                    users.PhoneNumber,
                    users.DoB
                 )
            ).ToList());*/
                Task.FromResult(_context.Users
                   .Include(p => p.Posts)
                   .ThenInclude(c => c.Comments)
                   .Include(f => f.FollowerUsers)
                   .ThenInclude(fu => fu.UserId2Navigation)
                   .Select(u => new Domain.User
                   {
                       Id = u.Id,
                       FirstName = u.FirstName,
                       LastName = u.LastName,
                       Email = u.Email,
                       Username = u.Username,
                       AboutMe = u.AboutMe,
                       State = u.State,
                       Country = u.Country,
                       Role = u.Role,
                       PhoneNumber = u.PhoneNumber,
                       DoB = u.DoB,
                       Comments = u.Comments.Select(c => new Domain.Comment(c.Id, c.UserId, c.User.Username, c.PostId, c.Created, c.CommentBody)).ToList(),
                       Friends = u.FollowerUsers.Select(f => new Domain.Follower(f.Id, f.UserId, f.User.Username, f.UserId2, f.UserId2Navigation.Username, f.FriendRequest)).ToList(),
                       Posts = u.Posts.Select(k => new Domain.Post(k.Id, k.UserId, k.User.Username, k.Image, k.Created, k.Title, k.Body, k.Comments.Select(k => new Domain.Comment(k.Id, k.UserId, k.PostId, k.User.Username, k.Created, k.CommentBody)).ToList())).ToList()
                   }
                ).ToList());
        }
        public async Task<Domain.User> GetUserById(int id)
        {
            var returnedUsers = await _context.Users
                   .Include(p => p.Posts)
                   .ThenInclude(c => c.Comments)
                   .Include(f => f.FollowerUsers)
                   .ThenInclude(fu => fu.UserId2Navigation)
                   .Select(u => new Domain.User
                   {
                       Id = u.Id,
                       FirstName = u.FirstName,
                       LastName = u.LastName,
                       Email = u.Email,
                       Username = u.Username,
                       AboutMe = u.AboutMe,
                       State = u.State,
                       Country = u.Country,
                       Role = u.Role,
                       PhoneNumber = u.PhoneNumber,
                       DoB = u.DoB,
                       Comments = u.Comments.Select(c => new Domain.Comment(c.Id, c.UserId, c.User.Username, c.PostId, c.Created, c.CommentBody)).ToList(),
                       Friends = u.FollowerUsers.Select(f => new Domain.Follower(f.Id, f.UserId, f.User.Username, f.UserId2, f.UserId2Navigation.Username, f.FriendRequest)).ToList(),
                       Posts = u.Posts.Select(k => new Domain.Post(k.Id, k.UserId, k.User.Username, k.Image, k.Created, k.Title, k.Body, k.Comments.Select(k => new Domain.Comment(k.Id, k.UserId, k.PostId, k.User.Username, k.Created, k.CommentBody)).ToList())).ToList()
                   }
                ).ToListAsync();
            Domain.User singleUser = returnedUsers.FirstOrDefault(p => p.Id == id);
                
            if(singleUser.Posts != null)
                singleUser.Posts?.Reverse();
            
            
            return singleUser;
        }
        public Task<Domain.User> GetUserwithPostComment(int id)
        {
            var returnedUsers =_context.Users
                   .Include(p => p.Posts)
                   .ThenInclude(c => c.Comments)
                   .Select(u => new Domain.User
                   {
                       Id = u.Id,
                       FirstName = u.FirstName,
                       LastName = u.LastName,
                       Email = u.Email,
                       Username = u.Username,
                       AboutMe = u.AboutMe,
                       State = u.State,
                       Country = u.Country,
                       Role = u.Role,
                       PhoneNumber = u.PhoneNumber,
                       DoB = u.DoB,
                       Comments = u.Comments.Select(c => new Domain.Comment(c.Id, c.UserId, c.User.Username, c.PostId, c.Created, c.CommentBody)).ToList(),
                       Posts = u.Posts.Select(k => new Domain.Post(k.Id, k.UserId, k.User.Username, k.Image, k.Created, k.Title, k.Body, k.Comments.Select(k => new Domain.Comment(k.Id, k.UserId, k.PostId, k.User.Username, k.Created, k.CommentBody)).ToList())).ToList()
                   }
                ).ToList();
            Domain.User singleUser = returnedUsers.FirstOrDefault(p => p.Id == id);
            singleUser.Posts?.Reverse();
            return Task.FromResult(singleUser);
        }

        public async Task<Domain.User> SearchUserById(int id)
        {
            var returnedUsers = await _context.Users
                   .Include(p => p.Posts)
                   .ThenInclude(c => c.Comments)
                   .Include(f => f.FollowerUsers)
                   .ThenInclude(fu => fu.UserId2Navigation)
                   .Select(u => new Domain.User
                   {
                       Id = u.Id,
                       FirstName = u.FirstName,
                       LastName = u.LastName,
                       Email = u.Email,
                       Username = u.Username,
                       AboutMe = u.AboutMe,
                       State = u.State,
                       Country = u.Country,
                       Role = u.Role,
                       PhoneNumber = u.PhoneNumber,
                       DoB = u.DoB,
                       Comments = u.Comments.Select(c => new Domain.Comment(c.Id, c.UserId, c.User.Username, c.PostId, c.Created, c.CommentBody)).ToList(),
                       Friends = u.FollowerUsers.Select(f => new Domain.Follower(f.Id, f.UserId, f.User.Username, f.UserId2, f.UserId2Navigation.Username, f.FriendRequest)).ToList(),
                       Posts = u.Posts.Select(k => new Domain.Post(k.Id, k.UserId, k.User.Username, k.Image, k.Created, k.Title, k.Body, k.Comments.Select(k => new Domain.Comment(k.Id, k.UserId, k.PostId, k.User.Username, k.Created, k.CommentBody)).ToList())).ToList()
                   }
                ).ToListAsync();
            Domain.User singleUser = returnedUsers.FirstOrDefault(p => p.Id == id);
            if (singleUser.Posts != null)
                singleUser.Posts?.Reverse();
            return singleUser;
        }
        public async Task<Domain.User> UpdateUser(int id, Domain.User user)
        {
            Entities.User foundUser = await _context.Users.FindAsync(id);
            if (foundUser != null)
            {
                foundUser.Id = id;
                foundUser.FirstName = user.FirstName;
                foundUser.LastName = user.LastName;
                foundUser.Email = user.Email;
                foundUser.PhoneNumber = user.PhoneNumber;
                foundUser.AboutMe = user.AboutMe;

                _context.Users.Update(foundUser);
                await _context.SaveChangesAsync();

                var updatedUser = await GetUserById(id);
                return updatedUser;
            }

            return new Domain.User();
        }

        public async Task<Domain.User> AddAUser(Domain.User user)
        {
            if (UniqueUsername(user.Username) is true)
            {
                throw new Exception($"Username {user.Username} has been already used");
            }
            if (UniqueEmail(user.Email) is true)
            {
                throw new Exception($"Email {user.Email} has been already used");
            }

            var newEntity = new Entities.User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Username = user.Username,
                Password = user.Password,
                AboutMe = user.AboutMe,
                State = user.State,
                Country = user.Country,
                Role = "User",
                PhoneNumber = user.PhoneNumber,
                DoB = user.DoB,
            };
            await _context.Users.AddAsync(newEntity);
            await _context.SaveChangesAsync();
            user.Id = newEntity.Id;
            return user;
        }

        //Username should be unique
        public bool UniqueUsername(string username)
        {
            if (_context.Users.Any(u => u.Username == username))
            {
                return true;
            }
            return false;
        }
        //Email should be unique
        public bool UniqueEmail(string email)
        {
            if (_context.Users.Any(user => user.Email == email))
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteUserById(int id)
        {
            Entities.User userToDelete = await _context.Users
                .FirstOrDefaultAsync(user => user.Id == id);
            if (userToDelete != null)
            {
                _context.Users.Remove(userToDelete);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        //For the explore controller

        public Domain.User SearchUsersByName(string username)
        {
            try
            {
                Entities.User foundUser = _context.Users
               .FirstOrDefault(user => user.Username == username);
                return new Domain.User(foundUser.Id, foundUser.FirstName, foundUser.LastName, foundUser.Email, foundUser.Username, foundUser.AboutMe, foundUser.State, foundUser.Country, foundUser.Role, foundUser.PhoneNumber, foundUser.DoB);
            }
            catch (System.InvalidOperationException e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public Task<List<Domain.Follower>> GetFollowers(int userId)
        {
            var friends = _context.Followers.Where(f => f.UserId == userId)
                .Include(n => n.UserId2Navigation).Select(u => new Domain.Follower
                {
                    Id = u.Id,
                    UserId = u.UserId,
                    Username = u.User.Username,
                    UserId2 = u.UserId2,
                    FriendUsername = u.UserId2Navigation.Username,
                    FriendRequest = u.FriendRequest
                }).ToList();
            //new Domain.Follower(f.Id, f.UserId, f.User.Username, f.UserId2, f.UserId2Navigation.Username, f.FriendRequest)
            return Task.FromResult(friends);

        }

        public async Task<bool> AddAFollower(int userId, int friendId)
        {
            try
            {
                if (_context.Followers.Any(u => u.UserId == userId && u.UserId2 == friendId))
                {
                    throw new Exception($"{friendId} is already following you.");
                }
                await _context.Followers.AddAsync(
                    new Entities.Follower
                    {
                        UserId = userId,
                        UserId2 = friendId,
                        FriendRequest = 1,
                    }
                );
                await _context.SaveChangesAsync();

                return true;
            } catch
            {
                return false;
            }
            
            
        }

        public async Task<bool> DeleteFollower(int userId, int friendId)
        {
            Entities.Follower foundFollower = await _context.Followers
                .FirstOrDefaultAsync(u => u.UserId == userId && u.UserId2 == friendId);
            if(foundFollower != null)
            {
                _context.Followers.Remove(foundFollower);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<Domain.User> UserLoginAsync(Domain.User user)
        {
            Entities.User foundUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == user.Username && u.Password == user.Password);

            if (foundUser != null)
            {
               Domain.User loginUser = await GetUserById(foundUser.Id);
                return loginUser;
            }
            return null;
        }

        public async Task<bool> CheckIfFriend(int userId, int friendId)
        {
            Entities.Follower foundFriend = await _context.Followers.FirstOrDefaultAsync(f => f.UserId == userId && f.UserId2 == friendId);

            if (foundFriend != null)
            {
                return true;
            }
            return false;
        }
    }
}