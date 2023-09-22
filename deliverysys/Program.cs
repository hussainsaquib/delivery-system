using Microsoft.OpenApi.Models;

using deliverysys.provider;
using deliverysys.repository;
using deliverysys.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the DI container
builder.Services.AddScoped<OrderRepository>();
builder.Services.AddScoped<OrderProvider>();
builder.Services.AddControllersWithViews(); // instead of just AddControllers

// Change this line
builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("DatabaseSettings")); // Modified this line

// Register the Swagger generator
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

// Other builder configurations
//builder.Services.AddControllers();

var app = builder.Build();

// Use middlewares
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    // Enable middleware to serve generated Swagger as a JSON endpoint
    app.UseSwagger();

    // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.)
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapDefaultControllerRoute(); // Add this line
});

app.Run();

