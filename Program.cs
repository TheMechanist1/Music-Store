﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Music_Store.Data;
using Music_Store.Models;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Music_StoreContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Music_StoreContext") ?? throw new InvalidOperationException("Connection string 'Music_StoreContext' not found.")));
builder.Services.AddScoped<ShoppingCart>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

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
