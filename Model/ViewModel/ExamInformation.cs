using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class ExamInformation
    {
        public int Id { get; set; }
        // tên bài thi
        public string TestName { get; set; }
        // chủ đề thi
        public string CategoryName { get; set; }
        // thời gian thi
        public float TestTime { get; set; }
        // tổng số câu hỏi
        public int QuestionNumber { get; set; }
        // tổng điểm
        public float TotalScore { get; set; }
        // số câu tự luận
        public int NumberStatementQuestion { get; set; }
        // số câu trắc nghiệm
        public int NumberChoiceQuestion { get; set; }
    }
}
