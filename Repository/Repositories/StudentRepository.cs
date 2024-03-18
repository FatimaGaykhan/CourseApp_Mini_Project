using System;
using Domain.Models;
using Repository.Data;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
    public class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
       public void DeleteAll(int? id)
        {
            AppDbContext<Student>.datas.RemoveAll(m => m.Group.Id == id);
        }
    }
}

