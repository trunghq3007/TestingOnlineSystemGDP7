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
   public class ExamQuestionServices:Interfaces.IExamQuestionServices<Question>
    {
        private IExamQuestion<Question> repository;
        public ExamQuestionServices()
        {
            repository = new ExamQuestionResponsitory(new DBEntityContext());
        }

        public IEnumerable<ViewQuestionExam> GetListQuestionById(int id)
        {
            return repository.GetListQuestionById(id);
        }

        public int Delete(int id)
        {
            return repository.Delete(id);
        }
        public int Insert(Model.ExamQuestion model)
        {
            return repository.Insert(model);

        }
    }
}
