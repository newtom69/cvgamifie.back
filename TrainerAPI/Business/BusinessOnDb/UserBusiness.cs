using Data;
using Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TrainerAPI.Business
{
    /// <summary>
    /// Classe de gestion des utilisateurs (User)
    /// </summary>
    public class UserBusiness : IUserBusiness
    {
        private readonly DefaultContext _defaultContext;

        public UserBusiness(DefaultContext defaultContext)
        {
            _defaultContext = defaultContext;
        }

        public User Create(User user)
        {
            var addResult = _defaultContext.Users.Add(user);
            var saveResult = _defaultContext.SaveChanges();
            return saveResult == 1 ? addResult.Entity : null;
        }

        public User Read(int id)
        {
            var user = _defaultContext.Users.AsNoTracking().FirstOrDefault(x => x.Id == id);
            return user;
        }

        public bool Update(User userToUpdate)
        {
            var user = _defaultContext.Users.AsNoTracking().FirstOrDefault(x => x.Id == userToUpdate.Id);
            if (user == null)
                return false;

            userToUpdate.ConcurrencyStamp = user.ConcurrencyStamp;

            _defaultContext.Users.Update(userToUpdate);
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

        public IEnumerable<User> List()
        {
            var users = _defaultContext.Users.AsNoTracking();
            return users;
        }

        internal List<User> TrainersList(TrainingCourse trainingCourse)
        {
            List<User> users = (from tct in _defaultContext.TrainingCourseTrainers
                                join u in _defaultContext.Users on tct.TrainerId equals u.Id
                                where tct.TrainingCourseId == trainingCourse.Id
                                select u).ToList();

            return users;

        }

        internal List<User> StudentsList(TrainingCourse trainingCourse)
        {
            List<User> users = (from tct in _defaultContext.TrainingCourseStudents
                                join u in _defaultContext.Users on tct.StudentId equals u.Id
                                where tct.TrainingCourseId == trainingCourse.Id
                                select u).ToList();

            return users;
        }

        internal User Owner(TrainingCourse trainingCourse)
        {
            User user = (from u in _defaultContext.Users
                         where u.Id == trainingCourse.OwnerId
                         select u).FirstOrDefault();

            return user;
        }
    }
}
