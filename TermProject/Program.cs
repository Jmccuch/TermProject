var builder = WebApplication.CreateBuilder(args);

// Add HttpClientFactory
builder.Services.AddHttpClient();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add session state
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Enable session middleware
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Specify the port to listen on
app.Run();

// Configure HttpClient options
var httpClientFactory = app.Services.GetRequiredService<IHttpClientFactory>();
var httpClient = httpClientFactory.CreateClient();

// Adjust buffer size here
httpClient.MaxResponseContentBufferSize = 1024 * 1024; // Example buffer size (1 MB)

