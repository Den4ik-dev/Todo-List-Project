using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using server.Dto;
using server.Models;
using server.Services.Authorization;
using server.Services.Todoes;
using server.Services.Tokens;
using server.Services.Users;
using server.Validation;

var builder = WebApplication.CreateBuilder(args);

// @cors
builder.Services.AddCors();

// @authentication & authorization
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
  .AddJwtBearer(options => options.TokenValidationParameters = 
    new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
      ValidateIssuer = true,
      ValidIssuer = builder.Configuration["JWT:ISSUER"],
      ValidateAudience = true,
      ValidAudience = builder.Configuration["JWT:AUDIENCE"],
      ValidateLifetime = true,
      ValidateIssuerSigningKey = true,
      IssuerSigningKey = new SymmetricSecurityKey(
        System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:KEY"]))
    });
builder.Services.AddAuthorization();

builder.Services.AddTransient<ITokenService, JwtTokenService>();
builder.Services.AddTransient<IUsersService, UsersService>();
builder.Services.AddTransient<ITodoesService, TodoesService>();

// @connect to database
string? connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<ApplicationContext>(options =>
  options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 34))));

// @validation
builder.Services.AddTransient<IValidator<LoginUser>, LoginUserValidator>();
builder.Services.AddTransient<IValidator<RegisteredUser>, RegisteredUserValidator>();
builder.Services.AddTransient<IValidator<ITodoDto>, TodoDtoValidator>();
builder.Services.AddTransient<IValidator<TokenDto>, TokenDtoValidator>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(options => options
  .WithOrigins("http://localhost:5173")
  .AllowCredentials()
  .AllowAnyMethod()
  .AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
