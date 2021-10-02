using SchoolCore.Entities;
using System.ComponentModel;

namespace School.Core.UserIndex.Dtos
{
    public class InputReportCardsDto
    {
        [DisplayName("课程名")]
        public string CourseName { get; set; }

        [DisplayName("学生学号")]
        public string StudentCode { get; set; }
        [DisplayName("学生姓名")]
        public string StudentName { get; set; }
        [DisplayName("成绩")]
        public double? Grades { get; set; }
    }
}
