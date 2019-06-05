using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public class TestResult
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "test id is not null")]
        public int TestId { get; set; }
        [Required(ErrorMessage = "user id is not null")]
        [ForeignKey("User")]
        public int UserId { get; set; }
        [Required(ErrorMessage = "question id is not null")]
        public int QuestionId { get; set; }
        [Required(ErrorMessage = "answer id is not null")]
        public int AnwserId { get; set; }
        [Required(ErrorMessage = "score is not null")]
        public float Score { get; set; }
        public int TestTimeNo { get; set; }
        public string Content { get; set; }
        public string DescriptionName { get; set; }
        public virtual User User { get; set; }
        public virtual Test Test { get; set; }
    }
}
