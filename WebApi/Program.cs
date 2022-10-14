using BusinessLogicalLayer.Impl;
using BusinessLogicalLayer.Interfaces;
using DataAccessLayer;
using DataAccessLayer.Impl;
using DataAccessLayer.Interfaces;
using Entities;
using Entities.Enums;
using log4net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddCors();

builder.Services.Configure<Settings>(builder.Configuration.GetSection("Settings"));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DemandasDbContext>(options =>
{
    options.UseSqlServer("name=ConnectionStrings:Demanda");
});
builder.Logging.AddLog4Net();
//builder.Services.AddTransient<ILog, Log>();
builder.Services.AddTransient<IClienteService, ClienteService>();
builder.Services.AddTransient<IClienteDAO, ClienteDAO>();
builder.Services.AddTransient<IDemandaService, DemandaService>();
builder.Services.AddTransient<IDemandaDAO, DemandaDAO>();
builder.Services.AddTransient<IFuncionarioDAO, FuncionarioDAO>();
builder.Services.AddTransient<IFuncionarioService, FuncionarioService>();
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddTransient<ITokenDAO, TokenDAO>();
builder.Services.AddTransient<IClassValidatorService, ClassValidatorService>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSingleton<ILog>(LogManager.GetLogger(typeof(object)));
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Settings.Secret)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("RequireADM", p => p.RequireRole(NivelDeAcesso.Adm.ToString()));
    opt.AddPolicy("RequireFunc", p => p.RequireRole(NivelDeAcesso.Funcionario.ToString()));
    opt.AddPolicy("RequireFuncOrAdm", p => p.RequireRole(NivelDeAcesso.Funcionario.ToString(), NivelDeAcesso.Adm.ToString()));

});

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionMiddleware>();


app.Use(async (HttpContext httpContext, RequestDelegate requestDelegate) =>
{
    await requestDelegate(httpContext);
    if (httpContext.Response.StatusCode == (int)HttpStatusCode.Unauthorized || httpContext.Response.StatusCode == (int)HttpStatusCode.Forbidden)
    {
        ExceptionMiddleware exception = new(requestDelegate);
        await exception.InvokeAsync(httpContext);
    }
});
app.UseCors(op =>
{
    op.WithOrigins("https://localhost:7054/");
    op.AllowAnyMethod();
    op.AllowAnyHeader();
    op.AllowAnyOrigin();
});
app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
