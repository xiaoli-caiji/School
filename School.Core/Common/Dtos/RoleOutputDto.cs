using EntityConfigurationBase;
using SchoolCore.Entities;
using System.Collections.Generic;
using System.ComponentModel;

namespace SchoolCore.Dtos
{
    public class RoleOutputDto:IDto
    {
        [DisplayName("角色名")]
        public string RoleName { get; set; }

        [DisplayName("角色成员")]
        public List<UserRole> RoleMembers { get; set; }

        [DisplayName("角色备注")]
        public RoleClaim RoleClaim { get; set; }

        [DisplayName("角色类型")]
        public string RoleType { get; set; }

    }
}
