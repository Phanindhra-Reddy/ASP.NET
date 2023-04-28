using System.Text;
using JWTAuthentication.Database;
using JWTAuthentication.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IDatabaseSettings>(db => db.GetRequiredService<IOptions<DatabaseSettings>>().Value);
builder.Services.AddScoped<StudentService>();
builder.Services.AddScoped<UserService>();
builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("ConnectionStrings")
);
builder.Services.AddAuthentication(x => {
x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x=>
{
x.RequireHttpsMetadata = false;
x.SaveToken = true;
x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
{
    // ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetSection("JwtKey").Value)),
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidAudience = builder.Configuration.GetSection("ValidateAudiance").Value,  
    ValidIssuer = builder.Configuration.GetSection("ValidateIssuer").Value, 
};
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
