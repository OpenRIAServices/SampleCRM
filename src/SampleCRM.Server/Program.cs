global using System.ComponentModel.DataAnnotations;
global using SampleCRM.Web.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using OpenRiaServices.Hosting.AspNetCore;
using OpenRiaServices.Server;


var domainServices = typeof(Program).Assembly.ExportedTypes
    .Where(t => t.IsAssignableTo(typeof(DomainService)) && !t.IsAbstract)
    .ToArray();

var builder = WebApplication.CreateBuilder(args);

// Additional dependencies for cookie based AuthenticationService
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();
builder.Services.AddAuthorization();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
        .WithHeaders("Content-Type", "Accept", "SOAPAction")
        .AllowAnyMethod();
    });
});

// Register services
builder.Services.AddOpenRiaServices();
foreach (var type in domainServices)
    builder.Services.Add(new ServiceDescriptor(type, type, ServiceLifetime.Transient));


var app = builder.Build();

if (!builder.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapOpenRiaServices(builder =>
{
    foreach (var type in domainServices)
    {
        try
        {
            builder.AddDomainService(type);
        }
        catch (global::System.Exception ex)
        {
            Console.WriteLine($"Ignoreing {type} due to exception: {ex.Message}");
        }
    }
});

app.MapGet("/", httpContext =>
{
    var dataSource = httpContext.RequestServices.GetRequiredService<EndpointDataSource>();

    var sb = new System.Text.StringBuilder();
    sb.Append("<html><body>");
    sb.AppendLine("<p>Endpoints:</p>");
    foreach (var endpoint in dataSource.Endpoints.OfType<RouteEndpoint>().OrderBy(e => e.RoutePattern.RawText, StringComparer.OrdinalIgnoreCase))
    {
        sb.AppendLine(FormattableString.Invariant($"- <a href=\"{endpoint.RoutePattern.RawText}\">{endpoint.RoutePattern.RawText}</a><br />"));
        foreach (var metadata in endpoint.Metadata)
        {
            sb.AppendLine("<li>" + metadata + "</li>");
        }
    }

    var response = httpContext.Response;
    response.StatusCode = 200;

    sb.AppendLine("</body></html>");
    response.ContentType = "text/html";
    return response.WriteAsync(sb.ToString());
});

app.Run();
