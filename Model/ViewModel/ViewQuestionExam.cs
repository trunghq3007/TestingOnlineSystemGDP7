using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class ViewQuestionExam
    {


        public int QuesId { get; set; }
        public string nameExam { get; set; }
        public string Content { get; set; }
        public int Level { get; set; }
        public string Suggestion { get; set; }
        public int Type { get; set; }

        public int Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int Total { get; set; }
        public int ExamId { get; set; }
        public string CategoryName { get; set; }
        public int Space { get; set; }
    }
}
