using Entekhab;
using Entekhab.Formatter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Persistence;
using Services.Salary;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers(options =>
{
    options.InputFormatters.Add(new XmlSerializerInputFormatter(options));
    options.InputFormatters.Add(new CustomInputFormatter());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ISalaryService,SalaryService>();
builder.Services.AddDbContext<ApplicationDbContext>
    (options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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
