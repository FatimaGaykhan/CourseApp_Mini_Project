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
			Name: string insertedName = Console.ReadLine();
			string name = insertedName.Trim().ToLower();
			if (string.IsNullOrWhiteSpace(name))
			{
                ConsoleColor.Red.WriteConsole("Input can't be empty.Please add again");
                goto Name;
            }
            bool isCorrectFormatByName = name.All(char.IsLetter);

            if (!isCorrectFormatByName)
            {
                ConsoleColor.Red.WriteConsole("Name format is wrong.Please Add again");
                goto Name;
            }
          

            ConsoleColor.Cyan.WriteConsole("Add student surname:");
            Surname: string insertedSurname = Console.ReadLine();
            string surname = insertedSurname.Trim().ToLower();
            if (string.IsNullOrWhiteSpace(surname))
			{
                ConsoleColor.Red.WriteConsole("Input can't be empty.Please add again");
                goto Surname;
            }
            //bool isNumericBySurname = surname.Any(char.IsDigit);
            bool isCorrectFormatBySurname = surname.All(char.IsLetter);

            if (!isCorrectFormatBySurname)
            {
                ConsoleColor.Red.WriteConsole("Surname format is wrong.Please Add again");
                goto Surname;
            }
           

            ConsoleColor.Cyan.WriteConsole("Add student age:");
			Age: string insertedAge = Console.ReadLine();
            string ageStr = insertedAge.Trim().ToLower();
            if (string.IsNullOrWhiteSpace(ageStr))
			{
                ConsoleColor.Red.WriteConsole("Input can't be empty.Please add again");
                goto Age;
            }
            bool strAgeFormat = ageStr.Any(char.IsLetter);
            bool isCorrectAgeFormatBySymbol = ageStr.All(char.IsDigit);
			if (strAgeFormat)
			{
				ConsoleColor.Red.WriteConsole("Age format is wrong.Please Add again");
				goto Age;
			}
            if (!isCorrectAgeFormatBySymbol)
            {
                ConsoleColor.Red.WriteConsole("Age format is wrong.Please Add again");
                goto Age;
            }

            int age;
            bool isCorrectAgeFormat = int.TryParse(ageStr, out age);

            if (age < 15 || age > 50)
            {
                ConsoleColor.Red.WriteConsole("age cannot be less than 15 or more than 50");
                goto Age;
            }


            ConsoleColor.Cyan.WriteConsole("Choose student's group Id:");
            List<Group> groups = _groupService.GetAll();
            foreach (var group in groups)
            {
                Console.WriteLine($"Group Id: {group.Id} Group Name:{group.Name}");
            }

            Id: string insertedId = Console.ReadLine();
            string idStr= insertedId.Trim().ToLower();
            if (string.IsNullOrWhiteSpace(idStr))
			{
                ConsoleColor.Red.WriteConsole("Input can't be empty.Please add again");
                goto Id;
            }
            bool idFormat= idStr.Any(char.IsLetter);
            bool isCorrectIdFormatBySymbol = idStr.All(char.IsDigit);

            if (!isCorrectIdFormatBySymbol)
            {
                ConsoleColor.Red.WriteConsole("Id format is wrong.Please Add again");
                goto Id;
            }

            if (idFormat)
            {
                ConsoleColor.Red.WriteConsole("Id format is wrong.Please Add again");
                goto Id;
            }

            int id;
            bool isCorrectIdFormat = int.TryParse(idStr, out id);

            if (id == 0 || id < 0)
            {
                ConsoleColor.Red.WriteConsole("Id cannot be eqaul to 0 or negative.Please add again");
                goto Id;
            }
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
                goto Id;
                
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
            string strId = insertedStudentId.Trim();
            if (string.IsNullOrWhiteSpace(strId))
            {
                ConsoleColor.Red.WriteConsole("Input can't be empty.Please add again");
                goto Id;
            }
            int convertedStudentId;
            bool isCorrectIdFormat = int.TryParse(strId, out convertedStudentId);
            bool isCorrectIdFormatBySymbol = strId.Any(char.IsSymbol);
            if (isCorrectIdFormatBySymbol)
            {
                ConsoleColor.Red.WriteConsole("Id format is wrong.Please add again");
                goto Id;
            }
            if (convertedStudentId == 0 || convertedStudentId < 0)
            {
                ConsoleColor.Red.WriteConsole("Id cannot be eqaul to 0 or negative.Please add again");
                goto Id;
            }

            if (isCorrectIdFormat)
            {
                try
                {
                    Student student = _studentService.GetById(convertedStudentId);
                    if (student is null) throw new NotFoundException(ResponseMessages.DataNotFound);


                    ConsoleColor.Cyan.WriteConsole("Type the student name:");
                    Name: string insertedName = Console.ReadLine();
                    string name = insertedName.Trim().ToLower();
                    bool isNumericByName = name.Any(char.IsDigit);
                    bool isCorrectNameFormatBySymbol = name.Any(char.IsSymbol);

                    if (isCorrectNameFormatBySymbol)
                    {
                        ConsoleColor.Red.WriteConsole("Name format is wrong.Please add again");
                        goto Name;
                    }
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
                    bool isCorrectSurnameFormatBySymbol = surname.Any(char.IsSymbol);


                    if (isCorrectSurnameFormatBySymbol)
                    {
                        ConsoleColor.Red.WriteConsole("Surname format is wrong.Please add again");
                        goto Surname;
                    }
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
                    bool isCorrectFormatByAgeForLetters = newStr.Any(char.IsLetter);
                    if (isCorrectFormatByAgeForLetters)
                    {
                        ConsoleColor.Red.WriteConsole("Age format is wrong.Please change student's age. To keep old age just insert empty");
                        goto Age;
                    }
                    bool isCorrectFormatByAge = int.TryParse(newStr, out age);
                    bool isCorrectAgeFormatBySymbol = newStr.Any(char.IsSymbol);

                    if (isCorrectAgeFormatBySymbol)
                    {
                        ConsoleColor.Red.WriteConsole("Age format is wrong.Please add again");
                        goto Age;
                    }


                    if (isCorrectFormatByAge)
                    {
                        if (age < 15||age>50)
                        {
                            ConsoleColor.Red.WriteConsole("Age cannot be less than 15 or more than 50");
                            goto Age;
                        }
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
                    bool isCorrectGroupIdFormatBySymbol = newGroupId.Any(char.IsSymbol);


                    if (isCorrectGroupIdFormatBySymbol)
                    {
                        ConsoleColor.Red.WriteConsole("Group Id format is wrong .Please add again");
                        goto GroupId;
                    }
                  

                    if (isCorrectFormatGroupId)
                    {
                        if (student.Group.Id == groupId)
                        {
                            ConsoleColor.Red.WriteConsole("This student already is in this group.Add again");
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
             
                }
                catch (Exception ex)
                {
                    ConsoleColor.Red.WriteConsole(ex.Message);
                }
                ConsoleColor.Green.WriteConsole("Data edited seccessfully");

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
                List<Student> students= _studentService.GetAll();
                if (students.Count == 0) throw new EmptyException(ResponseMessages.NotAddedYet);
                foreach (var student in students)
                {
                    string data = $"Id: {student.Id}, Student name : {student.Name}, Student Surname : {student.Surname}, Student Age : {student.Age}, Student Group Name: {student.Group.Name}";
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
            ConsoleColor.Cyan.WriteConsole("Choose id want to delete:");
            List<Student> students = _studentService.GetAll();
            foreach (var student in students)
            {
                Console.WriteLine($"Student id:{student.Id}, Student name:{student.Name}, Student surname:{student.Surname}, Student age:{student.Age}, Student group id:{student.Group.Id}, Student group name:{student.Group.Name}");
            }
            Id: string insertedId = Console.ReadLine();
            string idStr = insertedId.Trim();
            if (string.IsNullOrWhiteSpace(idStr))
            {
                ConsoleColor.Red.WriteConsole("Input can't be empty.Please add again:");
                goto Id;
            }
            int id;
            bool IsCorrectIdFormat = int.TryParse(idStr, out id);
          
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
            List<Group> groups = _groupService.GetAll();
            foreach (var group in groups)
            {
                ConsoleColor.Cyan.WriteConsole($"Group id:{group.Id} Group name:{group.Name}");
            }
            Id: string insertedId = Console.ReadLine();
            string idStr = insertedId.Trim();
            if (string.IsNullOrWhiteSpace(idStr))
            {
                ConsoleColor.Red.WriteConsole("Input can't be empty.Please add again:");
                goto Id;
            }
            int id;
            bool isCorrectIdFormat = int.TryParse(idStr, out id);
          
            if (isCorrectIdFormat)
            {
                try
                {
                    List<Student> students = _studentService.GetAllStudentsByGroupId(id);
                    if (students.Count==0) throw new NotFoundException(ResponseMessages.DataNotFound);
                    foreach (var student in students)
                    {
                        string data = $"Id: {student.Id}, Student name : {student.Name}, Student Surname : {student.Surname}, Student Age : {student.Age}, Student Group Name: {student.Group.Name}";
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
            List<Student> students = _studentService.GetAll();
            foreach (var student in students)
            {
                Console.WriteLine($"Student id:{student.Id} Student name:{student.Name} Student surname:{student.Surname}");
            }
            ConsoleColor.Cyan.WriteConsole("Choose one id:");
            Id: string insertedId = Console.ReadLine();
            string idStr = insertedId.Trim();
            if (string.IsNullOrWhiteSpace(idStr))
            {
                ConsoleColor.Red.WriteConsole("Input can't be empty.Please add again.");
                goto Id;
            }
            int id;
            bool isCorrectIdFormat = int.TryParse(idStr, out id);
           
            if (isCorrectIdFormat)
            {
                try
                {
                    Student student = _studentService.GetById(id);
                    if (student is null) throw new NotFoundException(ResponseMessages.DataNotFound);
                    string data = $"Id: {student.Id}, Student name : {student.Name}, Student Surname : {student.Surname}, Student Age : {student.Age}, Student Group Name: {student.Group.Name}";
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
            Age: string insertedAge = Console.ReadLine();
            string ageStr = insertedAge.Trim();
            if (string.IsNullOrWhiteSpace(ageStr))
            {
                ConsoleColor.Red.WriteConsole("Input can't be empty.Please Add again");
                goto Age;
            }

            int age;
            bool isCorrectAgeFormat = int.TryParse(ageStr, out age);
            bool isCorrectFormatAgeBySymbol = ageStr.All(char.IsDigit);
            if (!isCorrectFormatAgeBySymbol)
            {
                ConsoleColor.Red.WriteConsole("Age format is wrong.Please add again");
                goto Age;
            }
          
            if (age < 15 || age > 50)
            {
                ConsoleColor.Red.WriteConsole("Age cannot be less than 15 or more than 50.Please add again");
                goto Age;
            }

            if (isCorrectAgeFormat)
            {
                try
                {
                    List<Student> students = _studentService.GetStudentsByAge(age);
                    if (students.Count==0) throw new NotFoundException(ResponseMessages.DataNotFound);
                    foreach (var student in students)
                    {
                        string data = $"Id: {student.Id}, Student name : {student.Name}, Student Surname : {student.Surname}, Student Age : {student.Age}, Student Group Name: {student.Group.Name}";
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
                ConsoleColor.Red.WriteConsole("Age format is wrong. Please add again");
                goto Age;
            }

        }

        public void SearchByNameOrSurname()
        {
            ConsoleColor.Cyan.WriteConsole("Add text:");
            Text: string insertedText = Console.ReadLine();
            string searchText = insertedText.Trim().ToLower();
            if (string.IsNullOrWhiteSpace(searchText))
            {
                ConsoleColor.Red.WriteConsole("Input can't be empty.Please add again");
                goto Text;
            }

            try
            {
                List<Student> students = _studentService.SearchByNameOrSurname(searchText);
                if (students.Count == 0) throw new NotFoundException(ResponseMessages.DataNotFound);
                foreach (var student in students)
                {
                    string data = $"Id: {student.Id}, Student name : {student.Name}, Student Surname : {student.Surname}, Student Age : {student.Age}, Student Group Name: {student.Group.Name}";
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

