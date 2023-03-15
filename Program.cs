using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSession(optiions =>
{
    optiions.IdleTimeout = TimeSpan.FromMinutes(60);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();


app.UseSession();
app.UseAuthorization();

app.MapRazorPages();


app.MapControllerRoute(
    name: "Users",
    pattern: "{controller=User}/{action=All}");

app.Run();
