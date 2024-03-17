 using System;
using System.Xml.Linq;
using Domain.Models;
using Service.Services;
using Service.Services.Helpers.Constants;
using Service.Services.Helpers.Exceptions;
using Service.Services.Helpers.Extensions;
using Service.Services.Interfaces;

namespace CourseApp.Controllers
{
	public class GroupController
	{
		private readonly IGroupService _groupservice;
		public GroupController()
		{
			_groupservice = new GroupService();
		}

		public void Create()
		{
			ConsoleColor.Cyan.WriteConsole("Add Group Name:");
		    Name: string str1 = Console.ReadLine();
			string name = str1.Trim().ToLower();
            if (string.IsNullOrWhiteSpace(name))
            {
                ConsoleColor.Red.WriteConsole("Input can't be empty");
                goto Name;
            }
			ConsoleColor.Cyan.WriteConsole("Add Teacher name:");
		    TeacherName: string str2 = Console.ReadLine();
			string teacherName = str2.Trim().ToLower();
			if (string.IsNullOrWhiteSpace(teacherName))
			{
				ConsoleColor.Red.WriteConsole("Input can't be empty.");
				goto TeacherName;	
			}
            bool isNumeric = teacherName.Any(char.IsDigit);
            if (isNumeric)
            {
				ConsoleColor.Red.WriteConsole("Teacher name format is wrong.Please add again");
				goto TeacherName;
            }
            ConsoleColor.Cyan.WriteConsole("Add Room:");
			Room: string room = Console.ReadLine();
			if (string.IsNullOrWhiteSpace(room.Trim().ToLower()))
			{
				ConsoleColor.Red.WriteConsole("Input can't be empty");
				goto Room;
			}

			try
			{
				_groupservice.Create(new Group { Name = name.Trim().ToLower(), TeacherName = teacherName.Trim().ToLower(), Room = room.Trim().ToLower() });
				ConsoleColor.Green.WriteConsole("Data added successfully");
			}
			catch (Exception ex)
			{
				ConsoleColor.Red.WriteConsole(ex.Message);
				goto Name;
			}
		}
		public void Edit()
		{
			ConsoleColor.Cyan.WriteConsole("Type the Id of the data you want to change");
			Id: string strId = Console.ReadLine();
			if (string.IsNullOrWhiteSpace(strId))
			{
				ConsoleColor.Red.WriteConsole("Input can't be empty");
			}
			int id;
            bool isCorretIdFormat = int.TryParse(strId, out id);
            if (isCorretIdFormat)
            {
                try
                {
                    var response = _groupservice.GetById(id);
                    if (response is null) throw new NotFoundException(ResponseMessages.DataNotFound);

                    ConsoleColor.Cyan.WriteConsole("Type the group name:");
                    string str1 = Console.ReadLine();
                    string name = str1.Trim().ToLower();
                    ConsoleColor.Cyan.WriteConsole("Type the teacher name:");
                    TeacherName: string str2 = Console.ReadLine();
                    string teacherName = str2.Trim().ToLower();
                    bool isNumeric = teacherName.Any(char.IsDigit);
                    if (isNumeric)
                    {
                        ConsoleColor.Red.WriteConsole("Teacher name format is wrong please add again");
                        goto TeacherName;
                    }
                    ConsoleColor.Cyan.WriteConsole("Type the room name:");
                    string str3 = Console.ReadLine();
                    string roomName = str3.Trim().ToLower();
                    if (name == "")
                    {
                        response.Name = response.Name;

                    }
                    if (teacherName == "")
                    {
                        response.TeacherName = response.TeacherName;
                    }
                    if (roomName == "")
                    {
                        response.Room = response.Room;
                    }
                    else
                    {
                        response.Name = name;
                        response.TeacherName = teacherName;
                        response.Room = roomName;
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
                var response = _groupservice.GetAll();
                if (response.Count == 0) throw new EmptyException(ResponseMessages.NotAddedYet);
                foreach (var item in response)
                {
                    string data = $"Id: {item.Id}, Group name : {item.Name}, Group Teacher : {item.TeacherName}, Group Room Name : {item.Room}";
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
			ConsoleColor.Cyan.WriteConsole("Add Group Id:");
		    Id: string str = Console.ReadLine();
			string idStr = str.Trim().ToLower();
			int id;
			bool isCorretIdFormat = int.TryParse(idStr, out id);
			if (isCorretIdFormat)
			{
				try
				{
					_groupservice.Delete(id);
					ConsoleColor.Green.WriteConsole("Data successfully deleted");
				}
				catch (Exception ex)
				{
					ConsoleColor.Red.WriteConsole(ex.Message);
					goto Id;
				}
			}
			else
			{
				ConsoleColor.Red.WriteConsole("Id format is wrong, please add again");
				goto Id;
			}
		}

		public void GetAllGroupsByRoom()
		{
			ConsoleColor.Cyan.WriteConsole("Add Room Name:");
		    RoomName: string str = Console.ReadLine();
			string roomName = str.Trim().ToLower();
			
            if (string.IsNullOrWhiteSpace(roomName))
            {
                ConsoleColor.Red.WriteConsole("Input can't be empty");
                goto RoomName;
			}
			else
			{
                try
                {
                    var response = _groupservice.GetAllGroupsByRoom(roomName);
                    if (response.Count==0) throw new NotFoundException(ResponseMessages.DataNotFound);
                    foreach (var item in response)
                    {
                        string data = $"Id: {item.Id}, Group name : {item.Name}, Group Teacher : {item.TeacherName}, Group Room Name : {item.Room}";
                        Console.WriteLine(data);
                    }

                }
                catch (Exception ex)
                {
                    ConsoleColor.Red.WriteConsole(ex.Message);
                }
            }
            
		}

		public void GetGroupById()
		{
			ConsoleColor.Cyan.WriteConsole("Add Id:");
		    Id: string str = Console.ReadLine();
			string idStr = str.Trim();
			if (string.IsNullOrWhiteSpace(idStr))
			{
				ConsoleColor.Red.WriteConsole("Input can't be empty");
				goto Id;
			}
			int id;
			bool isCorrectIdFormat = int.TryParse(idStr, out id);
			if (isCorrectIdFormat)
			{
				try
				{
                    var response = _groupservice.GetById(id);
					if (response is null) throw new NotFoundException(ResponseMessages.DataNotFound);
                    string data = $"Id: {response.Id}, Group name : {response.Name}, Group Teacher : {response.TeacherName}, Group Room Name : {response.Room}";
                    Console.WriteLine(data);
                }
				catch (Exception ex)
				{
					ConsoleColor.Red.WriteConsole(ex.Message);
                   
                }
			}
			else
			{
                ConsoleColor.Red.WriteConsole("Id format is wrong, please add again");
                goto Id;
            }
			
		}

		public void GetAllGroupsByTeacher()
		{
			ConsoleColor.Cyan.WriteConsole("Add teacher name:");
			TeacherName: string str = Console.ReadLine();
			string teacherName = str.Trim().ToLower();
            if (string.IsNullOrWhiteSpace(teacherName))
            {
                ConsoleColor.Red.WriteConsole("Input can't be empty");
                goto TeacherName;
            }

            bool isNumeric = teacherName.Any(char.IsDigit);
			if (!isNumeric)
			{
                try
                {
                    var response = _groupservice.GetAllGroupsByTeacher(teacherName);
                    if (response.Count == 0) throw new NotFoundException(ResponseMessages.DataNotFound);
                    foreach (var item in response)
                    {
                        string data = $"Id: {item.Id}, Group name : {item.Name}, Group Teacher : {item.TeacherName}, Group Room Name : {item.Room}";
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
                ConsoleColor.Red.WriteConsole("Teacher name format is wrong, please add again");
                goto TeacherName;
            }
		}

		public void SearchGroupByName()
		{
			ConsoleColor.Cyan.WriteConsole("Add group name:");
			GroupName: string str = Console.ReadLine();
			string groupName = str.Trim().ToLower();
            if (string.IsNullOrWhiteSpace(groupName))
            {
                ConsoleColor.Red.WriteConsole("Input can't be empty");
                goto GroupName;
			}
			else
			{
                try
                {
                    var response = _groupservice.SearchGroupByName(groupName);
                    if (response.Count == 0) throw new NotFoundException(ResponseMessages.DataNotFound);
                    foreach (var item in response)
                    {
                        string data = $"Id: {item.Id}, Group name : {item.Name}, Group Teacher : {item.TeacherName}, Group Room Name : {item.Room}";
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
}

