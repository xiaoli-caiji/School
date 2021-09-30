using EntityConfigurationBase;
using SchoolCore.Entities;
using System.Collections.Generic;
using System.ComponentModel;

namespace SchoolCore.Dtos
{
    public class UserOutputDto:IDto
    {
        //学号=》学院、年级、班级
        [DisplayName("学号/教职工编号")]
        public string UserCode { get; set; }

        [DisplayName("姓名")]
        public string Name { get; set; }

        [DisplayName("身份证号")]
        //身份证号=》年龄、生日、性别（第15位，奇男偶女）
        public string IdCardNumber { get; set; }

        [DisplayName("人员类别")]
        public Role UserRole { get; set; }

        [DisplayName("备注1")]
        public UserClaim UserClaims { get; set; }

        [DisplayName("备注2")]
        public RoleClaim RoleClaim { get; set; }
        [DisplayName("课程")]
        public List<Course> UserCourse { get; set; }
        [DisplayName("部门")]
        public List<Department> UserDepartment { get; set; }
        [DisplayName("联系方式")]
        public string PhoneNumber { get; set; }
        [DisplayName("学院")]
        public string UserAcademic { get; set; }
        [DisplayName("班级")]
        public string UserClass { get; set; }

    }
}
