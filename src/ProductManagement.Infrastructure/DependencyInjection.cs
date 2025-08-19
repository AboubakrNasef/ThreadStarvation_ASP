using Microsoft.Extensions.DependencyInjection;
using ProductManagement.Infrastructure.FileStore;
using ProductManagement.Infrastructure.Repositories;
using MongoDB.Driver;
using ProductManagement.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace ProductManagement.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // MongoDB
            services.AddSingleton<IMongoDatabase>(sp =>
            {
                var config = configuration.GetSection("MongoDb");
                var client = new MongoClient(config["ConnectionString"]);
                return client.GetDatabase(config["Database"]);
            });

            // Azure Blob
            services.AddSingleton<AzureBlobFileStore>(sp =>
            {
                var config = configuration.GetSection("AzureBlob");
                return new AzureBlobFileStore(config["ConnectionString"], config["Container"]);
            });

            // Repository and FileStore abstractions
            services.AddScoped<IProductRepository, MongoProductRepository>();
            services.AddScoped<IFileStore>(sp => sp.GetRequiredService<AzureBlobFileStore>());

            return services;
        }
    }
}
