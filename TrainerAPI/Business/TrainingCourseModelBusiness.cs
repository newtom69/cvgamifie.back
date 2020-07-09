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
    public class TrainingCourseModelBusiness : ITrainingCourseModelBusiness
    {
        private readonly DefaultContext _defaultContext;
        private readonly TrainingCourseBusiness _trainingCourseBusiness;
        private readonly UserBusiness _userBusiness;
        private readonly QuestBusiness _questBusiness;

        public TrainingCourseModelBusiness(DefaultContext defaultContext)
        {
            _defaultContext = defaultContext;
            _trainingCourseBusiness = new TrainingCourseBusiness(defaultContext);
            _userBusiness = new UserBusiness(defaultContext);
            _questBusiness = new QuestBusiness(defaultContext);
        }

        public TrainingCourseModel Create(TrainingCourseModel trainingCourse)
        {
            throw new NotImplementedException();
        }

        public TrainingCourseModel Read(int id)
        {
            var trainingCourse = _trainingCourseBusiness.Read(id);

            if (trainingCourse == null)
                return null;

            var owner = _userBusiness.Owner(trainingCourse);
            var trainers = _userBusiness.TrainersList(trainingCourse);
            var students = _userBusiness.StudentsList(trainingCourse);
            var quests = _questBusiness.QuestsList(trainingCourse);

            var ownerModel = new UserModel {Id = owner.Id, LastName = owner.UserName};
            var trainersModel = trainers.Select(trainer => new UserModel {Id = trainer.Id, LastName = trainer.UserName}).ToList();
            var studentsModel = students.Select(student => new UserModel {Id = student.Id, LastName = student.UserName}).ToList();
            var questsModel = quests.Select(quest => new QuestModel {Id = quest.Id, Name = quest.Name}).ToList();

            var trainingCourseModel = new TrainingCourseModel
            {
                Id = trainingCourse.Id,
                Name = trainingCourse.Name,
                Owner = ownerModel,
                Quests = questsModel,
                Trainers = trainersModel,
                Students = studentsModel,
            };

            return trainingCourseModel;
        }

        public IEnumerable<TrainingCourseModel> List()
        {
            throw new NotImplementedException();
        }

        public bool Update(TrainingCourseModel trainingCourseToUpdate)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
