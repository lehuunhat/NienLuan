using HienTangToc.Data;
using HienTangToc.Helpers;
using HienTangToc.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers()
    .AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

// Enable Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policy => policy.WithOrigins("http://localhost:8080", "http://127.0.0.1:8080") // Update these URLs for your frontend
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

// Add database context with SQL Server connection
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyDb")));

// Add services to DI container
builder.Services.AddScoped<NguoiMuonService>();
builder.Services.AddScoped<NguoiHienService>();
builder.Services.AddScoped<SalonTocService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<PasswordHelper>();

// Add localization (if necessary)
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

// Build the application
var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();

app.UseStaticFiles();
app.UseDefaultFiles();

// Map controllers
app.MapControllers();

app.MapFallbackToFile("./Views/index.html");

app.Run();