
namespace School.Core
{
    public class UserTokenDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string[] Role { get; set; }
        public string Exp { get; set; }
    }
}
