using System.Configuration;
using Messenger.Core.DataBase;
using Messenger.Core.Options;
using Messenger.Core.Repositories;
using Messenger.Core.Services;
using Messenger.Core.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));

builder.Services.AddDbContext<DataBaseContext>(
    options => 
        options.UseNpgsql(
            builder.Configuration.GetConnectionString("DefaultConnection")
            )
        );

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPasswordUtils, PasswordUtils>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IJwtProvider, JwtProvider>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
