using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MvcClient
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {   

            

            services.AddAuthentication(config => {
                config.DefaultScheme = "Cookie";
                config.DefaultChallengeScheme = "oidc"; 

            })
                    .AddCookie("Cookie")   
                    .AddOpenIdConnect("oidc", config=> {
                        config.ClientId = "client_id_mvc";
                        config.ClientSecret = "client_secret_mvc";
                        config.SaveTokens = true; 
                        config.Authority = "https://localhost:44340/";
                        config.ResponseType = "code";
                        config.ClaimActions.DeleteClaim("amr");
                        config.ClaimActions.MapUniqueJsonKey("MyPersonalCookie", "Gallery.Claim");
                        config.GetClaimsFromUserInfoEndpoint = true;
                        config.Scope.Add("Gallery.Name");
                        config.Scope.Add("ApiOne");
                        config.Scope.Add("offline_access");
                            

                    });
            services.AddAuthorization();
            services.AddHttpClient();
            services.AddControllersWithViews(); 

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseRouting();
            
            app.UseAuthorization(); 


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
