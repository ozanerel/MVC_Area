using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVC_Area.Models.Context;
using MVC_Area.Models.Entities;
using MVC_Area.Services.Abstracts;
using MVC_Area.Services.Concretes;

var builder = WebApplication.CreateBuilder(args);


builder.Logging.ClearProviders();
builder.Logging.AddConsole();

//Configuration (appsetting.json dosyasýna ulaþmamýzý saðlayan interface)
string connectionString = builder.Configuration/*["ConnectionStrings:DefaultConnection"];*/.GetConnectionString("DefaultConnection");//Ýki türlüde kullanabiliriz.Yani bu amaç için tasarlanmýþ olan metottur.Bu sayede özel bilgi direkt burada kullanýlmýyor.

// Add services to the container.
builder.Services.AddControllersWithViews();//MVC kullanmak için servislere "AddControllersWithViews" ekliyoruz

//AddDbContext
builder.Services.AddDbContext<ProjectContext>(options => options.UseSqlServer(connectionString));

//Identity Customize = Burada kurallarý (þifre vs) özelleþtirmek için kullandýðýmýz alan
builder.Services.Configure<IdentityOptions>(x =>
{
    x.Password.RequiredLength = 4;//min 4 karakter olsun 
    x.Password.RequireUppercase = false; 
    x.Password.RequireLowercase = false;
    x.Password.RequireNonAlphanumeric = false;
});

//Identity Service = Eylemleri herhangibir controller da ulaþabilelim diye
builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<ProjectContext>()/*Ait olduðu temel çatý(context) neresi ise orasý alýnýr*/;

//Cookie
builder.Services.ConfigureApplicationCookie(x =>
{
    x.Cookie = new CookieBuilder
    {
        Name = "AspNetCore_Cookie",//Cookie ismi

    };
    x.LoginPath = new PathString("/Home/Login"); //Bu yoldan baþarýlý geçiþ yapan kullanýcýya ait bilgileri verdiðimiz cookie nin altýnda sakla 
    x.AccessDeniedPath = new PathString("/Home/AccessDenied");//Rolü uygun deðilse gönderilecek alan
    x.SlidingExpiration = true;//Cookie nin ömrü dolmak üzereyken yenilenmesini saðlar
    x.ExpireTimeSpan = TimeSpan.FromMinutes(1);//Cookie nin ömrü 1 dk. Bu cookie browserdan silinecek tekrar giriþ yapýlmasý gerekecek
});

//Service
builder.Services.AddScoped<IAppUserService, AppUserService>();//Scoped: Her istek için bir tane instance oluþturur

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

app.UseAuthentication();//Kimlik == kullanýcý .. rolü de bu .. 
app.UseAuthorization();//Oturum == bir kullanýcak gelecek onu karþýla 


//Ýlk baþta area oluþturulduðunda gelen scaffoldingreadme içerisindeki kod. Bunun sayesinde iki homecontroller ýn çakýþmasýný engelliyor
//Endpointler bünyesinde birden fazla ulaþým noktasýný barýndýran metotlardýr
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllerRoute(
//      name: "areas",
//      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
//    );

//    /*endpoints.MapControllerRoute(
//      name: "areas",
//      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
//    ); süslü parantezin içerisinde çoðaltmamýz yeterli*/

//});


//Area Map
app.MapControllerRoute(

      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

//MapController lar tek baþýna metotlardýr
//Home Map
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

/*app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); Her biri için yeni mapcontroller olarak tanýmlamamýz lazým*/

app.Run();
