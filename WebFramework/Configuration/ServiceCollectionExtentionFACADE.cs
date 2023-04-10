using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace WebFramework.Configuration
{
    public static class ServiceCollectionExtentionFACADE
    {
        private static string nameSpaceName = typeof(ServiceCollectionExtentionFACADE).Namespace;
        private static string serviceCollectionExtentionFACADEName = typeof(ServiceCollectionExtentionFACADE).Name;
        public static void AddServiceExtention(this IServiceCollection service)
        {
            var assembly = Assembly.GetExecutingAssembly();
            service.getServiceExtentions(assembly);
        }
        public static void getServiceExtentions(this IServiceCollection services, params Assembly[] assemblies)
        {
            var extentionsClassess = assemblies.SelectMany(x => x.DefinedTypes).Where(x => x.Namespace == nameSpaceName && x.Name != serviceCollectionExtentionFACADEName
           && x.IsClass && !x.Name.Contains("<>")
            );

            foreach (var extentionClass in extentionsClassess)
            {
                var methods = extentionClass.GetMethods().Where(x => x.IsStatic).FirstOrDefault();
                methods.Invoke(services, new object[] { services });
            }
        }
    }
}
