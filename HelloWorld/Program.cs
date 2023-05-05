using HelloWorld; // 1. add this
using HelloWorld.Models; // Add this for exercise

var builder = WebApplication.CreateBuilder(args);

// Add configuration here for Json exercise
builder.Configuration.AddJsonFile("MySettings.json",
    optional: false,
    reloadOnChange: true);

// Add for Json Exercise
builder.Services.AddSingleton<MyJsonSettings>(builder.Configuration.Get<MyJsonSettings>());

// Add services to the container.
builder.Services.AddControllersWithViews();

// DI Registeration
builder.Services.AddSingleton<IProductRepository, ProductRepository>(); // 2. add this

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//}

// revise to this for exercise
app.UseExceptionHandler("/Error");

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
