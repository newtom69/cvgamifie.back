using Data;
using Data.Model;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TrainerAPI.Business;
using Xunit;

namespace TrainerAPITest
{
    /// <summary>
    /// TUs TrainingCourseBusiness
    /// </summary>
    public class TrainingCourseBusinessTest
    {
        private readonly TrainingCourse _tc1 = new TrainingCourse { Name = "Training course 1" };
        private readonly TrainingCourse _tc2 = new TrainingCourse { Name = "Training course 2" };
        private readonly TrainingCourse _tc3 = new TrainingCourse { Name = "Training course 3" };

        [Fact]
        public void Create_TrainingCourse_Should_Return_TrainingCourse_With_Id_Grow_Up()
        {
            var trainingCourseBusiness = InitializeTrainingCourseBusiness(false);

            var trainingCourseReturned1 = trainingCourseBusiness.Create(_tc1);
            var trainingCourseReturned2 = trainingCourseBusiness.Create(_tc2);
            var trainingCourseReturned3 = trainingCourseBusiness.Create(_tc3);

            Assert.Equal(1, trainingCourseReturned1.Id);
            Assert.Equal(2, trainingCourseReturned2.Id);
            Assert.Equal(3, trainingCourseReturned3.Id);
            Assert.Equal(JsonConvert.SerializeObject(_tc1), JsonConvert.SerializeObject(trainingCourseReturned1));
            Assert.Equal(JsonConvert.SerializeObject(_tc2), JsonConvert.SerializeObject(trainingCourseReturned2));
            Assert.Equal(JsonConvert.SerializeObject(_tc3), JsonConvert.SerializeObject(trainingCourseReturned3));
        }

        [Fact(Skip = "Je n'ai pas trouvé de moyen de faire planter l'ajout en base")]
        // CodeReview : comment faire pour tester l'échec de l'ajout en base et donc le retour null de la méthode Create ?
        public void Create_TrainingCourse_Should_Return_Null_When_Create_Failed()
        {
            DefaultContext defaultContext = FalseFakeContext();
            TrainingCourseBusiness trainingCourseBusiness = new TrainingCourseBusiness(defaultContext);

            Assert.Null(trainingCourseBusiness.Create(_tc1));
        }

        [Fact]
        public void Read_All_TrainingCourse_Should_Return_TrainingCourses()
        {
            var trainingCourseBusiness = InitializeTrainingCourseBusiness(true);

            Assert.Equal(JsonConvert.SerializeObject(new List<TrainingCourse> { _tc1, _tc2, _tc3 }), JsonConvert.SerializeObject(trainingCourseBusiness.List()));
        }

        [Fact]
        public void Read_Found_TrainingCourse_Should_Return_TrainingCourse()
        {
            var trainingCourseBusiness = InitializeTrainingCourseBusiness(true);

            Assert.Equal(JsonConvert.SerializeObject(_tc1), JsonConvert.SerializeObject(trainingCourseBusiness.Read(1)));
            Assert.Equal(JsonConvert.SerializeObject(_tc2), JsonConvert.SerializeObject(trainingCourseBusiness.Read(2)));
            Assert.Equal(JsonConvert.SerializeObject(_tc3), JsonConvert.SerializeObject(trainingCourseBusiness.Read(3)));
        }

        [Fact]
        public void Read_Not_Found_TrainingCourses_ShouldReturn_Null()
        {
            var trainingCourseBusiness = InitializeTrainingCourseBusiness(true);

            Assert.Null(trainingCourseBusiness.Read(4));
            Assert.Null(trainingCourseBusiness.Read(18));
            Assert.Null(trainingCourseBusiness.Read(4789));
        }

        [Fact]
        public void Read_TrainingCourses_When_db_Empty_Should_Return_Null()
        {
            var trainingCourseBusiness = InitializeTrainingCourseBusiness(false);

            Assert.Null(trainingCourseBusiness.Read(1));
            Assert.Null(trainingCourseBusiness.Read(2));
            Assert.Null(trainingCourseBusiness.Read(3));
            Assert.Null(trainingCourseBusiness.Read(145));
        }

        [Fact]
        public void Update_Found_TrainingCourse_Should_Return_True()
        {
            var trainingCourseBusiness = InitializeTrainingCourseBusiness(true);

            var tc1 = new TrainingCourse { Id = 1, Name = "Name changed" };
            var tc2 = new TrainingCourse { Id = 2, Name = "Name also changed" };
            var tc3 = new TrainingCourse { Id = 3, Name = "Name also changed" };

            Assert.True(trainingCourseBusiness.Update(tc1));
            Assert.True(trainingCourseBusiness.Update(tc2));
            Assert.True(trainingCourseBusiness.Update(tc3));
        }

        [Fact]
        public void Update_Not_Found_TrainingCourse_Should_Return_False()
        {
            var trainingCourseBusiness = InitializeTrainingCourseBusiness(true);

            var tc1 = new TrainingCourse { Id = 17, Name = "Name changed" };
            var tc2 = new TrainingCourse { Id = 472, Name = "Name also changed" };
            var tc3 = new TrainingCourse { Id = 3047, Name = "Name also changed" };

            Assert.False(trainingCourseBusiness.Update(tc1));
            Assert.False(trainingCourseBusiness.Update(tc2));
            Assert.False(trainingCourseBusiness.Update(tc3));
        }

        [Fact]
        public void Delete_Found_TrainingCourse_Should_Return_True()
        {
            var trainingCourseBusiness = InitializeTrainingCourseBusiness(true);

            Assert.True(trainingCourseBusiness.Delete(1));
            Assert.True(trainingCourseBusiness.Delete(2));
            Assert.True(trainingCourseBusiness.Delete(3));
        }

        [Fact]
        public void Delete_Not_Found_TrainingCourse_Should_Return_False()
        {
            var trainingCourseBusiness = InitializeTrainingCourseBusiness(true);

            Assert.False(trainingCourseBusiness.Delete(12));
            Assert.False(trainingCourseBusiness.Delete(17));
            Assert.False(trainingCourseBusiness.Delete(1458));
        }






        private static DefaultContext FakeContext()
        {
            var contextOptions = new DbContextOptionsBuilder<DefaultContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new DefaultContext(contextOptions);
        }

        private TrainingCourseBusiness InitializeTrainingCourseBusiness(bool addData)
        {
            var defaultContext = FakeContext();

            if (addData)
                AddTrainingCourses(defaultContext);

            var trainingCourseBusiness = new TrainingCourseBusiness(defaultContext);
            return trainingCourseBusiness;
        }


        /// <summary>
        /// dbContext qui doit retourner une erreur en cas d'ajout de données
        /// </summary>
        /// <returns></returns>
        private static DefaultContext FalseFakeContext()
        {
            //TODO : faire une base de données qui va planter les opérations crud ?
            var contextOptions = new DbContextOptionsBuilder<DefaultContext>()
                .UseSqlite("D:\\temp\\temp.db")
                .Options;
            return new DefaultContext(contextOptions);
        }

        private void AddTrainingCourses(DefaultContext defaultContext)
        {
            defaultContext.TrainingCourses.Add(_tc1);
            defaultContext.TrainingCourses.Add(_tc2);
            defaultContext.TrainingCourses.Add(_tc3);
            defaultContext.SaveChanges();
            defaultContext.Entry(_tc1).State = EntityState.Detached;
            defaultContext.Entry(_tc2).State = EntityState.Detached;
            defaultContext.Entry(_tc3).State = EntityState.Detached;
        }
    }
}

