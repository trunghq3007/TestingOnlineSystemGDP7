using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Model;
using Repository;
using Repository.Interfaces;
using Services.Interfaces;

namespace Services
{
    public class SemesterCustomerSevice : Interfaces.ISemesterCustomerService<SemesterExam, Test, Exam>
    {

        private ISemesterCustomer<SemesterExam, Test, Exam> repository;

        public SemesterCustomerSevice()
        {
            repository = new SemesterExamCustomer(new DBEntityContext());
        }


        public IEnumerable<Test> getListExam(int id)
        {
            return repository.getListExam(id);
        }

        public IEnumerable<SemesterExam> getAll()
        {
            return repository.getAll();
        }

        public IEnumerable<Exam> getDetailExam(int id)
        {
            return repository.getDetailExam(id);
        }
    }
}