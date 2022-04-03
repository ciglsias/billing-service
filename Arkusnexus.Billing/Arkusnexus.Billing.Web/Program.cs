using Arkusnexus.Billing.Infrastructure;
using Arkusnexus.Billing.Infrastructure.Repositories;
using Arkusnexus.Billing.Infrastructure.Repositories.Abstractions;
using Arkusnexus.Billing.Web.Mapping;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(opts => {
    var enumConverter = new JsonStringEnumConverter();
    opts.JsonSerializerOptions.Converters.Add(enumConverter);
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//context
builder.Services.AddScoped<BillingContext>();

//repositories
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

//unit of work
builder.Services.AddScoped<IBillingUnitOfWork, BillingUnitOfWork>();

//mapper
builder.Services.AddAutoMapper(typeof(BillingAutoMapperProfile));

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
