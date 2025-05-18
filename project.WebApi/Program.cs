using Microsoft.EntityFrameworkCore;
using project.Data;
using project.Data.Interfaces;
using project.Data.Repositories;
using project.Service.Interfaces;
using project.Service.Profiles;
using project.Service.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//db
builder.Services.AddDbContext<TrialContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("TrialContext")));

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<GuestProfile>();
    cfg.AddProfile<RoomProfile>();
});

builder.Services.AddScoped<IGuestRepository, GuestRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();

builder.Services.AddScoped<IGuestService, GuestService>();
builder.Services.AddScoped<IRoomService, RoomService>();


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