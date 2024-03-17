using CourseApp.Controllers;
using Service.Services.Helpers.Enums;
using Service.Services.Helpers.Extensions;

GroupController groupController = new GroupController();
StudentController studentController = new StudentController();

GetMenues();


while (true)
{
    Operation: string operationStr = Console.ReadLine();
    int operation;
    bool IsCorrectOperationFormat = int.TryParse(operationStr, out operation);

    if (IsCorrectOperationFormat)
    {
        switch (operation)
        {
            case (int)OperationType.GroupCreate:
                groupController.Create();
                break;
            case (int)OperationType.GroupEdit:
                groupController.Edit();
                break;
            case (int)OperationType.GroupDelete:
                groupController.Delete();
                break;
            case (int)OperationType.GetGroupById:
                groupController.GetGroupById();
                break;
            case (int)OperationType.GetAllGroups:
                groupController.GetAll();
                break;
            case (int)OperationType.GetAllGroupsByRoom:
                groupController.GetAllGroupsByRoom();
                break;
            case (int)OperationType.GetAllGroupsByTeacher:
                groupController.GetAllGroupsByTeacher();
                break;
            case (int)OperationType.SearchGroupByName:
                groupController.SearchGroupByName();
                break;
            case (int)OperationType.StudentCreate:
                studentController.Create();
                break;
            case (int)OperationType.StudentEdit:
                studentController.Edit();
                break;
            case (int)OperationType.StudentDelete:
                studentController.Delete();
                break;
            case (int)OperationType.GetStudentById:
                studentController.GetStudentById();
                break;
            case (int)OperationType.GetAllStudents:
                studentController.GetAll();
                break;
            case (int)OperationType.GetAllStudentsByGroupId:
                studentController.GetAllStudentsByGroupId();
                break;
            case (int)OperationType.GetStudentsByAge:
                studentController.GetStudentsByAge();
                break;
            case (int)OperationType.SearchByNameOrSurname:
                studentController.SearchByNameOrSurname();
                break;
            default:
                ConsoleColor.Red.WriteConsole("Operation is wrong, please choose again");
                goto Operation;
                
        }
    }
    else
    {
        ConsoleColor.Red.WriteConsole("Operation format is wrong, please add operation again");
        goto Operation;
    }

}



















static void GetMenues()
{
    ConsoleColor.Cyan.WriteConsole("Choose One Operation: \n 1-Group Create \n 2-Group Edit \n 3-Group Delete \n 4-Get Group By Id \n 5-Get All Groups \n 6-Get All Groups By Room \n 7-Get All Groups By Teacher  \n 8-Search Group By Name \n 9-Student Create \n 10-Student Edit \n 11-Student Delete \n 12-Get Student By Id \n 13-Get All Students \n 14-Get All Studens By Group Id \n 15-Get All Students By Age  \n 16-Search By Name Or Surname ");
}