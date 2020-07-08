using Data;
using Data.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TrainerAPI.Business;
using TrainerAPI.Controllers;
using Xunit;

namespace TrainerAPITest
{
    public class TrainingCourseStudentsControllerTest
    {
        private readonly TrainingCourseStudent _tcs1 = new TrainingCourseStudent { TrainingCourseId = 1, StudentId = 1 };
        private readonly TrainingCourseStudent _tcs2 = new TrainingCourseStudent { TrainingCourseId = 1, StudentId = 2 };
        private readonly TrainingCourseStudent _tcs3 = new TrainingCourseStudent { TrainingCourseId = 2, StudentId = 3 };

        private static DefaultContext FakeContext()
        {
            var contextOptions = new DbContextOptionsBuilder<DefaultContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            DefaultContext defaultContext = new DefaultContext(contextOptions);
            return defaultContext;
        }

        private void AddTrainingCourses(DefaultContext defaultContext)
        {
            defaultContext.TrainingCourseStudents.Add(_tcs1);
            defaultContext.TrainingCourseStudents.Add(_tcs2);
            defaultContext.TrainingCourseStudents.Add(_tcs3);
            defaultContext.SaveChanges();
            defaultContext.Entry(_tcs1).State = EntityState.Detached;
            defaultContext.Entry(_tcs2).State = EntityState.Detached;
            defaultContext.Entry(_tcs3).State = EntityState.Detached;
        }

        private TrainingCourseStudentsController InitializeTrainingCourseController(bool addData)
        {
            var defaultContext = FakeContext();

            var business = new TrainingCourseStudentBusiness(defaultContext);

            if (addData)
                AddTrainingCourses(defaultContext);

            var trainingCoursesController = new TrainingCourseStudentsController(business);
            return trainingCoursesController;
        }

        [Fact]
        public void Create_TrainingCourse_Should_Return_201()
        {
            var trainingCourseController = InitializeTrainingCourseController(false);

            var result = (CreatedAtActionResult)trainingCourseController.Create(_tcs1);

            Assert.Equal(201, result.StatusCode);
            Assert.NotNull((TrainingCourseStudent)result.Value);
        }

        [Fact]
        public void Read_All_TrainingCourses_Should_Return_All_TrainingCourses()
        {
            var trainingCourseController = InitializeTrainingCourseController(true);

            var actual = JsonConvert.SerializeObject(((ObjectResult)trainingCourseController.Read().Result).Value);
            var expected = JsonConvert.SerializeObject(new List<TrainingCourseStudent> { _tcs1, _tcs2, _tcs3 });

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Read_Found_TrainingCourse_Should_Return_Good_TrainingCourse()
        {
            var trainingCourseController = InitializeTrainingCourseController(true);

            Assert.Equal(JsonConvert.SerializeObject(_tcs1), JsonConvert.SerializeObject(trainingCourseController.Read(1).Value));
        }

        [Fact]
        public void Read_NotFound_TrainingCourse_Should_Return_Status404()
        {
            var trainingCourseController = InitializeTrainingCourseController(true);

            Assert.Equal(404, ((StatusCodeResult)trainingCourseController.Read(4).Result).StatusCode);
            Assert.Equal(404, ((StatusCodeResult)trainingCourseController.Read(17).Result).StatusCode);
            Assert.Equal(404, ((StatusCodeResult)trainingCourseController.Read(193).Result).StatusCode);
        }

        [Fact]
        public void Update_Found_TrainingCourses_Should_Return_Status204()
        {
            var trainingCourseController = InitializeTrainingCourseController(true);
            TrainingCourseStudent tcs1 = new TrainingCourseStudent { Id = 1, StudentId = 7 };
            TrainingCourseStudent tcs2 = new TrainingCourseStudent { Id = 2, StudentId = 47 };
            TrainingCourseStudent tcs3 = new TrainingCourseStudent { Id = 3, StudentId = 587 };

            Assert.Equal(204, trainingCourseController.Update(tcs1).StatusCode);
            Assert.Equal(204, trainingCourseController.Update(tcs2).StatusCode);
            Assert.Equal(204, trainingCourseController.Update(tcs3).StatusCode);
        }

        [Fact]
        public void Update_NotFound_TrainingCourse_Should_Return_Status404()
        {
            var trainingCourseController = InitializeTrainingCourseController(true);
            TrainingCourseStudent tcs1 = new TrainingCourseStudent { Id = 4, StudentId = 7 };
            TrainingCourseStudent tcs2 = new TrainingCourseStudent { Id = 745, StudentId = 47 };
            TrainingCourseStudent tcs3 = new TrainingCourseStudent { Id = 158785, StudentId = 587 };

            Assert.Equal(404, trainingCourseController.Update(tcs1).StatusCode);
            Assert.Equal(404, trainingCourseController.Update(tcs2).StatusCode);
            Assert.Equal(404, trainingCourseController.Update(tcs3).StatusCode);
        }

        [Fact]
        public void Delete_Found_TrainingCourse_Should_Return_Status204()
        {
            var trainingCourseController = InitializeTrainingCourseController(true);

            Assert.Equal(204, trainingCourseController.Delete(1).StatusCode);
            Assert.Equal(204, trainingCourseController.Delete(2).StatusCode);
            Assert.Equal(204, trainingCourseController.Delete(3).StatusCode);
        }

        [Fact]
        public void Delete_NotFound_TrainingCourse_Should_Return_Status404()
        {
            var trainingCourseController = InitializeTrainingCourseController(true);

            Assert.Equal(404, trainingCourseController.Delete(18).StatusCode);
            Assert.Equal(404, trainingCourseController.Delete(147).StatusCode);
            Assert.Equal(404, trainingCourseController.Delete(2478).StatusCode);
        }
    }
}