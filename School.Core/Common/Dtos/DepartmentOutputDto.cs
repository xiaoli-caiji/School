using EntityConfigurationBase;
using SchoolCore.Entities;
using System.Collections.Generic;
using System.ComponentModel;

namespace SchoolCore.Dtos
{
    public class DepartmentOutputDto:IDto
    {
        [DisplayName("部门名称")]
        public string DepartmentName { get; set; }
        [DisplayName("上级部门")]
        public string ParentDepartment { get; set; }
        [DisplayName("部门职责")]
        public string DepartmentFunctions { get; set; }
        [DisplayName("部门人员")]
        public List<User> DepartmentPerson { get; set; }
    }
}
