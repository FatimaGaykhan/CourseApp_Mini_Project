using System;
using Domain.Models;

namespace Repository.Repositories.Interfaces
{
	public interface IStudentRepository:IBaseRepository<Student>
	{
        //List<Student> GetStudentsByAge(Func<Student,bool> predicate);
        //List<Student> GetAllStudentsByGroupId(Func<Student,bool> predicate);
        //List<Student> SearchByNameOrSurname(Func<Student, bool> predicate);
    }
}

