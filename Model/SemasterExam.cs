using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
  public  class SemasterExam
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "status is not null")]
        public bool Status { get; set; }
        [Required(ErrorMessage = "semaster is not null")]
        public string SemasterName { get; set; }
        [Required(ErrorMessage = "create by is not null")]
        public string CreateBy { get; set; }
        public DateTime? StartDate { get; set; }

        public string Code { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
