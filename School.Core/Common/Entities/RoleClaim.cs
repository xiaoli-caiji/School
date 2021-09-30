using EntityConfigurationBase;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolCore.Entities
{
    public class RoleClaim :EntityBase<int>
    {
        public int RoleId { get; set; }
        ///<summary>
        ///成绩、学分、补助
        ///教龄、工资、
        ///</summary>
        public string RoleClaimType { get; set; }
       
        public string RoleClaimValue { get; set; }
        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }

    }
}
