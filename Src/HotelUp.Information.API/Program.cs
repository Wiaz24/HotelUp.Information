using System.Text.Json.Serialization;
using HotelUp.Information.API.Cors;
using HotelUp.Information.API.Swagger;
using HotelUp.Information.Persistence;
using HotelUp.Information.Services;
using HotelUp.Information.Shared;
var builder = WebApplication.CreateBuilder(args);

builder.AddShared();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddCustomSwagger(builder.Configuration);
builder.Services.AddCorsForFrontend(builder.Configuration);
builder.Services.AddServiceLayer();
builder.Services.AddPersistenceLayer();

var app = builder.Build();
app.UseShared();
app.UseCorsForFrontend();
app.UseCustomSwagger();
app.MapGet("/", () => Results.Redirect("/api/information/swagger/index.html"))
    .Produces(200)
    .ExcludeFromDescription();
app.MapControllers();
app.Run();

public interface IApiMarker
{
}