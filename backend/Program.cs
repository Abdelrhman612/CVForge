using System.Text;
using backend.DataBase;
using backend.InterFaces;
using backend.Repo.Auth;
using backend.Services;
using backend.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(op =>
{
    var ConnectionsString = builder.Configuration.GetConnectionString("DefaultConnection");
    op.UseSqlServer(ConnectionsString);


});

var JWT = builder.Configuration.GetSection("JWT").Get<Jwt>();
if (JWT is not null)
{
    builder.Services.AddSingleton(JWT);
    builder.Services.AddSingleton<IJwtService>(prov => new JwtService(JWT));
}

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = JWT?.Issuer,
            ValidAudience = JWT?.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWT?.Key ?? string.Empty)),
            ClockSkew = TimeSpan.Zero
        };
    });


builder.Services.AddScoped<IAuthRepo, AuthRepo>();
builder.Services.AddScoped<IAuthService, AuthService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.MapGet("/", () => "Server Is Running ...");
app.Run();
