using Microsoft.EntityFrameworkCore;
using Services.External;
using System.Configuration;
using Web.Common;
using WebApplication1.Data;
using WebApplication2.services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDBContext>(op =>
op.UseSqlServer(builder.Configuration.GetConnectionString("myDb"))
);

builder.Services.AddScoped<IRoomTypeService, RoomTypeService>();
builder.Services.AddScoped<IReservationsService, ReservationService>();
builder.Services.AddScoped<IRoomService, RoomService>();

builder.Services.AddTransient<IImageManager>(x => new ImageManager(builder.Configuration.GetSection("Cloudinary")["CloudName"],
                                                                      builder.Configuration.GetSection("Cloudinary")["ApiKey"],
                                                                      builder.Configuration.GetSection("Cloudinary")["ApiSecret"]));
/*MappingConfig.RegisterMappings(AppDomain.CurrentDomain.GetAssemblies());*/
builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
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
    pattern: "{controller=RoomTypes}/{action=Index}/{id?}");

app.Run();