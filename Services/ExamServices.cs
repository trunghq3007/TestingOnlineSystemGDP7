using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using Model;
using DataAccessLayer;
using Repository.Interfaces;

namespace Services
{
    public class ExamServices : Interfaces.IServices<Exam>
    {
        private DBEntityContext context;
        private IRepository<Exam> examRepository;

        public ExamServices()
        {
            examRepository = new ExamRepository(new DBEntityContext());
        }
        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Exam> Filter(Exam t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Exam> GetAll()
        {
            return context.Exams.ToList();
            
        }

        public Exam GetById(int id)
        {
            throw new NotImplementedException();
        }

        public int Insert(Exam t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Exam> Search(string searchString)
        {
            throw new NotImplementedException();
        }

        public int Update(Exam t)
        {
            throw new NotImplementedException();
        }
    }
}
