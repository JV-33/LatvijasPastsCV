using Microsoft.EntityFrameworkCore;
using LatvijasPastsCV.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using LatvijasPastsCV.DBData;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IAdreseService, AdreseService>();
builder.Services.AddScoped<IIzglitibaService, IzglitibaService>();
builder.Services.AddScoped<IPamatdatiService, PamatdatiService>();
builder.Services.AddScoped<IPrasmesService, PrasmesService>();
builder.Services.AddScoped<ICVDbContext, CVDbContext>();


// Add SQLite DbContext
builder.Services.AddDbContext<CVDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Seed data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<CVDbContext>();

    try
    {
        var firstTabsText = new FirstTabsText(context);
        firstTabsText.SeedData();

    }
    catch (Exception ex)
    {
        Console.WriteLine("An error occurred while seeding the database: " + ex.Message);
    }

}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
