using Application.Interfaces;
using Application.Services;
using Elogica_RH.Config;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.Data.SqlClient;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddScoped<IDbConnection>(provider =>
{
    return new SqlConnection(connectionString);
    //SqlConnection connection = new SqlConnection(connectionString);
    //connection.Open();
    //return connection;
});

builder.Services.DependencInjection(builder.Configuration);

builder.Services.AddScoped<ICargosRepository, CargosRepository>();
builder.Services.AddScoped<ICargosServices, CargosService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", builder =>
    {
        builder.WithOrigins("http://localhost:4200")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseRouting();

app.UseCors("AllowAngularApp");

app.UseAuthorization();
app.MapControllers();

app.Run();
