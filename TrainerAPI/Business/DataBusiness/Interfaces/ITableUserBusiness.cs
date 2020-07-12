using Data.Model;
using System.Collections.Generic;

namespace TrainerAPI.Business
{
    public interface ITableUserBusiness
    {
        TableUser Create(TableUser tableUser);
        TableUser Read(int id);
        IEnumerable<TableUser> List();
        bool Update(TableUser tableUserToUpdate);
        bool Delete(int id);
    }
}
