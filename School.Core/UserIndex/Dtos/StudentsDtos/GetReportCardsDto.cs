using System.Collections.Generic;
using System.ComponentModel;

namespace School.Core.UserIndex.Dtos.StudentsDtos
{
    public class GetReportCardsDto
    {
        [DisplayName("课程编号")]
        public List<string> CourseCode { get; set; }
        [DisplayName("课程名称")]
        public List<string> CourseName { get; set; }
        [DisplayName("学分")]
        public List<double?> CourseCredits { get; set; }
        [DisplayName("成绩")]
        public List<double?> Grades { get; set; }
        [DisplayName("绩点")]
        public double? GPA { get; set; }
        [DisplayName("已获得学分")]
        public double? GotGrades { get; set; }
        [DisplayName("毕业要求学分")]
        public double? RequiredGrades { get; set; }
    }
}
