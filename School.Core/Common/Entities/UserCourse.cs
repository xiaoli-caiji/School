using EntityConfigurationBase;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolCore.Entities
{
    public class UserCourse : EntityBase<int>
    {
        public int UserId { get; set; }
        public int CourseId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }

    }
}
