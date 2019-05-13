using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
  public class Test
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int ExamId { get; set; }
        //[Required]
        //public int SemasterExamId { get; set; }
        
        public int Status { get; set; }
        public DateTime? StartDate { get; set; }
       
        public DateTime? EndDate { get; set; }
        [Required(ErrorMessage = "create by is not null")]
        public string CreateBy { get; set; }
        [Required(ErrorMessage = "pass score is not null")]
        public float PassScore { get; set; }
        [Required(ErrorMessage = "test name is not null")]
        public string TestName  { get; set; }
        [Required(ErrorMessage = "number of time is not null")]
        public int NumberTime { get; set; }
        [Required(ErrorMessage = "test time is not null")]
        public float TestTime { get; set; }
        public virtual ICollection<TestResult> TestResults { get; set; }
        public virtual Exam Exam { get; set; }
        public virtual SemesterExam SemesterExam { get; set; }

    }
}
