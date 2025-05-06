using BloopFishFarm.Core.Interfaces;
using BloopFishFarm.Core.Services;
using BloopFishFarm.Infrastructure.Data;
using BloopFishFarm.Infrastructure.Data.Repositories;
using BloopFishFarm.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IReviewService, ReviewService>();


// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register repositories
builder.Services.AddScoped<IFishRepository, FishRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

// Register services
builder.Services.AddScoped<IFishService, FishService>();
builder.Services.AddScoped<OrderService>();

// Configure WhatsApp service
builder.Services.AddHttpClient();
builder.Services.AddScoped<IWhatsAppService>(sp => {
    var httpClient = sp.GetRequiredService<HttpClient>();
    var whatsAppApiUrl = builder.Configuration["WhatsApp:ApiUrl"];
    var whatsAppApiToken = builder.Configuration["WhatsApp:ApiToken"];
    return new WhatsAppService(httpClient, whatsAppApiUrl, whatsAppApiToken);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Initialize database with some sample data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating or seeding the database.");
    }
}

app.Run();
