using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using VkNet;
using VkNet.Abstractions;
using VkNet.Model;

namespace vkontakteoknomusic_2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMvc();
            services.AddCors();
            services.AddSingleton<IVkApi>(sp => {
                var api = new VkApi();
                api.Authorize(new ApiAuthParams { AccessToken = Configuration["Config:AccessToken"] });
                return api;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            DefaultFilesOptions options = new DefaultFilesOptions();
            options.DefaultFileNames.Clear(); // удаляем имена файлов по умолчанию
            options.DefaultFileNames.Add("hello.html"); // добавляем новое имя файла
            app.UseDefaultFiles(options); // установка параметров
         
            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            
            app.UseCors(builder => builder.AllowAnyOrigin());
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
