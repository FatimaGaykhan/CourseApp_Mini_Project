using Service.Services.Helpers.Enums;
using Service.Services.Helpers.Extensions;

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
                Console.WriteLine("yes");
                break;
            case (int)OperationType.GroupEdit:
                Console.WriteLine("yes");
                break;
            case (int)OperationType.GroupDelete:
                Console.WriteLine("yes");
                break;
            case (int)OperationType.GetGroupById:
                Console.WriteLine("yes");
                break;
            case (int)OperationType.GetAllGroups:
                Console.WriteLine("yes");
                break;
            case (int)OperationType.GetAllGroupsByRoom:
                Console.WriteLine("yes");
                break;
            case (int)OperationType.GetAllGroupsByTeacher:
                Console.WriteLine("yes");
                break;
            case (int)OperationType.SearchGroupByName:
                Console.WriteLine("yes");
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
    ConsoleColor.Cyan.WriteConsole("Choose One Operation: \n 1-Group Create \n 2-Group Delete \n 3-Group Edit \n 4-Get Group By Id \n 5-Get All Groups \n 6-Get All Groups By Room \n 7-Get All Groups By Teacher  \n 8-Search Group By Name  ");
}