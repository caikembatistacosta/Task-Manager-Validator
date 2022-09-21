using AutoMapper;
using BLL.Impl;
using BLL.Interfaces;
using DAO;
using DAO.Impl;
using DAO.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DemandasDbContext>(options => { 
    options.UseSqlServer("name=ConnectionStrings:CasaCaike");
    options.EnableSensitiveDataLogging();
    }) ;
builder.Services.AddTransient<IClienteService, ClienteService>();
builder.Services.AddTransient<IClienteDAO, ClienteDAO>();
builder.Services.AddTransient<IDemandaService,DemandaService>();
builder.Services.AddTransient<IDemandaDAO, DemandaDAO>();
builder.Services.AddTransient<IFuncionarioDAO, FuncionarioDAO>();
builder.Services.AddTransient<IFuncionarioService, FuncionarioService>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
