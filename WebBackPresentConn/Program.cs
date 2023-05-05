using Microsoft.EntityFrameworkCore;
using PizzaOrderApi.Services.Implementations;
using PizzaOrderApi.Services.Interfaces;
using System.Text.Json.Serialization;
using WebBackPresentConn.Services.Implementations;
using WebBackPresentConn.Services.Interfaces;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("*");
            policy.AllowAnyHeader();
        });
});

builder.Services.AddDbContext<PizzaOrderContext>(options =>
        options.UseInMemoryDatabase("PizzaOrders"));

builder.Services.AddScoped<IPizzaOrderService, PizzaOrderService>();
builder.Services.AddScoped<IToppingsService, ToppingsService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCors();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
