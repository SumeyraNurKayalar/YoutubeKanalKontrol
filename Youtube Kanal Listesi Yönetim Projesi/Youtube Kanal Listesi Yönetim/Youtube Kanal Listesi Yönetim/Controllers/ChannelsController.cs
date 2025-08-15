namespace Youtube_Kanal_Listesi_Yönetim.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.OpenApi.Models;
    using OfficeOpenXml;
    using System.Reflection;
    using Youtube_Kanal_Listesi_Yönetim.Data;
    using Youtube_Kanal_Listesi_Yönetim.Services;
    using Swashbuckle.AspNetCore.Filters;
    using Youtube_Kanal_Listesi_Yönetim.Examples;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            builder.Services.AddSingleton<IAppDbContext>(provider =>
                new AppDbContext(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IChannelService, ChannelService>();
            builder.Services.AddScoped<CsService, CsService>();
            builder.Services.AddScoped<ExcelService, ExcelService>();

            builder.Services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerExamplesFromAssemblyOf<ChannelListExample>();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "YouTube Kanal Yönetim API",
                    Version = "v1",
                    Description = "YouTube kanallarını CSV/Excel üzerinden içe/dışa aktarma, listeleme, filtreleme ve sıralama için API",
                    Contact = new OpenApiContact
                    {
                        Name = "Proje Ekibi",
                        Email = "destek@youtubeapi.com"
                    }
                });

                c.ExampleFilters();

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (File.Exists(xmlPath))
                {
                    c.IncludeXmlComments(xmlPath);
                }
            });

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "YouTube Kanal API v1");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
