using RealEstate.WebAPILayer.Context;
using RealEstate.WebAPILayer.Repositories.Category;
using RealEstate.WebAPILayer.Repositories.Product;
using RealEstate.WebAPILayer.Repositories.Message;
using RealEstate.WebAPILayer.Repositories.Contact;
using RealEstate.WebAPILayer.Repositories.About;
using RealEstate.WebAPILayer.Repositories.Service;
using RealEstate.WebAPILayer.Repositories.Subscriber;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<DapperContext>();
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IMessageService, MessageService>();
builder.Services.AddTransient<IContactService, ContactService>();
builder.Services.AddTransient<IAboutService, AboutService>();
builder.Services.AddTransient<IServiceService, ServiceService>();
builder.Services.AddTransient<ISubscriberService, SubscriberService>();
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(opt =>
    {
        opt
        .WithTitle("DapperProject")
        .WithTheme(ScalarTheme.BluePlanet)
        .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
