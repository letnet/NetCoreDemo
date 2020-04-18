using AutoMapper;
using NetCoreTemplate.Application.Models;
using NetCoreTemplate.Domain.Entitys;
using NetCoreTemplate.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreTemplate.Application
{
    public class BasicProfile : Profile
    {
        public BasicProfile()
        {
            CreateMap<TestDto, Test>();
            CreateMap<Test, TestDto>();
        }
    }
}
