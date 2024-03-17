using System;
using Domain.Models;
using Service.Services;
using Service.Services.Helpers.Constants;
using Service.Services.Helpers.Exceptions;
using Service.Services.Helpers.Extensions;
using Service.Services.Interfaces;

namespace CourseApp.Controllers
{
	public class StudentController
	{
		private readonly IStudentService _studentService;
		private readonly IGroupService _groupService;
		public StudentController()
		{
			_studentService = new StudentService();
			_groupService = new GroupService();
		}

		public void Create()
		{
			ConsoleColor.Cyan.WriteConsole("Add student name:");
			Name: string str1 = Console.ReadLine();
			string name = str1.Trim().ToLower();
			if (string.IsNullOrWhiteSpace(name))
			{
                ConsoleColor.Red.WriteConsole("Input can't be empty");
            }
			bool isNumericByName = name.Any(char.IsDigit);
			if (isNumericByName)
			{
                ConsoleColor.Red.WriteConsole("Name format is wrong.Please Add again");
                goto Name;
            }

            ConsoleColor.Cyan.WriteConsole("Add student surname:");
            Surname: string str2 = Console.ReadLine();
            string surname = str2.Trim().ToLower();
            if (string.IsNullOrWhiteSpace(surname))
			{
                ConsoleColor.Red.WriteConsole("Input can't be empty");
            }
            bool isNumericBySurname = surname.Any(char.IsDigit);
			if (isNumericBySurname)
			{
                ConsoleColor.Red.WriteConsole("Surname format is wrong.Please Add again");
				goto Surname;
            }

            ConsoleColor.Cyan.WriteConsole("Add student age:");
			Age: string str3 = Console.ReadLine();
            string newStr = str3.Trim().ToLower();
            if (string.IsNullOrWhiteSpace(newStr))
			{
                ConsoleColor.Red.WriteConsole("Input can't be empty");
            }
            bool strFormat = newStr.Any(char.IsLetter);
			if (strFormat)
			{
				ConsoleColor.Red.WriteConsole("Age format is wrong.Please Add again");
				goto Age;
			}
            int age;
            bool isCorrectAgeFormat = int.TryParse(newStr, out age);

            ConsoleColor.Cyan.WriteConsole("Choose student's group Id:");
            var response = _groupService.GetAll();
            foreach (var item in response)
            {
                Console.WriteLine($"Group Id: {item.Id} Group Name:{item.Name}");
            }

            Id: string str4 = Console.ReadLine();
            string newStr4= str4.Trim().ToLower();
            if (string.IsNullOrWhiteSpace(newStr4))
			{
                ConsoleColor.Red.WriteConsole("Input can't be empty");
            }
            bool idFormat= newStr4.Any(char.IsLetter);
            if (idFormat)
            {
                ConsoleColor.Red.WriteConsole("Id format is wrong.Please Add again");
                goto Id;
            }
            int id;
            bool isCorrectIdFormat = int.TryParse(newStr4, out id);
            try
            {
                Group group = _groupService.GetById(id);
                if (group is null) throw new NotFoundException(ResponseMessages.DataNotFound);
                _studentService.Create(new Student { Name = name.Trim().ToLower(), Surname = surname.Trim().ToLower(), Age = age, Group=group });
                ConsoleColor.Green.WriteConsole("Data added successfully");
            }
            catch (Exception ex)
            {
                ConsoleColor.Red.WriteConsole(ex.Message);
            }

        }

        public void GetAll()
        {
            try
            {
                var response = _studentService.GetAll();
                if (response.Count == 0) throw new EmptyException(ResponseMessages.NotAddedYet);
                foreach (var item in response)
                {
                    string data = $"Id: {item.Id}, Student name : {item.Name}, Student Surname : {item.Surname}, Student Age : {item.Age}, Student Group Name: {item.Group.Name}";
                    Console.WriteLine(data);
                }
            }
            catch (Exception ex)
            {
                ConsoleColor.Red.WriteConsole(ex.Message);
            }
        }
	}
}

