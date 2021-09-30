using AutoMapper;
using EntityConfigurationBase;
using SchoolCore.Dtos;
using SchoolCore.Entities;
using System;

namespace School.Web.MappingMapper
{
    public class MapperProfile : Profile, IEntity<int>,IDto
    {
        public MapperProfile()
        {
            CreateMap<UserInputDto, User>();
            CreateMap<User, UserOutputDto>();
        }
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
