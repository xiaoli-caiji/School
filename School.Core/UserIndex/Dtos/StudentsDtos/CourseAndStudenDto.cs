using SchoolCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Core.UserIndex.Dtos.StudentsDtos
{
    public class CourseAndStudenDto
    {
        public List<User> Students { get; set; }
        public string CourseName { get; set; }
    }
}
