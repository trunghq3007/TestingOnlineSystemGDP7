using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Model
{

    [Table("Tag", Schema = "dbo")]
    public class Tag
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name tag is required.")]
        public string Name { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public virtual ICollection<Question> Questions { get; set; }

    }
}
