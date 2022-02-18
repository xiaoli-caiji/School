using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Core.UserIndex.Dtos.TeachingTeacherDtos
{
    public class StartCourseSelectionDto
    {
        public List<string> CourseCode { get; set; }
        public string ChooseRounds { get; set; }
    }
}
