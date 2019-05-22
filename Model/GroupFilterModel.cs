using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class GroupFilterModel
    {
        public int  Id { get; set; }
        public int GroupId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? AddedStartDate { get; set; }
        public DateTime? AddedEndDate { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }

    }
}
