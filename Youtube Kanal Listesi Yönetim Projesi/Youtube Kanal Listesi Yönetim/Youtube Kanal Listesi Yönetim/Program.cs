using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using OfficeOpenXml;
using System.Reflection;
using Youtube_Kanal_Listesi_Yönetim.Data;
using Youtube_Kanal_Listesi_Yönetim.Services;

ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

var builder = WebApplication.CreateBuilder(args);

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
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "YouTube Kanal Yönetim API",
        Version = "v1",
        Description = "YouTube kanal bilgilerini yönetmek için API"
    });

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

});

app.MapGet("/", () => Results.Redirect("/swagger"));

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
