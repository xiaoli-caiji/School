using EntityConfigurationBase;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolCore.Entities
{
    public class AcademicCourse : EntityBase<int>
    {
        public int AcademicId { get; set; }
        public int CourseId { get; set; }
        [ForeignKey("AcademicId")]
        public virtual Academic Academic { get; set; }
        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }

    }
}
