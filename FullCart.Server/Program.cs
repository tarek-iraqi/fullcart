using FluentValidation;
using FluentValidation.AspNetCore;
using FullCart.Server.Application.Contracts;
using FullCart.Server.Domain.Enums;
using FullCart.Server.InfraStructure.Services;
using FullCart.Server.Persistence.Context;
using FullCart.Server.Persistence.DataSeed;
using FullCart.Server.Shared.BaseModels;
using FullCart.Server.Shared.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();
builder.Services.AddSingleton<IApplicationConfiguration, ApplicationConfiguration>();
builder.Services.AddScoped<UsersDataSeed>();

builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(config => config.TokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuerSigningKey = true,
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ClockSkew = TimeSpan.Zero,
    ValidIssuer = KeyValueConstants.Issuer,
    ValidAudience = KeyValueConstants.Audience,
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>(KeyValueConstants.SecretHashKey)!))
});

builder.Services.AddAuthorizationBuilder()
    .AddPolicy(AuthorizationPolicies.AdminPolicy, policy => policy.RequireAuthenticatedUser().RequireRole(UserRole.Admin.ToString()));

builder.Services.AddAuthorizationBuilder()
    .AddPolicy(AuthorizationPolicies.CustomerPolicy, policy => policy.RequireAuthenticatedUser().RequireRole(UserRole.Customer.ToString()));
        
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("AppConnectionString")));

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("System"));

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));


var app = builder.Build();

var logger = app.Services.GetRequiredService<ILogger<Program>>();

try
{
    var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await dbContext.Database.MigrateAsync();

    var usersDataSeed = scope.ServiceProvider.GetRequiredService<UsersDataSeed>();
    await usersDataSeed.SeedUsersData();
}
catch (Exception ex)
{
    logger.LogError($"Migration Error: {ex.Message}");
}

app.UseDefaultFiles();
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
