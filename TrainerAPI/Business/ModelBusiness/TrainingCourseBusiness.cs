using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using Data.Model;
using TrainerAPI.Business.Model;

namespace TrainerAPI.Business
{
    /// <summary>
    /// Classe métier de la gestion des formations (TrainingCourse)
    /// </summary>
    public class TrainingCourseBusiness : ITrainingCourseBusiness
    {
        private readonly ITableTrainingCourseBusiness _tableTrainingCourseBusiness;
        private readonly ITableQuestBusiness _tableQuestBusiness;


        private readonly TableUserBusiness _tableUserBusiness;
        private readonly TableTrainingCourseStudentBusiness _tableTrainingCourseStudentBusiness;
        private readonly TableTrainingCourseTrainerBusiness _tableTrainingCourseTrainerBusiness;

        public TrainingCourseBusiness(DefaultContext defaultContext)
        {
            _tableTrainingCourseBusiness = new TableTrainingCourseBusiness(defaultContext);
            _tableQuestBusiness = new TableQuestBusiness(defaultContext);


            _tableUserBusiness = new TableUserBusiness(defaultContext);
            _tableTrainingCourseStudentBusiness = new TableTrainingCourseStudentBusiness(defaultContext);
            _tableTrainingCourseTrainerBusiness = new TableTrainingCourseTrainerBusiness(defaultContext);
        }

        public TrainingCourse Create(TrainingCourse trainingCourse)
        {
            var trainingCourseCreated = new TrainingCourse
            {
                Name = trainingCourse.Name,
                Quests = new List<Quest>(),
                Students = new List<User>(),
                Trainers = new List<User>()
            };
            var owner = trainingCourse.Owner.Id != 0 ? _tableUserBusiness.Read(trainingCourse.Owner.Id) : _tableUserBusiness.Create(new TableUser { UserName = trainingCourse.Owner.LastName });
            // todo : si id renseigné, prévoir le cas update ? 
            trainingCourse.Owner.Id = owner.Id;

            var tableTrainingCourse = _tableTrainingCourseBusiness.Create(new TableTrainingCourse { Name = trainingCourse.Name, OwnerId = trainingCourse.Owner.Id });
            trainingCourseCreated.Id = tableTrainingCourse.Id;
            foreach (var quest in trainingCourse.Quests)
            {
                var tableQuest = _tableQuestBusiness.Create(new TableQuest { TrainingCourseId = tableTrainingCourse.Id, Name = quest.Name, Number = quest.Number });
                quest.Id = tableQuest.Id;
                trainingCourseCreated.Quests.Add(quest);
            }

            foreach (var student in trainingCourse.Students)
            {
                var tableStudent = _tableUserBusiness.Create(new TableUser { UserName = student.LastName });
                var tableTrainingCourseStudent = _tableTrainingCourseStudentBusiness.Create(new TableTrainingCourseStudent { TrainingCourseId = tableTrainingCourse.Id, StudentId = tableStudent.Id });
                trainingCourseCreated.Students.Add(new User(tableStudent));
            }

            foreach (var trainer in trainingCourse.Trainers)
            {
                var tableTrainer = _tableUserBusiness.Create(new TableUser { UserName = trainer.LastName });
                var tableTrainingCourseTrainer = _tableTrainingCourseTrainerBusiness.Create(new TableTrainingCourseTrainer { TrainingCourseId = tableTrainingCourse.Id, TrainerId = tableTrainer.Id });
                trainingCourseCreated.Trainers.Add(new User(tableTrainer));
            }

            trainingCourseCreated.Owner = new User(owner);

            return trainingCourseCreated;
        }

        public TrainingCourse Read(int id)
        {
            var tableTrainingCourse = _tableTrainingCourseBusiness.Read(id);

            if (tableTrainingCourse == null)
                return null;

            var tableOwner = _tableUserBusiness.Owner(tableTrainingCourse);
            var tableTrainers = _tableUserBusiness.TrainersList(tableTrainingCourse);
            var tableStudents = _tableUserBusiness.StudentsList(tableTrainingCourse);
            var tableQuests = _tableQuestBusiness.QuestsList(tableTrainingCourse);

            var owner = new User { Id = tableOwner.Id, LastName = tableOwner.UserName };
            var trainers = tableTrainers.Select(trainer => new User { Id = trainer.Id, LastName = trainer.UserName }).ToList();
            var students = tableStudents.Select(student => new User { Id = student.Id, LastName = student.UserName }).ToList();
            var quests = tableQuests.Select(quest => new Quest { Id = quest.Id, Name = quest.Name }).ToList();

            var trainingCourseModel = new TrainingCourse
            {
                Id = tableTrainingCourse.Id,
                Name = tableTrainingCourse.Name,
                Owner = owner,
                Quests = quests,
                Trainers = trainers,
                Students = students,
            };

            return trainingCourseModel;
        }

        public IEnumerable<TrainingCourse> List()
        {
            return _tableTrainingCourseBusiness.List().Select(tableTrainingCourse => Read(tableTrainingCourse.Id)).ToList();
        }

        public bool Update(TrainingCourse trainingCourseToUpdate)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
