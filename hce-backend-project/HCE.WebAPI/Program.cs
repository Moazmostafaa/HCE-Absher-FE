using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HCE.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Environment.SetEnvironmentVariable("EPPlusLicenseContext", "Commercial");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
          (IHostBuilder)Host.CreateDefaultBuilder(args)
              .UseSerilog()
              .ConfigureWebHostDefaults(webBuilder =>
              {
                  webBuilder.ConfigureKestrel(serverOptions =>
                  {
                  });
                  webBuilder.UseContentRoot(Directory.GetCurrentDirectory());
                  webBuilder.UseIIS();
                  webBuilder.UseIISIntegration();
                  webBuilder.UseStartup<Startup>();
              });
    }
}
