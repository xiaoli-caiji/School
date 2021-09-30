using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using School.Core.Repository;
using SchoolCore.Dtos;
using SchoolCore.Entities;
using System.Linq;

namespace School.Web.Controllers
{
    [Route("/api/[controller]/[action]")]
    public class TestController : Controller
    {
        private readonly IRepository<User,int> _repository;
        private readonly IMapper _mapper;
        public TestController(IRepository<User, int> repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost]
        public void GetNull(UserInputDto userInput)
        {
            var user = _mapper.Map<User>(userInput);
            var res = _repository.GetEntities<User>(u => u.UserCode == user.UserCode);
            var w = res.Select(u => u.UserRoles).ToList();
            
            //var res = 

        }
    }
}
