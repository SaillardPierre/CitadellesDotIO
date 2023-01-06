using CitadellesDotIO.Engine.Hubs;
using CitadellesDotIO.Engine.Services;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.SignalR;

namespace CitadellesDotIO.Server
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR(hubOptions =>
            {
                hubOptions.EnableDetailedErrors = true;
                hubOptions.KeepAliveInterval = TimeSpan.FromSeconds(10);
            })
            .AddNewtonsoftJsonProtocol(opts =>
            {
                opts.PayloadSerializerSettings.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All;
                opts.PayloadSerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
            });

            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/octet-stream" });
            });

            services.AddCors();
            services.AddSingleton<IGamesService, GamesService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
        
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<LobbyHub>("/lobbyhub");
                endpoints.MapHub<GameHub>("/gamehub");
            });
        }
    }
}
