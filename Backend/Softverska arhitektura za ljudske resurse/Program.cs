using BusinessLayer;
using DataLayer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IOdeljenjeRepository, OdeljenjeRepository>();
builder.Services.AddScoped<IBusinessOdeljenje, BusinessOdeljenje>();
builder.Services.AddScoped<IOdsustvoRepository, OdsustvoRepository>();
builder.Services.AddScoped<IBusinessOdsustvo, BusinessOdsustvo>();
builder.Services.AddScoped<IZaposleniRepository, ZaposleniRepository>();
builder.Services.AddScoped<IBusinessZaposleni, BusinessZaposleni>();
builder.Services.AddScoped<IBusinessAdmin, BusinessAdmin>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(builder => builder
    .WithOrigins("http://localhost:3000") // Dodajte origin URL va�e React aplikacije
    .AllowAnyHeader()
    .AllowAnyMethod());

app.MapControllers();

app.Run();
