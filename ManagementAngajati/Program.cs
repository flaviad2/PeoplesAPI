using ManagementAngajati.Persistence.DbUtils;
using ManagementAngajati.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using ManagementAngajati.Controllers;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//
builder.Services.AddCors();
builder.Services.AddDbContextPool<ManagementAngajatiContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ManagementAngajatiContextConnectionString")));
//builder.Services.AddDbContextPool<AngajatiContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ManagementAngajatiContextConnectionString")));
//builder.Services.AddDbContextPool<AngajatiContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ManagementAngajatiContextConnectionString")));
//builder.Services.AddDbContextPool<AngajatiContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ManagementAngajatiContextConnectionString")));

//IServiceCollection serviceCollection = 
builder.Services.AddScoped<IRepositoryAngajat, RepositoryAngajat>();
builder.Services.AddScoped<IRepositoryPost, RepositoryPost>();
builder.Services.AddScoped<IRepositoryConcediu, RepositoryConcediu>();
builder.Services.AddScoped<IRepositoryIstoricAngajat, RepositoryIstoricAngajat>();



var app = builder.Build();


app.UseCors(options =>
options.WithOrigins("http://localhost:7293")
.AllowAnyMethod()
.AllowAnyHeader()
);

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
