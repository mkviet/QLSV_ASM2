using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QLSVVV.Data;
using System;

var builder = WebApplication.CreateBuilder(args);

// Cấu hình dịch vụ DbContext
builder.Services.AddDbContext<QLSVVVContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("QLSVVVContext") ?? throw new InvalidOperationException("Connection string 'QLSVVVContext' not found.")));

// Cấu hình dịch vụ Session
builder.Services.AddDistributedMemoryCache(); // Cung cấp dịch vụ cache cần thiết cho Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian hết hạn của session
    options.Cookie.HttpOnly = true; // Cookie chỉ có thể truy cập qua HTTP
    options.Cookie.IsEssential = true; // Cookie cần thiết cho ứng dụng
});
builder.Services.AddAuthentication("CookieAuthentication")
    .AddCookie("CookieAuthentication", options =>
    {
        options.LoginPath = "/Authentication/Login"; // Đường dẫn đến trang đăng nhập
    });


// Thêm dịch vụ MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Cấu hình pipeline HTTP request
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // Giá trị mặc định của HSTS là 30 ngày. Bạn có thể thay đổi giá trị này cho các kịch bản sản xuất.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Thêm middleware Session
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
