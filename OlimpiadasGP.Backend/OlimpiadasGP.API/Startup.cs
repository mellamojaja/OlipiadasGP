using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OlimpiadasGP.Services.Core;
using OlimpiadasGP.Services.Repositories;
using Swashbuckle.AspNetCore.Swagger;

namespace OlimpiadasGP.API
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Swagger service
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(Configuration["AppInfo:Version"], new Info
                {
                    Version = Configuration["AppInfo:Version"],
                    Title = Configuration["AppInfo:Name"]
                });

                var xmlFileName = Assembly.GetExecutingAssembly().GetName().Name + ".xml";
                var xmlFilePath = Path.Combine(AppContext.BaseDirectory, xmlFileName);
                c.IncludeXmlComments(xmlFilePath);
            });

            // Configuration service
            services.AddSingleton(Configuration);

            // NH Database service                 
            services.AddSingleton<INHSessionFactory, NHSessionFactory>(x =>
            {
                var dbSettings = GetSettingsFromConfiguration(Configuration, "DbSettings", new NHSettings());
                var dbProvider = new SqlServerProvider(dbSettings.ConnectionString);                
                return new NHSessionFactory(dbProvider, dbSettings);
            });
            services.AddTransient(x => x.GetService<INHSessionFactory>().OpenSession());

            // Bussiness layer services here
            services.AddScoped<ITeamRepository, TeamRepository>();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            app.UseCors(builder =>
                builder.WithOrigins(Configuration["CORSAllowedOrigins"]));

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{Configuration["AppInfo:Version"]}/swagger.json", $"{Configuration["AppInfo:Name"]} {Configuration["AppInfo:Version"]}");
            });
        }

        #region Private
        private static T GetSettingsFromConfiguration<T>(IConfiguration configuration, string configurationKey, T emptyObject)
        {
            configuration
                .GetSection(configurationKey)
                .Bind(emptyObject);

            return emptyObject;
        }
        #endregion
    }
}
