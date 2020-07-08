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
    public class TrainingCoursesControllerTest
    {
        private readonly TrainingCourse _tc1 = new TrainingCourse { Name = "Angular" };
        private readonly TrainingCourse _tc2 = new TrainingCourse { Name = "C#" };
        private readonly TrainingCourse _tc3 = new TrainingCourse { Name = ".net core" };

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
            defaultContext.TrainingCourses.Add(_tc1);
            defaultContext.TrainingCourses.Add(_tc2);
            defaultContext.TrainingCourses.Add(_tc3);
            defaultContext.SaveChanges();
            defaultContext.Entry(_tc1).State = EntityState.Detached;
            defaultContext.Entry(_tc2).State = EntityState.Detached;
            defaultContext.Entry(_tc3).State = EntityState.Detached;
        }

        private TrainingCoursesController InitializeTrainingCourseController(bool addData)
        {
            var defaultContext = FakeContext();

            var business = new TrainingCourseBusiness(defaultContext);

            if (addData)
                AddTrainingCourses(defaultContext);

            var trainingCoursesController = new TrainingCoursesController(business);
            return trainingCoursesController;
        }

        [Fact]
        public void Create_TrainingCourse_Should_Return_201()
        {
            var trainingCourseController = InitializeTrainingCourseController(false);

            var result = (CreatedAtActionResult)trainingCourseController.Create(_tc1);

            Assert.Equal(201, result.StatusCode);
            Assert.NotNull((TrainingCourse)result.Value);
        }

        [Fact]
        public void Read_All_TrainingCourses_Should_Return_All_TrainingCourses()
        {
            var trainingCourseController = InitializeTrainingCourseController(true);

            var actual = JsonConvert.SerializeObject(((ObjectResult)trainingCourseController.Read().Result).Value);
            var expected = JsonConvert.SerializeObject(new List<TrainingCourse> { _tc1, _tc2, _tc3 });

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Read_Found_TrainingCourse_Should_Return_Good_TrainingCourse()
        {
            var trainingCourseController = InitializeTrainingCourseController(true);

            Assert.Equal(JsonConvert.SerializeObject(_tc1), JsonConvert.SerializeObject(trainingCourseController.Read(1).Value));
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
            TrainingCourse tc1 = new TrainingCourse { Id = 1, Name = "OtherName1" };
            TrainingCourse tc2 = new TrainingCourse { Id = 2, Name = "OtherName2" };
            TrainingCourse tc3 = new TrainingCourse { Id = 3, Name = "OtherName3" };

            Assert.Equal(204, trainingCourseController.Update(tc1).StatusCode);
            Assert.Equal(204, trainingCourseController.Update(tc2).StatusCode);
            Assert.Equal(204, trainingCourseController.Update(tc3).StatusCode);
        }

        [Fact]
        public void Update_NotFound_TrainingCourse_Should_Return_Status404()
        {
            var trainingCourseController = InitializeTrainingCourseController(true);
            TrainingCourse tc1 = new TrainingCourse { Id = 4, Name = "TryChangeName1" };
            TrainingCourse tc2 = new TrainingCourse { Id = 18, Name = "TryChangeName2" };
            TrainingCourse tc3 = new TrainingCourse { Id = 694, Name = "TryChangeName3" };

            Assert.Equal(404, trainingCourseController.Update(tc1).StatusCode);
            Assert.Equal(404, trainingCourseController.Update(tc2).StatusCode);
            Assert.Equal(404, trainingCourseController.Update(tc3).StatusCode);
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