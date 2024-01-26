using BLL.Services;
using Cloud_APIDemo.Hubs;
using Cloud_APIDemo.Services;
using Cloud_APIDemo.Tools;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.Swagger;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

/*
 * pour que les commentaires <summary> des controleurs apparaissent dans la doc Swagger
 * Ajouter au minimum 
 * var filePath = Path.Combine(System.AppContext.BaseDirectory, "swagger.xml");
    c.IncludeXmlComments(filePath);
 */
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = "Ma super api de fou",
            Version = "v1",
            Contact = new Microsoft.OpenApi.Models.OpenApiContact() { Email = "steve@sieste.com" }
        }
     );

    var filePath = Path.Combine(System.AppContext.BaseDirectory, "swagger.xml");
    c.IncludeXmlComments(filePath);
});

builder.Services.AddScoped<IUserRepository, UserRepositoryDb>(sp =>
    new UserRepositoryDb(builder.Configuration.GetConnectionString("DevNetCloudDB")));

builder.Services.AddScoped<UserService>();
builder.Services.AddSingleton<MovieService>();

builder.Services.AddSingleton<ChatHub>();
builder.Services.AddSingleton<MovieHub>();

builder.Services.AddAuthorization(option =>
   {
       option.AddPolicy("adminPolicy", o => o.RequireRole("admin"));
       option.AddPolicy("connected", o => o.RequireAuthenticatedUser());
   });
//nécéssite le package Microsoft.AspNetCore.Authentication.JwtBearer
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenManager.key)),
            ValidateLifetime = true,
            ValidateIssuer = false,
            ValidateAudience = false
    };
    }
    );

builder.Services.AddSignalR();

builder.Services.AddCors(o => o.AddPolicy("signalRBlazor", p =>
    p.WithOrigins("https://localhost:7139").AllowCredentials().AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseCors(o => o.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseCors("signalRBlazor");

//Bien déclarer dans cet ordre
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapHub<ChatHub>("chathub");
app.MapHub<MovieHub>("moviehub");

app.Run();
