using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using KanbanBoard.Application.ServiceExtensions;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddServiceContainer(builder.Configuration);


builder.Services.AddNotyf(conf =>
{
    conf.DurationInSeconds = 5;
    conf.IsDismissable = true;
    conf.Position = NotyfPosition.TopRight;
});

var app = builder.Build();

app.UseNotyf();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
