using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVC_Area.Models.Context;
using MVC_Area.Models.Entities;
using MVC_Area.Services.Abstracts;
using MVC_Area.Services.Concretes;

var builder = WebApplication.CreateBuilder(args);


builder.Logging.ClearProviders();
builder.Logging.AddConsole();

//Configuration (appsetting.json dosyas�na ula�mam�z� sa�layan interface)
string connectionString = builder.Configuration/*["ConnectionStrings:DefaultConnection"];*/.GetConnectionString("DefaultConnection");//�ki t�rl�de kullanabiliriz.Yani bu ama� i�in tasarlanm�� olan metottur.Bu sayede �zel bilgi direkt burada kullan�lm�yor.

// Add services to the container.
builder.Services.AddControllersWithViews();//MVC kullanmak i�in servislere "AddControllersWithViews" ekliyoruz

//AddDbContext
builder.Services.AddDbContext<ProjectContext>(options => options.UseSqlServer(connectionString));

//Identity Customize = Burada kurallar� (�ifre vs) �zelle�tirmek i�in kulland���m�z alan
builder.Services.Configure<IdentityOptions>(x =>
{
    x.Password.RequiredLength = 4;//min 4 karakter olsun 
    x.Password.RequireUppercase = false; 
    x.Password.RequireLowercase = false;
    x.Password.RequireNonAlphanumeric = false;
});

//Identity Service = Eylemleri herhangibir controller da ula�abilelim diye
builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<ProjectContext>()/*Ait oldu�u temel �at�(context) neresi ise oras� al�n�r*/;

//Cookie
builder.Services.ConfigureApplicationCookie(x =>
{
    x.Cookie = new CookieBuilder
    {
        Name = "AspNetCore_Cookie",//Cookie ismi

    };
    x.LoginPath = new PathString("/Home/Login"); //Bu yoldan ba�ar�l� ge�i� yapan kullan�c�ya ait bilgileri verdi�imiz cookie nin alt�nda sakla 
    x.AccessDeniedPath = new PathString("/Home/AccessDenied");//Rol� uygun de�ilse g�nderilecek alan
    x.SlidingExpiration = true;//Cookie nin �mr� dolmak �zereyken yenilenmesini sa�lar
    x.ExpireTimeSpan = TimeSpan.FromMinutes(1);//Cookie nin �mr� 1 dk. Bu cookie browserdan silinecek tekrar giri� yap�lmas� gerekecek
});

//Service
builder.Services.AddScoped<IAppUserService, AppUserService>();//Scoped: Her istek i�in bir tane instance olu�turur

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

app.UseAuthentication();//Kimlik == kullan�c� .. rol� de bu .. 
app.UseAuthorization();//Oturum == bir kullan�cak gelecek onu kar��la 


//�lk ba�ta area olu�turuldu�unda gelen scaffoldingreadme i�erisindeki kod. Bunun sayesinde iki homecontroller �n �ak��mas�n� engelliyor
//Endpointler b�nyesinde birden fazla ula��m noktas�n� bar�nd�ran metotlard�r
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllerRoute(
//      name: "areas",
//      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
//    );

//    /*endpoints.MapControllerRoute(
//      name: "areas",
//      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
//    ); s�sl� parantezin i�erisinde �o�altmam�z yeterli*/

//});


//Area Map
app.MapControllerRoute(

      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

//MapController lar tek ba��na metotlard�r
//Home Map
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

/*app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); Her biri i�in yeni mapcontroller olarak tan�mlamam�z laz�m*/

app.Run();
