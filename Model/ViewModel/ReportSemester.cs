using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class ReportSemester
    {
        public ReportSemester()
        {
        }

        public ReportSemester(string semesterName, string creator, string startDay, string endDay, int numEXams)
        {
            SemesterName = semesterName;
            Creator = creator;
            StartDay = startDay;
            EndDay = endDay;
            NumEXams = numEXams;
        }

        public string SemesterName { get; set; }
        public string Creator { get; set; }
        
        public  string StartDay { get; set; }
        public string EndDay { get; set; }
        public int  NumEXams { get; set; }
        public int NumQuestions { get; set; }
        public int  NumCandiates { get; set; }
        public int NotParticipation { get; set; }
        public float AvgScore { get; set; }
        public int Low { get; set; }
        public int Good { get; set; }
        public int Medium { get; set; }
    }
}
