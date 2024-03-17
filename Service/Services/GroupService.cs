using System;
using Domain.Models;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Service.Services.Helpers.Constants;
using Service.Services.Helpers.Exceptions;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepo;
        private int count = 1;

        public GroupService()
        {
            _groupRepo = new GroupRepository();
        }

        public void Create(Group data)
        {
            if (data is null) throw new ArgumentNullException();
            data.Id = count;
            _groupRepo.Create(data);
            count++;
        }

        public void Delete(int? id)
        {
            if (id is null) throw new ArgumentNullException();
            Group group = _groupRepo.GetById((int)id);
            if (group is null) throw new NotFoundException(ResponseMessages.DataNotFound);
            _groupRepo.Delete(group);
        }

        public void Edit(Group data)
        {
            if (data is null) throw new ArgumentNullException();
            var response = _groupRepo.GetAllWithExpression(m => m.Id == data.Id);
            if(response != null)
            {
                foreach (var item in response)
                {
                    item.Name = data.Name;
                    item.Room = data.Room;
                    item.TeacherName = data.TeacherName;
                }
            }
            else
            {
                throw new NotFoundException(ResponseMessages.DataNotFound);
            }
            
        }

        public List<Group> GetAll()
        {
            return _groupRepo.GetAll();
        }

        public List<Group> GetAllGroupsByRoom(string room)
        {
            var response= _groupRepo.GetAllWithExpression(m => m.Room == room.Trim()||m.Room.Contains(room.Trim()));
            if (response is null) throw new NotFoundException(ResponseMessages.DataNotFound);
            return response;
        }

        public List<Group> GetAllGroupsByTeacher(string teacherName)
        {
            return _groupRepo.GetAllWithExpression(m => m.TeacherName == teacherName.Trim()||m.TeacherName.Contains(teacherName.Trim()));
        }

        public Group GetById(int? id)
        {
            if (id is null) throw new ArgumentNullException();
            Group group = _groupRepo.GetById((int)id);
            if (group is null) throw new NotFoundException(ResponseMessages.DataNotFound);
            return group;
        }

        public List<Group> SearchGroupByName(string groupName)
        {
            return _groupRepo.GetAllWithExpression(m => m.Name == groupName.Trim()||m.Name.Contains(groupName.Trim()));
        }
    }
}

