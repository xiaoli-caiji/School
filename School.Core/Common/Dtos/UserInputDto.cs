using EntityConfigurationBase;
using System.ComponentModel;

namespace SchoolCore.Dtos
{
    public class UserInputDto:IDto
    {
        [DisplayName("学生学号/教职工编号")]
        public string UserCode { get; set; }
        [DisplayName("密码")]
        public string Password { get; set; }
    }
}
