using Microsoft.EntityFrameworkCore;
using Platform.Entity.Interfaces;
using Platform.Repository;
using Platform.Transactional.Operations;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ServiceSystemConnection>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ServiceSystemDB")));

builder.Services.AddDbContext<CreditSystemConnection>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CreditSystemDB")));


builder.Services.AddScoped<ICompanyOperations, CompanyOperations>();
builder.Services.AddScoped<IUserOperations, UserOperations>();
builder.Services.AddScoped<IClientOperations, ClientOperations>();
builder.Services.AddControllersWithViews();


builder.Services.AddDistributedMemoryCache();  
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); 
    options.Cookie.HttpOnly = true; 
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseSession();
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}")
    .WithStaticAssets();

app.Run();
