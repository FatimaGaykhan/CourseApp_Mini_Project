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
                ConsoleColor.Red.WriteConsole("Input can't be empty.Please add again");
                goto Name;
            }

            List<Group> response = _groupservice.GetAll();

            foreach (Group item in response)
            {
                if (item.Name.ToLower() == name)
                {
                    ConsoleColor.Red.WriteConsole("Group already exists.Please add again");
					goto Name;
                }
            }


            ConsoleColor.Cyan.WriteConsole("Add Teacher name:");
		    TeacherName: string str2 = Console.ReadLine();
			string teacherName = str2.Trim().ToLower();
			if (string.IsNullOrWhiteSpace(teacherName))
			{
				ConsoleColor.Red.WriteConsole("Input can't be empty.Please add again");
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
				ConsoleColor.Red.WriteConsole("Input can't be empty.Please add again");
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
            Id: string insertedId = Console.ReadLine();
            string strId = insertedId.Trim();
			if (string.IsNullOrWhiteSpace(strId))
			{
				ConsoleColor.Red.WriteConsole("Input can't be empty.Please add again.");
                goto Id;
			}
			int id;
            bool isCorrectIdFormat = int.TryParse(strId, out id);
            if (id == 0 || id < 0)
            {
                ConsoleColor.Red.WriteConsole("Id cannot be eqaul to 0 or negative.Please add again");
                goto Id;
            }
            if (isCorrectIdFormat)
            {
                try
                {
                    Group response = _groupservice.GetById(id);
                    if (response is null) throw new NotFoundException(ResponseMessages.DataNotFound);

                    ConsoleColor.Cyan.WriteConsole("Type the group name:");
                    Name: string insertedName = Console.ReadLine();
                    string name = insertedName.Trim().ToLower();
                    if (response.Name==name)
                    {
                        ConsoleColor.Red.WriteConsole("Data already exists.Please add again");
                        goto Name;
                    }
                    if (name == "")
                    {
                        response.Name = response.Name;

                    }
                    else
                    {
                        response.Name = name;
                    }


                    ConsoleColor.Cyan.WriteConsole("Type the teacher name:");
                    TeacherName: string insertedTeacherName = Console.ReadLine();
                    string teacherName = insertedTeacherName.Trim().ToLower();
                    bool isNumeric = teacherName.Any(char.IsDigit);
                    if (isNumeric)
                    {
                        ConsoleColor.Red.WriteConsole("Teacher name format is wrong. Please add again");
                        goto TeacherName;
                    }
                    if (response.TeacherName == teacherName)
                    {
                        ConsoleColor.Red.WriteConsole("Data already exists.Please add again");
                        goto TeacherName;
                    }
                    if (teacherName == "")
                    {
                        response.TeacherName = response.TeacherName;
                    }
                    else
                    {
                        response.TeacherName = teacherName;
                    }


                    ConsoleColor.Cyan.WriteConsole("Type the room name:");
                    RoomName: string insertedRoomName = Console.ReadLine();
                    string roomName = insertedRoomName.Trim().ToLower();
                    if (response.Room == roomName)
                    {
                        ConsoleColor.Red.WriteConsole("Data already exists.Please add again");
                        goto RoomName;
                    }
                    if (roomName == "")
                    {
                        response.Room = response.Room;
                    }
                    else
                    {
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
				ConsoleColor.Red.WriteConsole("Id format is wrong. Please add again");
				goto Id;
			}

        


        }

		public void GetAll()
		{
			try
			{
                List<Group> groups = _groupservice.GetAll();
                if (groups.Count == 0) throw new EmptyException(ResponseMessages.NotAddedYet);
                foreach (var group in groups)
                {
                    string data = $"Id: {group.Id}, Group name : {group.Name}, Group Teacher : {group.TeacherName}, Group Room Name : {group.Room}";
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
            List<Group> groups = _groupservice.GetAll();
            foreach (var group in groups)
            {
                Console.WriteLine($"Group id:{group.Id} Group name:{group.Name} Group Teacher name:{group.TeacherName}");
            }
			ConsoleColor.Cyan.WriteConsole("Add Group Id:");
		    Id: string insertedGroupId = Console.ReadLine();
			string groupIdStr = insertedGroupId.Trim().ToLower();
            if (string.IsNullOrWhiteSpace(groupIdStr))
            {
                ConsoleColor.Red.WriteConsole("Input can't be empty.Please add again");
                goto Id;
            }
			int id;
			bool isCorrectIdFormat = int.TryParse(groupIdStr, out id);
           
            if (isCorrectIdFormat)
			{
				try
				{
					_groupservice.Delete(id);
					ConsoleColor.Green.WriteConsole("Data successfully deleted");
				}
				catch (Exception ex)
				{
					ConsoleColor.Red.WriteConsole(ex.Message);
					
				}
			}
			else
			{
				ConsoleColor.Red.WriteConsole("Id format is wrong. Please add again");
				goto Id;
			}
		}

		public void GetAllGroupsByRoom()
		{
			ConsoleColor.Cyan.WriteConsole("Add Room Name:");
		    RoomName: string insertedRoomName = Console.ReadLine();
			string roomName = insertedRoomName.Trim().ToLower();
			
            if (string.IsNullOrWhiteSpace(roomName))
            {
                ConsoleColor.Red.WriteConsole("Input can't be empty.Please add again");
                goto RoomName;
			}
			else
			{
                try
                {
                    List<Group> groups = _groupservice.GetAllGroupsByRoom(roomName);
                    if (groups.Count==0) throw new NotFoundException(ResponseMessages.DataNotFound);
                    foreach (var group in groups)
                    {
                        string data = $"Id: {group.Id}, Group name : {group.Name}, Group Teacher : {group.TeacherName}, Group Room Name : {group.Room}";
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
            List<Group> groups = _groupservice.GetAll();
            foreach (var group in groups)
            {
                Console.WriteLine($"Group id:{group.Id} Group name:{group.Name}");
            }
            ConsoleColor.Cyan.WriteConsole("Add Id:");
		    Id: string insertedId = Console.ReadLine();
			string idStr = insertedId.Trim();
			if (string.IsNullOrWhiteSpace(idStr))
			{
				ConsoleColor.Red.WriteConsole("Input can't be empty.PLease add again");
				goto Id;
			}
			int id;
			bool isCorrectIdFormat = int.TryParse(idStr, out id);

           
            if (isCorrectIdFormat)
			{
				try
				{
                    Group group = _groupservice.GetById(id);
					if (group is null) throw new NotFoundException(ResponseMessages.DataNotFound);
                    string data = $"Id: {group.Id}, Group name : {group.Name}, Group Teacher : {group.TeacherName}, Group Room Name : {group.Room}";
                    Console.WriteLine(data);
                }
				catch (Exception ex)
				{
					ConsoleColor.Red.WriteConsole(ex.Message);
                }
			}
			else
			{
                ConsoleColor.Red.WriteConsole("Id format is wrong.Please add again");
                goto Id;
            }
			
		}

		public void GetAllGroupsByTeacher()
		{
			ConsoleColor.Cyan.WriteConsole("Add teacher name:");
			TeacherName: string insertedTeacherName = Console.ReadLine();
			string teacherName = insertedTeacherName.Trim().ToLower();
            if (string.IsNullOrWhiteSpace(teacherName))
            {
                ConsoleColor.Red.WriteConsole("Input can't be empty.Please add again");
                goto TeacherName;
            }

            bool isNumeric = teacherName.Any(char.IsDigit);
			if (!isNumeric)
			{
                try
                {
                    List<Group> groups = _groupservice.GetAllGroupsByTeacher(teacherName);
                    if (groups.Count == 0) throw new NotFoundException(ResponseMessages.DataNotFound);
                    foreach (var group in groups)
                    {
                        string data = $"Id: {group.Id}, Group name : {group.Name}, Group Teacher : {group.TeacherName}, Group Room Name : {group.Room}";
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
                ConsoleColor.Red.WriteConsole("Teacher name format is wrong.Please add again");
                goto TeacherName;
            }
		}

		public void SearchGroupByName()
		{
			ConsoleColor.Cyan.WriteConsole("Add group name:");
			GroupName: string insertedGroupName = Console.ReadLine();
			string groupName = insertedGroupName.Trim().ToLower();
            if (string.IsNullOrWhiteSpace(groupName))
            {
                ConsoleColor.Red.WriteConsole("Input can't be empty.Please add again");
                goto GroupName;
			}
			else
			{
                try
                {
                    List<Group> groups = _groupservice.SearchGroupByName(groupName);
                    if (groups.Count == 0) throw new NotFoundException(ResponseMessages.DataNotFound);
                    foreach (var group in groups)
                    {
                        string data = $"Id: {group.Id}, Group name : {group.Name}, Group Teacher : {group.TeacherName}, Group Room Name : {group.Room}";
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

