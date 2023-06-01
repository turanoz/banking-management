using BankingManagement.Core.Models;
using BankingManagement.Core.Repositories;
using BankingManagement.Core.Services;
using BankingManagement.Core.UnitOfWorks;
using BankingManagement.Repository;
using BankingManagement.Repository.Repositories;
using BankingManagement.Repository.UnitOfWorks;
using BankingManagement.Service.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User, Role>().AddEntityFrameworkStores<AppDbContext>();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Auth/Sign/Login";
    options.AccessDeniedPath = "/Auth/Sign/AccessDenied";
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAuditLogService, AuditLogService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();


builder.Services.AddControllersWithViews();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Auth}/{controller=Sign}/{action=Login}/{id?}");

app.Run();