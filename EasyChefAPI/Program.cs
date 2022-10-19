using Data;
using Data.Contexts;
using Domain.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Repository.Bindings;
using Service.Bindings;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHealthChecks();
builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var mySqlConnection = builder.Configuration.GetSection("AppSettings")["ConnectionString"];

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(mySqlConnection));
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

ContextBindings.Configure(builder.Services);
RepositoryBindings.Configure(builder.Services);
ServiceBindings.Configure(builder.Services);

builder.Services.AddMvc();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "EasyChef API",
        Version = "v1"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header usando Bearer.\r\n\r\n Insira 'Bearer' e, em seguida, seu token na entrada de texto abaixo.\r\n\r\nExemplo: \"Bearer 12345abcdef",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });

    c.IncludeXmlComments(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EasyChefAPI.xml"));
    c.IncludeXmlComments(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Domain.xml"));
});

var app = builder.Build();

app.UseHealthChecks("/");
// Configure the HTTP request pipeline.
app.UseDeveloperExceptionPage();
app.UseSwagger();
app.UseSwaggerUI(x => { x.SwaggerEndpoint("/swagger/v1/swagger.yaml", "EasyChef API"); });

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();