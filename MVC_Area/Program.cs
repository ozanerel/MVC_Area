var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

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
