using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    interface ISemesterCustomerService<T1, T2, T3>
    {
        IEnumerable<T1> getAll();
        IEnumerable<T1> SeachCode(string code);
        IEnumerable<T2> getListExam(int id);
        object getDetailExam(int id);
    }
}