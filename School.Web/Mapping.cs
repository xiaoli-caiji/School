using AutoMapper;
using EntityConfigurationBase;
using SchoolCore.Dtos;
using SchoolCore.Entities;
using SchoolCore.UserIndex.Dtos;
using System;

namespace School.Web.MappingMapper
{
    public class MapperProfile : Profile, IEntity<int>,IDto
    {
        public MapperProfile()
        {
            CreateMap<TeachingTeacherRegistrationDto, User>();
            CreateMap<User, TeachingTeacherRegistrationDto>();
        }
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
