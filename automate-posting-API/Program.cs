using CamundaClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using PostingOnSociallMedia.sf_sezane;
using PostingOnSocialMedia.Models;
using static Dropbox.Api.TeamLog.EventCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<SfSezaneContext>(options => { options.UseMySQL(builder.Configuration.GetConnectionString("sf_sezaneCS")); }) ;
/*builder.Services.AddTransient<MySqlConnection>(_ =>
    new MySqlConnection(builder.Configuration.GetConnectionString("sf_sezaneCS")));*/
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
using var connection = new MySqlConnection(builder.Configuration.GetConnectionString("sf_sezaneCS"));

await connection.OpenAsync();

using var command = new MySqlCommand("SELECT * FROM medias LIMIT 1;", connection);
using var reader = await command.ExecuteReaderAsync();
if (await reader.ReadAsync())
{
    var value = reader.GetValue(0); // This will get the first column of the first row
}

await connection.CloseAsync();


app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();
