using Data;
using Data.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection;
using TrainerAPI.Controllers;
using Xunit;

namespace TrainerAPITest
{
    /// <summary>
    /// TUs POC du controller POC UsersController
    /// </summary>
    public class TrainerAPITest
    {
        private readonly TrainingCourse _tc1 = new TrainingCourse { Name = "Angular TC" };
        private readonly TrainingCourse _tc2 = new TrainingCourse { Name = "C# TC" };
        private readonly TrainingCourse _tc3 = new TrainingCourse { Name = ".net core TC" };
        private List<TrainingCourse> Tcs { get; }

        /// <summary>
        /// préparation de 3 TrainingCourse en mémoire
        /// </summary>
        public TrainerAPITest()
        {
            Tcs = new List<TrainingCourse> { _tc1, _tc2, _tc3 };
        }

        private static DefaultContext Context(string DbName)
        {
            var contextOptions = new DbContextOptionsBuilder<DefaultContext>()
                .UseInMemoryDatabase(databaseName: DbName)
                .Options;
            DefaultContext defaultContext = new DefaultContext(contextOptions);
            return defaultContext;
        }

        [Fact]
        public void DetailsWith0()
        {
            string DbName = MethodBase.GetCurrentMethod().Name;
            DefaultContext defaultContext = Context(DbName);

            TrainingCourseController trainingCourseController = new TrainingCourseController(defaultContext);
            for (int i = 1; i < 1000; i++)
            {
                JsonResult result = trainingCourseController.Details(i).Result;
                Assert.Null(result.Value);
            }
        }


        [Fact]
        public void DetailsWith3()
        {
            string DbName = MethodBase.GetCurrentMethod().Name;
            DefaultContext defaultContext = Context(DbName);
            defaultContext.TrainingCourses.AddRange(Tcs);
            defaultContext.SaveChanges();
            TrainingCourseController trainingCourseController = new TrainingCourseController(defaultContext);

            List<TrainingCourse> result = new List<TrainingCourse>();
            for (int i = 0; i < 10; i++)
            {
                result.Add((TrainingCourse)trainingCourseController.Details(i + 1).Result.Value);
            }
            Assert.Equal(_tc1.Name, result[0].Name);
            Assert.Equal(_tc2.Name, result[1].Name);
            Assert.Equal(_tc3.Name, result[2].Name);
            for (int i = 3; i < 10; i++)
            {
                Assert.Null(result[i]);
            }
        }

        [Fact]
        public void IndexWith0()
        {
            string DbName = MethodBase.GetCurrentMethod().Name;
            DefaultContext defaultContext = Context(DbName);

            TrainingCourseController trainingCourseController = new TrainingCourseController(defaultContext);

            JsonResult result = trainingCourseController.Index().Result;
            Assert.Equal(new List<TrainingCourse>(), result.Value);
        }

        [Fact]
        public void IndexWith3()
        {
            string DbName = MethodBase.GetCurrentMethod().Name;
            DefaultContext defaultContext = Context(DbName);
            defaultContext.TrainingCourses.AddRange(Tcs);
            defaultContext.SaveChanges();
            TrainingCourseController trainingCourseController = new TrainingCourseController(defaultContext);

            List<TrainingCourse> trainingCoursesReturnedByIndex = (List<TrainingCourse>)trainingCourseController.Index().Result.Value;
            Assert.Equal(3, trainingCoursesReturnedByIndex.Count);
            foreach (var tc in Tcs)
            {
                Assert.Contains(tc, trainingCoursesReturnedByIndex);
            }
        }

        [Fact]
        public void DeleteWith3()
        {
            string DbName = MethodBase.GetCurrentMethod().Name;
            DefaultContext defaultContext = Context(DbName);
            defaultContext.TrainingCourses.AddRange(Tcs);
            defaultContext.SaveChanges();
            TrainingCourseController trainingCourseController = new TrainingCourseController(defaultContext);

            List<TrainingCourse> trainingCoursesReturnedByIndex = (List<TrainingCourse>)trainingCourseController.Index().Result.Value;
            List<int> trainingCoursesId = new List<int>();
            foreach (var trainingCourse in trainingCoursesReturnedByIndex)
            {
                trainingCoursesId.Add(trainingCourse.Id);
            }

            for (int i = 0; i < trainingCoursesId.Count; i++)
            {
                List<TrainingCourse> trainingCoursesReturnedAfterDelete = (List<TrainingCourse>)trainingCourseController.Delete(trainingCoursesId[i]).Result.Value;
                Assert.Equal(trainingCoursesId.Count - i - 1, trainingCoursesReturnedAfterDelete.Count);
                Assert.DoesNotContain(trainingCoursesReturnedByIndex[i], trainingCoursesReturnedAfterDelete);
            }
        }
    }
}

