using SchoolCore.Entities;
using System.Collections.Generic;
using System.ComponentModel;

namespace SchoolCore.UserIndex.Dtos
{
    public class UserSelfSettingDto
    {
        [DisplayName("密码")]
        public string Password { get; set; }     

        [DisplayName("备注1")]
        public List<UserClaim> UserClaims { get; set; }
        
        [DisplayName("联系方式")]
        public string PhoneNumber { get; set; }
                     
    }
}
