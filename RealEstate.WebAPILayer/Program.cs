using RealEstate.WebAPILayer.Context;
using RealEstate.WebAPILayer.Repositories.Category;
using RealEstate.WebAPILayer.Repositories.Product;
using RealEstate.WebAPILayer.Repositories.Message;
using RealEstate.WebAPILayer.Repositories.Contact;
using RealEstate.WebAPILayer.Repositories.About;
using RealEstate.WebAPILayer.Repositories.Service;
using RealEstate.WebAPILayer.Repositories.Subscriber;
using RealEstate.WebAPILayer.Repositories.Statistics;
using RealEstate.WebAPILayer.Repositories.Client;
using RealEstate.WebAPILayer.Repositories.ProductDetail;
using RealEstate.WebAPILayer.Repositories.Employee;
using Scalar.AspNetCore;
using RealEstate.WebAPILayer.Hubs;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();
builder.Services.AddTransient<DapperContext>();
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IMessageService, MessageService>();
builder.Services.AddTransient<IContactService, ContactService>();
builder.Services.AddTransient<IAboutService, AboutService>();
builder.Services.AddTransient<IServiceService, ServiceService>();
builder.Services.AddTransient<ISubscriberService, SubscriberService>();
builder.Services.AddTransient<IStatisticsService, StatisticsService>();
builder.Services.AddTransient<IClientService, ClientService>();
builder.Services.AddTransient<IProductDetailService, ProductDetailService>();
builder.Services.AddTransient<IEmployeeService, EmployeeService>();
builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("Cors", builder =>
    {
        builder.AllowAnyHeader()
        .AllowAnyMethod()
        .SetIsOriginAllowed((host) => true)
        .AllowCredentials();
    });
});
builder.Services.AddSignalR();

var app = builder.Build();

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
app.UseCors("Cors");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHub<SignalRHub>("/signalrhub");
app.Run();
