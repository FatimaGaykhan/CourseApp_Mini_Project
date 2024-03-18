using System;
using Domain.Models;

namespace Service.Services.Interfaces
{
	public interface IStudentService
	{
        void Create(Student data);
        void Delete(int? id);
        void DeleteAll(int?id);
        void Edit(Student data);
        Student GetById(int? id);
        List<Student> GetAll();
        List<Student> GetAllStudentsByGroupId(int? id);
        List<Student> GetStudentsByAge(int? age);
        List<Student> SearchByNameOrSurname(string text);
    }
}

