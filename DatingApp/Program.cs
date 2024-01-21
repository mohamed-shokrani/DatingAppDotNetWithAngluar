using CloudinaryDotNet;
using DatingApp.Data;
using DatingApp.Extensions;
using DatingApp.Helper;
using DatingApp.Interfaces;
using DatingApp.Middleware;
using DatingApp.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ServiceStack.Text;
using System.Configuration;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;


//private readonly IConfiguration _config;
var builder = WebApplication.CreateBuilder(args);
JsonSerializerOptions options = new()
{
    ReferenceHandler = ReferenceHandler.IgnoreCycles,
    WriteIndented = true
};
// ** How to add Database in Dot net 6 **
//var connectionString = builder.Configuration.GetConnectionString("AppDb");
//builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(connectionString));
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Add services to the container.
builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(AutoMpperProfiles).Assembly);
builder.Services.AddDbContext<DataContext>(Options => Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));
//builder.Services.AddDbContext<DataContext>(options =>
//{
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

//});
//builder.Services.AddDbContext();
var app = builder.Build();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseCors(x => x.AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()
    .WithOrigins("https://localhost:4200"));
app.UseHttpsRedirection();

app.UseAuthentication(); // must come before use authorization

app.UseAuthorization();

app.MapControllers();

app.Run();
using var scope = app.Services.CreateScope();// creating scope for the services that we are gonna create in this part
var services = scope.ServiceProvider; 
try
{
    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();
    await Seed.SeedUsers(context);

}
catch (Exception ex)
{
    var logger= services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An Error ocurred during migration ");
}



