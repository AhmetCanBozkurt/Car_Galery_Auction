using Car_Gallery_Auction.Business.Abstract;
using Car_Gallery_Auction.Business.Concrete;
using Car_Gallery_Auction.DataBase.Context;
using Car_Gallery_Auction.DataBase.Modals;
using Car_Gallery_Auction.Extensions;
using Car_Galllery_Auction.Core.Modals;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddPersistenceLayer(builder.Configuration);
builder.Services.AddApplicationLayer(builder.Configuration);
builder.Services.AddSwaggerCollection(builder.Configuration);
builder.Services.AddSwaggerGen();

//builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
