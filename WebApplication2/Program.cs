using Microsoft.EntityFrameworkCore;
using Services.External;
using Web.Common;

using WebApplication2.services;
using Microsoft.AspNetCore.Identity;
using WebApplication2.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("WebApplication2ContextConnection") ?? throw new InvalidOperationException("Connection string 'WebApplication2ContextConnection' not found.");

builder.Services.AddDbContext<WebApplication2DBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("myDb")));

builder.Services.AddDefaultIdentity<WebApplication2Admin>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<WebApplication2DBContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
/*builder.Services.AddDbContext<ApplicationDBContext>(op =>
op.UseSqlServer(builder.Configuration.GetConnectionString("myDb"))
);*/

builder.Services.AddScoped<IRoomTypeService, RoomTypeService>();
builder.Services.AddScoped<IReservationsService, ReservationService>();
builder.Services.AddScoped<IRoomService, RoomService>();
/**/
builder.Services.AddScoped<IRoomImageService, RoomImageService>();
builder.Services.AddScoped<IEscortService, EscortService>();

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
app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=RoomTypes}/{action=Index}/{id?}");

app.Run();