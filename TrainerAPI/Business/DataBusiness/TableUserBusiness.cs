using Data;
using Data.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace TrainerAPI.Business
{
    /// <summary>
    /// Classe de gestion des utilisateurs (TableUser)
    /// </summary>
    public class TableUserBusiness : ITableUserBusiness
    {
        private readonly DefaultContext _defaultContext;

        public TableUserBusiness(DefaultContext defaultContext)
        {
            _defaultContext = defaultContext;
        }

        public TableUser Create(TableUser tableUser)
        {
            var addResult = _defaultContext.Users.Add(tableUser);
            var saveResult = _defaultContext.SaveChanges();
            return saveResult == 1 ? addResult.Entity : null;
        }

        public TableUser Read(int id)
        {
            var user = _defaultContext.Users.AsNoTracking().FirstOrDefault(x => x.Id == id);
            return user;
        }

        public bool Update(TableUser tableUserToUpdate)
        {
            var user = _defaultContext.Users.AsNoTracking().FirstOrDefault(x => x.Id == tableUserToUpdate.Id);
            if (user == null)
                return false;

            tableUserToUpdate.ConcurrencyStamp = user.ConcurrencyStamp;

            _defaultContext.Users.Update(tableUserToUpdate);
            var saveResult = _defaultContext.SaveChanges();
            return saveResult == 1;
        }

        public bool Delete(int id)
        {
            var user = _defaultContext.Users.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if (user == null)
                return false;

            _defaultContext.Users.Remove(user);
            var saveResult = _defaultContext.SaveChanges();
            return saveResult == 1;
        }

        public IEnumerable<TableUser> List()
        {
            var users = _defaultContext.Users.AsNoTracking();
            return users;
        }

        internal List<TableUser> TrainersList(TableTrainingCourse tableTrainingCourse)
        {
            List<TableUser> users = (from tct in _defaultContext.TrainingCourseTrainers
                                     join u in _defaultContext.Users on tct.TrainerId equals u.Id
                                     where tct.TrainingCourseId == tableTrainingCourse.Id
                                     select u).ToList();

            return users;

        }

        internal List<TableUser> StudentsList(TableTrainingCourse tableTrainingCourse)
        {
            List<TableUser> users = (from tct in _defaultContext.TrainingCourseStudents
                                     join u in _defaultContext.Users on tct.StudentId equals u.Id
                                     where tct.TrainingCourseId == tableTrainingCourse.Id
                                     select u).ToList();

            return users;
        }

        internal TableUser Owner(TableTrainingCourse tableTrainingCourse)
        {
            TableUser tableUser = (from u in _defaultContext.Users
                                   where u.Id == tableTrainingCourse.OwnerId
                                   select u).FirstOrDefault();

            return tableUser;
        }
    }
}
