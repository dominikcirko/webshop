using Microsoft.EntityFrameworkCore;
using webshopAPI.DAL.Repositories.Implementations;
using webshopAPI.DataAccess.Repositories.Interfaces;
using webshopAPI.Models;
using webshopAPI.Services.Implementation;
using webshopAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<webshopdbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection")));

//Register repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICartItemRepository, CartItemRepository>();
builder.Services.AddScoped<IItemCategoryRepository, ItemCategoryRepository>();
builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
builder.Services.AddScoped<IStatusRepository, StatusRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();

// Register services
builder.Services.AddScoped<IUserService, UserService>(); 
builder.Services.AddScoped<IItemService, ItemService>(); 
builder.Services.AddScoped<IOrderService, OrderService>(); 
builder.Services.AddScoped<ICartService, CartService>(); 
builder.Services.AddScoped<ICartItemService, CartItemService>(); 
builder.Services.AddScoped<IItemCategoryService, ItemCategoryService>(); 
builder.Services.AddScoped<IOrderItemService, OrderItemService>(); 
builder.Services.AddScoped<IStatusService, StatusService>(); 
builder.Services.AddScoped<ITagService, TagService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
