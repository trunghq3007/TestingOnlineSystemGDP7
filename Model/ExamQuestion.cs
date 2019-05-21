using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ExamQuestion
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public int Id { get; set; }
        [ForeignKey("Exam")]
        public int ExamId { get; set; }
        [ForeignKey("Question")]
        public int QuestionId { get; set; }
        public virtual Exam Exam { get; set; }
        public virtual Question Question { get; set; }
    }
}
