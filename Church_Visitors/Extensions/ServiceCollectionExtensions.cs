using Church_Visitors.Data;
using Church_Visitors.Interfaces;
using Church_Visitors.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Church_Visitors.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services, string connectionString, string databaseName)
        {
            services.AddSingleton(new DataContext(connectionString, databaseName));
            services.AddSingleton<IAnnouncementService, AnnouncementService>();
            services.AddSingleton<IVisitorService, VisitorService>();

            return services;
        }
    }
}
