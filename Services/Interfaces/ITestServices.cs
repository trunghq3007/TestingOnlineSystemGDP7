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
        IEnumerable<ViewTest> Search(string searchString);
        //IEnumerable<T> Search(string searchString);
		IEnumerable<T> Filter(T t);
		int Insert(T t);
		int Update(T t);
		int Delete(int id);
		T Export_exam(int id);
        List<object> GetById(int id, int Userid);
        T getByTestId(int id);

    }
}
