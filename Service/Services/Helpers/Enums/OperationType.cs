using System;
namespace Service.Services.Helpers.Enums
{
	public enum OperationType
	{
		GroupCreate=1,
		GroupEdit,
		GroupDelete,
		GetGroupById,
		GetAllGroups,
		GetAllGroupsByRoom,
		GetAllGroupsByTeacher,
		SearchGroupByName,
		StudentCreate,
		StudentEdit,
        StudentDelete,
        GetStudentById,
		GetAllStudents,
        GetAllStudentsByGroupId,
        GetStudentsByAge,
        SearchByNameOrSurname,
    }
}

