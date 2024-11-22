using DomoExtrato.Domain.Interfaces;
using DomoExtrato.Domain.Mappings;
using DomoExtrato.Infra.CrossCutting.ExtensionMethods;
using DomoExtrato.Infra.Data.Connection;
using DomoExtrato.Infra.Data.Repositories;
using DomoExtrato.Service.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Sinks.Elasticsearch;
using Serilog;
using Serilog.Exceptions;
using DinkToPdf.Contracts;
using DinkToPdf;


namespace DomoExtrato.Infra.IoC
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IPeriodosService, PeriodosService>();
            services.AddScoped<IPeriodosRepository, PeriodosRepository>();
            services.AddScoped<IConnectionFactory, ConnectionFactory>();
            services.AddScoped<IExtratoService, ExtratoService>();
            services.AddScoped<IExtratoRepository, ExtratoRepository>();
            services.AddAutoMapper(typeof(DtoMappingToProfileDomain));
            services.AddSingleton<IConverter, SynchronizedConverter>();
            services.AddSingleton<ITools, PdfTools>();
            ConfigureLogging();

            ExatractConfiguration.Initialize(configuration);
        }

        public static void ConfigureLogging()
        {
            var environment = "Development";
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings{environment}.json", optional: true)
                .Build();

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .WriteTo.Debug()
                .WriteTo.Console()
                .WriteTo.Elasticsearch(ConfigureElasticSkin(configuration, environment))
                .Enrich.WithProperty("Environment", environment)
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }

        static ElasticsearchSinkOptions ConfigureElasticSkin(IConfigurationRoot configuration, string environment)
        {
            var elasticsearchSinkOptions = new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
            {
                AutoRegisterTemplate = true,
                IndexFormat = configuration["ElasticConfiguration:Index"],
                NumberOfReplicas = 1,
                NumberOfShards = 2
            };

            return elasticsearchSinkOptions;
        }
    }
}
