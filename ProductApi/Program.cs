using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using ProductApi.Models;
using ProductApi.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<APIDBContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("Local")));
builder.Services.AddScoped<APIDBContext>();
builder.Services.AddTransient<ICustomerService, CustomerService>();


builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    // options.ApiVersionReader = new QueryStringApiVersionReader("api-version");  // this is used for query string api version 
    options.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
});


builder.Services.AddCors(options =>
{

    options.AddDefaultPolicy(coreBuilder =>
    {
        coreBuilder.AllowAnyHeader();
        coreBuilder.AllowAnyMethod();
        coreBuilder.AllowAnyOrigin();
    });
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.UseCors();

app.MapControllers();

app.Run();
