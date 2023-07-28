using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication2.services;
using WebApplication2.services.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDBContext>(op =>
op.UseSqlServer(builder.Configuration.GetConnectionString("myDb"))
);

builder.Services.AddScoped<IRoomTypeService, RoomTypeService>();
builder.Services.AddScoped<IReservationsService, ReservationService>();
MappingConfig.RegisterMappings(AppDomain.CurrentDomain.GetAssemblies());
/*builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);*/
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