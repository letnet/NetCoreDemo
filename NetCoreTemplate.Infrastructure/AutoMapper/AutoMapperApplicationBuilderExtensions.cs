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
        private static IServiceProvider _serviceProvider;
        public static void UseStateAutoMapper(this IApplicationBuilder applicationBuilder)
        {
            _serviceProvider = applicationBuilder.ApplicationServices;
        }

        public static TDestination Map<TDestination>(object source)
        {
            var mapper = _serviceProvider.GetRequiredService<IMapper>();
            return mapper.Map<TDestination>(source);
        }

        public static TDestination Map<TSource, TDestination>(TSource source)
        {
            var mapper = _serviceProvider.GetRequiredService<IMapper>();

            return mapper.Map<TSource, TDestination>(source);
        }

        public static TDestination MapTo<TSource, TDestination>(this TSource source)
        {
            var mapper = _serviceProvider.GetRequiredService<IMapper>();
            return mapper.Map<TSource, TDestination>(source);
        }

        public static TDestination MapTo<TDestination>(this object source)
        {
            var mapper = _serviceProvider.GetRequiredService<IMapper>();
            return mapper.Map<TDestination>(source);
        }

        public static List<TDestination> MapToList<TDestination>(this IEnumerable source)
        {
            var mapper = _serviceProvider.GetRequiredService<IMapper>();
            return mapper.Map<List<TDestination>>(source);
        }

        public static List<TDestination> MapToList<TSource, TDestination>(this IEnumerable<TSource> source)
        {
            var mapper = _serviceProvider.GetRequiredService<IMapper>();
            return mapper.Map<List<TDestination>>(source);
        }
    }
}
