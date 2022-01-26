using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Core.UserIndex.Dtos.StudentsDtos
{
    public class CourseReportDto
    {
        [DisplayName("课程编号")]
        public string CourseCode { get; set; }
        [DisplayName("课程名称")]
        public string CourseName { get; set; }
        [DisplayName("学分")]
        public double? CourseCredits { get; set; }
        [DisplayName("成绩")]
        public double? Grades { get; set; }
    }
}
