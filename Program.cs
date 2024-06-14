using ApiCatalog.Context;
using ApiCatalog.DTOs.Mappings;
using ApiCatalog.Extensions;
using ApiCatalog.Filters;
using ApiCatalog.Models;
using ApiCatalog.Repositories;
using ApiCatalog.Repositories.Interfaces;
using ApiCatalog.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)));

var KeyOfAppSetting = builder.Configuration["key1"];

//JWT AUTHENTICATION
builder.Services.AddAuthorization();
//builder.Services.AddAuthentication("Bearer").AddJwtBearer();

var secretKey = builder.Configuration["JWT:SecretKey"] ?? throw new ArgumentException("Invalidade secret key!!");

builder.Services.AddAuthentication(options =>
{
    
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // this configured this method to default Authentication method used in this applicatoins
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; //if someone try to access protected endpont without token, applications will ask for token
}).AddJwtBearer(options =>  //Especify configuration of bearer scheme authentication
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters() // here is parameter used to validate token
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    };
});
//
builder.Services.AddTransient<IMyService, MyService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddAutoMapper(typeof(ProductDTOMappingProfile));
builder.Services.AddScoped<ApiLoggingFilter>();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
builder.Services.AddScoped<ITokenService, TokenService>();


//Used to disable FromService parameters on actions
//
//builder.Services.Configure<ApiBehaviorOptions>(options =>
//    {
//        options.DisableImplicitFromServicesParameters = true;
//    }
//);

//CORS----------
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "originWithAllowedAccess", policy => { policy.WithOrigins("http://localhost").AllowAnyMethod().AllowAnyHeader(); });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ConfigureExceptionsHandler();
}

app.UseHttpsRedirection();
app.UseCors("originWithAllowedAccess");
app.UseAuthorization();

app.MapControllers();

app.Run();
