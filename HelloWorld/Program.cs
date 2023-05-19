using HelloWorld; // 1. add this
using HelloWorld.Models; // Add this for exercise

var builder = WebApplication.CreateBuilder(args);

// Add configuration here for Json exercise
builder.Configuration.AddJsonFile("MySettings.json",
    optional: false,
    reloadOnChange: true);

// Add for Json Exercise
builder.Services.AddSingleton<MyJsonSettings>(builder.Configuration.Get<MyJsonSettings>());

// Add this for HttpContext Accessor - DI
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Add services to the container.
builder.Services.AddControllersWithViews();

// DI Registeration
builder.Services.AddSingleton<IProductRepository, ProductRepository>(); // 2. add this

// Action Filter Registerations
builder.Services.AddMvc(options => {
   // The order of Actions Filters are important
    options.Filters.Add<IPAddressExcludeActionFilter>();  // Added
    options.Filters.Add<LoggingActionFilter>();
});

builder.Services.AddSession();

// Add Data Caching Services 
builder.Services.AddMemoryCache();

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

app.UseSession(); // add before app.MapControllerRoute( )

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
