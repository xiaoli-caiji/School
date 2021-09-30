using EntityConfigurationBase;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolCore.Entities
{
    /// <summary>
    /// 用户声明表 （老师：ClaimType="teacher"）
    /// </summary>
    public class UserClaim :EntityBase<int>
    {
        public int UserId { get; set; }

        //年级、课程、奖学金
        public string UserClaimType { get; set; }

        public string UserClaimValue { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

    }
}
