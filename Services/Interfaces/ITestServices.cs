using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
 public	interface ITestServices<T> where T : class
	{
		IEnumerable<ViewTest> GetAll();
        IEnumerable<ViewTest> SearchName(string searchString);
        IEnumerable<T> Search(string searchString);
		IEnumerable<T> Filter(T t);
		int Insert(T t);
		int Update(T t);
		int Delete(int id);
		T Export_exam(int id);
		List<ViewDetailTest> GetById(int id);
        //T getId(int id);

    }
}
