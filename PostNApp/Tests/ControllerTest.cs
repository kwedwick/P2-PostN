using System;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using PostN.WebApi.Controllers;
using PostN.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using PostN.DataAccess;
using PostN.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Entity = PostN.DataAccess.Entities;
using System.Collections.Generic;
using Post = PostN.Domain.Post;

namespace Tests
{
    public class ControllerTest
    {
        private readonly DbContextOptions<CMKWDTP2Context> options;

        public ControllerTest()
        {
            options = new DbContextOptionsBuilder<CMKWDTP2Context>().UseSqlite("Filename=Test.db").Options;
        
        }
        //[Fact]
        //public void ProveThatUserControllerIsCalled()
        //{
        //    var logger = new Mock<ILogger<UserController>>();
        //    var mockRepo = new Mock<IUserRepo>();
        //
        //    var userController = new UserController(mockRepo.Object, logger.Object);
        //
        //    var result = userController.GetUsers();
        //
        //    var viewResult = Assert.IsType<ViewResult>(result);
        //    int i = result.Result.Count;
        //    Assert.Equal(viewResult.Count, result.Count);
        //
        //}

        [Fact]
        public void ProveThatPostControllerIsCalled()
        {
            var logger = new Mock<ILogger<PostController>>();
            var mockRepo = new Mock<IPostRepo>();
        
            var postController = new PostController(mockRepo.Object, logger.Object);
        
            var result = postController.Get();
            
            var viewResult = Assert.IsType<ViewResult>(result);
            
            Assert.Equal(viewResult, result.Result);
        
        }
        [Fact]
        public async Task GetAllProductsAsync_ShouldReturnAllProducts()
        {
            var logger = new Mock<ILogger<PostController>>();
            var mockRepo = new Mock<IPostRepo>();
            using (var testcontext = new CMKWDTP2Context(options))
            {
                IPostRepo _repo = new PostRepo(testcontext);
                var testPost = _repo.GetAllPosts();
                var controller = new PostController(mockRepo.Object, logger.Object);

                var result = await controller.GetAllPostAsync() as List<Post>;
                Assert.Equal(testPost.Result, result);
            }
            
        }

        //[Fact]
        //public void ProveThatPostControllerIsCalled()
        //{
        //    var logger = new Mock<ILogger<PostController>>();
        //    var mockRepo = new Mock<IPostRepo>();
        //
        //    var PostController = new PostController(mockRepo.Object, logger.Object);
        //
        //    var result = PostController.Get();
        //    int x = result.Result.Count;
        //    using (var testcontext = new CMKWDTP2Context(options))
        //    {
        //        IPostRepo _repo = new PostRepo(testcontext);
        //
        //        //Act
        //        var repoResult = _repo.GetAllPosts();
        //        //var viewResult = Assert.IsType<ViewResult>(result); }
        //        int i = repoResult.Result.Count;
        //        Assert.Equal(repoResult.Result.Count, result);
        //
        //    }
        //    
        //}
        
    }
}
