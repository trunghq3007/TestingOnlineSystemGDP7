using DataAccessLayer;
using Model;
using Model.ViewModel;
using Repository;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
	public class TestService : Interfaces.ITestServices<Test>
	{
		private ITestRepository<ViewTest> viewtestrepository;
		private ITestRepository<Test> repository;
		private DBEntityContext context;

		public TestService()
		{
			repository = new TestRepository(new DBEntityContext());
			viewtestrepository = new ViewTestRepository(new DBEntityContext());
		}
		public int Delete(int id)
		{
			return repository.Delete(id);
		}

		public Test Export_exam(int id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Test> Filter(Test t)
		{
			throw new NotImplementedException();
		}

        public IEnumerable<ViewTest> GetAll()
        {
            return viewtestrepository.GetAll();
        }

        //public IEnumerable<Test> GetAll()
        //      {
        //          return repository.GetAll();
        //      }

        public List<ViewDetailTest> GetById(int id)
		{
            return repository.GetById(id);
		}

        //public Test getId(int id)
        //{
        //    return repository.GetByTestId(id);
        //}

        public int Insert(Test t)
        {
            return repository.Insert(t);
        }

		public IEnumerable<Test> Search(string searchString)
        {
            return repository.Search(searchString);
        }

        public IEnumerable<ViewTest> SearchName(string searchString)
        {
            return viewtestrepository.Search(searchString);
        }

        public int Update(Test test)
        {
            return repository.Update(test);
            //throw new NotImplementedException();
        }
	}
}
