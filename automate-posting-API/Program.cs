using CamundaClient;
using Microsoft.EntityFrameworkCore;
using PostingOnSocialMedia.Models;
using static Dropbox.Api.TeamLog.EventCategory;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<SocialMediaDbContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("SocialMediaCS")
    ));
builder.Services.AddCors(options => options.AddDefaultPolicy(p => p.AllowAnyOrigin()
                                                                       .AllowAnyMethod()
                                                                       .AllowAnyHeader())
                                                                       );
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
CamundaEngineClient camunda = new CamundaEngineClient();
camunda.Startup(); // Deploys all models to Camunda and Start all found ExternalTask-Workers

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();
