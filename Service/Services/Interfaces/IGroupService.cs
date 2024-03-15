using System;
using Domain.Models;

namespace Service.Services.Interfaces
{
	public interface IGroupService
	{
		void Create(Group data);
		void Delete(int? id);
		void Edit(Group data);
		Group GetById(int? id);
		List<Group> GetAll();
		List<Group> GetAllGroupsByRoom(string room);
		List<Group> GetAllGroupsByTeacher(string teacherName);
		List<Group> SearchGroupByName(string groupName); 

	}
}

