﻿using DataAccessLayer;
using Model;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;

namespace Services
{
    public class GroupServices : Interfaces.IGroupServices<Group>
    {
        private IGroupRepository<Group> groupRepository;

        public GroupServices()
        {
            groupRepository = new GroupRepository(new DBEntityContext());
        }
        public int Delete(int id)
        {
            return groupRepository.Delete(id);
        }

        public int DeleteUserGroup(int iduser, int idgroup)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Group> Filter(Group t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Group> FilterGroup(GroupFilterModel model)
        {
            return groupRepository.FilterGroup(model);
        }

        public IEnumerable<Group> FilterUser(UserFilterModel model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Group> FilterUserInGroup(GroupFilterModel model, int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Group> GetAll()
        {
            return groupRepository.GetAll();
        }

        public Group GetById(int id)
        {
            return groupRepository.GetById(id);
        }

        public IEnumerable<Group> GetUserInGroup(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Group> GetUserOutGroup(int idgroup)
        {
            throw new NotImplementedException();
        }

        public int Insert(Group t)
        {
            return groupRepository.Insert(t);
        }

        public int InsertUserGroup(int iduser, int idgroup)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Group> Search(string searchString)
        {
            return groupRepository.Search(searchString);
        }

        public IEnumerable<Group> SearchUserInGroup(int id, string searchString)
        {
            throw new NotImplementedException();
        }

        public int Update(Group t)
        {
            throw new NotImplementedException();
        }
    }
}
