using EntityConfigurationBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolCore.Entities
{
    public partial class User : EntityBase<int>
    {       
        public string Name { get; set; }
        public string UserCode { get; set; }
        public string Password { get;set; }
        public string Sex { get; set; }
        public string IdCardNumber { get; set; }
        public int Age { get; set; }
        public virtual Academic UserAcademic { get; set; }
        public string PhoneNumber { get; set; }
        public virtual ICollection<UserClaim> UserClaims { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<UserCourse> UserCourse { get; set; }
        //public Guid Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
