using SchoolCore.Entities;
using System.ComponentModel;

namespace SchoolCore.UserIndex.Dtos
{
    public class OfficeTeacherRegistrationDto
    {
        //学号=》学院、年级、班级
        [DisplayName("老师编号")]
        public string UserCode { get; set; }
        [DisplayName("姓名")]
        public string Name { get; set; }
        [DisplayName("身份证号")]
        //身份证号=》年龄、生日、性别（第15位，奇男偶女）
        public string IdCardNumber { get; set; }
        [DisplayName("联系方式")]
        public string PhoneNumber { get; set; }
        [DisplayName("学院")]
        public string Academic { get; set; }
        [DisplayName("部门")]
        public string Department { get; set; }
    }
}
