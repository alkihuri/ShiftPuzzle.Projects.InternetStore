using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyStore.Services; // Убедитесь, что ваш namespace указан правильно
using MyStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MyStore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Этот метод вызывается средой выполнения. Используйте его для добавления сервисов в контейнер.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<MyStoreContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))); 
            services.AddScoped<ProductService>();
        }

        // Этот метод вызывается средой выполнения. Используйте его для настройки конвейера HTTP-запросов.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
 
        }
    }
}
