using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DataAccessLayer;
using Repository.Interfaces;
using Repository;
namespace Services
{
    public class TestServices : Interfaces.IServices<Test>
    {
        private IRepository<Test> testRepository;

        public TestServices()
        {
            testRepository = new TestRepository(new DBEntityContext());
        }
        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Test> Filter(Test t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Test> GetAll()
        {
            throw new NotImplementedException();
        }

        public Test GetById(int id)
        {
            throw new NotImplementedException();
        }

        public int Insert(Test t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Test> Search(string searchString)
        {
            throw new NotImplementedException();
        }

        public int Update(Test t)
        {
            throw new NotImplementedException();
        }
    }
}
