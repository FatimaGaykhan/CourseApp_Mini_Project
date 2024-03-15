using System;
using System.Linq;
using Domain.Models;
using Repository.Data;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
    public class GroupRepository : BaseRepository<Group>, IGroupRepository
    {
        //public List<Group> GetAllGroupsByRoom(Func<Group,bool> predicate)
        //{
        //    return AppDbContext<Group>.datas.Where(predicate).ToList();
        //}

        //public List<Group> GetAllGroupsByTeacher(Func<Group,bool> predicate)
        //{
        //    return AppDbContext<Group>.datas.Where(predicate).ToList();
        //}

        //public List<Group> SearchGroupByName(Func<Group,bool> predicate)
        //{
        //    return AppDbContext<Group>.datas.Where(predicate).ToList();
        //}
    }
}

