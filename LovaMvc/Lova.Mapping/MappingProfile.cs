using AutoMapper;
using System;

namespace Lova.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Entities.sys_category, Sys_CategoryMapping>();
            CreateMap<Sys_CategoryMapping, Entities.sys_category>();

        }
    }
}
