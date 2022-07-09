using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UserMicroservice.Data;
using UserMicroservice.Repository;
using UserMicroservice.Repository.IRepository;

namespace UserMicroservice
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddScoped<IAppUserRepository, AppUserRepository>();
            services.AddSingleton<IAppUserRepository, AppUserInmemRepository>();

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            //services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase(Configuration.GetConnectionString("DefaultConnection")));

            services.AddSwaggerGen(options => {
                options.SwaggerDoc("UserOpenApiSpec", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "UserApi",
                    Version = "1",
                    Description = "Cognizant Project User API",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                    {
                        Name = "Naman Garg",
                        Url= new Uri("https://github.com/adityapal99/MFPE-Pod3")
                    },
                    License = new Microsoft.OpenApi.Models.OpenApiLicense()
                    {
                        Name = "Cognizant License",
                        Url = new Uri("https://www.cognizant.com/in/en")
                    }
                });
                var xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var cmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);
                options.IncludeXmlComments(cmlCommentsFullPath);

            });
            services.AddControllers();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/UserOpenApiSpec/swagger.json", "UserApi");
                options.RoutePrefix="";
            });
            app.UseRouting();
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
