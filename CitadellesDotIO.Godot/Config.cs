using Microsoft.Extensions.Configuration;

namespace CitadellesDotIO.Godot
{
    public static class Config
    {
        // Returns the Citadelles.IO url for hub connection
        public static string SiteUrl
        => new ConfigurationBuilder()
          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
          .Build().GetValue<string>("Connectivity:SiteUrl");
    }
}
