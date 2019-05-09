using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Model
{
    public class SemasterExam
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SemasterExamId { get; set; }
        public bool Status { get; set; }
        [Required(ErrorMessage ="semaster name is not null")]
        public string SemasterName { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? StartDate { get; set; }
        [Required(ErrorMessage ="code is not null")]
        public string Code { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
