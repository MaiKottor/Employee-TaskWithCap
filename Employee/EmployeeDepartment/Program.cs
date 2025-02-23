using EmployeeDepartment.Application.Services.Interfaces;
using EmployeeDepartment.Application.Services.Services;
using EmployeeDepartment.Domain.Interfaces;
using EmployeeDepartment.Infrastrcture.Persistence;
using EmployeeDepartment.Infrastrcture.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using MediatR;
using EmployeeDepartment.Application.Features.Employee.Querey;
using Microsoft.Extensions.DependencyInjection;
using DotNetCore.CAP;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add DbContext with SqlServer connection
builder.Services.AddDbContext<EmployeeContext>(options =>
    options.UseSqlServer(connectionString));

// Register the generic repository
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IEmployeeService, EmployeeService>(); // Register Service


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(GetAllEmployeesHandler).Assembly);
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy => policy.WithOrigins("http://localhost:4200")
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

builder.Services.AddCap(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); // Store messages
    options.UseRabbitMQ(cfg => {
        cfg.HostName = builder.Configuration["RabbitMQ:HostName"];
        cfg.UserName = builder.Configuration["RabbitMQ:UserName"];
        cfg.Password = builder.Configuration["RabbitMQ:Password"];
        cfg.Port = int.Parse(builder.Configuration["RabbitMQ:Port"]);
        }

    );
    options.DefaultGroupName = builder.Configuration["CAP:DefaultGroup"];
    options.UseDashboard();
});
    // Configure the HTTP request pipeline.
var app = builder.Build();
app.UseCors("AllowAngularApp");

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
if (app.Environment.IsDevelopment()) // Show Swagger only in development mode
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty; 
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapControllers();
app.Run();
