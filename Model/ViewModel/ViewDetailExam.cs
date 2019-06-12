using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public class ViewDetailExam
    {
        public int Id { get; set; }

        public string NameExam { get; set; }

        public string CreateBy { get; set; }

        public int QuestionNumber { get; set; }

        public bool Status { get; set; }
        public int SpaceQuestionNumber { get; set; }
        public DateTime? CreateAt { get; set; }
        public string Note { get; set; }
        public string NameCategory { get; set; }
        public int CategoryId { get; set; }
        
    }
}
