using Model;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
 public	interface IExamServices<T>
	{
		ListFilter listFilters();
		//IEnumerable<T> GetAll();
		IEnumerable<T> Search(string searchString);
		
		int Insert(T t);
		int Update(T t);
		int Delete(int id);
		T GetById(int id);
		IEnumerable<T> Filter(ExamFilterModel filterModel);
           
		
		
		string Export_exam(int id);
        string GetCategoryName(int idExam);
        IEnumerable<ViewDetailExam> GetDetailExams(int id);
        IEnumerable<T> GetAll();


        int Import(List<T> list);
    }
}
