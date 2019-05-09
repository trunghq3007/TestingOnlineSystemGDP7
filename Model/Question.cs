using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{

    [Table("Question", Schema = "dbo")]
    public class Question
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public int Id { get; set; }
        public Category Category { get; set; }
        [Required(ErrorMessage = "Content question is required.")]
        public string Content { get; set; }
        public int Level { get; set; }
        public string Suggestion { get; set; }
        public int Type { get; set; }
        public string Media { get; set; }
        public int Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public ICollection<Answer> Answers { get; set; }
    }
}
