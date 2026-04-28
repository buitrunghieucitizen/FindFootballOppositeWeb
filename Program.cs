using FindFootballOppsite.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<PortalDataService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseHttpsRedirection();
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapPost("/contact-submit", async context =>
{
    var isAjaxRequest = string.Equals(
        context.Request.Headers["X-Requested-With"],
        "XMLHttpRequest",
        StringComparison.OrdinalIgnoreCase);

    if (isAjaxRequest)
    {
        context.Response.ContentType = "text/plain";
        await context.Response.WriteAsync("OK");
        return;
    }

    context.Response.Redirect("/thank-you.html");
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
