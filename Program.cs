using System.Globalization;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.OpenApi;
using WebApplication1.Data;
using WebApplication1.Services.AI;
using WebApplication1.Services.Cart;
using WebApplication1.Services.Checkout;
using WebApplication1.Services.ProductImages;
using WebApplication1.Services.Recommendations;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Data Source=meubanco.db";

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options
        .UseSqlite(connectionString)
        .ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning)));

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedEmail = false;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
})
.AddErrorDescriber<IdentityMensagensEmPortugues>()
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Events.OnRedirectToLogin = context =>
    {
        if (context.Request.Path.StartsWithSegments("/api"))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Task.CompletedTask;
        }

        context.Response.Redirect(context.RedirectUri);
        return Task.CompletedTask;
    };

    options.Events.OnRedirectToAccessDenied = context =>
    {
        if (context.Request.Path.StartsWithSegments("/api"))
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            return Task.CompletedTask;
        }

        context.Response.Redirect(context.RedirectUri);
        return Task.CompletedTask;
    };
});

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    const string cookieAuthenticationScheme = "IdentityCookie";

    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Beauty Marketplace API",
        Version = "v1",
        Description = "Documentação OpenAPI da Entrega 4: catálogo, carrinho, checkout, pedidos e IA."
    });
    options.DocInclusionPredicate((_, apiDescription) =>
        apiDescription.RelativePath?.StartsWith("api/", StringComparison.OrdinalIgnoreCase) == true);

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }

    options.AddSecurityDefinition(cookieAuthenticationScheme, new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.ApiKey,
        In = ParameterLocation.Cookie,
        Name = ".AspNetCore.Identity.Application",
        Description = "Autenticação por cookie do ASP.NET Identity. Faça login no site com o perfil adequado antes de chamar endpoints protegidos."
    });
});

builder.Services.AddScoped<IProductRecommendationStrategy, SkinHairRecommendationStrategy>();
builder.Services.AddScoped<IProductRecommendationStrategy, CategoryRecommendationStrategy>();
builder.Services.AddScoped<IProductRecommendationStrategy, VeganPriceRecommendationStrategy>();
builder.Services.AddScoped<IProductRecommendationService, ProductRecommendationService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ICheckoutFacade, CheckoutFacade>();
builder.Services.AddScoped<IProductImageService, ProductImageService>();
builder.Services.AddScoped<LocalAiRecommendationService>();
builder.Services.AddHttpClient<OpenAiRecommendationService>();
builder.Services.AddScoped<IAiRecommendationServiceFactory, AiRecommendationServiceFactory>();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

await MarketplaceSeeder.SeedAsync(app.Services);

var supportedCultures = new[] { new CultureInfo("pt-BR") };
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("pt-BR"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

var configuredUrls = app.Configuration["urls"] ?? app.Configuration["ASPNETCORE_URLS"] ?? string.Empty;
var configuredHttpsPort = app.Configuration["https_port"] ?? app.Configuration["HTTPS_PORT"];
if (!string.IsNullOrWhiteSpace(configuredHttpsPort) ||
    configuredUrls.Contains("https://", StringComparison.OrdinalIgnoreCase))
{
    app.UseHttpsRedirection();
}
app.UseStaticFiles();
app.UseRouting();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Beauty Marketplace API v1");
});

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();

public partial class Program
{
}
