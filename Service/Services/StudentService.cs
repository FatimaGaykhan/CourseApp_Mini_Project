using System;
using Domain.Models;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Service.Services.Helpers.Constants;
using Service.Services.Helpers.Exceptions;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepo;
        private int count = 1;

        public StudentService()
        {
            _studentRepo = new StudentRepository();
        }
        public void Create(Student data)
        {
            if (data is null) throw new ArgumentNullException();
            data.Id = count;
            _studentRepo.Create(data);
            count++;
        }

        public void Delete(int? id)
        {
            if (id is null) throw new ArgumentNullException();
            Student student = _studentRepo.GetById((int)id);
            if (student is null) throw new NotFoundException(ResponseMessages.DataNotFound);
           _studentRepo.Delete(student);
        }
        public void DeleteAll(int? id)
        {
            if (id is null) throw new ArgumentNullException();
            List<Student> students = _studentRepo.GetAllWithExpression(m => m.Group.Id == id);
            if (students is null) throw new NotFoundException(ResponseMessages.DataNotFound);
            _studentRepo.DeleteAll(id);
        }


        public void Edit(Student data)
        {
            if (data is null) throw new ArgumentNullException();
            var response = _studentRepo.GetAllWithExpression(m => m.Id == data.Id);
            if (response != null)
            {
                foreach (var item in response)
                {
                    item.Name = data.Name;
                    item.Surname = data.Surname;
                    item.Age = data.Age;
                    item.Group.Name = data.Group.Name;
                }
            }
            else
            {
                throw new NotFoundException(ResponseMessages.DataNotFound);
            }
        }

        public List<Student> GetAll()
        {
            return _studentRepo.GetAll();
        }

        public List<Student> GetAllStudentsByGroupId(int? id)
        {
            if (id is null) throw new ArgumentNullException();
            return _studentRepo.GetAllWithExpression(m => m.Group.Id == id);

        }

        public Student GetById(int? id)
        {
            if (id is null) throw new ArgumentNullException();
            Student student= _studentRepo.GetById((int)id);
            if (student == null) throw new NotFoundException(ResponseMessages.DataNotFound);
            return student;
        }

        public List<Student> GetStudentsByAge(int? age)
        {
            if (age is null) throw new ArgumentNullException();
            return _studentRepo.GetAllWithExpression(m => m.Age == age);
           
        }

        public List<Student> SearchByNameOrSurname(string text)
        {
            return _studentRepo.GetAllWithExpression(m => m.Name.Contains(text.Trim()) || m.Surname.Contains(text.Trim()));
        }
    }
}

