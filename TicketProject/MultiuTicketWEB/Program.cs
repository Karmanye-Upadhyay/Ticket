using Application;
using InfraStructure.DbFolder;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<DbClass>(options =>
    options.UseSqlServer(
        builder.
        Configuration.GetConnectionString("Connection"),
        builder => builder.MigrationsAssembly(typeof(DbClass).Assembly.FullName)));

builder.Services
    .AddScoped<IApplicationDBContext>(
    provider => provider.GetRequiredService<DbClass>()
    );

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options =>
options.WithOrigins("http://localhost:4200", "http://localhost:62518")
.AllowAnyOrigin()
.AllowAnyMethod()
.AllowAnyHeader());

app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();

app.Run();
