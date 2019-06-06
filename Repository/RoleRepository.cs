﻿using DataAccessLayer;
using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RoleRepository : Interfaces.IGroupRepository<Role>
    {
        private DBEntityContext context;
        public RoleRepository(DBEntityContext context)
        {
            this.context = context;
        }

        public bool CheckNameGroup(string groupName)
        {
            throw new NotImplementedException();
        }

        public int Delete(int id)
        {
            var role = context.Roles.Find(id);
            context.Roles.Remove(role);
            return context.SaveChanges();
        }

        public int DeleteUserGroup(int iduser, int idgroup)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Role> Filter(Role t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Role> FilterGroup(GroupFilterModel model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Role> FilterUser(UserFilterModel model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Role> FilterUserInGroup(GroupFilterModel model, int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Role> GetAll()
        {
            return context.Roles.ToList();
        }

        public Role GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Role GetByUsername(string userName)
        {
            throw new NotImplementedException();
        }

        public List<UserDetail> GetDetailUser(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Role> GetUserInGroup(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Role> GetUserOutGroup(int idgroup)
        {
            throw new NotImplementedException();
        }

        public int Insert(Role t)
        {
            context.Roles.Add(new Role());
            return context.SaveChanges();
        }

        public int InsertUserGroup(int iduser, int idgroup)
        {
            throw new NotImplementedException();
        }

        public bool Login(string userName, string passWord)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Role> Search(string searchString)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Role> SearchUserInGroup(int id, string searchString)
        {
            throw new NotImplementedException();
        }

        public int Update(Role t)
        {
            context.Entry(t).State = EntityState.Modified;
            return context.SaveChanges();
        }

        public int Update(int id, string groupname)
        {
            throw new NotImplementedException();
        }
    }
}