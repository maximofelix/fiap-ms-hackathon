using EVSWeb.Application;
using EVSWeb.Application.Interfaces.Categories;
using EVSWeb.Application.Interfaces.Products;
using EVSWeb.Application.Services;
using EVSWeb.Infrastructure;
using EVSWeb.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<EVSContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DbContext"),
        providerOptions => providerOptions.EnableRetryOnFailure()
        );
});


builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<EVSContext>();
    //context.Database.Migrate();
    context.Database.EnsureCreated();
    context.Seed();
}

app.Run();
