using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostN.Domain;
using PostN.DataAccess;
using PostN.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Entity = PostN.DataAccess.Entities;

using Xunit;

namespace Tests
{
    public class RepoTest
    {

        private readonly DbContextOptions<CMKWDTP2Context> options;

        public RepoTest()
        {
            options = new DbContextOptionsBuilder<CMKWDTP2Context>().UseSqlite("Filename=Test.db").Options;
            Seed();
        }
       
        [Fact]
        public void GetAllUsersShouldGetAllUsers()
        {
            using (var testcontext = new CMKWDTP2Context(options))
            {
                IUserRepo _repo = new UserRepo(testcontext);
                var users = _repo.GetUsers();
                int i = users.Result.Count;
                Assert.Equal(4, i);
            }
        }

        [Fact]
        public void AddAUserShouldAddAUser()
        {
            using (var testcontext = new CMKWDTP2Context(options))
            {
                IUserRepo _repo = new UserRepo(testcontext);
        
                //Act
                _repo.AddAUser(
                    new PostN.Domain.User
                    {
                        Id = 1,
                        FirstName = "Emma",
                        LastName = "Lee",
                        Username = "el12",
                        Email = "emmaw@gmail.com",
                        State = "MI",
                        Country = "US",
                        Password = "emlee12",
                        Role = "Administrator",
                        PhoneNumber = "608-479-1147",
                        DoB = System.DateTime.Today
                    }
                );
            }
            using (var assertContext = new CMKWDTP2Context(options))
            {
                PostN.DataAccess.Entities.User user = assertContext.Users.FirstOrDefault(user => user.Id == 1);
        
        
                Assert.NotNull(user);
                //Assert.Equal(1, customer.Id);
                //Assert.Equal("Emma", customer.FirstName);
                //Assert.Equal("Lee", customer.LastName);
                //Assert.Equal("emmaw@gmail.com", customer.Email);
            }
        }


        private void Seed()
        {
            using (var context = new Entity.CMKWDTP2Context(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Users.AddRange(
                    new Entity.User
                    {
                        Id = 1,
                        FirstName = "Keegan",
                        LastName = "Wedwick",
                        Email = "kwedwick@gmail.com",
                        Username = "kwedwick",
                        Password = "password",
                        AboutMe = "Keegan About Me",
                        State = "WI",
                        Country = "USA",
                        Role = "Administrator",
                        PhoneNumber = "608-479-1147",
                        DoB = System.DateTime.Today
                    }
                    ,
                    new Entity.User
                    {
                        Id = 2,
                        FirstName = "Christopher",
                        LastName = "Mesidor",
                        Email = "cMesidor@gmail.com",
                        Username = "cmesidor",
                        Password = "password1234",
                        AboutMe = "New .NET developer",
                        State = "NY",
                        Country = "USA",
                        Role = "Administrator",
                        PhoneNumber = "123-456-1234",
                        DoB = System.DateTime.Today
                    },
                    new Entity.User
                    {
                        Id = 3,
                        FirstName = "Du",
                        LastName = "Traun",
                        Email = "dTran@gmail.com",
                        Username = "dTran",
                        Password = "password",
                        AboutMe = "New ASP.NET developer",
                        State = "MA",
                        Country = "USA",
                        Role = "Administrator",
                        PhoneNumber = "789-123-1111",
                        DoB = System.DateTime.Today

                    },
                    new Entity.User
                    {
                        Id = 4,
                        FirstName = "Elizabeth",
                        LastName = "Jackson",
                        Email = "eJackson@gmail.com",
                        Username = "ejackson",
                        Password = "password",
                        AboutMe = "New JavasScript developer",
                        State = "CA",
                        Country = "USA",
                        Role = "User",
                        PhoneNumber = "399-555-1928",
                        DoB = System.DateTime.Today

                    }
                );


                context.Posts.AddRange(
                    new Entity.Post
                    {
                        Id = 1,
                        UserId = 1,
                        Created = DateTime.Now,
                        Title = "Keegan first title",
                        Body = "This is Keegan''s post body!"
                    },
                    new Entity.Post
                    {
                        Id = 2,
                        UserId = 2,
                        Created = DateTime.Now,
                        Title = "Chris first title",
                        Body = "This is Chris''s post body!"
                    },
                    new Entity.Post
                    {
                        Id = 3,
                        UserId = 3,
                        Created = DateTime.Now,
                        Title = "Du first title",
                        Body = "This is Du''s post body!"
                    },
                    new Entity.Post
                    {
                        Id = 4,
                        UserId = 4,
                        Created = DateTime.Now,
                        Title = "Elizabeth first title",
                        Body = "This is Elizabeth''s post body!"
                    }
                ); ;

                context.Comments.AddRange(
                    new Entity.Comment
                    {
                        Id = 1,
                        UserId = 2,
                        PostId = 1,
                        Created = DateTime.Now,
                        CommentBody = "Great post Keegan! - Chris"
                    },
                    new Entity.Comment
                    {
                        Id = 2,
                        UserId = 3,
                        PostId = 2,
                        Created = DateTime.Now,
                        CommentBody = "Great post Chris! - Du"
                    },
                    new Entity.Comment
                    {
                        Id = 3,
                        UserId = 4,
                        PostId = 3,
                        Created = DateTime.Now,
                        CommentBody = "Great post Du! - Elizabeth"
                    },
                    new Entity.Comment
                    {
                        Id = 4,
                        UserId = 1,
                        PostId = 4,
                        Created = DateTime.Now,
                        CommentBody = "Great post Elizabeth! - Keegan"
                    }
                );

                context.Followers.AddRange(
                    new Entity.Follower
                    {
                        Id = 1,
                        UserId = 1,
                        UserId2 = 2,
                        FriendRequest = 1,
                    },
                    new Entity.Follower
                    {
                        Id = 2,
                        UserId = 2,
                        UserId2 = 3,
                        FriendRequest = 1,
                    },
                    new Entity.Follower
                    {
                        Id = 3,
                        UserId = 3,
                        UserId2 = 4,
                        FriendRequest = 1,
                    },
                    new Entity.Follower
                    {
                        Id = 4,
                        UserId = 4,
                        UserId2 = 1,
                        FriendRequest = 1,
                    }
                );
                context.SaveChanges();
            }
        }

    }
}
