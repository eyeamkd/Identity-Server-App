using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;

namespace IdentityServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using(var scope = host.Services.CreateScope())
            {
                    var userManager = scope.ServiceProvider
                                        .GetRequiredService<UserManager<IdentityUser>>();
                var user = new IdentityUser("kunal");
                userManager.CreateAsync(user, "P@ssw0rd").GetAwaiter().GetResult();
                userManager.AddClaimAsync(user, new Claim("Gallery.Claim", "Kunal.ClaimValue")).GetAwaiter().GetResult();
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
