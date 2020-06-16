using Data;
using Data.Model;
using EntityFrameworkCore3Mock;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PremiereAPI.Controllers;
using System.Collections.Generic;
using Xunit;

namespace PremiereAPITest
{
    public class UsersControllerTest
    {
        public DbContextOptions<Context> DbContextOptions { get; } = new DbContextOptionsBuilder<Context>().Options;
        private DbContextMock<Context> DbContextMock { get; set; }

        readonly User PaulBismuth = new User { Id = 1, UserName = "Paul Bismuth" };
        readonly User AlainDeloin = new User { Id = 2, UserName = "Alain Deloin" };
        public List<User> TwoUsers { get; set; }

        public UsersControllerTest()
        {
            DbContextMock = new DbContextMock<Context>(DbContextOptions);
            TwoUsers = new List<User>() { PaulBismuth, AlainDeloin };
        }


        [Fact]
        public void DetailsWithoutUsers()
        {
            var emmptyUsersDbSetMock = DbContextMock.CreateDbSetMock(x => x.Users, null);
            UsersController userController = new UsersController(DbContextMock.Object);
            for (int i = 1; i < 10; i++)
            {
                JsonResult result = userController.Details(i).Result;
                Assert.Null(result.Value);
            }
        }

        [Fact]
        public void DetailsWithTwoUsers()
        {
            var usersDbSetMock = DbContextMock.CreateDbSetMock(x => x.Users, TwoUsers);
            UsersController userController = new UsersController(DbContextMock.Object);

            List<User> result = new List<User>();
            for (int i = 0; i < 10; i++)
            {
                result.Add((User)userController.Details(i + 1).Result.Value);
            }
            Assert.Equal(PaulBismuth.UserName, result[0].UserName);
            Assert.Equal(AlainDeloin.UserName, result[1].UserName);
            for (int i = 2; i < 10; i++)
            {
                Assert.Null(result[i]);
            }
        }


        [Fact]
        public void IndexWithoutUsers()
        {
            var emmptyUsersDbSetMock = DbContextMock.CreateDbSetMock(x => x.Users, null);
            UsersController userController = new UsersController(DbContextMock.Object);

            JsonResult result = userController.Index().Result;
            Assert.Equal(new List<User>(), result.Value);
        }

        [Fact]
        public void IndexWithTwoUsers()
        {
            var usersDbSetMock = DbContextMock.CreateDbSetMock(x => x.Users, TwoUsers);
            UsersController userController = new UsersController(DbContextMock.Object);

            List<User> index = (List<User>)userController.Index().Result.Value;
            Assert.Equal(2, index.Count);
            Assert.Equal(PaulBismuth.UserName, index[0].UserName);
            Assert.Equal(AlainDeloin.UserName, index[1].UserName);
        }

        [Fact]
        public void DeleteWithTwoUsers()
        {
            var usersDbSetMock = DbContextMock.CreateDbSetMock(x => x.Users, TwoUsers);
            UsersController userController = new UsersController(DbContextMock.Object);
            List<User> index = (List<User>)userController.Delete(1).Result.Value;
            Assert.Single(index);
            Assert.Equal(AlainDeloin.UserName, index[0].UserName);
            index = (List<User>)userController.Delete(2).Result.Value;
            Assert.Empty(index);
        }
    }
}

// Query dbContextMock.Object.Users to see if certain users were added or removed
// or use Mock Verify functionality to verify if certain methods were called: usersDbSetMock.Verify(x => x.Add(...), Times.Once);
