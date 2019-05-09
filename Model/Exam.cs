using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Model
{
    public class Exam
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExamId { get; set; }
        [Required(ErrorMessage ="exam name is not null")]
        public string NameExam { get; set; }
        [Required(ErrorMessage ="created by is not null")]
        public string CreatedBy { get; set; }
        [Required(ErrorMessage ="question number is not null")]
        public int QuestionNumber { get; set; }
        public bool Status { get; set; }
        [Required(ErrorMessage ="type exam is not null")]
        public string TypeExam { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string Note { get; set; }
    }
}
