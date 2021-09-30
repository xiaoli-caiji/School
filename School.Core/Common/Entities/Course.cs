using EntityConfigurationBase;
using System;
using System.Collections.Generic;

namespace SchoolCore.Entities
{
    public class Course : EntityBase<int>
    {
        public virtual User TeachingTeacher { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public int CourseCapacity { get; set; }
        public int CourseChoosenNumber { get; set; }
        public double CourseHour { get; set; }
        public double CourseCredit { get; set; }
        //录入时以逗号或别的符号区分开
        public string CourseTime { get; set; }
        public virtual ICollection<UserCourse> CourseMember { get; set; }
        public virtual ICollection<AcademicCourse> CourseAcademic { get; set; }
    }
}
