using Church_Visitors.Services;
using Microsoft.Extensions.DependencyInjection;
using Church_Visitors.Repositories;
using Church_Visitors.Interfaces;
using System.Net.Http;
using Amazon.Runtime;

namespace Church_Visitors.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services, string apiUrl)
        {
            services.AddTransient<IVisitorService, VisitorService>();
            services.AddTransient<IAnnouncementService, AnnouncementService>();

            // Register a named HttpClient with the base address
            services.AddHttpClient("DefaultHttpClient", client =>
            {
                client.BaseAddress = new Uri(apiUrl);
            });

            // Register repositories with the named HttpClient
            services.AddTransient<IVisitorRepository>(provider =>
            {
                var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
                var httpClient = httpClientFactory.CreateClient("DefaultHttpClient");
                return new VisitorRepository(httpClient);
            });

            services.AddTransient<IAnnouncementRepository>(provider =>
            {
                var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
                var httpClient = httpClientFactory.CreateClient("DefaultHttpClient");
                return new AnnouncementRepository(httpClient);
            });

            return services;
        }
    }
}
