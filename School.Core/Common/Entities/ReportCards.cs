using EntityConfigurationBase;
using SchoolCore.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.Core.Common.Entities
{
    /// <summary>
    /// 一个学生有一张成绩单
    /// 成绩单有多个学生，可以按照学生编号来区分
    /// 一张成绩单有多门课程
    /// </summary>
    public class ReportCards: EntityBase<int>
    {
        public int UserId { get; set; }
        public int CourseId { get; set; }
        [ForeignKey("UserId")]
        public virtual User Student { get; set; }
        [ForeignKey("CourseId")]
        public virtual Course Courses { get; set; }
        public double? Report { get; set; }
    }
}
