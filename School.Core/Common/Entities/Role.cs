using EntityConfigurationBase;
using System;
using System.Collections.Generic;

namespace SchoolCore.Entities
{
    public class Role:EntityBase<int>
    {
        public string RoleName { get; set; }
        /// <summary>
        /// 老师：teacher；学生：student 可自定义，带职责的职工，比如食堂大妈
        /// </summary>
        public string RoleType { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        //public List<UserRole> UserRoles { get; set; }
        public virtual ICollection<RoleClaim> RoleClaims { get; set; }
        //public List<RoleClaim> RoleClaims { get; set; }
    }
}
