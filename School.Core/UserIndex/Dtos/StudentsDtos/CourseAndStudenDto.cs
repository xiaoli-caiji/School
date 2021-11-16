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
        public string StudentName { get; set; }
        public string StudentCode { get; set; }
        public double? Report { get; set; }
    }
}
