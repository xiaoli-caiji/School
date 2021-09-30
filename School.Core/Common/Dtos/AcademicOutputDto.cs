using EntityConfigurationBase;
using SchoolCore.Entities;
using System.Collections.Generic;
using System.ComponentModel;

namespace SchoolCore.Dtos
{
    public class AcademicOutputDto : IDto
    {
        [DisplayName("学院名称")]
        public string AcademicName { get; set; }


        [DisplayName("所属学校")]
        public string AcademicParentName { get; set; }


        [DisplayName("院内名师")]
        public List<User> Teachers { get; set; }


        [DisplayName("开设课程")]
        public List<Course> Courses { get; set; }
    }
}
