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
                goto Name;
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
                goto Surname;
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
                goto Age;
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
                goto Id;
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
                if (group is null)
                {
                    ConsoleColor.Red.WriteConsole("Data not found.Please add again");
                    goto Id;
                }
                _studentService.Create(new Student { Name = name.Trim().ToLower(), Surname = surname.Trim().ToLower(), Age = age, Group=group });
                ConsoleColor.Green.WriteConsole("Data added successfully");
            }
            catch (Exception ex)
            {
                ConsoleColor.Red.WriteConsole(ex.Message);
                
            }

        }

        public void Edit()
        {
            ConsoleColor.Cyan.WriteConsole("Type the Id of the data you want to change");
            List<Student> students = _studentService.GetAll();
            foreach (Student student in students)
            {
                Console.WriteLine($"Student Id:{student.Id} Student name:{student.Name} Student surname:{student.Surname} Student age:{student.Age} Student group id:{student.Group.Id} Student Group name:{student.Group.Name}");
            }
            Id: string insertedStudentId = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(insertedStudentId))
            {
                ConsoleColor.Red.WriteConsole("Input can't be empty.Please add again");
                goto Id;
            }
            int convertedStudentId;
            bool isCorretIdFormat = int.TryParse(insertedStudentId, out convertedStudentId);

            if (isCorretIdFormat)
            {
                try
                {
                    Student student = _studentService.GetById(convertedStudentId);
                    if (student is null) throw new NotFoundException(ResponseMessages.DataNotFound);


                    ConsoleColor.Cyan.WriteConsole("Type the student name:");
                    Name: string insertedName = Console.ReadLine();
                    string name = insertedName.Trim().ToLower();
                    bool isNumericByName = name.Any(char.IsDigit);
                    if (isNumericByName)
                    {
                        ConsoleColor.Red.WriteConsole("Name format is wrong.Please add again");
                        goto Name;
                    }
                    if (student.Name == name)
                    {
                        ConsoleColor.Red.WriteConsole("Can't insert the same name. Please change student's name. To keep old name just insert empty");
                        goto Name;
                    }
                    else if (name == "")
                    {
                        student.Name = student.Name;

                    }
                    else
                    {
                        student.Name = name;
                    }


                    ConsoleColor.Cyan.WriteConsole("Type the student surname:");
                    Surname: string insertedSurname = Console.ReadLine();
                    string surname = insertedSurname.Trim().ToLower();
                    bool isNumericBySurname = surname.Any(char.IsDigit);
                    if (isNumericBySurname)
                    {
                        ConsoleColor.Red.WriteConsole("Surname format is wrong.Please add again");
                        goto Surname;
                    }
                    if (student.Surname == surname)
                    {
                        ConsoleColor.Red.WriteConsole("Can't insert the same surname. Please change student's surname. To keep old surname just insert empty");
                        goto Surname;
                    }
                    else if (surname == "")
                    {
                        student.Surname = student.Surname;

                    }
                    else
                    {
                        student.Surname = surname;
                    }



                    ConsoleColor.Cyan.WriteConsole("Type the student age:");
                    Age: string insertedAge = Console.ReadLine();
                    string newStr = insertedAge.Trim();
                  
                    int age;
                    bool isCorrectFormatByAge = int.TryParse(newStr, out age);
                    if (isCorrectFormatByAge)
                    {
                        if (student.Age == age)
                        {
                            ConsoleColor.Red.WriteConsole("Can't insert the same age. Please change student's age. To keep old age just insert empty");
                            goto Age;
                        }
                        else if (newStr == "")
                        {
                            student.Age = student.Age;
                        }
                        else
                        {
                            student.Age = age;
                        }

                    }
                   
                    ConsoleColor.Cyan.WriteConsole("Choose group id:");
                    List<Group> groups = _groupService.GetAll();
                    if (groups.Count == 0) throw new NotFoundException(ResponseMessages.DataNotFound);
                    foreach (Group group in groups)
                    {
                        Console.WriteLine($"Group Id:{group.Id} Group name:{group.Name}");
                    }
                    GroupId: string insertedGroupId = Console.ReadLine();
                    string newGroupId = insertedGroupId.Trim();
                    int groupId;
                    bool isCorrectFormatGroupId = int.TryParse(newGroupId, out groupId);
                    

                    if (isCorrectFormatGroupId)
                    {
                        if (student.Group.Id == groupId)
                        {
                            ConsoleColor.Red.WriteConsole("This student already is in this group");
                            goto GroupId;
                        }
                        else if (newGroupId == "")
                        {
                            student.Group.Id = student.Group.Id;
                        }
                        else
                        {
                            student.Group = groups.Find(group=>group.Id==groupId);
                        }
                    }
                    else
                    {
                        ConsoleColor.Red.WriteConsole("Group's id format is wrong.Please add again.");
                        goto GroupId;
                    }


             
                }
                catch (Exception ex)
                {
                    ConsoleColor.Red.WriteConsole(ex.Message);
                }

            }
            else
            {
                ConsoleColor.Red.WriteConsole("Id format is wrong please add again");
                goto Id;
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

        public void Delete()
        {
            ConsoleColor.Cyan.WriteConsole("Add id want to delete:");
            Id: string str = Console.ReadLine();
            string newStr = str.Trim();
            if (string.IsNullOrWhiteSpace(newStr))
            {
                ConsoleColor.Red.WriteConsole("Input can't be empty.Please add again:");
                goto Id;
            }
            int id;
            bool IsCorrectIdFormat = int.TryParse(newStr, out id);
            if (IsCorrectIdFormat)
            {
                try
                {
                    _studentService.Delete(id);
                    ConsoleColor.Green.WriteConsole("Data deleted successfully");
                }
                catch (Exception ex)
                {
                    ConsoleColor.Red.WriteConsole(ex.Message);
                }
            }
            else
            {
                ConsoleColor.Red.WriteConsole("Id Format is wrong.Please add again.");
                goto Id;
            }

        }

        public void GetAllStudentsByGroupId()
        {
            ConsoleColor.Cyan.WriteConsole("Choose id:");
            var result = _groupService.GetAll();
            foreach (var item in result)
            {
                ConsoleColor.Cyan.WriteConsole($"Group id:{item.Id} Group name:{item.Name}");
            }
            Id: string str = Console.ReadLine();
            string newStr = str.Trim();
            if (string.IsNullOrWhiteSpace(newStr))
            {
                ConsoleColor.Red.WriteConsole("Input can't be empty.Please add again:");
                goto Id;
            }
            int id;
            bool IsCorrectIdFormat = int.TryParse(newStr, out id);
            if (IsCorrectIdFormat)
            {
                try
                {
                    var response = _studentService.GetAllStudentsByGroupId(id);
                    if (response.Count==0) throw new NotFoundException(ResponseMessages.DataNotFound);
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
            else
            {
                ConsoleColor.Red.WriteConsole("Id Format is wrong.Please add again.");
                goto Id;
            }

        }

        public void GetStudentById()
        {
            ConsoleColor.Cyan.WriteConsole("Add id:");
            Id: string str = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(str))
            {
                ConsoleColor.Red.WriteConsole("Input can't be empty.Please add again.");
                goto Id;
            }
            string idStr = str.Trim();
            int id;
            bool isCorrectIdFormat = int.TryParse(str, out id);
            if (isCorrectIdFormat)
            {
                try
                {
                    var response = _studentService.GetById(id);
                    if (response is null) throw new NotFoundException(ResponseMessages.DataNotFound);
                    string data = $"Id: {response.Id}, Student name : {response.Name}, Student Surname : {response.Surname}, Student Age : {response.Age}, Student Group Name: {response.Group.Name}";
                    Console.WriteLine(data);

                }
                catch (Exception ex)
                {
                    ConsoleColor.Red.WriteConsole(ex.Message);
                }
            }
            else
            {
                ConsoleColor.Red.WriteConsole("Id format is wrong. Please add again.");
                goto Id;
            }

        }

        public void GetStudentsByAge()
        {
            ConsoleColor.Cyan.WriteConsole("Add age:");
            Age: string str = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(str))
            {
                ConsoleColor.Red.WriteConsole("Input can't be empty.Please Add again");
                goto Age;
            }

            string newStr = str.Trim();
            int age;
            bool isCorrectAgeFormat = int.TryParse(newStr, out age);
            if (isCorrectAgeFormat)
            {
                try
                {
                    var response = _studentService.GetStudentsByAge(age);
                    if (response.Count==0) throw new NotFoundException(ResponseMessages.DataNotFound);
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
            else
            {
                ConsoleColor.Red.WriteConsole("Age format is wrong please add again");
                goto Age;
            }

        }

        public void SearchByNameOrSurname()
        {
            ConsoleColor.Cyan.WriteConsole("Add text:");
            Text: string str = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(str))
            {
                ConsoleColor.Red.WriteConsole("Input ca't be empty");
                goto Text;
            }
            string searchText = str.Trim().ToLower();
            bool isNumericText = searchText.Any(char.IsDigit);
            if (!isNumericText)
            {
                try
                {
                    var response = _studentService.SearchByNameOrSurname(searchText);
                    if (response.Count == 0) throw new NotFoundException(ResponseMessages.DataNotFound);
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
            else
            {
                ConsoleColor.Red.WriteConsole("Text format is wrong please add again");
                goto Text;
            }

        }

    }
}

