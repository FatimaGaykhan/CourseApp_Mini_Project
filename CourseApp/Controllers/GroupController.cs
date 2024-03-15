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
			ConsoleColor.Cyan.WriteConsole("Add Name:");
			Name: string name = Console.ReadLine();
			if (string.IsNullOrWhiteSpace(name.Trim().ToLower()))
			{
				ConsoleColor.Red.WriteConsole("Input can't be empty");
				goto Name;
			}
			ConsoleColor.Cyan.WriteConsole("Add Teacher name:");
			TeacherName: string teacherName = Console.ReadLine();
			if (string.IsNullOrWhiteSpace(teacherName.Trim().ToLower()))
			{
				ConsoleColor.Red.WriteConsole("Input can't be empty");
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

		public void GetAll()
		{
			var response = _groupservice.GetAll();
			foreach (var item in response)
			{
                string data = $"Id: {item.Id}, Group name : {item.Name}, Group Teacher : {item.TeacherName}, Group Room Name : {item.Room}";
                Console.WriteLine(data);
            }
		}

		public void Delete()
		{
			ConsoleColor.Cyan.WriteConsole("Add Group Id:");
			Id: string idStr = Console.ReadLine();
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
            try
			{
				var response=_groupservice.GetAllGroupsByRoom(roomName);
				if (response is null) throw new NotFoundException(ResponseMessages.DataNotFound);
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

		public void GetGroupById()
		{
			ConsoleColor.Green.WriteConsole("Add Id:");
		    Id: string idStr = Console.ReadLine();
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
                    goto Id;
                }
			}
			
		}
	}
}

