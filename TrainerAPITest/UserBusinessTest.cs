using Data;
using Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using TrainerAPI.Business;
using Xunit;

namespace TrainerAPITest
{
    public class UserBusinessTest
    {
        private readonly TableUser _user1 = new TableUser { UserName = "TableUser 1" };
        private readonly TableUser _user2 = new TableUser { UserName = "TableUser 2" };
        private readonly TableUser _user3 = new TableUser { UserName = "TableUser 3" };


        private static DefaultContext Context(string dbName)
        {
            var contextOptions = new DbContextOptionsBuilder<DefaultContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            return new DefaultContext(contextOptions);
        }

        private static DefaultContext FalseContext()
        {
            //TODO : make a good database wich fail data when asked
            var contextOptions = new DbContextOptionsBuilder<DefaultContext>()
                .UseSqlite("D:\\temp\\temp.db")
                .EnableSensitiveDataLogging()
                .Options;
            return new DefaultContext(contextOptions);
        }

        [Fact]
        public void Create_User_ShouldReturn_User_With_Id_Grow_Up()
        {
            DefaultContext defaultContext = Context(Guid.NewGuid().ToString());
            TableUserBusiness trainingCourseBusiness = new TableUserBusiness(defaultContext);

            Assert.Equal(1, trainingCourseBusiness.Create(_user1).Id);
            Assert.Equal(2, trainingCourseBusiness.Create(_user2).Id);
            Assert.Equal(3, trainingCourseBusiness.Create(_user3).Id);
        }

        [Fact(Skip = "I did not find a way to fail the addition in database")]
        public void Create_User_Should_Return_Null_When_Create_Failed()
        {
            DefaultContext defaultContext = FalseContext();
            TableUserBusiness trainingCourseBusiness = new TableUserBusiness(defaultContext);

            var tcReturn = trainingCourseBusiness.Create(_user1);

            Assert.Null(tcReturn);
        }

        [Fact]
        public void Read_Users_With_Goods_Id_ShouldReturn_Users()
        {
            DefaultContext defaultContext = Context(Guid.NewGuid().ToString());
            TableUserBusiness trainingCourseBusiness = new TableUserBusiness(defaultContext);

            defaultContext.Users.Add(_user1);
            defaultContext.Users.Add(_user2);
            defaultContext.Users.Add(_user3);
            defaultContext.SaveChanges();

            Assert.Equal(1, trainingCourseBusiness.Read(1).Id);
            Assert.Equal(2, trainingCourseBusiness.Read(2).Id);
            Assert.Equal(3, trainingCourseBusiness.Read(3).Id);
        }

        [Fact]
        public void Read_Users_With_Not_Exist_Id_ShouldReturn_Null()
        {
            DefaultContext defaultContext = Context(Guid.NewGuid().ToString());
            defaultContext.Users.Add(_user1);
            defaultContext.Users.Add(_user2);
            defaultContext.Users.Add(_user3);
            defaultContext.SaveChanges();
            TableUserBusiness trainingCourseBusiness = new TableUserBusiness(defaultContext);

            Assert.Null(trainingCourseBusiness.Read(4));
            Assert.Null(trainingCourseBusiness.Read(18));
            Assert.Null(trainingCourseBusiness.Read(4789));
        }

        [Fact]
        public void Read_Users_When_db_Empty_ShouldReturn_Null()
        {
            DefaultContext defaultContext = Context(Guid.NewGuid().ToString());
            TableUserBusiness trainingCourseBusiness = new TableUserBusiness(defaultContext);

            Assert.Null(trainingCourseBusiness.Read(1));
            Assert.Null(trainingCourseBusiness.Read(2));
            Assert.Null(trainingCourseBusiness.Read(3));
            Assert.Null(trainingCourseBusiness.Read(145));
        }

        [Fact]
        public void Update_User_ShouldReturn_True()
        {
            DefaultContext defaultContext = Context(Guid.NewGuid().ToString());
            defaultContext.Users.Add(_user1);
            defaultContext.Users.Add(_user2);
            defaultContext.Users.Add(_user3);
            defaultContext.SaveChanges();
            defaultContext.Entry(_user1).State = EntityState.Detached;
            defaultContext.Entry(_user2).State = EntityState.Detached;
            defaultContext.Entry(_user3).State = EntityState.Detached;
            TableUserBusiness trainingCourseBusiness = new TableUserBusiness(defaultContext);
            TableUser user1 = new TableUser { Id = 1, UserName = "Name changed" };
            TableUser user2 = new TableUser { Id = 2, UserName = "Name also changed" };
            TableUser user3 = new TableUser { Id = 3, UserName = "Name also also changed" };

            Assert.True(trainingCourseBusiness.Update(user1));
            Assert.True(trainingCourseBusiness.Update(user2));
            Assert.True(trainingCourseBusiness.Update(user3));
        }

        [Fact]
        public void Update_Unknown_User_ShouldReturn_False()
        {
            DefaultContext defaultContext = Context(Guid.NewGuid().ToString());
            defaultContext.Users.Add(_user1);
            defaultContext.Users.Add(_user2);
            defaultContext.Users.Add(_user3);
            defaultContext.SaveChanges();
            TableUserBusiness trainingCourseBusiness = new TableUserBusiness(defaultContext);
            TableUser tc1 = new TableUser { Id = 85, UserName = "Try change name" };
            TableUser tc2 = new TableUser { Id = 147, UserName = "Try change name again" };
            TableUser tc3 = new TableUser { Id = 1487, UserName = "Try change name again again" };

            Assert.False(trainingCourseBusiness.Update(tc1));
            Assert.False(trainingCourseBusiness.Update(tc2));
            Assert.False(trainingCourseBusiness.Update(tc3));
        }

        [Fact]
        public void Delete_User_ShouldReturn_True()
        {
            DefaultContext defaultContext = Context(Guid.NewGuid().ToString());
            defaultContext.Users.Add(_user1);
            defaultContext.Users.Add(_user2);
            defaultContext.Users.Add(_user3);
            defaultContext.SaveChanges();
            defaultContext.Entry(_user1).State = EntityState.Detached;
            defaultContext.Entry(_user2).State = EntityState.Detached;
            defaultContext.Entry(_user3).State = EntityState.Detached;
            TableUserBusiness trainingCourseBusiness = new TableUserBusiness(defaultContext);

            Assert.True(trainingCourseBusiness.Delete(1));
            Assert.True(trainingCourseBusiness.Delete(2));
            Assert.True(trainingCourseBusiness.Delete(3));
        }

        [Fact]
        public void Delete_Unknown_User_ShouldReturn_False()
        {
            DefaultContext defaultContext = Context(Guid.NewGuid().ToString());
            defaultContext.Users.Add(_user1);
            defaultContext.Users.Add(_user2);
            defaultContext.Users.Add(_user3);
            defaultContext.SaveChanges();
            TableUserBusiness trainingCourseBusiness = new TableUserBusiness(defaultContext);

            Assert.False(trainingCourseBusiness.Delete(12));
            Assert.False(trainingCourseBusiness.Delete(17));
            Assert.False(trainingCourseBusiness.Delete(1458));
        }

    }
}

