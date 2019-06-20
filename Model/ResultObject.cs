using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ResultObject
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public int Success { get; set; }
        public ResultObject()
        {
            Status = 200;
            Success = -1;
            Data = null;
            Message = null;
        }
    }
}