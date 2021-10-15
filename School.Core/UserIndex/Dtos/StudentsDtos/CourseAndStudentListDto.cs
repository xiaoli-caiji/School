using SchoolCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Core.UserIndex.Dtos.StudentsDtos
{
    public class CourseAndStudentListDto
    {
        public List<CourseAndStudenDto> CourseAndStudents { get; set; }
        public List<string> Courses { get; set; }
    }
}
