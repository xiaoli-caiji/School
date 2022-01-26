using EntityConfigurationBase;
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
        public int Percentage { get; set; }
        public string CourseState { get; set; }
        public int PercentageLeft { get; set; }

    }
}
