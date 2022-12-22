using CitadellesDotIO.Engine.Hubs;
using CitadellesDotIO.Engine.Services;
using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSignalR(hubOptions =>
{
    hubOptions.EnableDetailedErrors = true;
    hubOptions.KeepAliveInterval = TimeSpan.FromSeconds(1);
})
 .AddNewtonsoftJsonProtocol(opts =>
 {
     opts.PayloadSerializerSettings.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All;
     opts.PayloadSerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
 });
builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/octet-stream" });
});
builder.Services.AddCors();
builder.Services.AddSingleton<IGamesService, GamesService>();
var app = builder.Build();


app.UseExceptionHandler("/Error");
// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
app.UseHsts();


app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseCors(policy => policy
            .WithOrigins("http://localhost:7243")
            .AllowAnyHeader()
            .AllowAnyMethod());
app.MapHub<LobbyHub>("/lobbyhub");
app.MapHub<GameHub>("/gamehub");
app.Run();
