using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Model;
namespace Repository
{
   public class SemesterExamUserRepository : Repository.Interfaces.ISemesterExamUserRepository<SemesterExam_User>
    {
        private DBEntityContext context;

        public SemesterExamUserRepository(DBEntityContext context)
        {
            this.context = context;
        }
        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SemesterExam_User> GetAll()
        {
            throw new NotImplementedException();

        }

        public SemesterExam_User GetById(int id)
        {
            throw new NotImplementedException();
        }

        public int Insert(SemesterExam_User t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SemesterExam_User> Search(string searchString)
        {
            throw new NotImplementedException();
        }
         private bool disposed = false;
        public void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IEnumerable<SemesterExam_User> GetCandidatesOfASemester(int id)
        {
            return context.SemesterExamUsers.Where(S => S.SemesterExam.ID == id && S.Type == 2).ToList();
        }
    }
}
