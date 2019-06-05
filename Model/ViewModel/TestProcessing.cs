using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class TestProcessing
    {
        public int Id { get; set; }
        public string TestName { get; set; }
        public float TestTime { get; set; }
        public ICollection<Question> questions  { get; set; }
    }
}
