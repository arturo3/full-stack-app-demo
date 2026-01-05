using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

#region Entity Framework Core Configuration
builder.Services.AddDbContext<full_stack_app_demo.Server.Data.AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<full_stack_app_demo.Server.Data.Repositories.ICategoryRepository, full_stack_app_demo.Server.Data.Repositories.CategoryRepository>();
builder.Services.AddScoped<full_stack_app_demo.Server.Data.Repositories.IProductRepository, full_stack_app_demo.Server.Data.Repositories.ProductRepository>();
builder.Services.AddScoped<full_stack_app_demo.Server.Services.ICategoryService, full_stack_app_demo.Server.Services.CategoryService>();
builder.Services.AddScoped<full_stack_app_demo.Server.Services.IProductService, full_stack_app_demo.Server.Services.ProductService>();
#endregion

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
