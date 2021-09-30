using EntityConfigurationBase;
using SchoolCore.Entities;
using System.Collections.Generic;
using System.ComponentModel;

namespace SchoolCore.Dtos
{
    public class AclassOutputDto :IDto
    {
        [DisplayName("班级名称")]
        public string AClassName { get; set; }
        [DisplayName("班内学生")]
        public List<User> Students { get; set; }
        [DisplayName("隶属学院")]
        public string AcademicName { get; set; }
    }
}
