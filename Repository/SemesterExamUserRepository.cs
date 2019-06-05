using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Model;
using Model.ViewModel;
using Repository.Interfaces;

namespace Repository
{
    public class SemesterExamUserRepository : Repository.Interfaces.ISemesterExamUserRepository<SemesterExam_User>
    {
        private DBEntityContext context;

        public SemesterExamUserRepository(DBEntityContext context)
        {
            this.context = context;
        }

        //----------------------------------------------Delete----------------------------------------------------------
        public int DeleteCandidates(int userId, int semesterId)
        {
            if (userId != null)
            {
                var del = (from a in context.SemesterExamUsers
                           where a.User.UserId == userId && a.SemesterExam.ID == semesterId
                           select a).SingleOrDefault();
                context.SemesterExamUsers.Remove(del);
                return context.SaveChanges();
            }
            return 0;
        }
        //----------------------------------------------Delete----------------------------------------------------------


        //----------------------------------------------Insert----------------------------------------------------------
        public int InsertCandidates(int userid, int semesterid)
        {

            SemesterExam_User item = new SemesterExam_User();
            item.User = context.Users.Find(userid);

            item.SemesterExam = context.SemesterExams.Find(semesterid);
            item.Type = 2;
            context.SemesterExamUsers.Add(item);
            return context.SaveChanges();
        }
        //----------------------------------------------Insert----------------------------------------------------------


        //------------------------------------------Compare-SemesterId-----------------------------------------------
        public IEnumerable<User> GetUserOutSemester(int semesterid)
        {
            var userInSemester = context.Users.Where(s => s.SemesterExam_Users.Where(x => x.SemesterExam.ID == semesterid).Count() > 0);
            var user = new List<User>(context.Users);
            foreach (var item in userInSemester)
            {
                var t = user.Remove(item);
            }
            var userss = user.ToList();
            return userss;
        }
        //------------------------------------------Compare-SemesterId-----------------------------------------------


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

        public IEnumerable<Candidates> Search(string searchString, int id, int type)
        {
            var query = from U in context.Users
                        join SEU in context.SemesterExamUsers
                        on U.UserId equals SEU.User.UserId
                        where U.UserName.Contains(searchString)
                        && SEU.SemesterExam.ID == id && SEU.Type == type
                        select U;
            List<User> list = query.ToList();
            List<Candidates> candidates = new List<Candidates>();
            foreach (User item in list)
            {
                Candidates a = new Candidates();
                a.UserId = item.UserId;
                a.UserName = item.UserName;
                a.FullName = item.FullName;
                a.Department = item.Department;
                a.Email = item.Email;
                a.Position = item.Position;
                candidates.Add(a);
            }
         

            return candidates;
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
            //List<User> users = context.Users.ToList();
            //List<SemesterExam> semesterExamsterExams = context.SemesterExams.ToList();
            return context.SemesterExamUsers.Where(S => S.SemesterExam.ID == id && S.Type == 2).ToList();

        }

        public List<Model.ViewModel.Candidates> candidates(int id)
        {
            User user = context.Users.Find(id);
            List<SemesterExam_User> semesterExam_Users =
            context.SemesterExamUsers.Where(SU => SU.SemesterExam.ID == id && SU.Type == 2).ToList(); ;
            var query = from U in context.Users
                        join SEU in context.SemesterExamUsers on U.UserId equals SEU.User.UserId
                        select U;

            int count = query.ToList().Count;
            List<Candidates> candidates = new List<Candidates>();

            List<User> users = context.Users.ToList();
            List<SemesterExam> semesterExamsterExams = context.SemesterExams.ToList();
            List<Candidates> abc = new List<Candidates>();

            foreach (SemesterExam_User item in semesterExam_Users)
            {
                Candidates I = new Candidates();
                I.UserId = item.User.UserId;
                I.UserName = item.User.UserName;
                I.FullName = item.User.FullName;
                I.Email = item.User.Email;
                I.Department = item.User.Department;
                I.Position = item.User.Position;
                candidates.Add(I);
            }
            int sa = 0;
            return candidates;
        }

        public IEnumerable<SemesterExam_User> Filter(Candidates t)
        {

            var result = context.SemesterExamUsers.ToList();

            //if (!string.IsNullOrEmpty(t.UserName))
            //{
            //    result = result.Where(x => x.User.UserName.Contains(t.UserName)).ToList();
            //}

            if (t.Department != null)
            {
                result = result.Where(x => x.User.Department == t.Department).ToList();
            }
            if (t.Department != null)
            {
                result = result.Where(x => x.User.Position == t.Position).ToList();
            }

            return result;
        }
    }
}