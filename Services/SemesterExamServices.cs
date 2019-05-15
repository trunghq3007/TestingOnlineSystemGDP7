using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Model;
using Model.ViewModel;
using Repository;
using Repository.Interfaces;
using Services.Interfaces;

namespace Services
{
  public  class SemesterExamServices : Interfaces.ISemesterExamServices<SemesterExam>
  {
      private ISemesterExamRepository<SemesterExam> SemesterExamRepository;
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

        public SemesterDetail GetById(int id)
        {
            return SemesterExamRepository.GetById(id);
        }

        public int Insert(SemesterExam t)
        {
            return SemesterExamRepository.Insert(t);
        }

        public ReportSemester Report(int id)
        {
            return SemesterExamRepository.Report(id);
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
