using Blog.Application.Core;
using Blog.Infrastructure.Data.Extensions;
using Blog.Infrastructure.IOC;
using Blog.Infrastructure.Options;
using Blog.Presentation.Web;

var builder = WebApplication.CreateBuilder(args);

var connectionStringsOptions = new ConnectionStringsOptions();
builder.Configuration.GetSection("ConnectionStrings").Bind(connectionStringsOptions);


builder.Services
    .AddPresentationServices(builder.Configuration)
    .AddApplicationServices()
    .AddInfrastructureServices(connectionStringsOptions);

var app = builder.Build();

app.UsePresentationServices();
if (builder.Environment.IsDevelopment())
{
    app.InitializeDatabaseAsync();
    app.SeedDatabaseAsync().Wait();
}

if(app.Environment.IsProduction())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();



app.Run();