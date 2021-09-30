using EntityConfigurationBase;
using System;
using System.Collections.Generic;

namespace SchoolCore.Entities
{
    public class AClass : EntityBase<int>
    {
        /// <summary>
        /// 班级上一级是学院
        /// </summary>       
        public string AClassName { get; set; }
        public virtual ICollection<User> AclassUsers { get; set; }
        public virtual Academic Academic { get; set; }
      
    }
}
