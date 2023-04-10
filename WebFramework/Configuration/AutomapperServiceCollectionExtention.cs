using AutoMapper;
using Mapper.Profiles.Incomes;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace WebFramework.Configuration
{
    public static class AutomapperServiceCollectionExtention
    {
        public static void AddAutomapperServiceConfiguration(this IServiceCollection service)
        {
            Assembly assembly = typeof(IncomeProfile).Assembly;
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<string, int>().ConvertUsing(s => Convert.ToInt32(s));
                cfg.AddMaps(assembly);
            });
            var mapper = configuration.CreateMapper();
            service.AddAutoMapper(assembly);
        }
    }
}
