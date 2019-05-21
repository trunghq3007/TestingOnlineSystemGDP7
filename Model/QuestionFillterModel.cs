using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class QuestionFillterModel
    {
        public string Id { get; set; }
        public string CategoryId { get; set; }
        public string Content { get; set; }
        public string Level { get; set; }
        public string Suggestion { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Type { get; set; }
        public string Media { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string TagsId { get; set; }
        public string PageIndex { get; set; }
        public string PageSize { get; set; }
    }
}
