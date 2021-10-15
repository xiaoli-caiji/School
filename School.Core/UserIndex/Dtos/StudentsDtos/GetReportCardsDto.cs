using System.Collections.Generic;
using System.ComponentModel;

namespace School.Core.UserIndex.Dtos.StudentsDtos
{
    public class GetReportCardsDto
    {
        public List<CourseReportDto> Course { get; set; }
        
        [DisplayName("绩点")]
        public double? GPA { get; set; }
        [DisplayName("已获得学分")]
        public double? GotGrades { get; set; }
        [DisplayName("毕业要求学分")]
        public double? RequiredGrades { get; set; }
    }
}
