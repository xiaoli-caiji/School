using EntityConfigurationBase;
using System;
using System.Collections.Generic;

namespace SchoolCore.Entities
{
    public class Course : EntityBase<int>
    {
        //一门课程可在多个学院开设，所以这里是课程编号不唯一，Id和课程名唯一；
        //课程名格式： 课程名称+学院简称+班号，eg：大学物理信通1班
        //感觉思想不合理，问同事
        public virtual User TeachingTeacher { get; set; }
        public string CourseName { get; set; }

        /// <summary>
        /// CourseCode 前四位表示该课程，比如大学物理
        /// 第五、六位表示学院，第七、八位表示班
        /// 例如： 00010101 可以表示为大学物理（0001）信通（01）一班（01）
        /// </summary>
        public string CourseCode { get; set; }
        public int CourseCapacity { get; set; }

        // 第一轮或者第二轮开始之前的选课人数统计（第一轮结束之后该数据清零）
        public int CourseChoosenNumber { get; set; }
        public int CourseSelectionNumber { get; set; }
        public double CourseHour { get; set; }
        public double CourseCredit { get; set; }
        // 例如： 1-8周，周一周三周五，3-4节
        public string CourseTime { get; set; }
        public string ChooseRounds { get; set; }
        public virtual ICollection<UserCourse> CourseMember { get; set; }
        public virtual ICollection<AcademicCourse> CourseAcademic { get; set; }
    }
}
