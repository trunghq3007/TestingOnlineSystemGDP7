using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
  public  class Exam
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "name exam is not null")]
        public string NameExam { get; set; }
        [Required(ErrorMessage = "create by is not null")]
        public string CreateBy { get; set; }
        [Required(ErrorMessage = "question number is not null")]
        public int QuestionNumber { get; set; }
        [Required(ErrorMessage = "status is not null")]
        public bool Status { get; set; }
        public int SpaceQuestionNumber { get; set; }
        public virtual Category Category { get; set; }
        public DateTime? CreateAt { get; set; }
        public string Note { get; set; }
        //public virtual Category Categories { get; set; }
      
        public  ICollection<Test> Tests { get; set; }
        public virtual ICollection<ExamQuestion> ExamQuestions { get; set; }
    }
}
