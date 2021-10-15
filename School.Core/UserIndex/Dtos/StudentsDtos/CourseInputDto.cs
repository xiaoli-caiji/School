using System.ComponentModel;

namespace School.Core.UserIndex.Dtos.StudentsDtos
{
    public class CourseInputDto
    {
        [DisplayName("选择学院")]
        public string AcademicName { get; set; }

        [DisplayName("课程名称")]
        public string CourseName { get; set; }

        [DisplayName("授课教师")]
        public string TeachingTeacher { get; set; }
        public string UserCode { get; set; }
    }
}
