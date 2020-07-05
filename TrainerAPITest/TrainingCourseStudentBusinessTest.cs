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
    /// TUs TrainingCourseStudentBusiness
    /// </summary>
    public class TrainingCourseStudentStudentBusinessTest
    {
        private readonly TrainingCourseStudent _tc1s1 = new TrainingCourseStudent { TrainingCourseId = 1, StudentId = 1 };
        private readonly TrainingCourseStudent _tc1s2 = new TrainingCourseStudent { TrainingCourseId = 1, StudentId = 2 };
        private readonly TrainingCourseStudent _tc2s3 = new TrainingCourseStudent { TrainingCourseId = 2, StudentId = 3 };

        [Fact]
        public void Create_TrainingCourseStudent_Should_Return_TrainingCourseStudent_With_Id_Grow_Up()
        {
            var trainingCourseBusiness = InitializeTrainingCourseStudentBusiness(false);

            var trainingCourseReturned1 = trainingCourseBusiness.Create(_tc1s1);
            var trainingCourseReturned2 = trainingCourseBusiness.Create(_tc1s2);
            var trainingCourseReturned3 = trainingCourseBusiness.Create(_tc2s3);

            Assert.Equal(1, trainingCourseReturned1.Id);
            Assert.Equal(2, trainingCourseReturned2.Id);
            Assert.Equal(3, trainingCourseReturned3.Id);
            Assert.Equal(JsonConvert.SerializeObject(_tc1s1), JsonConvert.SerializeObject(trainingCourseReturned1));
            Assert.Equal(JsonConvert.SerializeObject(_tc1s2), JsonConvert.SerializeObject(trainingCourseReturned2));
            Assert.Equal(JsonConvert.SerializeObject(_tc2s3), JsonConvert.SerializeObject(trainingCourseReturned3));
        }

        [Fact(Skip = "Je n'ai pas trouvé de moyen de faire planter l'ajout en base")]
        // CodeReview : comment faire pour tester l'échec de l'ajout en base et donc le retour null de la méthode Create ?
        public void Create_TrainingCourseStudent_Should_Return_Null_When_Create_Failed()
        {
            DefaultContext defaultContext = FalseFakeContext();
            TrainingCourseStudentBusiness trainingCourseBusiness = new TrainingCourseStudentBusiness(defaultContext);

            Assert.Null(trainingCourseBusiness.Create(_tc1s1));
        }

        [Fact]
        public void Read_All_TrainingCourseStudent_Should_Return_TrainingCourseStudents()
        {
            var trainingCourseBusiness = InitializeTrainingCourseStudentBusiness(true);

            Assert.Equal(JsonConvert.SerializeObject(new List<TrainingCourseStudent> { _tc1s1, _tc1s2, _tc2s3 }), JsonConvert.SerializeObject(trainingCourseBusiness.List()));
        }

        [Fact]
        public void Read_Found_TrainingCourseStudent_Should_Return_TrainingCourseStudent()
        {
            var trainingCourseBusiness = InitializeTrainingCourseStudentBusiness(true);

            Assert.Equal(JsonConvert.SerializeObject(_tc1s1), JsonConvert.SerializeObject(trainingCourseBusiness.Read(1)));
            Assert.Equal(JsonConvert.SerializeObject(_tc1s2), JsonConvert.SerializeObject(trainingCourseBusiness.Read(2)));
            Assert.Equal(JsonConvert.SerializeObject(_tc2s3), JsonConvert.SerializeObject(trainingCourseBusiness.Read(3)));
        }

        [Fact]
        public void Read_Not_Found_TrainingCourseStudents_ShouldReturn_Null()
        {
            var trainingCourseBusiness = InitializeTrainingCourseStudentBusiness(true);

            Assert.Null(trainingCourseBusiness.Read(4));
            Assert.Null(trainingCourseBusiness.Read(18));
            Assert.Null(trainingCourseBusiness.Read(4789));
        }

        [Fact]
        public void Read_TrainingCourseStudents_When_db_Empty_Should_Return_Null()
        {
            var trainingCourseBusiness = InitializeTrainingCourseStudentBusiness(false);

            Assert.Null(trainingCourseBusiness.Read(1));
            Assert.Null(trainingCourseBusiness.Read(2));
            Assert.Null(trainingCourseBusiness.Read(3));
            Assert.Null(trainingCourseBusiness.Read(145));
        }

        [Fact]
        public void Update_Found_TrainingCourseStudent_Should_Return_True()
        {
            var trainingCourseBusiness = InitializeTrainingCourseStudentBusiness(true);

            var tcs1 = new TrainingCourseStudent { Id = 1, TrainingCourseId = 1, StudentXp = 10 };
            var tcs2 = new TrainingCourseStudent { Id = 2, TrainingCourseId = 1, StudentXp = 28 };
            var tcs3 = new TrainingCourseStudent { Id = 3, TrainingCourseId = 1, StudentXp = 47 };

            Assert.True(trainingCourseBusiness.Update(tcs1));
            Assert.True(trainingCourseBusiness.Update(tcs2));
            Assert.True(trainingCourseBusiness.Update(tcs3));
        }

        [Fact]
        public void Update_Not_Found_TrainingCourseStudent_Should_Return_False()
        {
            var trainingCourseBusiness = InitializeTrainingCourseStudentBusiness(true);

            var tcs1 = new TrainingCourseStudent { Id = 178, TrainingCourseId = 1, StudentXp = 10 };
            var tcs2 = new TrainingCourseStudent { Id = 4785, TrainingCourseId = 187, StudentXp = 28 };
            var tcs3 = new TrainingCourseStudent { Id = 32568, TrainingCourseId = 1, StudentXp = 47 };

            Assert.False(trainingCourseBusiness.Update(tcs1));
            Assert.False(trainingCourseBusiness.Update(tcs2));
            Assert.False(trainingCourseBusiness.Update(tcs3));
        }

        [Fact]
        public void Delete_Found_TrainingCourseStudent_Should_Return_True()
        {
            var trainingCourseBusiness = InitializeTrainingCourseStudentBusiness(true);

            Assert.True(trainingCourseBusiness.Delete(1));
            Assert.True(trainingCourseBusiness.Delete(2));
            Assert.True(trainingCourseBusiness.Delete(3));
        }

        [Fact]
        public void Delete_Not_Found_TrainingCourseStudent_Should_Return_False()
        {
            var trainingCourseBusiness = InitializeTrainingCourseStudentBusiness(true);

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

        private TrainingCourseStudentBusiness InitializeTrainingCourseStudentBusiness(bool addData)
        {
            var defaultContext = FakeContext();

            if (addData)
                AddTrainingCourseStudents(defaultContext);

            var trainingCourseStudentBusiness = new TrainingCourseStudentBusiness(defaultContext);
            return trainingCourseStudentBusiness;
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

        private void AddTrainingCourseStudents(DefaultContext defaultContext)
        {
            defaultContext.TrainingCourseStudents.Add(_tc1s1);
            defaultContext.TrainingCourseStudents.Add(_tc1s2);
            defaultContext.TrainingCourseStudents.Add(_tc2s3);
            defaultContext.SaveChanges();
            defaultContext.Entry(_tc1s1).State = EntityState.Detached;
            defaultContext.Entry(_tc1s2).State = EntityState.Detached;
            defaultContext.Entry(_tc2s3).State = EntityState.Detached;
        }
    }
}

