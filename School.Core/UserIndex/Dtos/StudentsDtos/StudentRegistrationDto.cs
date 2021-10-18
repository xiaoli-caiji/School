using SchoolCore.Entities;
using System.ComponentModel;

namespace SchoolCore.UserIndex.Dtos
{
    public class StudentRegistrationDto
    {
        //学号=》学院、年级、班级
        [DisplayName("学号")]
        public string UserCode { get; set; }
        [DisplayName("姓名")]
        public string Name { get; set; }
        [DisplayName("身份证号")]
        //身份证号=》年龄、生日、性别（第15位，奇男偶女）
        public string IdCardNumber { get; set; }
        //[DisplayName("备注1")]
        //public string UserClaims { get; set; }
        //[DisplayName("备注2")]
        ////指导老师可以在编辑这个的时候加进去吗？？
        //public string RoleClaims { get; set; }
        [DisplayName("联系方式")]
        public string PhoneNumber { get; set; }
        [DisplayName("学院")]
        public string Academic { get; set; }
        [DisplayName("班级")]
        public string Class { get; set; }               
    }
}
