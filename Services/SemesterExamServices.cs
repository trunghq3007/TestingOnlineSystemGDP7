using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Model;
using Repository;
using Repository.Interfaces;

namespace Services
{
  public  class SemesterExamServices : Interfaces.IServices<SemesterExam>
    {
        private IRepository<SemesterExam> SemesterExamRepository;
        public SemesterExamServices()
        {
            SemesterExamRepository = new SemesterExamRepository(new DBEntityContext());
        }
        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SemesterExam> Filter(SemesterExam t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SemesterExam> GetAll()
        {
            return SemesterExamRepository.GetAll();
        }

        public SemesterExam GetById(int id)
        {
            throw new NotImplementedException();
        }

        public int Insert(SemesterExam t)
        {
            return SemesterExamRepository.Insert(t);
        }

        public IEnumerable<SemesterExam> Search(string searchString)
        {
            throw new NotImplementedException();
        }

        public int Update(SemesterExam t)
        {
            throw new NotImplementedException();
        }
    }
}
