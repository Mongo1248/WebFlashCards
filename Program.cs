using Microsoft.EntityFrameworkCore;
using WebFlashCards.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<WebFlashCardsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WebFlashCardsContext") ?? throw new InvalidOperationException("Connection string 'WebFlashCardsContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
