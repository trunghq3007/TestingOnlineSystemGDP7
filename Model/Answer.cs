using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{

    [Table("Answer", Schema = "dbo")]
    public class Answer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public int Id { get; set; }
        public virtual Question Question { get; set; }
        [Required(ErrorMessage = "Content answer is required.")]
        public string Content { get; set; }
        public string Media { get; set; }
        public int Status { get; set; }
        public bool IsTrue { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
