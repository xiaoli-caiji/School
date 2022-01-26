using EntityConfigurationBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Core.UserIndex.Entities
{
    public class NewsType:EntityBase<int>
    {
        public string NewsTypeType { get; set; }
        public string NewsTypeName { get; set; }
        public ICollection<News> News { get; set; }
    }
}
