using eServiceDemo.Clients;
using eServiceDemo.Configurations;
using eServiceDemo.Interfaces;
using Microsoft.OpenApi.Models;
using NTUC.Web.APIs.Interfaces;
using NTUC.Web.APIs.Services;

var builder = WebApplication.CreateBuilder(args);



const string AllowAllHeadersPolicy = "AllowAllHeadersPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(AllowAllHeadersPolicy,
        builder =>
        {
            builder.WithOrigins("http://localhost:4200", "https://eservice-demo-webapp-client.azurewebsites.net")
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});
// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Demo",
        Version = "v1"
    });    
});

builder.Services.AddTransient<IUcemClient, UcemClient>();
builder.Services.AddTransient<IApimService, ApimService>();

builder.Services.Configure<NtucApimConfiguration>(builder.Configuration.GetSection("NtucApimConfiguration"));


var app = builder.Build();

app.UseCors(builder =>
{
    builder
       .WithOrigins("http://localhost:4200", "https://eservice-demo-webapp-client.azurewebsites.net")
       .SetIsOriginAllowedToAllowWildcardSubdomains()
       .AllowAnyHeader()
       .AllowCredentials()
       .WithMethods("GET", "PUT", "POST", "DELETE", "OPTIONS")
       .SetPreflightMaxAge(TimeSpan.FromSeconds(3600));
}
);

app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowAll");
app.MapControllers();

app.Run();

