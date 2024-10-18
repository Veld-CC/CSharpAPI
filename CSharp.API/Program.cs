
using CShapr.API.Helpers;
using CSharp.API.Extensions;
using CSharp.Models;
using GSM.Data;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;
using System;
using System.IO.Compression;
using System.Reflection;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");


try
{
    var builder = WebApplication.CreateBuilder(args);
    var _allowOrigin = "AllowOrigin";

    // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    // ================================
    // Add services to the container.
    // ================================

    // Add allow origins
    builder.Services.AddCors(_ =>
                    _.AddPolicy(_allowOrigin, builder => builder.
                        AllowAnyHeader().
                        AllowAnyMethod().
                        AllowAnyOrigin())
                );

    // Se agrega el servicio de compresion de respuesta
    builder.Services.AddResponseCompression(options =>
    {
        options.EnableForHttps = true;
        options.Providers.Add<GzipCompressionProvider>();
        options.MimeTypes =
            ResponseCompressionDefaults.MimeTypes.Concat(
                new[] { "application/json", "application/octet-stream" });
    });

    //  Configurar el nivel de compresion
    builder.Services.Configure<GzipCompressionProviderOptions>(options =>
    {
        options.Level = CompressionLevel.SmallestSize;
    });


    // Add database connection

    IConfigurationSection connectionStringSection;

#if DEBUG
    connectionStringSection = builder.Configuration.GetSection($"Development:ConnectionStrings");
#else
    connectionStringSection = builder.Configuration.GetSection($"Production:ConnectionStrings");
#endif

    var connectionsStrings = connectionStringSection.GetChildren();
    List<Database> conns = new List<Database>();

    foreach (var connection in connectionsStrings)
        conns.Add(new Database { Name = connection.Key, ConnectionString = connection.Value });

    var sqlConnectionConf = new SqlConfigurationData(conns);
    builder.Services.AddSingleton(sqlConnectionConf);

    // Add action filter in scoped, created once per request.
    builder.Services.AddScoped<ValidationFilterAttribute>();

    // Dependencias de la capa de datos
    builder.Services.ConfigureDataLayerServices();

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "CSHarp API",
            Description = "An ASP.NET Core Web API",
            //TermsOfService = new Uri("https://example.com/terms"),
            //Contact = new OpenApiContact
            //{
            //    Name = "Osmar",
            //    Url = new Uri("")
            //}
            //,License = new OpenApiLicense
            //{
            //    Name = "Example License",
            //    Url = new Uri("https://example.com/license")
            //}
        });

        //var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        //options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    });

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseCors(_allowOrigin);

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception exception)
{
    // NLog: catch setup errors
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}