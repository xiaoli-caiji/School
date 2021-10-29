using System.ComponentModel;


namespace SchoolCore.UserIndex.Dtos
{
    public class UserSelfSettingDto
    {
        [DisplayName("姓名")]
        public string Name { get; set; }
        [DisplayName("性别")]
        public string Gender { get; set; }
        [DisplayName("生日")]
        public string BirthDate { get; set; }
        [DisplayName("头像")]
        public string HeadImg { get; set; }

        [DisplayName("账号")]
        public string UserCode { get; set; } 

        //[DisplayName("备注1")]
        //public List<UserClaim> UserClaims { get; set; }
        
        [DisplayName("联系方式")]
        public string PhoneNumber { get; set; }
                     
    }
}
