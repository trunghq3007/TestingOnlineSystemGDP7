using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
   public interface IExamQuestionServices<T> where T : class
    {
        IEnumerable<Model.ViewModel.ViewQuestionExam> GetListQuestionById(int id);

    }
}
