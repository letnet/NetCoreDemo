using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreTemplate.Infrastructure
{
    public static class AutoMapperApplicationBuilderExtensions
    {
        private static IMapper _mapper { set; get; }
        public static void UseStateAutoMapper(this IApplicationBuilder applicationBuilder)
        {
            var serviceProvider = applicationBuilder.ApplicationServices;
            _mapper = serviceProvider.GetRequiredService<IMapper>();
        }

        public static TDestination Map<TDestination>(object source)
        {
            return _mapper.Map<TDestination>(source);
        }

        public static TDestination Map<TSource, TDestination>(TSource source)
        {
            return _mapper.Map<TSource, TDestination>(source);
        }

        public static TDestination MapTo<TSource, TDestination>(this TSource source)
        {
            return _mapper.Map<TSource, TDestination>(source);
        }

        public static TDestination MapTo<TDestination>(this object source)
        {
            return _mapper.Map<TDestination>(source);
        }

        public static List<TDestination> MapToList<TDestination>(this IEnumerable source)
        {
            return _mapper.Map<List<TDestination>>(source);
        }

        public static List<TDestination> MapToList<TSource, TDestination>(this IEnumerable<TSource> source)
        {
            return _mapper.Map<List<TDestination>>(source);
        }
    }
}
