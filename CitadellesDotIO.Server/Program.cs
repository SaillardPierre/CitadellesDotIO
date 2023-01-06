using CitadellesDotIO.Engine.Hubs;
using CitadellesDotIO.Engine.Services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.ResponseCompression;

namespace CitadellesDotIO.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
           WebHost.CreateDefaultBuilder(args)
               .UseStartup<Startup>();
    }
}