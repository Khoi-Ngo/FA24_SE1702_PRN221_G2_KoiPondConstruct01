using KoiPondConstruct.Service.Configs;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

#region Add services to the container

builder.Services.AddRazorPages();
builder.Services.AddHttpContextAccessor(); // For accessing HttpContext
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
});

// Your custom service configuration
builder.Services.ConfigureBALServices();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Use session
app.UseSession();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
