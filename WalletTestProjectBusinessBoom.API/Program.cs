using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.OpenApi.Models;
using WalletTestProjectBusinessBoom.API.Extensions;
using WalletTestProjectBusinessBoom.BAL.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "User Management API",
        Version = "v1.0",
        Description = "An API for managing users, their balance, and transactions.",
        Contact = new OpenApiContact
        {
            Name = "Ivan Kobtsev",
            Email = "vanomc77@gmail.com",
            Url = new Uri("https://www.linkedin.com/in/ivanrocafellla/")
        }
    });
}
);
builder.Services.AddApplication(builder.Configuration);
// Disables automatic ModelState validation in ASP.NET Core
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
// Remove "Controller" from the end and convert to lowercase
builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
