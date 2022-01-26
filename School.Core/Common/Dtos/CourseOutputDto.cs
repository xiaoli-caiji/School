using EntityConfigurationBase;
using System.Collections.Generic;
using System.ComponentModel;

namespace SchoolCore.Dtos
{
    public class CourseOutputDto:IDto
    {
        [DisplayName("选择学院")]
        public string AcademicName { get; set; }

        [DisplayName("课程编号")]
        public string CourseCode { get; set; }

        [DisplayName("课程名称")]
        public string CourseName { get; set; }

        [DisplayName("课程容量")]
        public int CourseCapacity { get; set; }

        [DisplayName("授课教师")]
        public string TeachingTeacher { get; set; }

        [DisplayName("选课人数")]
        public int CourseChoosenNumber { get; set; }

        [DisplayName("开课学院")]
        public List<string> Academics { get; set; }

        [DisplayName("学时")]
        public double CourseHour { get; set; }

        [DisplayName("学分")]
        public double CourseCredit { get; set; }

        [DisplayName("上课时间")]
        public string CourseTime { get; set; }
        public string ChoosenOrNot { get; set; }
        public int Percentage { get; set; }
        public int PercentageLeft { get; set; }
        public string CourseState { get; set; }

    }
}
