using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Core.UserIndex.Entities
{
    public class NewsContent
    {
        public int NewsId { get; set; }

        [ForeignKey("NewsId")]
        public virtual News news { get; set; }
    }
}
