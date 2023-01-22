using Data.AutoMapper;
using Data.Context;
using Data.Models;
using Data.Repositories.Classes;
using Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Services.ServiceClasses;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Connect repositories
builder.Services.AddScoped<UserRepository, UserRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();

// Connect services
builder.Services.AddTransient<UserService>();
builder.Services.AddTransient<CategoryService>();
builder.Services.AddTransient<TicketService>();
builder.Services.AddTransient<JwtTokenService>();


builder.Services.AddAutoMapper(typeof(AutoMapperConfiguration));


// Authorization
var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("JwtOptions:JwtKey").Value);
var tokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(key),
    ValidateIssuer = false,
    ValidateAudience = false,
    ValidateLifetime = false,
    RequireExpirationTime = true,

    // Allow to use seconds for expiration of token
    // Required only when token lifetime less than 5 minutes
    // THIS ONE
    ClockSkew = TimeSpan.Zero
};
builder.Services.AddSingleton(tokenValidationParameters);
builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(jwt => {
    jwt.SaveToken = true;
    jwt.TokenValidationParameters = tokenValidationParameters;
});


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Identity
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedEmail = false;
    options.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
