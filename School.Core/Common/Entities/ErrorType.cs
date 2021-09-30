using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.Core.Common.Entities
{
    public class ErrorType
    {
        [Key,DisplayName("错误编号"),DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ErrorCode { get; set; }
        [DisplayName("错误类型")]
        public string ErrorReason { get; set; }
    }
}
