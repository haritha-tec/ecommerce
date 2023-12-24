using ECommerse.Api.Search.Interfaces;
using ECommerse.Api.Search.Services;
using Polly;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ISearchService, SearchService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddHttpClient("OrderService", config =>
{
    config.BaseAddress = new Uri(builder.Configuration.GetValue<string>("Services:Orders"));
});

//polly (WaitAndRetryAsync)used for resiliency--> if a microservice is down application should not break
builder.Services.AddHttpClient("ProductService", config =>
{
    config.BaseAddress = new Uri(builder.Configuration.GetValue<string>("Services:Products"));
}).AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(5,_ => TimeSpan.FromMilliseconds(500)));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
