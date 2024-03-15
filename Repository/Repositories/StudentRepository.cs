using System;
using Domain.Models;
using Repository.Data;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
    public class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        //public List<Student> GetAllStudentsByGroupId(Func<Student,bool> predicate)
        //{
        //    return AppDbContext<Student>.datas.Where(predicate).ToList();
        //}

        //public List<Student> GetStudentsByAge(Func<Student,bool> predicate)
        //{
        //    return AppDbContext<Student>.datas.Where(predicate).ToList();
        //}

        //public List<Student> SearchByNameOrSurname(Func<Student,bool> predicate)
        //{
        //    return AppDbContext<Student>.datas.Where(predicate).ToList();
        //}
    }
}

