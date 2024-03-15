using System;
using Domain.Models;

namespace Repository.Repositories.Interfaces
{
	public interface IGroupRepository:IBaseRepository<Group>
	{
		//List<Group> GetAllGroupsByTeacher(Func<Group, bool> predicate);
		//List<Group> GetAllGroupsByRoom(Func<Group, bool> predicate);
		//List<Group> SearchGroupByName(Func<Group,bool> predicate);
	}
}

