using Microsoft.EntityFrameworkCore;
using Vuelos.BLL.Service;
using Vuelos.BLL.ServiceApi;
using Vuelos.BLL.ServiceRepository;
using Vuelos.BLL.ServiceRepository.Interfaces;
using Vuelos.DAL.DataContext;
using Vuelos.DAL.Repository;
using Vuelos.DAL.ServiceApi;
using Vuelos.Models.Entity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<VuelosContext>();

builder.Services.AddScoped<IGenericRepository<Transport>, TransportRepository>();
builder.Services.AddScoped<IGenericRepository<Flight>, FlightRepository>();
builder.Services.AddScoped<IGenericRepository<Journey>, JourneyRespository>();
builder.Services.AddScoped<IGenericRepository<JourneyFlight>, JourneyFlightRepository>();

builder.Services.AddScoped<IFlightApiRepository, FlightApiRepository>();

builder.Services.AddScoped<ITransportService, TranportService>();
builder.Services.AddScoped<IFlightApiService, FlightApiService>();
builder.Services.AddScoped<IFlightService, FlightService>();
builder.Services.AddScoped<IJourneyService, JourneyService>();
builder.Services.AddScoped<IJourneyFlightService, JourneyFlightService>();


builder.Services.AddHttpClient("myApi");



var app = builder.Build();

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
