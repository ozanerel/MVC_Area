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
