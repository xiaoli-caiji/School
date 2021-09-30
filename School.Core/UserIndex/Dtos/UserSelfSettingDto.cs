using SchoolCore.Entities;
using System.Collections.Generic;
using System.ComponentModel;

namespace SchoolCore.UserIndex.Dtos
{
    public class UserSelfSettingDto
    {
        public string UserCode { get; set; }
        
        [DisplayName("密码")]
        public string Password { get; set; }

        [DisplayName("姓名")]
        public string Name { get; set; }
        [DisplayName("性别")]
        public string Sex { get; set; }        

        [DisplayName("备注1")]
        public List<UserClaim> UserClaims { get; set; }
        
        [DisplayName("联系方式")]
        public string PhoneNumber { get; set; }
                     
    }
}
