using BebopTransferService.Extensions;
using BebopTransferService.Infrastructure.EntityFrameworkCore.DbContext;
using BebopTransferService.Middlewares;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader()
           .WithExposedHeaders("X-Pagination-Total-Pages")
           .WithExposedHeaders("X-Pagination-Next-Page")
           .WithExposedHeaders("X-Pagination-Has-Next-Page")
           .WithExposedHeaders("X-Pagination-Total-Pages");
});

app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<BebopDbContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    dbContext.Database.EnsureCreated();
    await dbContext.Database.MigrateAsync();
    logger.LogInformation("Database created successfully or already exists.");
}

app.Run();
