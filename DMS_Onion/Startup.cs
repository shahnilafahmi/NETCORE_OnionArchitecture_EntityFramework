using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using RepositoryLayer.DBContextLayer;
using ServiceLayer.Service.Contract;
using ServiceLayer.Service.Implementation;
using System;
using System.Collections.Generic; 
using System.Linq;
using System.Threading.Tasks;

namespace DMS_Onion
{
    public class Startup
    {
        private const string DefaultCorsPolicyName = "localhost"; 
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDBContext>(con => con.UseSqlServer(Configuration.GetConnectionString("SqlConnection")) );
            services.AddControllers();
            services.AddScoped<IUser, UserService>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DMS_Onion", Version = "v1" });
            });

            services.Configure<IISServerOptions>(options =>
            {
                options.AutomaticAuthentication = false;
            });

            services.Configure<IISOptions>(options =>
            {
                options.ForwardClientCertificate = false;
            });

            #region Cors

            string corsOriginsSettings = Configuration["App:CorsOrigins"];
            //Configure CORS for angular2 UI
            services.AddCors(options =>
            {
              
                options.AddPolicy("Security",
               builder => builder.WithOrigins("http://localhost", "http://localhost:3000", "http://192.168.61.32:999").AllowAnyHeader().AllowAnyMethod()

               );
            });

            #endregion


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ERP.API v1");
                    //c.RoutePrefix = string.Empty;
                }
               );

                app.UseCors("Security");

            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseDeveloperExceptionPage();
            app.UseRouting();

            app.UseCors(DefaultCorsPolicyName); //Enable CORS!

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
               
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Ui}/{action=Index}/{id?}"

                    );

            });
        }
    }
}
